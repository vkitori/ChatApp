using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Pages.ChatRooms
{
    public class CreateModel : PageModel
    {
        private readonly ChatAppContext _context;

        public CreateModel(ChatAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ChatRoom ChatRoom { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ChatRoom.Add(ChatRoom);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
