using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAutomation.Library
{
    /// <summary>
    /// Can be shared with actual website's ViewModel class.
    /// </summary>
    public class ContactUsViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
