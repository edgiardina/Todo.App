using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Todo.Models
{
    public class TodoList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Title { get; set; }

        [ManyToOne]
        public List<TodoItem> Items { get; set; }

        [NotNull]
        public bool IsDeleted { get; set; }

        [NotNull]
        public DateTime CreatedOn { get; set; }
        [NotNull]
        public DateTime UpdatedOn { get; set; }
    }
}
