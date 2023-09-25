using courses.Models;

namespace courses.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Courses> Courses { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
    }
}
