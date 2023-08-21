
using Chinook.Domain.Abstraction.Common;
using Chinook.Domain.Abstraction.Repositories;
using Chinook.Migrations;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Infrastructure.Repositores
{
    public class PlayListRepository : IPlayListRepository
    {
        private readonly ChinookContext _context;


        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public PlayListRepository(ChinookContext context)
        {
            _context = context;
        }

       
        /// <summary>
        /// Add New Playlist to the Databse 
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public Playlist Add(Playlist playlist)
        {
            var Playlist =  _context.Playlists.Add(playlist).Entity;
            UnitOfWork.SaveChangesAsync();
            return Playlist;
        }

        /// <summary>
        /// get all playlists 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Playlist>> GetAll()
        {
              return await _context.Playlists.Include(x => x.PlaylistId).ToListAsync(); ;
        }

        /// <summary>
        /// get playlist by its Id
        /// </summary>
        /// <param name="id"> playlist ID</param>
        /// <returns>Playlist obejct</returns>
        public async Task<Playlist> GetPlaylist(long id)
        {
            return await _context.Playlists
                .Include(a => a.Tracks)
                .ThenInclude(a => a.Album)
                .ThenInclude(a => a.Artist)
                .FirstOrDefaultAsync(o => o.PlaylistId == id);
        }

        public async Task<List<Playlist>> GetPlaylists(long id)
        {
            return await _context.Playlists
                .Include(a => a.Tracks)
                .ThenInclude(a => a.Album)
                .ThenInclude(a => a.Artist)
                .Where(o => o.PlaylistId == id)
                .ToListAsync();
        }

        /// <summary>
        /// remove playlist 
        /// </summary>
        /// <param name="playlist"></param>
        public void Remove(Playlist playlist)
        {
           _context.Playlists.Remove(playlist);
        }

        /// <summary>
        /// add track to exsiting playlist 
        /// </summary>
        /// <param name="track"></param>
        /// <param name="playlsitId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddTrackToPlaylistAsync(long trackId, long playlsitId)
        {
           var Playlist = await _context.Playlists.Include(x => x.UserPlaylists).FirstOrDefaultAsync(o => o.PlaylistId == playlsitId);

           if(Playlist == null)
            {
                throw new Exception("Cannot Find Playlist With Given Id !");
            }
            else
            {
                var track = await _context.Tracks
                   .FirstOrDefaultAsync(t => t.TrackId == trackId);

                Playlist.Tracks.Add(track);
                await UnitOfWork.SaveChangesAsync();
                
            }
        }

        /// <summary>
        /// Add New Playlist to the database 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="playlsitId"></param>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public async Task AddPlaylistToUser(string UserId, string playListName, long trackId)
        {
            //todo : implment static create methode for object creation
            Playlist NewplayList = new Playlist();
            NewplayList.Name = playListName;
            
            _context.Playlists.Add(NewplayList);

            if  (NewplayList != null)
            {
                var track = await _context.Tracks
                    .FirstOrDefaultAsync(t => t.TrackId == trackId);

                if (track != null)
                {
                    NewplayList.UserPlaylists.Add(new UserPlaylist { UserId = UserId, PlaylistId = NewplayList.PlaylistId });
                  
                   
                    NewplayList.Tracks.Add(track);

                    await UnitOfWork.SaveChangesAsync();
                }
            }


        }

        /// <summary>
        /// get users playlist 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Task<List<Playlist>> GetUsersPlayList(string UserId)
        {
           return _context.Playlists
                .Where(x=>x.UserPlaylists.Any(up => up.UserId == UserId))
                .ToListAsync();
           
        }


        /// <summary>
        /// AddTofavorite using track id and user id 
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddTofavorite(long trackId,string userId)
        {
           

            var favorite =   await _context.Playlists
                .Include(x=>x.UserPlaylists)
                .Where(x=>x.UserPlaylists
                .Any(up => up.UserId == userId && up.Playlist.Name == "Favorites"))
                .ToListAsync();

          
            if (favorite == null || favorite.Count ==0)
            {
                //todo : implment static create method for playlist creations 
                Playlist _Playlist = new Playlist();

                _Playlist.Name = "Favorites";

                _context.Playlists.Add(_Playlist);


               var _track = await  _context.Tracks
                    .Where(x=>x.TrackId == trackId)
                    .FirstOrDefaultAsync();

                if(_track !=null)
                {
                    _Playlist.UserPlaylists.Add(new UserPlaylist { UserId = userId, PlaylistId = _Playlist.PlaylistId });

                    _Playlist.Tracks.Add(_track);

                    await UnitOfWork.SaveChangesAsync();
                }
            }
            else
            {
                var _Playlist = await _context.Playlists
               .Include(x => x.UserPlaylists)
               .Where(x => x.UserPlaylists
               .Any(up => up.UserId == userId && up.Playlist.Name == "Favorites"))
               .FirstOrDefaultAsync();

                var _track = await _context.Tracks
                     .Where(x => x.TrackId == trackId)
                     .FirstOrDefaultAsync();

                if (_track != null)
                {
                 

                    _Playlist.Tracks.Add(_track);

                    await UnitOfWork.SaveChangesAsync();
                }

            }

        }


        // check is favorite track
        public bool IsFavoriteTrack(Track Tracks, string UserId)
        {
            bool isFavorites = false;
          
                var favorite =  _context.Playlists
                 .Include(x => x.Tracks)
            
                 .Where(x => x.UserPlaylists
                 .Any(up => up.UserId == UserId && up.Playlist.Name == "Favorites"))
                 .FirstOrDefault();

                var MatchedTrack = favorite.Tracks.Where(x => x.TrackId == Tracks.TrackId);
                if (MatchedTrack.Count()>0){
                    isFavorites = true;
                }
          
            return isFavorites;
        }

        // remove track from favorite
        public async Task RemoveFromfavorite(long trackId, string UserId)
        {
            
            var PlayList = await _context.Playlists
               .Include(x => x.Tracks)
               .Where(x => x.UserPlaylists
               .Any(up => up.UserId == UserId && up.Playlist.Name == "Favorites"))
               .FirstOrDefaultAsync();

            var track =   PlayList.Tracks.Where(x=>x.TrackId== trackId).SingleOrDefault();

            

            if (track != null)
            {
                PlayList.Tracks.Remove(track);
                await UnitOfWork.SaveChangesAsync();
            }


        }
    }



    
}
