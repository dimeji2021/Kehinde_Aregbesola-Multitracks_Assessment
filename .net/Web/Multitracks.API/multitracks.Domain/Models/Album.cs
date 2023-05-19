using System.ComponentModel.DataAnnotations.Schema;

namespace multitracks.Domain.Models
{

    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public DateTime DateCreation { get; set; }

        //Navigation property
        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        //public virtual IList<Song> Songs { get; set; }
    }
}
