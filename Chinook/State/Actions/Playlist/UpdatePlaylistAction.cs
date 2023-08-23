namespace Chinook.State.Actions.Playlist
{
    public class UpdatePlaylistAction
    {
        public Models.Playlist NewPlaylist { get; }

        public string UserId { get; set; }

        public UpdatePlaylistAction(Models.Playlist newPlaylist, string userId)
        {
            NewPlaylist = newPlaylist;
            UserId = userId;
        }
    }
}
