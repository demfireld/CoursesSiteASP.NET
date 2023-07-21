namespace courses.Models
{
    public class Categories
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public List<Courses> Courses { get; set; }
    }
}
