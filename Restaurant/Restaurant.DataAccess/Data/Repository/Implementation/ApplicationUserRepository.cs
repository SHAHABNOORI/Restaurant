using System;
using Restaurant.DataAccess.Data.Repository.Contract;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Implementation
{
    public class ApplicationUserRepository : Repository<ApplicationUser, Guid>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Lock(ApplicationUser applicationUser)
        {
            applicationUser.LockoutEnd = DateTimeOffset.Now;
        }

        public void UnLock(ApplicationUser applicationUser)
        {
            applicationUser.LockoutEnd = DateTimeOffset.Now.AddYears(100);
        }
    }
}