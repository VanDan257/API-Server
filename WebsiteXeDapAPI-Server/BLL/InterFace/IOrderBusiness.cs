using Models.Models;
using Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFace
{
    public interface IOrderBusiness
    {
        Task<bool> Create(OrderVM model);

        Task<OrderVM> GetByID(long id);

        Task<List<OrderVM>> GetAll();

        Task<bool> Delete(long id);

        List<OrderVM> Search(int pageIndex, int pageSize, out long total, string CusName, string Phone, string Email, DateTime? fr_OrderDate, DateTime? to_OrderDate);
    }
}