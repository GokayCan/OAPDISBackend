using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.FormActivityCategoryRepository;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.FormActivityCategoryRepository
{
    public class EfFormActivityCategoryDal : EfEntityRepositoryBase<FormActivityCategory, SimpleContextDb>, IFormActivityCategoryDal
    {
    }
}
