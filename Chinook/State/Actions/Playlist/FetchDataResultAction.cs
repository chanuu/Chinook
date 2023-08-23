namespace Chinook.State.Actions.Playlist
{
    public class FetchDataResultAction
    {
        public List<Models.Playlist> _Playlists { get; }

        public FetchDataResultAction(List<Models.Playlist> Playlists)
        {
            _Playlists = Playlists;
        }
    }
}
