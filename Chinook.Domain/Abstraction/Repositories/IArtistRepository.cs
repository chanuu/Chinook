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
        Task<Artist> GetAsync(long airportId);

        Task<List<Artist>> GetAllAsync();


    }
}
