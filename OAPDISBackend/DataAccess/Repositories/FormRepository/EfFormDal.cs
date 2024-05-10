using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.FormRepository;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.FormRepository
{
    public class EfFormDal : EfEntityRepositoryBase<Form, SimpleContextDb>, IFormDal
    {
    }
}
