// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantsProvider.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Entity Framework implementation for getting the game constants.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Space.DTO.Entities;
    using Space.Infrastructure;

    /// <summary>
    /// Entity Framework implementation for getting the game constants.
    /// </summary>
    public class ConstantsProvider : IConstantsProvider
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantsProvider"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ConstantsProvider(EntityFrameworkDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets a value by using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The name (Foo.Bar.StringProperty) of the object.</typeparam>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <returns>An item from a datastore cast by TType.</returns>
        public TType Get<TObjectType, TType>()
        {
            return Get<TType>(typeof(TObjectType).ToString());
        }

        /// <summary>
        /// Gets a value by using a key.
        /// </summary>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        public TType Get<TType>(string key)
        {
            var item = this.context.Constants.FirstOrDefault(o => o.ID == key);
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
            Set(typeof(TObjectType).ToString(), value);
        }

        /// <summary>
        /// Set a value using a key.
        /// </summary>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <param name="value">The object value to set in the datastore.</param>
        public void Set<TType>(string key, TType value)
        {
            var item = this.context.Constants.FirstOrDefault(o => o.ID == key);
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
