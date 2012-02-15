// -----------------------------------------------------------------------
// <copyright file="ConstantsProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq.Expressions;
using Space.DTO.Entities;
using Space.Infrastructure;

namespace Space.Console
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
        private Dictionary<string, string> _constants;

        public ConstantsProvider()
        {
            _constants = new Dictionary<string, string>
                             {
                                 {"SolarSystemConstants.MinimumEntities", "10"},
                                 {"SolarSystemConstants.MaximumEntities", "25"},
                                 // Spawning Probability
                                 {"SolarSystemConstants.SpawningProbability.Planet", "3"},
                                 {"SolarSystemConstants.SpawningProbability.GasGiant", ".3"},
                                 {"SolarSystemConstants.SpawningProbability.Nebula", ".02"},
                                 {"SolarSystemConstants.SpawningProbability.Star", ".2"},
                                 {"SolarSystemConstants.SpawningProbability.RedGiant", ".1"},
                                 {"SolarSystemConstants.SpawningProbability.PlanetaryNebula", ".05"},
                                 {"SolarSystemConstants.SpawningProbability.WhiteDwarf", ".2"},
                                 {"SolarSystemConstants.SpawningProbability.BlackDwarf", ".2"},
                                 {"SolarSystemConstants.SpawningProbability.NeutronStar", ".08"},
                                 {"SolarSystemConstants.SpawningProbability.BlackHole", ".05"},
                                 {"SolarSystemConstants.SpawningProbability.Supernova", ".02"},
                                 // Maximum radius
                                 {"SolarSystemConstants.MaximumRadius.Planet", "1"},
                                 {"SolarSystemConstants.MaximumRadius.GasGiant", "1"},
                                 {"SolarSystemConstants.MaximumRadius.Nebula", "1"},
                                 {"SolarSystemConstants.MaximumRadius.Star", "1"},
                                 {"SolarSystemConstants.MaximumRadius.RedGiant", "1"},
                                 {"SolarSystemConstants.MaximumRadius.PlanetaryNebula", "1"},
                                 {"SolarSystemConstants.MaximumRadius.WhiteDwarf", "1"},
                                 {"SolarSystemConstants.MaximumRadius.BlackDwarf", "1"},
                                 {"SolarSystemConstants.MaximumRadius.NeutronStar", "1"},
                                 {"SolarSystemConstants.MaximumRadius.BlackHole", "1"},
                                 {"SolarSystemConstants.MaximumRadius.Supernova", "1"},
                                 // Minimum radius
                                 {"SolarSystemConstants.MinimumRadius.Planet", "1"},
                                 {"SolarSystemConstants.MinimumRadius.GasGiant", "1"},
                                 {"SolarSystemConstants.MinimumRadius.Nebula", "1"},
                                 {"SolarSystemConstants.MinimumRadius.Star", "1"},
                                 {"SolarSystemConstants.MinimumRadius.RedGiant", "1"},
                                 {"SolarSystemConstants.MinimumRadius.PlanetaryNebula", "1"},
                                 {"SolarSystemConstants.MinimumRadius.WhiteDwarf", "1"},
                                 {"SolarSystemConstants.MinimumRadius.BlackDwarf", "1"},
                                 {"SolarSystemConstants.MinimumRadius.NeutronStar", "1"},
                                 {"SolarSystemConstants.MinimumRadius.BlackHole", "1"},
                                 {"SolarSystemConstants.MinimumRadius.Supernova", "1"},
                                 // Maximum Mass
                                 {"SolarSystemConstants.MaximumMass.Planet", "1"},
                                 {"SolarSystemConstants.MaximumMass.GasGiant", "2"},
                                 {"SolarSystemConstants.MaximumMass.Nebula", "2"},
                                 {"SolarSystemConstants.MaximumMass.Star", "2"},
                                 {"SolarSystemConstants.MaximumMass.RedGiant", "2"},
                                 {"SolarSystemConstants.MaximumMass.PlanetaryNebula", "2"},
                                 {"SolarSystemConstants.MaximumMass.WhiteDwarf", "2"},
                                 {"SolarSystemConstants.MaximumMass.BlackDwarf", "2"},
                                 {"SolarSystemConstants.MaximumMass.NeutronStar", "2"},
                                 {"SolarSystemConstants.MaximumMass.BlackHole", "2"},
                                 {"SolarSystemConstants.MaximumMass.Supernova", "2"},
                                 // Minimum Mass
                                 {"SolarSystemConstants.MinimumMass.Planet", "1"},
                                 {"SolarSystemConstants.MinimumMass.GasGiant", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.Nebula", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.Star", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.RedGiant", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.PlanetaryNebula", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.WhiteDwarf", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.BlackDwarf", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.NeutronStar", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.BlackHole", "1.1"},
                                 {"SolarSystemConstants.MinimumMass.Supernova", "1.1"},
                             };
        }
 
        #region Implementation of IConstantsProvider

        /// <summary>
        /// Gets a value by using the Type name of an object as an identifier.
        /// </summary>
        /// <typeparam name="TObjectType">The name (Foo.Bar.StringProperty) of the object.</typeparam>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <returns>An item from a datastore cast by TType.</returns>
        public TType Get<TObjectType, TType>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value by using a key.
        /// </summary>
        /// <typeparam name="TType">The type of object you want to cast to.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <returns>An item from a datastore cast by TType.</returns>
        public TType Get<TType>(string key)
        {
            return _constants[key].To<TType>();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set a value using a key.
        /// </summary>
        /// <typeparam name="TType">The object type to set.</typeparam>
        /// <param name="key">A unique key in a datastore.</param>
        /// <param name="value">The object value to set in the datastore.</param>
        public void Set<TType>(string key, TType value)
        {
            throw new NotImplementedException();
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

        #endregion
    }
}
