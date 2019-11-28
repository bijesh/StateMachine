using System;
using System.Diagnostics;
using System.IO;

namespace StateMachine
{
    public class DotFileHelper
    {
        public void CreateDotFile()
        {
            var orderStateMachine = new OrderStateMachine(TestState.Idle);
            var filename = $"OrderStateChart{new Random().Next(1, 100)}";
            var filePath = $@"C:\work\{filename}.dot";
            var dotExePath = @"C:\Program Files (x86)\Graphviz2.38\bin\";
            using (FileStream fileStream =
                File.Create(filePath))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                string dotGraph = orderStateMachine.ExportToGraph();
                writer.WriteLine(dotGraph);
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = $"{dotExePath}dot.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = $@"-T pdf -o C:\Work\{filename}.pdf C:\Work\{filename}.dot";
            Process.Start(startInfo);

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}
