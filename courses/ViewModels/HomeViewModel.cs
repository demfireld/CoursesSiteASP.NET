using courses.Models;

namespace courses.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Courses> Courses { get; set; }
        public IEnumerable<Staffs> Staffs { get; set; }
    }
}
