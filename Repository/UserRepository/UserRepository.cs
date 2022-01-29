using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly CompEmpIdentityServerContext _dbAppContext;

        public UserRepository(CompEmpIdentityServerContext dbAppContext)
        {
            _dbAppContext = dbAppContext;
        }
        public async Task<User> Login(string userName, string password)
        {
            User userDataModel = null;
            try
            {

                userDataModel = await _dbAppContext.Users.FirstOrDefaultAsync(x =>
                      x.UserName == userName &&
                      x.Password == password
                    );

                if (userDataModel == null)
                {
                    throw null;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return userDataModel;

        }
    }
}
