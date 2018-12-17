using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLock.Mobile.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Login,
        Register
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
