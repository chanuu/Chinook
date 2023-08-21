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

        Task<ClientModels.Playlist> GetPlaylistById(int id, string CurrentUserId);

        Task<Playlist> CreatePlaylist(string playlistName, string UserId,long trackId,long playlistId);

        Task<List<Playlist>> GetPlayListOfUsers(string UserId);

        Task AddTofavorite(long trackId, string userId);




    }
}
