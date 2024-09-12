using HospitalDataLayer.Interfaces;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Services.Interfaces;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace HospitalPresentationLayer.Services
{
    public class ListEntityService<E> : IListEntityService<E> where E : IEntity
    {
        private readonly ParameterExpression parameterExpression = Expression.Parameter(typeof(E), "e");

        public List<int> GetIds(IQueryable<E> query, FilterOptions filter, OrderOptions orderOptions, PageOptions pageOptions)
        {
            QueryFilter(ref query, filter);
            QueryOrder(ref query, orderOptions);

            var count = query.Count();
            pageOptions.Count = count;
            pageOptions.PageTotal = count / pageOptions.PageSize + 1;

            QueryPage(ref query, pageOptions);

            return query.Select(i => i.Id).ToList();
        }

        /// <summary>
        /// Применить фильтр, унаследованный от <see cref="FilterOptions"/>.
        /// </summary>
        public void QueryFilter(ref IQueryable<E> query, FilterOptions filter)
        {
            var filterProps = filter.GetType().GetProperties();

            foreach (var filterProp in filterProps)
            {
                var value = filterProp.GetValue(filter);
                if (value is null)
                {
                    continue;
                }

                var filterPropType = filterProp.PropertyType;
                var isCollection = filterPropType.IsGenericType
                    && new[] { typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Contains(filterPropType.GetGenericTypeDefinition());
                var entityProp = GetMatchedEntityProperty(filterProp, isCollection);

                // Превращаем ICollection<T> в List<T> иначе Linq не сможет корректно построить sql
                var constantType = isCollection
                    ? typeof(List<>).MakeGenericType(filterPropType.GetGenericArguments())
                    : entityProp.PropertyType;
                var property = Expression.Property(parameterExpression, entityProp.Name);
                var constant = Expression.Constant(value, constantType);

                if (filterPropType == typeof(string))
                {
                    var method = filterPropType.GetMethod("Contains");
                    var containsMethodExpression = Expression.Call(property, method, constant);
                    query = query.Where(Expression.Lambda<Func<E, bool>>(containsMethodExpression, parameterExpression));

                    continue;
                }

                // Проверяем на коллекцию
                if ((value as ICollection) is { Count: > 0 })
                {
                    var method = filterPropType.GetMethod("Contains");
                    var containsMethodExpression = Expression.Call(constant, method, property);
                    query = query.Where(Expression.Lambda<Func<E, bool>>(containsMethodExpression, parameterExpression));

                    continue;
                }

                query = query.Where(Expression.Lambda<Func<E, bool>>(Expression.Equal(property, constant), parameterExpression));
            }
        }

        public void QueryOrder(ref IQueryable<E> query, OrderOptions orderOptions)
        {
            if (orderOptions.Direction == OrderDirection.None)
            {
                return;
            }

            var propExpression = Expression.Property(parameterExpression, orderOptions.Field);
            var convPropExpression = Expression.Convert(propExpression, typeof(object));

            if (orderOptions.Direction == OrderDirection.Asc)
            {
                query = query.OrderBy(Expression.Lambda<Func<E, dynamic>>(convPropExpression, parameterExpression));
            }
            else
            {
                // OrderDirection.Desc
                query = query.OrderByDescending(Expression.Lambda<Func<E, dynamic>>(convPropExpression, parameterExpression));
            }
        }

        public void QueryPage(ref IQueryable<E> query, PageOptions pageOptions)
        {
            query = query
                .Skip((pageOptions.PageCurrent - 1) * pageOptions.PageSize)
                .Take(pageOptions.PageSize);
        }

        /// <summary>
        /// Ищем совпадение имени поля фильтра и имени поля EF-сущности.
        /// </summary>
        /// <param name="filterProp">Поле фильтра.</param>
        /// <param name="isCollection">Является ли поле фильтра коллекцией.</param>
        /// <returns>Найденное поле EF-сущности.</returns>
        /// <exception cref="InvalidOperationException">Исключение некорректного имени поля фильтра.</exception>
        private static PropertyInfo GetMatchedEntityProperty(PropertyInfo filterProp, bool isCollection)
        {
            var entityProps = typeof(E).GetProperties();
            var matchedPropName = isCollection
                ? filterProp.Name[..^1] // Убираем букву 's' с конца имени поля фильтра
                : filterProp.Name;

            return entityProps.FirstOrDefault(e => e.Name.Equals(matchedPropName, StringComparison.InvariantCultureIgnoreCase))
                ?? throw new InvalidOperationException(
                    $"Поле фильтра '{filterProp.Name}' не может сопоставиться ни с одним полем '{typeof(E).Name}'.");
        }
    }
}
