// See https://aka.ms/new-console-template for more information


using MimeKit;
using MailKit.Net.Smtp;
using System.Text.Json;
using dotenv.net;



class Program {
    async static Task Main(string[] args) {
        
        DotEnv.Load();
        
        HttpClient client = new HttpClient();

        double latitude = 33.7215;
        double longitude = 73.0433;
        
        WeatherResponse weatherResponse;
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            string response = await client.GetStringAsync($"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=rain");
            // Console.WriteLine(response);

            weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(response, options);                
        }
        catch (System.Exception)
        {
            throw;
        }
        
        int len = weatherResponse.Hourly.Rain.Count;
        string data = "";

        for (int i = 0; i < len; i++)
        {
            if (weatherResponse.Hourly.Rain[i] > 1)
            {
                Console.WriteLine($"Rain on {weatherResponse.Hourly.Time[i].DayOfWeek} at {weatherResponse.Hourly.Time[i].Hour}:00 approximately {weatherResponse.Hourly.Rain[i]}mm");
                data = data + ($"Rain on {weatherResponse.Hourly.Time[i].DayOfWeek} at {weatherResponse.Hourly.Time[i].Hour}:00 approximately {weatherResponse.Hourly.Rain[i]}mm") + "\n";
            }
        }
        
        if (data != "")
        {
            SendEmail(data);
            Console.WriteLine("Email SENT!!!");
        }

    }

    public static void SendEmail(string body)
    {

        string _GMAIL = Environment.GetEnvironmentVariable("GMAIL");
        string _GMAIL_USER = Environment.GetEnvironmentVariable("GMAIL_USER");
        string _GMAIL_APP_PASSWORD = Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_GMAIL_USER, _GMAIL));
        message.To.Add(new MailboxAddress(_GMAIL_USER, _GMAIL));
        message.Subject = "Rain Alert";

        message.Body = new TextPart("plain")
        {
            Text = @"Hey," + "\n" + "Be prepared its gonna rain." + "\n" + body
        };

        using (var client = new SmtpClient())
        {
            // Connect to the server (e.g., Gmail's SMTP)
            // 587 is the port, false indicates we are not using SSL immediately but starttls
            client.Connect("smtp.gmail.com", 587, false);

            // Authenticate
            client.Authenticate(_GMAIL, _GMAIL_APP_PASSWORD);

            client.Send(message);
            client.Disconnect(true);
        }
    }

}
