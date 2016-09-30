using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    static public class FileUtilsService
    {

        static public byte[] ConvertHtmlToPdf(string url)
        {

            const string fileName = " - ";
            const string wkhtmlDir = @"C:\Program Files\wkhtmltopdf\";
            const string wkhtml = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";
            var p = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    FileName = wkhtml,
                    WorkingDirectory = wkhtmlDir
                }
            };

            var switches = "";
            switches += "--print-media-type ";
            switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
            switches += "--page-size Letter ";
            p.StartInfo.Arguments = switches + " " + url + " " + fileName;
            p.Start();

            //read output
            var buffer = new byte[32768];
            byte[] file;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    var read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, read);
                }
                file = ms.ToArray();
            }
            // wait or exit
            p.WaitForExit(60000);
            p.Close();
            return file;
        }
    }

    }

