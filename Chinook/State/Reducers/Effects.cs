using Chinook.Infrastructure.Services.PlaylistService;
using Chinook.State.Actions.Playlist;
using Chinook.State.Playlist;
using Fluxor;
using static System.Net.WebRequestMethods;

namespace Chinook.State.Reducers
{
    public class Effects
    {
        private readonly IPlaylistService playlistService;


        public Effects(IPlaylistService playlistService)
        {
            this.playlistService = playlistService;
        }

        [EffectMethod]
        public async Task HandleFetchDataAction(FetchDataAction action, IDispatcher dispatcher)
        {
            var playlsits = await playlistService.GetPlayListOfUsers(action.UserId);
            if (playlsits is not null)
            {
                dispatcher.Dispatch(new FetchDataResultAction(Playlists: playlsits!));
            }
        }

        [EffectMethod]
        public async Task HandleUpdatePlaylistAction(UpdatePlaylistAction action, IDispatcher dispatcher)
        {
           // add newly created playlsit for the state 
            var playlsits = await playlistService.GetPlayListOfUsers(action.UserId);
            playlsits.Add(action.NewPlaylist);
            var newState = new PlaylistState(playlists: playlsits);

            dispatcher.Dispatch(newState);
        }

    }
}
