using Blog.Core.Interfaces;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class GenaricRepository<TableModel> : IGenaricRepository<TableModel>
        where TableModel : class
    {
        //DI
        private readonly BlogDbContext _context;
        private readonly DbSet<TableModel> _dbSet;

        public GenaricRepository(BlogDbContext context)
        {
            _context = context;
            //used to query to save instance of tableModel
            _dbSet = context.Set<TableModel>(); //Posts
        }


        public async Task<IEnumerable<TableModel>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TableModel> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task CreateAsync(TableModel model)
        {
            await _dbSet.AddAsync(model);
        }

         async Task IGenaricRepository<TableModel>.SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        Task<IEnumerable<TableModel>> IGenaricRepository<TableModel>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TableModel model)
        {
            _dbSet.Update(model);
        }

        public void Delete(TableModel model)
        {
            _dbSet.Remove(model);
        }
    }
}
