﻿using Chinook.Domain.Abstraction.Common;
using Chinook.Domain.Abstraction.Repositories;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Infrastructure.Repositores
{
    public class PlayListRepository : IPlayListRepository
    {
        private readonly ChinookContext _context;


        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public PlayListRepository(ChinookContext context)
        {
            _context = context;
        }

       
        /// <summary>
        /// Add New Playlist to the Databse 
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public Playlist Add(Playlist playlist)
        {
            return _context.Playlists.Add(playlist).Entity;
        }

        /// <summary>
        /// get all playlists 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Playlist>> GetAll()
        {
              return await _context.Playlists.Include(x => x.PlaylistId).ToListAsync(); ;
        }

        /// <summary>
        /// get playlist by its Id
        /// </summary>
        /// <param name="id"> playlist ID</param>
        /// <returns>Playlist obejct</returns>
        public async Task<Playlist> GetPlaylist(long id)
        {
            return await _context.Playlists.Include(x => x.UserPlaylists).FirstOrDefaultAsync(o => o.PlaylistId == id);
        }

        /// <summary>
        /// remove playlist 
        /// </summary>
        /// <param name="playlist"></param>
        public void Remove(Playlist playlist)
        {
           _context.Playlists.Remove(playlist);
        }
    }
}
