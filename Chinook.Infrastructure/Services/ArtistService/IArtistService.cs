using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Infrastructure.Services.ArtistService
{
    public interface IArtistService 
    {
        Task<List<Artist>> GetArtist();

        Task<List<Album>> GetAlbumByArtist(int ArtistId);
    }
}
