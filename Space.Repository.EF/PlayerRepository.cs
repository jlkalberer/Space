﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Space.DTO;

namespace Space.Repository.EF
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly EFDBContext _context;

        public PlayerRepository(EFDBContext context)
        {
            _context = context;
        }

        #region Implementation of ICrud<in int,Player>

        public Player Create()
        {
            var player = _context.Players.Create();
            return _context.Players.Add(player);
        }

        public Player Get(int key)
        {
            var player = _context.Players.SingleOrDefault(p => p.ID == key);
            return player;
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Player> All
        {
            get { throw new NotImplementedException(); }
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public bool Update(Player value)
        {
            throw new NotImplementedException();          
        }

        #endregion
    }
}
