using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Helper.Interfaces;
using DAL.Helper;
using Models.ViewModels.Order;
using System.Numerics;

namespace DAL
{
    public class OrderRepository : IOrderRepository
    {
        public IDatabaseHelper _context;

        public OrderRepository(IDatabaseHelper context)
        {
            _context = context;
        }

        public async Task<bool> Create(OrderVM model)
        {
            string msgError = "";
            try
            {
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_order_create",
                    "@Phone", model.Phone,
                    "@Email", model.Email,
                    "@Recipient", model.CusName,
                    "@DeliveryAddress", model.Address,
                    "@TotalPay", model.TotalPay,
                    "@Note", model.Note,
                    "@listjson_detail", model.listjson_detail != null ? MessageConvert.SerializeObject(model.listjson_detail) : null);
                //"@objectjson_customer", model.objectjson_customer != null ? MessageConvert.SerializeObject(model.objectjson_customer) : null,
                //if ((result != null && string.IsNullOrEmpty(result.ToString())) || (result != "" && string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                //{
                //    throw new Exception(Convert.ToString(result) + msgError);
                //}
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderVM> GetByID(long id)
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_order_get_by_id", "@OrdID", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return dt.ConvertTo<OrderVM>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderVM>> GetAll()
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_order_getall");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return dt.ConvertTo<OrderVM>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrderVM> Search(int pageIndex, int pageSize, out long total, string CusName, string Phone, string Email, DateTime? fr_OrderDate, DateTime? to_OrderDate)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_order_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@TenKhachHang", CusName,
                    "@SoDienThoai", Phone,
                    "@Email", Email,
                    "@fr_NgayDat", fr_OrderDate,
                    "@to_NgayDat", to_OrderDate);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<OrderVM>().ToList();
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
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_order_delete",
                    "@OrdID", id);
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