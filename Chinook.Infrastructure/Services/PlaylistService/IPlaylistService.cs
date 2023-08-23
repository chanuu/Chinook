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
        //get all playlist from database
        Task<List<Playlist>> GetAllPlaylist();

        //getplaylist by ID
        Task<ClientModels.Playlist> GetPlaylistById(int id, string CurrentUserId);

        Task<Playlist> CreatePlaylist(string playlistName, string UserId,long trackId,long playlistId);

        //get playlist by created userId
        Task<List<Playlist>> GetPlayListOfUsers(string UserId);

        // add track to favorite
        Task AddTofavorite(long trackId, string userId);

        // remove from favorite
        Task RemoveFromfavorite(long trackId, string userId);

        Task RemoveFromPlaylist(long trackId, long PlalistId);


    }
}
