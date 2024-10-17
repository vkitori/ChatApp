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
    public class DeleteModel : PageModel
    {
        private readonly ChatAppContext _context;

        public DeleteModel(ChatAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatroom = await _context.ChatRoom.FindAsync(id);
            if (chatroom != null)
            {
                ChatRoom = chatroom;
                _context.ChatRoom.Remove(ChatRoom);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
