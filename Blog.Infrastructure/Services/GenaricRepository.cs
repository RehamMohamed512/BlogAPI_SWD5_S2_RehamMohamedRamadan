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
        Task<IEnumerable<TableModel>> IGenaricRepository<TableModel>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
