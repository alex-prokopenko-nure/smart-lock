using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationRole Role { get; set; }

        public virtual ICollection<LockRent> LockRents { get; set; }
    }
}
