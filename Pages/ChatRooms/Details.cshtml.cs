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
    public class DetailsModel : PageModel
    {
        private readonly ChatAppContext _context;

        public DetailsModel(ChatAppContext context)
        {
            _context = context;
        }

        public ChatRoom ChatRoom { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatroom = await _context.ChatRoom.FirstOrDefaultAsync(m => m.Id == id);
            if (chatroom == null)
            {
                return NotFound();
            }
            else
            {
                ChatRoom = chatroom;
            }
            return Page();
        }
    }
}
