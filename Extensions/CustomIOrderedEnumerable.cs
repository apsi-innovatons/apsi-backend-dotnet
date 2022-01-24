using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Extensions
{
    public static class CustomIOrderedEnumerable
        {
        // https://stackoverflow.com/questions/1606454/conditional-orderby-sort-order-in-linq/9589728
        public static IOrderedEnumerable<Post> OrderByDate<Post>(
                this IOrderedEnumerable<Post> elements,
                Func<bool> conditionSort,
                Func<bool> conditionDesc,
                Func<IOrderedEnumerable<Post>, IOrderedEnumerable<Post>> sortPath,
                Func<IOrderedEnumerable<Post>, IOrderedEnumerable<Post>> sorthDescPath)
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
