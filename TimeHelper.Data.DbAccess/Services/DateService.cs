using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeHelper.Data.DbAccess.Models;

namespace TimeHelper.Data.DbAccess.Services
{
    public static class DateService
    {
        #region Constants

        private const int DB_MAX_TRY_COUNT = 3; // READ_COMMITTED_SNAPSHOT is ON, thus clients are expected to retry

        #endregion

        #region Public methods

        public static void AddOrUpdateDate(string name, DateTime date)
        {

        }

        public static Date GetDate(string name)
        {
            return null;
        }

        public static IEnumerable<Date> GetDates()
        {
            return null;
        }

        public static void DeleteDate(string name)
        {

        }

        #endregion
    }
}
