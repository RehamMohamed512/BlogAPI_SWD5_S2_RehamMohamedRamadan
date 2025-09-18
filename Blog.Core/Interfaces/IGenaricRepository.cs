using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface IGenaricRepository<TableModel> where TableModel:class
    {

        //get all
        Task<IEnumerable<TableModel>> GetAllAsync();
        ////get by id
        Task<IEnumerable<TableModel>> GetByIdAsync(int id);
        ////create
        Task CreateAsync(TableModel model);

        ////update
        void Update(TableModel model);
        ////delete
        void Delete(TableModel model);
        ////save
        Task SaveAsync();
    }
}
