using System.Drawing;
using System.IO;
using iTextSharp.text.pdf;
using Console = Colorful.Console;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace sonet.cra
{
    public class PdfWatermarker
    {
        public void AddWatermark(string input, string destination, string watermark)
        {
            Console.WriteLine($"Adding sign on pdf... ", Color.Gainsboro);

            using (var pdfReader = new PdfReader(input))
            {
                using (Stream output = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (var pdfStamper = new PdfStamper(pdfReader, output))
                    {
                        var pageIndex = 1;
                        pdfStamper.FormFlattening = false;

                        var pdfData = pdfStamper.GetOverContent(pageIndex);

                        pdfData.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10);

                        var graphicsState = new PdfGState { FillOpacity = 1F };

                        pdfData.SetGState(graphicsState);
                        pdfData.BeginText();

                        var jpeg = Image.GetInstance(watermark, true);

                        jpeg.ScaleToFit(jpeg.Width, jpeg.Height);

                        jpeg.SetAbsolutePosition(80, 260);
                        
                        pdfData.AddImage(jpeg);

                        pdfData.EndText();

                    }

                }

            }
        }

    }
}
