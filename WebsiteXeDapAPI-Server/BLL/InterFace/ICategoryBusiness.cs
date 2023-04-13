using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFace
{
    public interface ICategoryBusiness
    {
        Task<List<Category>> GetAll();

        Task<Category> GetById(long CateID);

        Task<List<Category>> GetAllParentCategories();
    }
}