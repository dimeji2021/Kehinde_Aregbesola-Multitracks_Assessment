using multitracks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multitracks.Core.Dtos
{
    public class GetSongDto
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public string TimeSignature { get; set; }
        public decimal Bpm { get; set; }
        public bool Multitracks { get; set; }
        public bool CustomMix { get; set; }
        public bool Chart { get; set; }
        public bool RehearsalMix { get; set; }
        public bool Patches { get; set; }
        public bool SongSpecificPatches { get; set; }
        public bool ProPresenter { get; set; }
        public DateTime DateCreation { get; set; }

        public Artist Artist { get; set; }
        public Album Album { get; set; }
    }
}
