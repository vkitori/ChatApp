using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Pages.ChatRooms;

public class EditModel : PageModel
{
    private readonly ChatAppContext _context;

    public EditModel(ChatAppContext context)
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
        ChatRoom = chatroom;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(ChatRoom).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChatRoomExists(ChatRoom.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool ChatRoomExists(int id)
    {
        return _context.ChatRoom.Any(e => e.Id == id);
    }
}
