using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SuhailApps.Core.Models.Identity;
using SuhailApps.Core.ViewModels.Accounts;

namespace SuhailApps.Core.Interfaces
{
    public interface IAccountService
    {

        Task<ApplicationUser> GetUser(string userId);

        Task<ApplicationUser> SaveUser(UserViewModel userViewModel);


    }
}
