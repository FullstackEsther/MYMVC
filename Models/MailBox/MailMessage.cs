using MailKit.Net.Smtp;
using MimeKit;

namespace MYMVC.Models.MailBox
{
    public class MailMessage: IMailMessage
    {

        public async void SendEmailWhenItIsTime(string recieverEmail)
        {
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(recieverEmail));
            mssg.Subject = "Scheduled Meeting In 5 minutes";
            mssg.Body = new TextPart("html")
            {
                Text = "<p>Dear </p>"
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }

        public void SendEmailWhenMeetingIsScheduled(string recieverEmail)
        {
            throw new NotImplementedException();
        }
    }
    
}