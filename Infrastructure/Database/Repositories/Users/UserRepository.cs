using Domain.Models.User;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Database.Repositories.Users
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly AppDbContext _context;

//        public UserRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        public Task<List<User>> GetAllUsers()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<User> RegisterUser(User userToRegister)
//        {
//            _context.Users.Add(userToRegister);
//            _context.SaveChanges();
//            return await Task.FromResult(userToRegister);
//        }
//    }
//}
