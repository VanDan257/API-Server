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
    public class CustomerBusiness : ICustomerBusiness
    {
        private ICustomerRepository _res;

        public CustomerBusiness(ICustomerRepository res)
        {
            _res = res;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _res.GetAll();
        }

        public async Task<bool> Create(Customer model)
        {
            return await _res.Create(model);
        }

        public async Task<Customer> GetByID(long id)
        {
            return await _res.GetByID(id);
        }

        public List<Customer> Search(int pageIndex, int pageSize, out long total, long? CusID, string CusName, string option)
        {
            return _res.Search(pageIndex, pageSize, out total, CusID, CusName, option);
        }

        public async Task<bool> Update(Customer model)
        {
            return await _res.Update(model);
        }

        public async Task<bool> Delete(long id)
        {
            return await _res.Delete(id);
        }
    }
}