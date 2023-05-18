using System;
using System.Collections.Generic;

namespace TimeHelper.Data.DbAccess.Models;

public partial class Date
{
    public int DateId { get; set; }

    public string DateName { get; set; } = null!;

    public DateTime DateValue { get; set; }

    public string TimePassed
    {
        get
        {
            var result = CalculateDifference(DateTime.Now, this.DateValue);
            return $"{result.years} years, {result.months} months, {result.days} days";
        }
    }

    public string TimeUntilNextAnniversary
    {
        get
        {
            var result = TimeUntilNextAnniversaryInner(this.DateValue);
            return $"{result.years} years, {result.months} months, {result.days} days";
        }
    }

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
}
