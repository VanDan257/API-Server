using BLL.InterFace;
using DAL.Interface;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusiness(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(long CateID)
        {
            return await _categoryRepository.GetByID(CateID);
        }

        public async Task<List<Category>> GetAllParentCategories()
        {
            return await _categoryRepository.GetAllParentCategories();
        }
    }
}