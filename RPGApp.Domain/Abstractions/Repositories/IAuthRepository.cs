using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.Abstractions.Repositories
{
    public interface IAuthRepository
    {
        Task<int> Register(User user, string password);

        Task<string> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}
