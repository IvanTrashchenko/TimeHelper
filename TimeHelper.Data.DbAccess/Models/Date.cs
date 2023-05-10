using System;
using System.Collections.Generic;

namespace TimeHelper.Data.DbAccess.Models;

public partial class Date
{
    public int DateId { get; set; }

    public string DateName { get; set; } = null!;

    public DateTime DateValue { get; set; }
}
