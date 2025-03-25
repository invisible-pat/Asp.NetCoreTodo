using AspNetCoreTodo.Models;
using AspNetCoreTodo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;
        public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);
            TodoViewModel todoViewModel = new TodoViewModel { TodoItems = items };

            return View(todoViewModel);
        }

        //[ValidateAntiForgeryToken]确保是从正确的网站提交过的数据
        //框架会自动添加隐藏的防伪令牌字段         
        //首先传过来的参数会进行模型参数检验 例如Title:Required 必须是拥有的
        //并且检查这个模型参数检验的结果是否有效
        //一般我们将数据传输对象(Dto) 和 数据库的实体分开 数据传输对象他可以只包含Title
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService.AddItemAsync(todoItem, currentUser);

            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> MarkDone(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService.MarkDoneAsync(Id, currentUser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }
            return RedirectToAction("Index");
        }
    }
}
