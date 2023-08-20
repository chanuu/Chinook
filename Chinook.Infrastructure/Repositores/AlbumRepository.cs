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
    public class AlbumRepository : IAlbumRepository
    {
       
        private readonly ChinookContext _context;


        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public async Task<Album> Get(int id)
        {
            return await _context.Albums.Include(x => x.Tracks).FirstOrDefaultAsync(o => o.AlbumId == id);
        }

        public async Task<List<Album>> GetAll()
        {
            return await _context.Albums.Include(x => x.Tracks).ToListAsync(); 
        }

        public async Task<List<Album>> GetByArtistId(int artistId)
        {
            return  await _context.Albums.Where(x=>x.ArtistId == artistId).ToListAsync();
            
        }
    }
}
