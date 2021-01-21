using luby_app.Domain.Common;
using luby_app.Domain.ValueObjects;
using System.Collections.Generic;

namespace luby_app.Domain.Entities
{
    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Colour Colour { get; set; } = Colour.White;

        public virtual ICollection<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
