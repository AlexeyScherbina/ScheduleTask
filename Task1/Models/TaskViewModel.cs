namespace Task1.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
        public UserViewModel User { get; set; }
    }
}