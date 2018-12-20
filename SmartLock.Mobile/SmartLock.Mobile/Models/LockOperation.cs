using SmartLock.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLock.Mobile.Models
{
    public class LockOperation
    {
        public int Id { get; set; }
        public LockState State { get; set; }
        public DateTime CreateTime { get; set; }
        public int LockId { get; set; }

        public virtual Lock Lock { get; set; }
    }
}
