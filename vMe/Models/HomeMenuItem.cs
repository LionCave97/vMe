using System;
using System.Collections.Generic;
using System.Text;

namespace vMe.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Robot
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
