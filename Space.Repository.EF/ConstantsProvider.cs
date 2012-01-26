// -----------------------------------------------------------------------
// <copyright file="ConstantsProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq.Expressions;
using Space.Infrastructure;
using Space.Repository.Entities;

namespace Space.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConstantsProvider : IConstantsProvider
    {
        private readonly EFDBContext _context;

        public ConstantsProvider(EFDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a value by using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The name (Foo.Bar.StringProperty) of the object.</typeparam>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <returns>An item from a datastore cast by TType.</returns>
        public TType Get<TObjectType, TType>()
        {
            return Get<TType>(typeof (TObjectType).ToString());
        }

        /// <summary>
        /// Gets a value by using a key.
        /// </summary>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        public TType Get<TType>(string key)
        {
            var item = _context.Constants.FirstOrDefault(o => o.ID == key);
            if (item == null)
            {
                return default(TType);
            }

            return item.Value.To<TType>();
        }

        /// <summary>
        /// Gets a value using an expression.
        /// </summary>
        /// <typeparam name="TObjectTYpe">The name (Foo.Bar.StringProperty) of the object.</typeparam>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="expression">An expression used to get the exact item.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        public TType Get<TObjectTYpe, TType>(Expression<Func<TObjectTYpe, TType>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set a value using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The type of object to use as a name.</typeparam>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="value">The object value to set in the datastore.</param>
        public void Set<TObjectType, TType>(TType value)
        {
            Set(typeof (TObjectType).ToString(), value);
        }

        /// <summary>
        /// Set a value using a key.
        /// </summary>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <param name="value">The object value to set in the datastore.</param>
        public void Set<TType>(string key, TType value)
        {
            var item = _context.Constants.FirstOrDefault(o => o.ID == key);
            if (item == null)
            {
                return;
            }

            item.Value = value.ToString();
        }

        /// <summary>
        /// Set a value using an expression.
        /// </summary>
        /// <typeparam name="TObjectTYpe">The type of object to use as a name.</typeparam>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="expression">The expression used to grab the value.</param>
        public void Set<TObjectTYpe, TType>(Expression<Func<TObjectTYpe, TType>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
