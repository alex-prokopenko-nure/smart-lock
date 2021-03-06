﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLock.Mobile.Models
{
    public class Lock
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Info { get; set; }
        public bool Locked { get; set; }

        public virtual ICollection<LockOperation> LockOperations { get; set; }
        public virtual ICollection<LockRent> LockRents { get; set; }
    }
}
