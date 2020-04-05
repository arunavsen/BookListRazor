﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Data;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new{data=await _db.Books.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var booksFromDb = await _db.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (booksFromDb==null)
            {
                return Json(new { success = false, message = "Error while deleting"});
            }

            _db.Books.Remove(booksFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Deleted successfully."});
        }
    }
}