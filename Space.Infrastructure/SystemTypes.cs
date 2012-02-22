// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemTypes.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Class for extension methods on System Types
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for extension methods on System Types
    /// </summary>
    public static class SystemTypes
    {
        /// <summary>
        /// Converts from one System Type to another.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <typeparam name="T">
        /// The Type to conver to.
        /// </typeparam>
        /// <returns>
        /// The converted object.
        /// </returns>
        public static T To<T>(this string text)
        {
            return (T)Convert.ChangeType(text, typeof(T));
        }

        /// <summary>
        /// Converts an enum to an IList.
        /// </summary>
        /// <typeparam name="T">
        /// The Type of enum.
        /// </typeparam>
        /// <returns>
        /// The enum IList
        /// </returns>
        public static IList<T> EnumToList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
