using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkerProject.Models;

public partial class ManyWorker
{
    public ManyWorker(string email, string name)
    {
        if(email == null) throw new ArgumentNullException("email nem lehet üres!");
        if (!email.Contains('@')) throw new ArgumentException("Hibás email cím!");
        if (name == null) throw new ArgumentNullException("Név nem lehet üres!");
        else
        {
            this.email = email;
            salary = 0;
        }
    }

    public string Name { get; set; }

    public string? Email { get => email; set => email = value; }

    public int Salary { get => salary; set => salary = (int)value; }

    private string email { get; set; }
    private int salary { get; set; }



    public override string ToString()
    {
        return $"{Name}({Email}): {Salary} Ft";
    }
}
