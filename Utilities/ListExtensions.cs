using System;
using System.Collections.Generic;
using System.Linq;

namespace AOOR.Utilities
{
    public static class ListExtensions
    {
        public static void DemoteEntry<T>(this List<T> list, Predicate<T> predicate)
        {
            int newIndex = list.Count - 1;
            int oldIndex = list.FindIndex(predicate);
            if (oldIndex < newIndex)
            {
                newIndex = oldIndex + 1;
            }

            MoveTo(list, newIndex, oldIndex);
        }

        public static void PromoteEntry<T>(this List<T> list, Predicate<T> predicate)
        {
            int newIndex = 0;
            int oldIndex = list.FindIndex(predicate);
            if (oldIndex > 0)
            {
                newIndex = oldIndex - 1;
            }

            MoveTo(list, newIndex, oldIndex);
        }

        private static void MoveTo<T>(List<T> collection, int newIndex, int oldIndex)
        {
            var item = collection[oldIndex];

            collection.RemoveAt(oldIndex);

            collection.Insert(newIndex, item);
        }
    }
}