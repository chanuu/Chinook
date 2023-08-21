

using Chinook.Domain.Abstraction.Common;
using Chinook.Domain.Abstraction.Repositories;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// get playlist by it id and return client model 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public async Task<ClientModels.Playlist> GetPlaylistById(int id,string CurrentUserId)
        {
           var _Playlists = await _PlayListRepository.GetPlaylist(id);

             ClientModels.Playlist _client = new ClientModels.Playlist();
            _client.Tracks = new List<ClientModels.PlaylistTrack>();
            _client.Name = _Playlists.Name;
           foreach(var tracks in _Playlists.Tracks)
            {
                ClientModels.PlaylistTrack PlayListTrack = new ClientModels.PlaylistTrack();
                PlayListTrack.AlbumTitle = tracks.Album.Title;
                PlayListTrack.ArtistName = tracks.Album.Artist.Name;
                PlayListTrack.TrackId = tracks.TrackId;
                PlayListTrack.TrackName = tracks.Name;
                PlayListTrack.IsFavorite = tracks.Playlists?
               .Any(p => p.UserPlaylists.Any(up => up.UserId == CurrentUserId && up.Playlist.Name == "Favorites")) ?? false;

                _client.Tracks.Add(PlayListTrack);

            }

            return _client;
        }

        public async Task<Playlist> CreatePlaylist(string playlistName,string UserId,long trackId,long playlistId)
        {
          
          if(playlistName != null)
            {
                await _PlayListRepository.AddPlaylistToUser(UserId, playlistName, trackId);
            }
            else
            {
                await _PlayListRepository.AddTrackToPlaylistAsync(trackId, playlistId);
            }
          

           return null;
        }

        public async Task<List<Playlist>> GetPlayListOfUsers(string UserId)
        {
           return await _PlayListRepository.GetUsersPlayList(UserId);
        }

        public async Task AddTofavorite(long trackId, string userId)
        {
            await _PlayListRepository.AddTofavorite(trackId,userId);
        }
    }
}
