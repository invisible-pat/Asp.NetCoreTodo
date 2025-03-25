using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Service
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user)
        {
            newItem.id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);
            newItem.UserId = user.Id;

            _dbContext.Item.Add(newItem);

            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
        {
            var Items = await _dbContext.Item.Where(x => x.IsDone == false && x.UserId == user.Id).ToArrayAsync();
            return Items;
        }

        public async Task<bool> MarkDoneAsync(Guid Id, IdentityUser user)
        {
            //用来查主键的
            //精确检索单条记录的异步方法，适用于明确预期结果唯一性的场景。
            //使用时需确保查询条件能限制结果为 0 或 1 条记录，否则需处理异常或改用 FirstOrDefaultAsync()
            var markDoneItem = await _dbContext.Item.Where(x => x.id == Id && x.UserId == user.Id).SingleOrDefaultAsync();
            if (markDoneItem == null) return false;

            markDoneItem.IsDone = true;

            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult == 1;

        }
    }
}
