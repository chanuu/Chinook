using Chinook.Domain.Abstraction.Common;
using System;
using System.Collections.Generic;

namespace Chinook.Models
{
    public partial class Playlist : BaseEntity
    {
        public Playlist()
        {
            Tracks = new HashSet<Track>();
            UserPlaylists = new List<UserPlaylist>();
        }

        public long PlaylistId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
        public virtual ICollection<UserPlaylist> UserPlaylists { get; set; }

    }
}
