using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Test Script Initialized");

        // dummy macro script
        /*string testScript = @"
            # Move mouse slightly and click to focus the window
            mouse move 500 500
            mouse wait R[50,150] press left

            # Wait a moment, then type text bulk
            engine wait 1000
            keyboard type hello world! this macro engine actually works.

            # Perform a delayed enter press
            keyboard wait 500 press enter

            # Test a randomized delay before another action
            keyboard wait R[200,600] type testing r interval syntax...
        ";*/

        string testScript = @"
            # Type speed test
            keyboard type This is a typing speed test for the macro engine. It should type at around 200WPM with high consistency. Lorem ipsum dolor sit amet, consectetur adipiscing elit.
            ";

        var controller = new InputController();
        var parser = new ScriptParser();
        var engine = new ExecutionEngine(controller);

        try
        {
            Console.WriteLine("Parsing script...");
            var commands = parser.ParseScript(testScript);
            Console.WriteLine($"Successfully parsed {commands.Count} commands.");

            Console.WriteLine("\n[PREPARATION] Open Notepad and click inside it now!");
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine($"Starting in {i}...");
                await Task.Delay(1000);
            }

            Console.WriteLine("\nRunning macro...");
            await engine.RunAsync(commands);
            
            Console.WriteLine("\nTest Finished Successfully!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[TEST FAILED] {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPress any key to exit.");
        Console.ReadKey();
    }
}      
   