using Chinook.ClientModels;
using Chinook.Domain.Abstraction.Repositories;
using Chinook.Infrastructure.Repositores;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private IPlayListRepository _PlayListRepository { get; set; }

        public ArtistService(IArtistRepository artistRepository, IAlbumRepository albumRepository, IPlayListRepository playListRepository)
        {
            _ArtistRepository = artistRepository;
            _AlbumRepository = albumRepository;
            _PlayListRepository = playListRepository;
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

        public async Task<List<PlaylistTrack>> GetTracksByArtist(long ArtistId, string currentUserId)
        {
            var tracks = await _ArtistRepository.GetAllTracksAsync(ArtistId);
            return tracks.Select(t => new PlaylistTrack()
            {
                AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                TrackId = t.TrackId,
                TrackName = t.Name,
               // IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == currentUserId && up.Playlist.Name == "Favorites")).Any()
               IsFavorite = _PlayListRepository.IsFavoriteTrack(t,currentUserId)
            })
             .ToList();
        }

        public async Task<List<Artist>> SearchArtistByArtist(string key)
        {
            return await _ArtistRepository.GetAllByNameAsync(key);
        }
    }
}
