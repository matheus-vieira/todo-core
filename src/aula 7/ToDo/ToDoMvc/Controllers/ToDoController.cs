using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoMvc.Models;
using ToDoMvc.Models.View;
using ToDoMvc.Services;

namespace ToDoMvc.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly IToDoItemService _toDoItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoController(IToDoItemService todoItemsService,
                              UserManager<ApplicationUser> userManager)
        {
            _toDoItemService = todoItemsService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Challenge();

            // busca items de algum lugar
            //se necessário cria uma view model
            // encaminha para a view
            var vm = new ToDoViewModel
            {
                Items = await _toDoItemService.GetIncompleteItemsAsync(currentUser)
            };
            return View(vm);
        }

        public async Task<IActionResult> AddItem(NewToDoItem newItem)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var succesfull = await _toDoItemService.AddItemAsync(newItem, currentUser);

            if (!succesfull)
                return BadRequest(new { error = "Could not add item" });
            return Ok();
        }

        public async Task<IActionResult> MarkDone(Guid id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return Unauthorized();

            if (id == Guid.Empty) return BadRequest();

            var succesfull = await _toDoItemService
                .MarkDoneAsync(id, currentUser);

            if (!succesfull)
                return BadRequest(
                    new { error = "Could not mark item as done" });
            return Ok();
        }
    }
}