using BLL.InterFace;
using DAL.Interface;
using Models.Models;
using Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderBusiness : IOrderBusiness
    {
        private IOrderRepository _res;

        public OrderBusiness(IOrderRepository res)
        {
            _res = res;
        }

        public async Task<bool> Create(OrderVM model)
        {
            return await _res.Create(model);
        }

        public async Task<bool> Delete(long id)
        {
            return await _res.Delete(id);
        }

        public async Task<OrderVM> GetByID(long id)
        {
            return await _res.GetByID(id);
        }

        public async Task<bool> UpdateStatusOrder(OrderStatusUpdateRequest model)
        {
            return await _res.UpdateStatusOrder(model);
        }

        public async Task<List<OrderVM>> GetAll()
        {
            return await _res.GetAll();
        }

        public List<OrderVM> Search(int pageIndex, int pageSize, out long total, string CusName, string Phone, string Email, DateTime? fr_OrderDate, DateTime? to_OrderDate)
        {
            return _res.Search(pageIndex, pageSize, out total, CusName, Phone, Email, fr_OrderDate, to_OrderDate);
        }
    }
}