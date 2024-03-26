using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Helper
{
    public static class ClaimsStore
    {
        public  static List<Claim> claims = new()
        {
            new Claim("Add","False"),
            new Claim("Update","False"),
            new Claim("Delete","False"),
        };
    }
}
