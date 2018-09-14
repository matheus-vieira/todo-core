using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoMvc.Models;

namespace ToDoMvc.Services
{
    public class FakeToDoService : IToDoItemService
    {
        public Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync()
        {
            var items = new[]
                {
                    new ToDoItem
                    {
                        Title = "Estudar C# do service",
                        DueAt = DateTimeOffset.Now.AddDays(1)
                    },
                    new ToDoItem
                    {
                        Title = "Montar um aplicativo do service",
                        DueAt = DateTimeOffset.Now.AddDays(2)
                    }
                };

            return Task.FromResult(items as IEnumerable<ToDoItem>);
        }
    }
}
