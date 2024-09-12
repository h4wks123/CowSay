using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            while (true)
            {
                // Prompt the user for input
                Console.WriteLine("POV: You are 🐄. Say something! Type 'exit' to quit.");
                string? input = Console.ReadLine();
		
		if (string.IsNullOrWhiteSpace(input)){
		    Console.WriteLine("Please say something!");
		    continue;
		}

                // If user types exit, the cow dies and session ends
                if (input?.Trim().ToLower() == "exit")
                {
                    Console.WriteLine("Thanks for having me!");
                    break;
                }

                // Set up the process start information for cowsay
                ProcessStartInfo startInfo = CreateProcessStartInfo();

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    // Write the input to the process
                    using (var sw = process.StandardInput)
                    {
                        if (sw.BaseStream.CanWrite)
                        {
                            sw.WriteLine(input);
                        }
                    }

                    // Read and display the output from the process
                    string output = process.StandardOutput?.ReadToEnd() ?? string.Empty;
                    string error = process.StandardError?.ReadToEnd() ?? string.Empty;

                    process.WaitForExit();

                    Console.WriteLine(output);

                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception($"Process error: {error}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{ex.Message}");
        }
    }

    static ProcessStartInfo CreateProcessStartInfo()
    {
        return new ProcessStartInfo
        {
            FileName = "/usr/games/cowsay",
            RedirectStandardInput = true,  
            RedirectStandardOutput = true, 
            RedirectStandardError = true,  
            UseShellExecute = false,       
            CreateNoWindow = true           
        };
    }
}
