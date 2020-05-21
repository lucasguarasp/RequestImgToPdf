using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

public class FileDownloader
{
    public static int GetPagesAndDownload(string urlSource)
    {
        int countPage = 1;

        WebResponse response = null;
        try
        {
            do
            {
                string url = string.Concat(urlSource, $"page_{countPage}.jpg");
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Method = "HEAD";

                response = webRequest.GetResponse();

                if (response != null)
                {
                    Console.WriteLine("Request Ok: " + countPage);
                    DownloadFile(response.ResponseUri.AbsoluteUri, countPage);
                    countPage++;
                }

            } while (response != null);
        }
        catch
        {
            /* Any other response */
            return (countPage - 1);
        }
        finally
        {
            response.Close();
            Console.WriteLine("Total request success: " + (countPage - 1));
        }

        return (countPage - 1);
    }


    public static void DownloadFile(string url, int i)
    {
        try
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(url, $"pdf_{i}.jpg"); //Adress and Nome_File
                                                      //wc.DownloadFileAsync(new Uri(url), $"pdf_{i}.jpg");
                Console.WriteLine("Download Ok: " + i);

            }
        }
        catch (Exception e)
        {
            throw;
        }
    }
}