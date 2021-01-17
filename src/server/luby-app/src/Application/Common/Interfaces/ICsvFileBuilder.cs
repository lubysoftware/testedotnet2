using luby_app.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace luby_app.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
