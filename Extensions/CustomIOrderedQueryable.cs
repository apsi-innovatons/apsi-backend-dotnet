using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Extensions
{
    public static class CustomIOrderedQueryable
        {
        // https://stackoverflow.com/questions/1606454/conditional-orderby-sort-order-in-linq/9589728
        public static IOrderedQueryable<Post> OrderByDate<Post>(
                this IOrderedQueryable<Post> elements,
                Func<bool> conditionSort,
                Func<bool> conditionDesc,
                Func<IOrderedQueryable<Post>, IOrderedQueryable<Post>> sortPath,
                Func<IOrderedQueryable<Post>, IOrderedQueryable<Post>> sorthDescPath)
            {
                if (conditionSort())
                {
                    if (conditionDesc())
                    {
                        return sorthDescPath(elements);
                    }
                    else
                    {
                        return sortPath(elements);
                    }
                }
                return elements;
            }
        }
}
