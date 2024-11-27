using BusinessObject.Models;
using BusinessObject.RequestModel;
using Repository.Interface;
using Service.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ISubcriptionRepository _subcriptionRepo;

        public AccountService(IAccountRepository accountRepo, ISubcriptionRepository subcriptionRepo)
        {
            _accountRepo = accountRepo;
            _subcriptionRepo = subcriptionRepo;
        }

        public async Task<List<ApplicationUsers>> GetAll()
        {
            return await _accountRepo.GetAllAsync();
        }

        public async Task<ApplicationUsers> GetById(int id)
        {
            return await _accountRepo.GetByIdAsync(id);
        }

        public async Task<ApplicationUsers> Login(string email, string password)
        {
            var account = await _accountRepo.GetByEmail(email);
            if (account != null)
            {
                if (account.Password == HashPasswordToSha256(password))
                {
                    return account;
                }
                else
                {
                    throw new Exception("Wrong password");
                }
            }
            else
            {
                throw new Exception("Account does not exist");
            }
        }

        public async Task Register(AccountRequest request)
        {
            try
            {

                var user = await _accountRepo.GetByEmail(request.Email);
                if (user != null)
                {
                    throw new Exception("This email already taken !!");
                }

                ApplicationUsers User = new ApplicationUsers
                {
                    Email = request.Email,
                    Password = HashPasswordToSha256(request.Password),
                    Role = "Member",
                    Image = "https://res.cloudinary.com/dkedkbs8d/image/upload/v1731318264/gaidul2pwcgm0tiojro2.jpg"
                };
                await _accountRepo.Add(User);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SubcriptionRegister(int userId, int subcriptionId)
        {
            Subcriptions subcription = await _subcriptionRepo.GetById(subcriptionId);
            ApplicationUsers users = await _accountRepo.GetByIdAsync(userId);

            var existSubcription = users.UserSubcriptions?
                .Where(s => s.EndDate > DateTime.Now)
                .ToList();
            if (!existSubcription.Any())
            {
                var newSubcription = new UserSubcription
                {
                    StartDate = DateTime.Now.ToUniversalTime(),
                    EndDate = DateTime.Now.AddMonths(subcription.Duration).ToUniversalTime(),
                    UserId = userId,
                    SubcriptionId = subcriptionId
                };
                users.UserSubcriptions.Add(newSubcription);
                await _accountRepo.Update(users);
            }
            else
            {
                throw new Exception("Already have subcription");
            }
        }


        private string HashPasswordToSha256(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
