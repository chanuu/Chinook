using Chinook.Domain.Abstraction.Common;
using Chinook.Models;
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

        Task AddTrackToPlaylistAsync(Track track,long playlsitId);



    }
}
