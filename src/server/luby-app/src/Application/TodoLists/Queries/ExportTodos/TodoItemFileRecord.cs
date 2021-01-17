using luby_app.Application.Common.Mappings;
using luby_app.Domain.Entities;

namespace luby_app.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
