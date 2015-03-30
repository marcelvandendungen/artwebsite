using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ListExtensions
    {
        public static void PromoteEntry<T>(this List<T> list, int index)
        {
            int newIndex = index - 1;
            var item = list[index];

            list.RemoveAt(index);
            list.Insert(newIndex, item);
        }

        public static void DemoteEntry<T>(this List<T> list, int index)
        {
            int newIndex = index + 1;
            var item = list[index];

            list.RemoveAt(index);
            list.Insert(newIndex, item);
        }
    }
}
