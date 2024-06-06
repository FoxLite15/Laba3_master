using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class FilmGenre
    {
        public int FilmGenreId { get; set; }
        public int FilmId { get; set; }
        [ForeignKey("FilmId")]
        public Film? Film { get; set; }
        public int GenreId {  get; set; }
        [ForeignKey("GenreId")]
    
        public Genre? Genre { get; set; }

    }
}
