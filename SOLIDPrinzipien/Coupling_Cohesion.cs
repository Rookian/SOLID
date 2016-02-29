using System;
using System.Net.Mail;

namespace SOLIDPrinzipien
{
    public class Program
    {
         public static void Main() { }
    }

    public class MailSender : IMailSender
    {
        public string ConcatContent(string content2, string content1)
        {
            throw new NotImplementedException();
        }

        public string FormatBody(string body)
        {
            throw new NotImplementedException();
        }

        public string GetSubject(string configuration)
        {
            throw new NotImplementedException();
        }

        public void SendMail(string content, string @from, string to)
        {
            throw new NotImplementedException();
        }

        public string SearchMailAdress(string search)
        {
            throw new NotImplementedException();
        }

        public SmtpClient GetSmptClient(string configuration)
        {
            throw new NotImplementedException();
        }
    }

    // Cohesion: niedrig oder hoch?
    public interface IMailSender
    {
        string ConcatContent(string content2, string content1);
        string FormatBody(string body);
        string GetSubject(string configuration);
        void SendMail(string content, string from, string to);
        string SearchMailAdress(string search);
        SmtpClient GetSmptClient(string configuration);
    }

    // Cohesion: niedrig oder hoch?
    public interface IBodyRenderer
    {
        bool ShouldRender(string configuration);
        void Render(string content);
    }

    // Coupling sortieren von niedrig zu hoch
    public class MailController1
    {
        private readonly IMailSender _mailSender;
        private readonly ILogger _logger;

        public MailController1(IMailSender mailSender, ILogger logger)
        {
            _mailSender = mailSender;
            _logger = logger;
        }

        public void DoSomething()
        {
            _logger.Log("DoSomething");
            _mailSender.SendMail("Hello, it's me.", "Alex", "Thomas");
            _logger.Log("DoSomething");
        }
    }

    public class MailController2
    {
        public void DoSomething()
        {
            var mailSender = new MailSender();
            mailSender.SendMail("Hello, it's me.", "Alex", "Thomas");
        }
    }
}