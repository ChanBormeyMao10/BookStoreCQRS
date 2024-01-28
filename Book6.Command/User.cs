using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6
{
    public class NewUser : IMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
    public class UpdateUser : IMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class DeleteUser : IMessage
    {
        public Guid Id { get; set; }
    }
}
