namespace Space.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Space.Repository.Entities;

    /// <summary>
    /// Keeps track of building costs for members.
    /// </summary>
    public sealed class BuildCosts
    {
        /// <summary>
        /// Key to assosciate the buiding costs with.
        /// </summary>
        private readonly string _key;

        /// <summary>
        /// Access constants stored in a datastore.
        /// </summary>
        private readonly IConstantsProvider _constantsProvider;

        /// <summary>
        /// Construct a building cost with a datastore key.
        /// </summary>
        /// <param name="key"></param>
        public BuildCosts(string key, IConstantsProvider constantsProvider)
        {
            _key = key;
            _constantsProvider = constantsProvider;
        }

        /// <summary>
        /// Access the building cost in Cash
        /// </summary>
        public double Cash
        {
            get
            {
                return _constantsProvider.Get<double>(_key + "Cash");
            }
            set
            {
                _constantsProvider.Set(_key + "Cash", value);
            }
        }

        /// <summary>
        /// Access the building cost in Food
        /// </summary>
        public double Food
        {
            get
            {
                return _constantsProvider.Get<double>(_key + "Food");
            }
            set
            {
                _constantsProvider.Set(_key + "Food", value);
            }
        }

        /// <summary>
        /// Access the building cost in Iron
        /// </summary>
        public double Iron
        {
            get
            {
                return _constantsProvider.Get<double>(_key + "Iron");
            }
            set
            {
                _constantsProvider.Set(_key + "Iron", value);
            }
        }

        /// <summary>
        /// Access the building cost in Energy
        /// </summary>
        public double Energy
        {
            get
            {
                return _constantsProvider.Get<double>(_key + "Energy");
            }
            set
            {
                _constantsProvider.Set(_key + "Energy", value);
            }
        }

        /// <summary>
        /// Access the building cost in Mana
        /// </summary>
        public double Mana
        {
            get
            {
                return _constantsProvider.Get<double>(_key + "Mana");
            }
            set
            {
                _constantsProvider.Set(_key + "Mana", value);
            }
        }
    }
}
