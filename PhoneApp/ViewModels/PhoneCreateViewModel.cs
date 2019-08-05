using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApp.ViewModels
{
    public class PhoneCreateViewModel: Phone
    {
        public SelectList Companies { get; set; }
    }
}
