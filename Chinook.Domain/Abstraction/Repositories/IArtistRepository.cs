using Chinook.Domain.Abstraction.Common;
using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Domain.Abstraction.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist> GetAsync(long artistId);

        Task<List<Artist>> GetAllAsync();

        Task<List<Artist>> GetAllByNameAsync(string key);

        Task<List<Track>> GetAllTracksAsync(long artistId);


    }
}
