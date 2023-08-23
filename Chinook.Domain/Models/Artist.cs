using Chinook.Domain.Abstraction.Common;
using System;
using System.Collections.Generic;

namespace Chinook.Models
{
    public partial class Artist : BaseEntity
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public long ArtistId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
