using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using CommandLine;
using CommandLine.Text;
using sonet.cra.Model;
using Console = Colorful.Console;

namespace sonet.cra
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteAscii("SONET CRA", Color.Chartreuse);

                var options = new Options();
                var isValid = Parser.Default.ParseArgumentsStrict(args, options);
                if (!isValid)
                {
                    Console.Write(HelpText.AutoBuild(options).ToString());
                    return;
                }

                var userpass = new CredentialManager().GetOrCreateCredential();
                var cookies = new Authenticator().Authenticate(userpass);
                var client = new HttpClient(new HttpClientHandler { CookieContainer = cookies });

                SignAllMissionsCra(client, options.Year, options.Month);

                Console.WriteLine("\n\nALL DONE !" , Color.LawnGreen);
            }
            catch (Exception e)
            {
                Console.WriteLine("FAILED: " + e.Message, Color.Red);
            }

            Console.ReadLine();
        }

        public static CRA GetCraForMonth(HttpClient client, int year, int month)
        {
            var content = new StringContent($"{{\"month\":\"{year}-{month:00}-01T00:00:00.000Z\" }}", Encoding.UTF8, "application/json");
            var result = client.PostAsync("http://sonet.soat.fr/CRA/GetEditViewModel", content).Result;
            var response = result.Content.ReadAsStringAsync().Result;

            var cra = Newtonsoft.Json.JsonConvert.DeserializeObject<CRA>(response);

            return cra;
        }

        public static void SignAllMissionsCra(HttpClient client, int year, int month)
        {
            var cra = GetCraForMonth(client, year, month);

            foreach (var mission in cra.Missions)
            {
                var dest = Path.Combine(AppContext.BaseDirectory, $"{cra.Month.Year}-{cra.Month.Month:00}_CRA_Client_{mission.Value.Client}.pdf");

                var tempfile = new CraDownloader().DownloadCra(client, cra, mission).Result;
                new PdfWatermarker().AddWatermark(tempfile, dest, "Sign.png");
                Console.Write($"file created : ", Color.Gainsboro);
                Console.WriteLine(dest, Color.DarkKhaki);
            }
        }
    }
}
