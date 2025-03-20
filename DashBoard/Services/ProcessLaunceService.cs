using System.Diagnostics;
using System.IO;

namespace DashBoard.Services.IntroLauncher
{
    public class ProcessLaunceService
    {
        public void StartProgram(string programPath)
        {
            if (!File.Exists(programPath)) return;

            Process.Start(new ProcessStartInfo
            {
                FileName = programPath,
                UseShellExecute = true
            });
        }
    }
}
