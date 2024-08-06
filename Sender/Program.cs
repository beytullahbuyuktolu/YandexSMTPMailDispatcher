using Microsoft.Extensions.Configuration;
using Sender;
class Program
{
    public static IConfiguration _configuration { get; set; }
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        _configuration = builder.Build();
        EmailSender emailSender = new EmailSender(_configuration);
        emailSender.SendEmail("hi@beytullah.net", "Test Konusu", "Bu bir test mailidir.");
        Console.WriteLine("Mail gönderildi.");
    }
}