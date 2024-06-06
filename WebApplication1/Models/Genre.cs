using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public string? Name { get; set; }
        public ICollection <FilmGenre>? FilmGenres { get; set; }
    }
}
