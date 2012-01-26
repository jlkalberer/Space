// -----------------------------------------------------------------------
// <copyright file="SolarSystemConstants.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Space.Repository.Entities;

namespace Space.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SolarSystemConstants
    {
        private readonly IConstantsProvider _constantsProvider;

        public SolarSystemConstants(IConstantsProvider constantsProvider)
        {
            _constantsProvider = constantsProvider;
        }

        /// <summary>
        /// The minimum number of entities to spawn in a solar system
        /// </summary>
        public int MinimumEntities
        {
            get { return _constantsProvider.Get<int>("SolarSystemConstants.MinimumEntities"); }
            set { _constantsProvider.Set("SolarSystemConstants.MinimumEntities", value); }
        }

        /// <summary>
        /// The maximum number of entities to spawn in a solar system
        /// </summary>
        public int MaximumEntities
        {
            get { return _constantsProvider.Get<int>("SolarSystemConstants.MaximumEntities"); }
            set { _constantsProvider.Set("SolarSystemConstants.MaximumEntities", value); }
        }

        /// <summary>
        /// Get the Spawning Probability of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating if the entity should be spawned.</returns>
        public float SpawningProbability<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>("SolarSystemConstants.SpawningProbability." + type);
        }

        /// <summary>
        /// Set the Spawning Probability of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating if the entity should be spawned.</param>
        public void SpawningProbability<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set("SolarSystemConstants.SpawningProbability." + type, value);
        }

        /// <summary>
        /// Get the Maximum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the radius of the entity.</returns>
        public float MaximumRadius<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>("SolarSystemConstants.MaximumRadius." + type);
        }

        /// <summary>
        /// Set the Maximum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the radius of the entity.</param>
        public void MaximumRadius<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set("SolarSystemConstants.MaximumRadius." + type, value);
        }

        /// <summary>
        /// Get the Minimum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the radius of the entity.</returns>
        public float MinimumRadius<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>("SolarSystemConstants.MinimumRadius." + type);
        }

        /// <summary>
        /// Set the Minimum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the radius of the entity.</param>
        public void MinimumRadius<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set("SolarSystemConstants.MinimumRadius." + type, value);
        }

        /// <summary>
        /// Get the Maximum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the mass of the entity.</returns>
        public float MaximumMass<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>("SolarSystemConstants.MaximumMass." + type);
        }

        /// <summary>
        /// Set the Maximum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the mass of the entity.</param>
        public void MaximumMass<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set("SolarSystemConstants.MaximumMass." + type, value);
        }

        /// <summary>
        /// Get the Minimum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the mass of the entity.</returns>
        public float MinimumMass<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>("SolarSystemConstants.MinimumMass." + type);
        }

        /// <summary>
        /// Set the Minimum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the mass of the entity.</param>
        public void MinimumMass<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set("SolarSystemConstants.MinimumMass." + type, value);
        }
    }
}
