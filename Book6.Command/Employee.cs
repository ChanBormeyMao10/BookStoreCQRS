using Employee2;
using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6
{
    public class NewEmployee : IMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role role { get; set; }
    }
    public class UpdateName : IMessage
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
    }
    public class UpdateRole : IMessage
    {
        public Guid Id { get; set; }
        public Role NewRole { get; set; }
    }
}
