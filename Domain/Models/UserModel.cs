using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserModel
    {	    
        public Guid UserId { get; set; }
        public required string UserName { get; set; } = string.Empty;
        public required string UserPasswordHash { get; set; } = string.Empty;

    }
}

