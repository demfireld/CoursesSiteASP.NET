namespace courses.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string? TeacherPatronymic { get; set; }
        public int TeacherAge { get; set; }
        public int TeacherWorkExperience { get; set; }
        public string TeacherPhoneNumber { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherImg { get; set; }
    }
}
