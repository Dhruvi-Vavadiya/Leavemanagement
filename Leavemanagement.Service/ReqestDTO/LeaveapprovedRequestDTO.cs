using System;
using System.Collections.Generic;
using System.Text;

namespace Leavemanagement.Service.ReqestDTO
{
    public class LeaveapprovedRequestDTO
    {
        public int Id { get; set; }
       
        public bool isApproved { get; set; }

        public string Reject_Reason { get; set; }
    }
}
