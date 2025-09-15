using Blog.Core.DTos;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    //DI
    public class CategoryService : ICategoryService
    {
        private readonly BlogDbContext _context;
        public CategoryService(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var Categories = await _context.Categories.ToListAsync();
            //var Categories = _context.Categories.
                //Select(cat => new Category { Name = cat.Name, Id = cat.Id, //Posts cat.Posts
                //});
            return Categories;
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            //  ID PK
            return await _context.Categories.FindAsync(id);
            //return await _context.Categories.FirstAsync(cat => cat.Id == id);
            //return await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
            //return await _context.Categories.SingleAsync(c=>c.Id == id);



        }

        public async Task<Category> CreateAsync(CategoryDTO category)
        {
            var cat = new Category
            {
                Name = category.Name,
            };
            await _context.Categories.AddAsync(cat);
            await _context.SaveChangesAsync();
            return cat;
            
        }

       
        public async Task<bool> UpdateAsync(CategoryDTO category)
        {
            var oldcat =  await GetByIdAsync(category.Id);
            if(oldcat is null) return false;
            oldcat.Name = category.Name;
            _context.Categories.Update(oldcat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, CategoryDTO category)
        {
            var Oldcat = await GetByIdAsync(id);
            if(Oldcat is null) return false;

            Oldcat.Name = category.Name;
            _context.Categories.Update(Oldcat);
            await _context.SaveChangesAsync();
            return true;

        }

        public  async Task<bool> DeleteAsync(int id)
        {
            var cat = await GetByIdAsync(id);
            if (cat is null) 
                return false;

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
