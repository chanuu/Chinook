using Chinook.Domain.Abstraction.Repositories;
using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Infrastructure.Services.PlaylistService
{
    public class PlaylistService : IPlaylistService
    {
        IPlayListRepository _PlayListRepository { get; set; }

        public PlaylistService(IPlayListRepository playListRepository)
        {
            _PlayListRepository = playListRepository;
        }

        public async Task<List<Playlist>> GetAllPlaylist()
        {
            return await _PlayListRepository.GetAll();
        }


    }
}
