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
            # Initial delay for setup
            engine wait 1000

            # Basic keyboard typing and once-off delay
            keyboard type hello from the macro script
            keyboard wait 200 press enter

            # Loop block for repeat support
            engine repeat 2
                keyboard type repeated output line
                keyboard wait 150 press enter
            engine endrepeat

            # Keyboard conditional block: hold shift to execute
            keyboard ifheld shift
                keyboard type shift is held
                keyboard wait 150 press enter
            keyboard endif

            # Mouse conditional block: hold left mouse button to execute
            mouse ifheld left
                keyboard type left mouse button is held
                keyboard wait 150 press enter
            mouse endif

            # Toggle conditionals for capslock state
            engine ifon capslock
                keyboard type capslock is on
                keyboard wait 150 press enter
            engine endif

            engine ifoff capslock
                keyboard type capslock is off
                keyboard wait 150 press enter
            engine endif

            # Nested repeat inside a toggle conditional
            engine ifon numlock
                engine repeat 2
                    keyboard type nested repeat under numlock
                    keyboard wait 150 press enter
                engine endrepeat
            engine endif
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
   