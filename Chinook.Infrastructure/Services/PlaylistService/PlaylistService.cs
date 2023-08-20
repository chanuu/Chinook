﻿

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

            //return _Playlists.Select(p => new ClientModels.Playlist()
            //{
            //    Name = p.Name,
            //    Tracks = p.Tracks.Select(t => new ClientModels.PlaylistTrack()
            //    {
            //        AlbumTitle = t.Album.Title,
            //        ArtistName = t.Album.Artist.Name,
            //        TrackId = t.TrackId,
            //        TrackName = t.Name,
            //        IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == CurrentUserId && up.Playlist.Name == "Favorites")).Any()
            //    }).ToList()
            //})
            //.FirstOrDefault(); ;
            return _client;
        }
    }
}
