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
            keyboard type Hey guys, did you know that in terms of male human and female Pokémon breeding, Vaporeon is the most compatible Pokémon for humans? Not only are they in the field egg group, which is mostly comprised of mammals, Vaporeon are an average of 3”03’ tall and 63.9 pounds, this means they’re large enough to be able handle human dicks, and with their impressive Base Stats for HP and access to Acid Armor, you can be rough with one. Due to their mostly water based biology, there’s no doubt in my mind that an aroused Vaporeon would be incredibly wet, so wet that you could easily have sex with one for hours without getting sore. They can also learn the moves Attract, Baby-Doll Eyes, Captivate, Charm, and Tail Whip, along with not having fur to hide nipples, so it’d be incredibly easy for one to get you in the mood. With their abilities Water Absorb and Hydration, they can easily recover from fatigue with enough water. No other Pokémon comes close to this level of compatibility. Also, fun fact, if you pull out enough, you can make your Vaporeon turn white. Vaporeon is literally built for human dick. Ungodly defense stat+high HP pool+Acid Armor means it can take cock all day, all shapes and sizes and still come for more
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
   