using System;
using System.Collections.Generic;

namespace SchoolManagementApp.MVC.Data;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Enrolllment> Enrolllments { get; set; } = new List<Enrolllment>();
}
