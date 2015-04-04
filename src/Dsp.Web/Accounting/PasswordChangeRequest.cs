using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class PasswordChangeRequest
    {
        public string ConfirmPassword { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}