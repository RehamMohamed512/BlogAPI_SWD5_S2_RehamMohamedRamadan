using Blog.Core.DTos;
using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface ICategoryService
    {
        //get all categories
        Task<IEnumerable<Category>> GetAllAsync();

        //get category by id
        Task<Category> GetByIdAsync(int id);

        //create category
        Task<Category> CreateAsync(CategoryDTO category); //change parameter datatype

        //update category
        Task<bool> UpdateAsync(CategoryDTO category); //change parameter datatype
        Task<bool> UpdateAsync(int id,CategoryDTO category); //change parameter datatype

        //delete category
        Task<bool> DeleteAsync(int id);


    }
}
