using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Infrastructure.Services.PlaylistService
{
    public interface IPlaylistService
    {
        Task<List<Playlist>> GetAllPlaylist();



    }
}
