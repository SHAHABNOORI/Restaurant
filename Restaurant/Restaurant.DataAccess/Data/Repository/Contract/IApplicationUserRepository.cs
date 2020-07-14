using System;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Contract
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, Guid>, IRepositoryAsync<ApplicationUser, Guid>
    {
        void Lock(ApplicationUser applicationUser);

        void UnLock(ApplicationUser applicationUser);
    }
}