using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Pages.ChatRooms
{
    public class IndexModel : PageModel
    {
        private readonly ChatAppContext _context;

        public IndexModel(ChatAppContext context)
        {
            _context = context;
        }

        public IList<ChatRoom> ChatRoom { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ChatRoom = await _context.ChatRoom.ToListAsync();
        }
    }
}
