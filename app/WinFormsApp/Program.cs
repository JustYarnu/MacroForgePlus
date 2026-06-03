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
            # Variables must be declared before any executable commands.
            engine setvar greeting Hello from the macro script
            engine setvar x 500
            engine setvar y 300
            engine setvar repeatCount 2
            engine setvar comboKey v
            engine updatevar greeting Hello from variables
            engine deletevar comboKey

            # Function definitions must appear before the main flow.
            engine setfunction greet
                keyboard type ${greeting}
                keyboard wait 150 press enter
            engine endfunction

            engine setfunction clickAndReturn
                mouse move ${x} ${y}
                mouse wait 100 press left
                mouse moveby -100 0
            engine endfunction

            engine setfunction roundTrip
                mouse move 100 100
                mouse wait 100 moveby 50 50
                mouse wait 100 moveby -50 -50
            engine endfunction

            # Begin main script. This section exercises all documented features.
            keyboard type ***START SCRIPT***
            keyboard wait 200 press enter

            engine callfunction greet

            mouse move 500 500
            mouse wait 200 press left
            mouse wait 150 press right

            mouse moveby 80 0
            mouse wait 150 scroll down 2
            mouse wait R[50,120] scroll up 1

            mouse hold left
            engine wait 100
            mouse release left

            keyboard hold shift
            engine wait 100
            keyboard release shift

            keyboard press a
            keyboard tap b
            keyboard combo ctrl c
            keyboard combo control v
            keyboard type Text after combo

            engine wait R[200,400]
            engine callfunction clickAndReturn

            engine repeat ${repeatCount}
                keyboard type loop iteration ${repeatCount}
                keyboard wait 100 press enter
            engine endrepeat

            engine ifon capslock
                keyboard type capslock is on
                keyboard wait 150 press enter
            engine endif

            engine ifoff capslock
                keyboard type capslock is off
                keyboard wait 150 press enter
            engine endif

            keyboard ifheld shift
                keyboard type shift key still held block
                keyboard wait 150 press enter
            keyboard endif

            mouse ifheld left
                keyboard type left mouse button still held block
                keyboard wait 150 press enter
            mouse endif

            engine callfunction roundTrip

            keyboard type ***END SCRIPT***
            keyboard wait 200 press enter
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
   