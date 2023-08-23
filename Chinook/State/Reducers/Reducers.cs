using Chinook.State.Actions.Playlist;
using Chinook.State.Playlist;
using Fluxor;

namespace Chinook.State.Reducers
{
    public static class Reducers
    {
        [ReducerMethod]
        public static PlaylistState ReduceFetchDataAction(PlaylistState state, FetchDataAction action) =>
        new(playlists: null);

        [ReducerMethod]
        public static PlaylistState ReduceFetchDataResultAction(PlaylistState state, FetchDataResultAction action) =>
          new(playlists: action._Playlists);

        [ReducerMethod]
        public static PlaylistState ReduceUpdatePlaylistAction(PlaylistState state, UpdatePlaylistAction action) =>
        new PlaylistState(playlists: state.Playlists.Append(action.NewPlaylist).ToList());
    }
}
