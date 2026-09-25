namespace StudentRosterApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Course { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int YearLevel { get; set; }
    }
}