using System;
using System.Collections.Generic;
using System.Text;

namespace TimeHelper.Data.DbAccess.Models;

public partial class Date
{
    #region Properties

    public int DateId { get; set; }

    public string DateName { get; set; } = null!;

    public DateTime DateValue { get; set; }

    public string TimePassed
    {
        get
        {
            var result = CalculateDifference(DateTime.Now, this.DateValue);
            return CreateStringRepresentation(result.years, result.months, result.days);
        }
    }

    public string TimeUntilNextAnniversary
    {
        get
        {
            var result = TimeUntilNextAnniversaryInner(this.DateValue);
            return CreateStringRepresentation(result.years, result.months, result.days);
        }
    }

    #endregion

    #region Public methods

    public TimeSpan GetTimeUntilNextAnniversary()
    {
        DateTime today = DateTime.Today;
        DateTime nextAnniversary = new DateTime(today.Year, DateValue.Month, DateValue.Day);

        if (nextAnniversary < today)
        {
            nextAnniversary = nextAnniversary.AddYears(1);
        }

        return nextAnniversary - today;
    }

    #endregion

    #region Private methods

    private static (int years, int months, int days) CalculateDifference(DateTime date1, DateTime date2)
    {
        int years = date1.Year - date2.Year;
        int months = date1.Month - date2.Month;
        int days = date1.Day - date2.Day;

        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(date1.Year, date1.Month);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return (years, months, days);
    }

    private static (int years, int months, int days) TimeUntilNextAnniversaryInner(DateTime date)
    {
        DateTime today = DateTime.Today;
        DateTime nextAnniversary = new DateTime(today.Year, date.Month, date.Day);

        if (nextAnniversary < today)
        {
            nextAnniversary = nextAnniversary.AddYears(1);
        }

        int years = nextAnniversary.Year - today.Year;
        int months = nextAnniversary.Month - today.Month;
        int days = nextAnniversary.Day - today.Day;

        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(today.Year, today.Month);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return (years, months, days);
    }

    private string CreateStringRepresentation(int years, int months, int days)
    {
        StringBuilder sb = new StringBuilder();

        if (years > 0)
        {
            sb.Append($"{years} year");

            if (years != 1)
            {
                sb.Append("s");
            }
        }

        if (months > 0)
        {
            if (sb.ToString() != string.Empty)
            {
                sb.Append(" ");
            }

            sb.Append($"{months} month");

            if (months != 1)
            {
                sb.Append("s");
            }
        }

        if (days > 0)
        {
            if (sb.ToString() != string.Empty)
            {
                sb.Append(" ");
            }

            sb.Append($"{days} day");

            if (days != 1)
            {
                sb.Append("s");
            }
        }

        return sb.ToString();
    }

    #endregion
}
