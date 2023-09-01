using courses.Models;

namespace courses.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Courses> Courses { get; set; }
        public IEnumerable<Teachers> Teachers { get; set; }
    }
}
