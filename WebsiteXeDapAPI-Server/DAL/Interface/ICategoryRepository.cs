using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();

        Task<Category> GetByID(long CateID);

        Task<List<Category>> GetAllParentCategories();
    }
}