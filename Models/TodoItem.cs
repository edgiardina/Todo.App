using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(nameof(TodoList))]
        public int TodoListId { get; set; }

        [NotNull]
        public string Title { get; set; }

        [NotNull]
        public bool IsCompleted { get; set; }

        [NotNull]
        public bool IsDeleted { get; set; }

        [NotNull]
        public DateTime CreatedOn { get; set; }
        
        [NotNull]
        public DateTime UpdatedOn { get; set; }
    }
}
