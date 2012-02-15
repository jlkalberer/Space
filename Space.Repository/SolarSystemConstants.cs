namespace Space.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Space.Repository.Entities;

    /// <summary>
    /// Constants for the solar systems
    /// </summary>
    public sealed class SolarSystemConstants
    {
        /// <summary>
        /// Key to assosciate the buiding costs with.
        /// </summary>
        private readonly string _key;

        /// <summary>
        /// Access constants stored in a datastore.
        /// </summary>
        private readonly IConstantsProvider _constantsProvider;

        public SolarSystemConstants(string key, IConstantsProvider constantsProvider)
        {
            _key = key;
            _constantsProvider = constantsProvider;
        }

        /// <summary>
        /// The minimum number of entities to spawn in a solar system
        /// </summary>
        public int MinimumEntities
        {
            get { return _constantsProvider.Get<int>(_key + "MinimumEntities"); }
            set { _constantsProvider.Set(_key + "MinimumEntities", value); }
        }

        /// <summary>
        /// The maximum number of entities to spawn in a solar system
        /// </summary>
        public int MaximumEntities
        {
            get { return _constantsProvider.Get<int>(_key + "MaximumEntities"); }
            set { _constantsProvider.Set(_key + "MaximumEntities", value); }
        }

        /// <summary>
        /// Get the Spawning Probability of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating if the entity should be spawned.</returns>
        public float SpawningProbability<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>(_key + "SpawningProbability." + type);
        }

        /// <summary>
        /// Set the Spawning Probability of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating if the entity should be spawned.</param>
        public void SpawningProbability<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set(_key + "SpawningProbability." + type, value);
        }

        /// <summary>
        /// Get the Maximum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the radius of the entity.</returns>
        public float MaximumRadius<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>(_key + "MaximumRadius." + type);
        }

        /// <summary>
        /// Set the Maximum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the radius of the entity.</param>
        public void MaximumRadius<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set(_key + "MaximumRadius." + type, value);
        }

        /// <summary>
        /// Get the Minimum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the radius of the entity.</returns>
        public float MinimumRadius<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>(_key + "MinimumRadius." + type);
        }

        /// <summary>
        /// Set the Minimum Radius of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the radius of the entity.</param>
        public void MinimumRadius<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set(_key + "MinimumRadius." + type, value);
        }

        /// <summary>
        /// Get the Maximum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the mass of the entity.</returns>
        public float MaximumMass<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>(_key + "MaximumMass." + type);
        }

        /// <summary>
        /// Set the Maximum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the mass of the entity.</param>
        public void MaximumMass<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set(_key + "MaximumMass." + type, value);
        }

        /// <summary>
        /// Get the Minimum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <returns>Float value for calculating the mass of the entity.</returns>
        public float MinimumMass<TEntityType>(TEntityType type)
        {
            return _constantsProvider.Get<float>(_key + "MinimumMass." + type);
        }

        /// <summary>
        /// Set the Minimum Mass of this type.
        /// </summary>
        /// <typeparam name="TEntityType">The type of entity which will be spawned.</typeparam>
        /// <param name="type">Used to generate the unique key.</param>
        /// <param name="value">Float value for calculating the mass of the entity.</param>
        public void MinimumMass<TEntityType>(TEntityType type, float value)
        {
            _constantsProvider.Set(_key + "MinimumMass." + type, value);
        }
    }
}
