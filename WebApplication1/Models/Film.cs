using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string? Title { get; set; }
        public int ProducerId { get; set; }
        [ForeignKey("AuthorId")]

        public Producer? Producer { get; set; }
        public ICollection<FilmGenre>?  FilmGenres { get; set; }


    }
}
