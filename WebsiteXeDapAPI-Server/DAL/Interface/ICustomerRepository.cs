using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();

        Task<Customer> GetByID(long id);

        Task<bool> Create(Customer model);

        Task<bool> Update(Customer model);

        Task<bool> Delete(long id);

        List<Customer> Search(int pageIndex, int pageSize, out long total, long? CusID, string CusName, string option);
    }
}