// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.Infrastructure
{
    using System;

    /// <summary>
    /// Class for extension methods on System Types
    /// </summary>
    public static class SystemTypes
    {
        public static T To<T>(this string text)
        {
            return (T)Convert.ChangeType(text, typeof(T));
        }
    }
}
