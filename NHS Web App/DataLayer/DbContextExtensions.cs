using System.Linq;
using System.Text.RegularExpressions;

namespace System.Data.Entity
{

    public static class DbContextExtensions
    {
        public static void Delete<TContext, TEntity>(this TContext ctx, Func<TContext, IQueryable<TEntity>> query, Func<TEntity, IComparable> compares, object value) where TEntity : class where TContext : DbContext
        {
            foreach (var item in query(ctx)) if (compares(item).Equals(value)) ctx.Set(item.GetType()).Remove(item);
        }
    }
}