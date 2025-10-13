namespace TodoListapp.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string ? Password  { get; set; }

        // navigation

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
