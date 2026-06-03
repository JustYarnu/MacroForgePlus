using System;
using System.Linq;
using System.Reflection;

var packagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages", "windowsinput", "6.4.1", "lib", "net6.0-windows7.0", "WindowsInput.dll");
Console.WriteLine($"Loading assembly from: {packagePath}");

var asm = Assembly.LoadFrom(packagePath);
var types = asm.GetTypes();

Console.WriteLine("Candidate types containing 'Key' or 'Keyboard':");
foreach (var type in types.Where(t => t.Name.IndexOf("Key", StringComparison.OrdinalIgnoreCase) >= 0 || t.Name.IndexOf("Keyboard", StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(t => t.Name))
{
    Console.WriteLine(type.FullName);
}

var keyEnum = types.FirstOrDefault(t => t.IsEnum && t.Name.Equals("KeyCode", StringComparison.OrdinalIgnoreCase));
if (keyEnum == null)
{
    Console.WriteLine("No KeyCode enum type found.");
    return;
}

Console.WriteLine($"Found enum: {keyEnum.FullName}");
var names = Enum.GetNames(keyEnum);
var aliases = new[] { "ctrl", "control", "shift", "alt", "oskey", "altgr", "fn", "fnlock", "capslock", "numlock", "scrolllock" };
var filtered = names.Where(n => aliases.Any(alias => n.IndexOf(alias, StringComparison.OrdinalIgnoreCase) >= 0) || n.IndexOf("Menu", StringComparison.OrdinalIgnoreCase) >= 0 || n.IndexOf("Win", StringComparison.OrdinalIgnoreCase) >= 0)
    .OrderBy(n => n);

foreach (var name in filtered)
    Console.WriteLine(name);
