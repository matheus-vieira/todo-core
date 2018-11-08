using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoMvc.Models;

namespace ToDoMvc.Services
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync(ApplicationUser currentUser);
        Task<bool> AddItemAsync(NewToDoItem newItem, ApplicationUser currentUser);
        Task<bool> MarkDoneAsync(Guid id, ApplicationUser currentUser);
    }
}
