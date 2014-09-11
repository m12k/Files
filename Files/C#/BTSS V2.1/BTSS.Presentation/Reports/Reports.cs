using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Presentation.Reports
{
    class GroupOfUsers
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; } 
    }

    class GroupOfUsersDetails
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ComputerName { get; set; }    
    }


}
