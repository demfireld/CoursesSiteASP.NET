namespace courses.Models
{
    public class Courses
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Img { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Categories Category { get; set; }
    }
}
