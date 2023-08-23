namespace Chinook.State.Actions.Playlist
{
    public class FetchDataAction
    {
        public string UserId { get; set; }

        public FetchDataAction(string userId)
        {
            UserId = userId;
        }
    }
}
