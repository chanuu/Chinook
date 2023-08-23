using Chinook.ClientModels;
using Fluxor;

namespace Chinook.State.Playlist
{
    [FeatureState]
    public class PlaylistState
    {
        public List<Models.Playlist> Playlists { get; }

        private PlaylistState() { }
        public PlaylistState(List<Models.Playlist> playlists)
        {
            Playlists = playlists ?? new List<Models.Playlist>();
        }
    }
}
