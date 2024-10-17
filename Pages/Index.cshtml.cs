using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ChatApp.Data.ChatAppContext _context;

        public IndexModel(ChatApp.Data.ChatAppContext context)
        {
            _context = context;
        }

        public IList<ChatRoom> ChatRooms { get; set; }

        public async Task OnGetAsync()
        {
            ChatRooms = await _context.ChatRoom.ToListAsync();
        }
    }
}
