using iTextSharp.text;
using iTextSharp.text.pdf;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace DownloadIMGToPDF
{
    public class Program
    {
        static void Main(string[] args)
        {
            string fileUrlSource = "https://image.isu.pub/200401132353-78fd8993414ea0750ea44387bcc5366d/jpg/";

            string path = Directory.GetCurrentDirectory();

            string target = @"c:\temp";

            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            //set CurrentDirectory
            var currentDirectory = Environment.CurrentDirectory = (target);

            int filesDownloads = FileDownloader.GetPagesAndDownload(fileUrlSource);

            if (filesDownloads > 0)
                ImgToPdf.CreatePdf(currentDirectory);

            Console.WriteLine("press any key to finish");
            Console.ReadLine();
        }

    }


}
