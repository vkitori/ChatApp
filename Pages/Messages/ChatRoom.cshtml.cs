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
    public class ChatRoomModel : PageModel
    {
        private readonly ChatApp.Data.ChatAppContext _context;

        public ChatRoomModel(ChatApp.Data.ChatAppContext context)
        {
            _context = context;
        }

        public IList<Message> Messages { get; set; } = default!;
        public ChatRoom ChatRoom { get; set; }

        public async Task<IActionResult> OnGetAsync(int? roomId)
        {
            if (roomId == null)
            {
                return NotFound();
            }

            ChatRoom = await _context.ChatRoom.FirstOrDefaultAsync(m => m.Id == roomId);

            if (ChatRoom == null)
            {
                return NotFound();
            }

            Messages = await _context.Message
                .Include(m => m.User)
                .Where(m => m.ChatRoomId == roomId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int roomId, string messageContent)
        {
            if (string.IsNullOrEmpty(messageContent))
            {
                return RedirectToPage(new { roomId });
            }

            var message = new Message
            {
                Content = messageContent,
                Timestamp = DateTime.UtcNow,
                ChatRoomId = roomId,
                UserId = 1
    };

            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { roomId });
        }
    }
}