// -----------------------------------------------------------------------
// <copyright file="IConstantsProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq.Expressions;
using System;

namespace Space.DTO.Entities
{
    /// <summary>
    /// IConstantsProvider is used to grab constants from a datastore
    /// </summary>
    public interface IConstantsProvider
    {
        /// <summary>
        /// Gets a value by using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The name (Foo.Bar.StringProperty) of the object.</typeparam>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <returns>An item from a datastore cast by TType.</returns>
        TType Get<TObjectType, TType>();

        /// <summary>
        /// Gets a value by using a key.
        /// </summary>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        TType Get<TType>(string key);

        /// <summary>
        /// Gets a value using an expression.
        /// </summary>
        /// <typeparam name="TObjectTYpe">The name (Foo.Bar.StringProperty) of the object.</typeparam>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="expression">An expression used to get the exact item.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        TType Get<TObjectTYpe, TType>(Expression<Func<TObjectTYpe, TType>> expression);

        /// <summary>
        /// Set a value using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The type of object to use as a name.</typeparam>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="value">The object value to set in the datastore.</param>
        void Set<TObjectType, TType>(TType value);

        /// <summary>
        /// Set a value using a key.
        /// </summary>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <param name="value">The object value to set in the datastore.</param>
        void Set<TType>(string key, TType value);

        /// <summary>
        /// Set a value using an expression.
        /// </summary>
        /// <typeparam name="TObjectTYpe">The type of object to use as a name.</typeparam>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="expression">The expression used to grab the value.</param>
        void Set<TObjectTYpe, TType>(Expression<Func<TObjectTYpe, TType>> expression);
    }

    /// <summary>
    /// IConstantsProvider is used to grab constants from a datastore
    /// </summary>
    public interface IConstantsProvider<TObjectType>
    {
        /// <summary>
        /// Gets a value by using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The name (Foo.Bar.StringProperty) of the object.</typeparam>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <returns>An item from a datastore cast by TType.</returns>
        TType Get<TType>();

        /// <summary>
        /// Gets a value by using a key.
        /// </summary>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        TType Get<TType>(string key);

        /// <summary>
        /// Gets a value using an expression.
        /// </summary>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="expression">An expression used to get the exact item.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        TType Get<TType>(Expression<Func<TObjectType, TType>> expression);

        /// <summary>
        /// Set a value using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The type of object to use as a name.</typeparam>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="value">The object value to set in the datastore.</param>
        void Set<TType>(TType value);

        /// <summary>
        /// Set a value using a key.
        /// </summary>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <param name="value">The object value to set in the datastore.</param>
        void Set<TType>(string key, TType value);

        /// <summary>
        /// Set a value using an expression.
        /// </summary>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="expression">The expression used to grab the value.</param>
        void Set<TType>(Expression<Func<TObjectType, TType>> expression);

        /// <summary>
        /// Set a value using an expression.
        /// </summary>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="expression">The expression used to grab the value.</param>
        /// <param name="value">The value to set.</param>
        void Set<TType>(Expression<Func<TObjectType, TType>> expression, TType value);
    }
}
