using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interface;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        public IDatabaseHelper _context;

        public CategoryRepository(IDatabaseHelper context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_categories_getall");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Category>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> GetByID(long CateID)
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_categories_getbyid",
                    "@CateID", CateID);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Category>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Category>> GetAllParentCategories()
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_categories_getparentcategories");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Category>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}