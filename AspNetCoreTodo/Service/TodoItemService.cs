using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
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

        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);

            _dbContext.Item.Add(newItem);

            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return await _dbContext.Item.Where(x => x.IsDone == false).ToArrayAsync();
        }

        public async Task<bool> MarkDoneAsync(Guid Id)
        {
            //用来查主键的
            //精确检索单条记录的异步方法，适用于明确预期结果唯一性的场景。
            //使用时需确保查询条件能限制结果为 0 或 1 条记录，否则需处理异常或改用 FirstOrDefaultAsync()
            var markDoneItem = await _dbContext.Item.Where(x => x.id == Id).SingleOrDefaultAsync();
            if (markDoneItem == null) return false;

            markDoneItem.IsDone = true;

            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult == 1;

        }
    }
}
