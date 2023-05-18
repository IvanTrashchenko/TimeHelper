using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeHelper.Data.DbAccess.Context;
using TimeHelper.Data.DbAccess.Models;
using TimeHelper.Data.DbAccess.Repositories;

namespace TimeHelper.Data.DbAccess.Services
{
    public static class DateService
    {
        #region Public methods

        public static void AddOrUpdateDate(string name, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            using (var context = new TimeHelperContext())
            {
                var dateRepository = new DateRepository(context);

                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var savedDate = dateRepository.GetByName(name);

                        if (savedDate == null)
                        {
                            savedDate = new Date()
                            {
                                DateName = name,
                                DateValue = date
                            };
                            dateRepository.Add(savedDate);
                        }
                        else
                        {
                            savedDate.DateValue = date;
                            dateRepository.Update(savedDate);
                        }

                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static Date? GetDate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            using (var context = new TimeHelperContext())
            {
                var dateRepository = new DateRepository(context);

                return dateRepository.GetByName(name);
            }
        }

        public static IEnumerable<Date> GetDates()
        {
            using (var context = new TimeHelperContext())
            {
                var dateRepository = new DateRepository(context);

                return dateRepository.GetAll();
            }
        }

        public static void DeleteDate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            using (var context = new TimeHelperContext())
            {
                var dateRepository = new DateRepository(context);

                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var savedDate = dateRepository.GetByName(name);

                        if (savedDate == null)
                        {
                            throw new InvalidOperationException($"Date with name {name} not found.");
                        }

                        dateRepository.Delete(savedDate);

                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}
