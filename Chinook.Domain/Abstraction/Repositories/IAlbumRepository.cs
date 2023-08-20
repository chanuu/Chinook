using Chinook.Domain.Abstraction.Common;
using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Domain.Abstraction.Repositories
{
    //todo : implement each database operation related method to abum. 
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<Album> Get(int id);

        Task<List<Album>> GetAll();

        Task<List<Album>> GetByArtistId(int artistId);


    }
}
