using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.UserTypeRepository;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.UserTypeRepository
{
    public class EfUserTypeDal : EfEntityRepositoryBase<UserType, SimpleContextDb>, IUserTypeDal
    {
    }
}
