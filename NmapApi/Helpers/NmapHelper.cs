using NmapApi.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

public class NmapHelper
{
    public static List<NmapResult> RunNmapProcess(string targetIp)
    {
        try
        {
            Process process = new Process();
            process.StartInfo.FileName = "nmap";
            process.StartInfo.Arguments = $"-oX - {targetIp}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            StringBuilder output = new StringBuilder();
            StringBuilder error = new StringBuilder();

            process.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);
            process.ErrorDataReceived += (sender, args) => error.AppendLine(args.Data);


            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                return XmlParser.ParseXmlString(output.ToString(), targetIp);
            }
        }
        catch (Exception ex)
        {
            // Add logging here
        }

        return new List<NmapResult>();
    }
}