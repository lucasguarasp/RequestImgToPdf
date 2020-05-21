using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DownloadIMGToPDF
{
     public class ImgToPdf
    {
        public static void CreatePdf(string path)
        {
            List<string> images = GetImg(path);
            //var a = images.OrderBy(q => q).ToList();

            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            using (var stream = new FileStream("Download.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, stream);
                document.Open();

                foreach (var img in images)
                {
                    //using (var imageStream = new FileStream("pdf_136.jpg", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var imageStream = new FileStream(img, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var image = Image.GetInstance(imageStream);
                        image.ScaleAbsolute(PageSize.A4);
                        document.Add(image);
                    }
                }

                document.Close();
            }
        }

        public static List<string> GetImg(string path)
        {
            try
            {
                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

                List<string> imageFiles = new List<string>();
                foreach (string filename in files)
                {
                    if (Regex.IsMatch(filename, @".jpg|.png|.gif$"))
                        imageFiles.Add(filename.Replace(path + "\\", ""));
                }

                var listOrdened = SortListImg(imageFiles).ToArray();

                return listOrdened.ToList();

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static IEnumerable<string> SortListImg(IEnumerable<string> list)
        {
            int maxLen = list.Select(s => s.Length).Max();
            Func<string, char> PaddingChar = s => char.IsDigit(s[0]) ? ' ' : char.MaxValue;

            return list
                    .Select(s =>
                        new
                        {
                            OrgStr = s,
                            SortStr = Regex.Replace(s, @"(\d+)|(\D+)", m => m.Value.PadLeft(maxLen, PaddingChar(m.Value)))
                        })
                    .OrderBy(x => x.SortStr)
                    .Select(x => x.OrgStr);
        }


    }
}
