using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoMvc.Data;
using ToDoMvc.Models;

namespace ToDoMvc.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemAsync(NewToDoItem newItem,
            ApplicationUser currentUser)
        {
            var entity = new ToDoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = newItem.DueAt,
                OwnerId = currentUser.Id
            };
            _context.Items.Add(entity);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync(
            ApplicationUser currentUser)
        {
            var items = await _context.Items
                .Where(i => !i.IsDone && i.OwnerId == currentUser.Id)
                .ToArrayAsync();

            return items;
        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser currentUser)
        {
            var item = await _context.Items
                .FirstOrDefaultAsync(
                i => i.Id == id && i.OwnerId == currentUser.Id);

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context
                .SaveChangesAsync();

            return saveResult == 1;
        }
    }
}
