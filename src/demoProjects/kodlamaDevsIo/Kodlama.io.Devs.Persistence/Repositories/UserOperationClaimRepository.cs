using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Persistence.Contexts;

namespace Kodlama.io.Devs.Persistence.Repositories
{
    internal class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, KodlamaIoContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(KodlamaIoContext context) : base(context)
        {
        }
    }
}
