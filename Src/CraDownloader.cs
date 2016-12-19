using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Console = Colorful.Console;
using sonet.cra.Model;

namespace sonet.cra
{
    public class CraDownloader
    {
        public async Task<string> DownloadCra(HttpClient client, CRA cra, KeyValuePair<int, Mission> mission)
        {
            Console.Write($"Download cra of ", Color.Gainsboro);
            Console.WriteLine($"{mission.Value.Client} for the {cra.Month.Month}/{cra.Month.Year}", Color.DarkKhaki);

            var model = new { cra.IdCol, cra.Missions.First().Value.IdClient, cra.Month.Year, cra.Month.Month };
            var downloadUrl = $"http://sonet.soat.fr/PDF/CRAClient/{model.IdCol}/{model.IdClient}/{model.Year}/{model.Month}";
            var filename = Path.GetTempFileName();
            
            using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var file = await client.GetAsync(downloadUrl);
                await file.Content.CopyToAsync(fileStream);
            }

            return filename;
        }
    }
}
