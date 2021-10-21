using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArkWeb
{
    public class ArkUsers : IdentityUser
    {
        public string UsPass { get; set; }
        public int GroupID { get; set; }
    }
}
