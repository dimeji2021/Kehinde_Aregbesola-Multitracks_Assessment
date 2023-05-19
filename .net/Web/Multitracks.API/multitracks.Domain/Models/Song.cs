using System.ComponentModel.DataAnnotations.Schema;

namespace multitracks.Domain.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TimeSignature { get; set; } = string.Empty;
        public decimal Bpm { get; set; }
        public bool Multitracks { get; set; }
        public bool CustomMix { get; set; }
        public bool Chart { get; set; }
        public bool RehearsalMix { get; set; }
        public bool Patches { get; set; }
        public bool SongSpecificPatches { get; set; }
        public bool ProPresenter { get; set; }
        public DateTime DateCreation { get; set; }

        //Navigation property
        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
