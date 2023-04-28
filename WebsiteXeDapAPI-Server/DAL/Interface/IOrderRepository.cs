﻿using Models.Models;
using Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IOrderRepository
    {
        Task<bool> Create(OrderVM model);

        Task<OrderVM> GetByID(long id);

        Task<List<OrderVM>> GetAll();

        Task<bool> Delete(long id);

        Task<bool> UpdateStatusOrder(OrderStatusUpdateRequest model);

        List<OrderVM> Search(int pageIndex, int pageSize, out long total, string Recipient, string Phone, string Email, DateTime? fr_OrderDate, DateTime? to_OrderDate);
    }
}