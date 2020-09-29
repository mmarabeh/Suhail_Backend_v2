using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuhailApps.Core.ViewModels.Accounts
{

    public class ChangeNumberViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string VerificationCode { get; set; }
    }
}
