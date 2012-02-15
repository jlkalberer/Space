// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.Infrastructure
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class for extension methods on System Types
    /// </summary>
    public static class SystemTypes
    {
        public static T To<T>(this string text)
        {
            return (T)Convert.ChangeType(text, typeof(T));
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
