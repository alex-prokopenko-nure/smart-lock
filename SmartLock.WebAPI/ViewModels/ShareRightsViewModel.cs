using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.ViewModels
{
    public class ShareRightsViewModel
    {
        public int AdminId { get; set; }
        public int OwnerId { get; set; }
        public int LockId { get; set; }
        public RentRights Rights { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
