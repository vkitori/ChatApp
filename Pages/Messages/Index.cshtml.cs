using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly ChatApp.Data.ChatAppContext _context;

        public IndexModel(ChatApp.Data.ChatAppContext context)
        {
            _context = context;
        }

        public IList<Message> Message { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Message = await _context.Message
                .Include(m => m.User).ToListAsync();
        }
    }
}
