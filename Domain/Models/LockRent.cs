using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LockRent
    {
        public int UserId { get; set; }
        public int LockId { get; set; }
        public DateTime RentStart { get; set; }
        public DateTime? RentFinish { get; set; }
        public RentRights Rights { get; set; }

        public virtual Lock Lock { get; set; }
        public virtual User User { get; set; }
    }
}
