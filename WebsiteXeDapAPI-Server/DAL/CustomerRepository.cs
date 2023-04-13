using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Helper.Interfaces;
using DAL.Interface;
using DAL.Helper;

namespace DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        public IDatabaseHelper _context;

        public CustomerRepository(IDatabaseHelper context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAll()
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_customer_getall");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Customer>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Create(Customer model)
        {
            string msgError = "";
            try
            {
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_customer_create",
                    "@CusID", model.CusID,
                    "@CusName", model.CusName,
                    "@Phone", model.Phone,
                    "@Email", model.Email,
                    "@Address", model.Address,
                    "@UserName", model.UserName,
                    "@Password", model.Password,
                    "@Image", model.Image);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetByID(long id)
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_customer_get_by_id", "@CusID", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return dt.ConvertTo<Customer>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Customer> Search(int pageIndex, int pageSize, out long total, long? CusID, string CusName, string option)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_customer_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@CusName", CusName,
                    "@CusID", CusID,
                    "@option", option);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Customer>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Customer model)
        {
            string msgError = "";
            try
            {
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_customer_UPDATE",
                    "@CusID", model.CusID,
                    "@CusName", model.CusName,
                    "@Phone", model.Phone,
                    "@Email", model.Email,
                    "@Address", model.Address,
                    "@UserName", model.UserName,
                    "@Password", model.Password,
                    "@Image", model.Image);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(long id)
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_customer_delete",
                    "@CusID", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}