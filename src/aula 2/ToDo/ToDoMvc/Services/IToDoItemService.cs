﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoMvc.Models;

namespace ToDoMvc.Services
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync();
    }
}
