using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.Common
{
    public class Guid
    {
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsReserved { get; set; }
        public bool IsInWaitlist { get; set; }
    }
    public class User
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class Reservation
    {
        public System.Guid Id { get; set; }
        public Guid Book { get; set; }
        public User User { get; set; }
    }
    public class Waitlist
    {
        public System.Guid Id { get; set; }
        public Guid Book { get; set; }
        public User User { get; set; }
    }
}
