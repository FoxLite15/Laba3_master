namespace WebApplication1.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string? Name{get;   set;}
        public ICollection<Film>? Films { get; set; }
    }
}
