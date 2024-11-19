using BusinessObject.Models;
using BusinessObject.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<ApplicationUsers> Login(string email, string password);

        Task Register(AccountRequest request);

        Task SubcriptionRegister(int userId, int subcriptionId);

        Task<ApplicationUsers> GetById(int id);

        Task<List<ApplicationUsers>> GetAll();
    }
}
