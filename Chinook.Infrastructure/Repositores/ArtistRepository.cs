using Chinook.Domain.Abstraction.Common;
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
    public class ArtistRepository : IArtistRepository
    {
        private readonly ChinookContext _context;

        public ArtistRepository(ChinookContext context)
        {
            _context = context;
        }

   
        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        // get all available artist 
        public async Task<List<Artist>> GetAllAsync()
        {
            return await _context.Artists.Include(x => x.Albums).ToListAsync();
        }

        public async Task<List<Track>> GetAllTracksAsync(long artistId)
        {
            return await _context.Tracks.Where(a => a.Album.ArtistId == artistId)
            .Include(a => a.Album).ToListAsync();
        }

        // get artist by ID 
        public async Task<Artist> GetAsync(long artistId)
        {
            return await _context.Artists.Include(x => x.Albums).FirstOrDefaultAsync(o => o.ArtistId == artistId);
        }


    }
}
