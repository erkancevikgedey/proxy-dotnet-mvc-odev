using System.Linq.Expressions;

namespace Hafta11.MyExtensions
{
    public static class MyExtensions
    {

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> input,
            bool condition, Expression<Func<T, bool>> expression)
        {
            return condition ? input.Where(expression) : input;
        }
    }
}
