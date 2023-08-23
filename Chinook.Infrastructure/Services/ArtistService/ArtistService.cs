﻿using Chinook.ClientModels;
using Chinook.Domain.Abstraction.Repositories;
using Chinook.Infrastructure.Repositores;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
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
            if (tracks.Any())
            {
                return tracks.Select(t => new PlaylistTrack()
                {
                    AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = _PlayListRepository.IsFavoriteTrack(t, currentUserId)
                })
             .ToList();
            }
            else
            {
                return new List<PlaylistTrack>();
            }

        }

        public async Task<List<Artist>> SearchArtistByName(string key)
        {
            return await _ArtistRepository.GetAllByNameAsync(key);
        }

        public async Task<Artist> GetAsync(long artistId)
        {
            return await _ArtistRepository.GetAsync(artistId);
        }
    }
}
