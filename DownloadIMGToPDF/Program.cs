using System;
using System.IO;

namespace DownloadIMGToPDF
{
    public class Program
    {
        static void Main(string[] args)
        {
            string fileUrlSource = "https://image.isu.pub/200401132353-78fd8993414ea0750ea44387bcc5366d/jpg/";

            #region path
            string path = Directory.GetCurrentDirectory();

            string target = @"c:\temp";

            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            //set CurrentDirectory
            var currentDirectory = Environment.CurrentDirectory = (target);
            #endregion

            int filesDownloads = FileDownloader.GetPagesAndDownload(fileUrlSource);

            if (filesDownloads > 0)
                ImgToPdf.CreatePdf(currentDirectory);

            Console.WriteLine("press any key to finish");
            Console.ReadLine();
        }

    }


}
