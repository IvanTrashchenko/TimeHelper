using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeHelper.Data.DbAccess.Models;
using TimeHelper.Data.DbAccess.Repositories.Base;

namespace TimeHelper.Data.DbAccess.Repositories
{
    internal class DateRepository : EFRepositoryBase<Date>
    {
        public DateRepository(DbContext context) : base(context)
        {
        }

        public Date? GetByName(string name)
        {
            return _dbSet.FirstOrDefault(d => d.DateName == name);
        }
    }
}
