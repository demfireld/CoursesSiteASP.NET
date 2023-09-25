using courses.Models;

namespace courses.ViewModels
{
    public class AllCoursesViewModel
    {
        public IEnumerable<Courses> Courses { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
    }
}
