using Chinook.Domain.Abstraction.Common;
using Chinook.Models;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Domain.Abstraction.Repositories
{
    public interface IPlayListRepository : IRepository<Playlist>
    {
        Playlist Add(Playlist playlist);

        void Remove(Playlist playlist);

        Task<List<Playlist>> GetAll();

        Task<Playlist> GetPlaylist(long id);

        Task<List<Playlist>> GetPlaylists(long id);

        Task AddTrackToPlaylistAsync(long trackId,long playlsitId);

        Task AddPlaylistToUser(string UserId, string playListName,long trackId);

        Task<List<Playlist>> GetUsersPlayList(string UserId);

        Task AddTofavorite(long trackId, string userId);


        Task RemoveFromfavorite(long trackId, string UserId);

        bool IsFavoriteTrack(Track Tracks, string UserId);








    }
}
