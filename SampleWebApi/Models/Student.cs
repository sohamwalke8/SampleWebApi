namespace SampleWebApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public string AppUserId { get; set; }

        public virtual AppUser? AppUser { get; set; }
    }
}
