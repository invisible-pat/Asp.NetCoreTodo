using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Service
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user);

        Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user);

        Task<bool> MarkDoneAsync(Guid Id, IdentityUser user);
    }
}
