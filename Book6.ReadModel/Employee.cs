using Employee2;
using Raven.Abstractions.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel
{
    public class Employee
    {
        public Employee()
        {

        }
        public Employee(Guid id)
        {
            Id = id;
        }
        public Employee(string name)
        {
            Name = name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role role { get; set; }
    }
}

