using BusinessObject.Models;
using BusinessObject.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderService
    {
        Task<List<Orders>> GetAll();
        Task<Orders> GetById(int id);
        Task Add (OrderRequest order);
        Task Delete(int id);
        Task Update (int id, OrderRequest order);
    }
}
