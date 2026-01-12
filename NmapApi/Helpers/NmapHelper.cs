using NmapApi.Models;
using System.Diagnostics;
using System.Text;

public static class NmapHelper
{
    public static async Task<List<NmapResult>> RunNmapProcess(string targetIp)
    {
        // Properly handle and dispose process to not force GC to do it for us
        using (Process process = new Process())
        {
            // We will only handle a simple scan for now. In the future, we can add
            // various arguments and flags to our scans as well as supporting more 
            // verbose scanning.
            process.StartInfo.FileName = "nmap";
            process.StartInfo.Arguments = $"-oX - {targetIp}";

            // Have to not use shell execute here so that we can redirect output to 
            // our .NET thread.
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            StringBuilder output = new StringBuilder();
            process.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);

            // Use start here to not block main thread
            process.Start();

            // Make async I/O calls here to not cause deadlock
            process.BeginOutputReadLine();

            // wait for I/O data to be received
            await process.WaitForExitAsync();

            if (process.ExitCode == 0)
            {
                return XmlParser.ParseXmlString(output.ToString(), targetIp);
            }
        }

        return new List<NmapResult>();
    }
}