using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Data;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty] public Book Book { get; set; }

        public async Task <IActionResult> OnGet(int? Id)
        {
            Book = new Book();

            //create
            if (Id==null)
            {
                return Page();
            }
            //edit
            Book = await _db.Books.FirstOrDefaultAsync(u => u.Id == Id);
            if (Book==null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
         {
            if (ModelState.IsValid)
            {
                if (Book.Id==0)
                {
                    _db.Books.Add(Book);
                }
                else
                {
                    _db.Books.Update(Book);
                }

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }

        }
    }
}