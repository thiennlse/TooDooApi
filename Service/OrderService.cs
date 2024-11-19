using BusinessObject.Models;
using BusinessObject.RequestModel;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _repo;

        public OrderService(IOrderRepo repo)
        {
            _repo = repo;
        }

        public async Task Add(OrderRequest order)
        {
            Orders orders = new Orders
            {
                UserId = order.UserId,
                SubcriptionsId = order.SubcriptionId,
                OrderDate = DateTime.Now.ToUniversalTime(),
                CloseDate = DateTime.Now.AddHours(1).ToUniversalTime(),
                Price = order.Price,
                Code = order.Code,
                Status = order.Status
            };
            await _repo.Add(orders);
        }

        public async Task Delete(int id)
        {
            var orders = await _repo.GetById(id);
            await _repo.Delete(orders);
        }

        public async Task<List<Orders>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Orders> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task Update(int id, OrderRequest order)
        {
            var _order = await _repo.GetById(id);
            _order.SubcriptionsId = order.SubcriptionId;
            _order.CloseDate = order.CloseDate.ToUniversalTime();
            _order.Status = order.Status;
            _order.Price = order.Price;
            _order.Code = order.Code;
            _order.UserId = order.UserId;
        }
    }
}
