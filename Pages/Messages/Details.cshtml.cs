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
    public class DetailsModel : PageModel
    {
        private readonly ChatApp.Data.ChatAppContext _context;

        public DetailsModel(ChatApp.Data.ChatAppContext context)
        {
            _context = context;
        }

        public Message Message { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            else
            {
                Message = message;
            }
            return Page();
        }
    }
}
