using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Service
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync();

        Task<bool> AddItemAsync(TodoItem newItem);

        Task<bool> MarkDoneAsync(Guid Id);
    }
}
