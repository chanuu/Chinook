using Chinook.Domain.Abstraction.Repositories;
using Chinook.Infrastructure.Repositores;
using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Infrastructure.Services.ArtistService
{
    public class ArtistService : IArtistService
    {
       private IArtistRepository _ArtistRepository { get; set; }
       private IAlbumRepository _AlbumRepository { get; set; }

        public ArtistService(IArtistRepository artistRepository, IAlbumRepository albumRepository)
        {
            _ArtistRepository = artistRepository;
            _AlbumRepository = albumRepository;
        }

        public async Task<List<Artist>> GetArtist()
        {
            return await _ArtistRepository.GetAllAsync();
        }

        /// <summary>
        /// get album by artist name 
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        public async Task<List<Album>> GetAlbumByArtist(int ArtistId)
        {
            return await _AlbumRepository.GetByArtistId(ArtistId);
        }
    }
}
