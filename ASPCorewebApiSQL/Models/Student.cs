using System;
using System.Collections.Generic;

namespace ASPCorewebApiSQL.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public int? Salary { get; set; }
}
