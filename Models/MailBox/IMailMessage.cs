using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.MailBox
{
    public interface IMailMessage
    {
        public void SendEmailWhenItIsTime (string recieverEmail);
        public void SendEmailWhenMeetingIsScheduled (string recieverEmail);
        
    }
}