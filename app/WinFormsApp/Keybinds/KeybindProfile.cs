using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WinFormsApp.Keybinds;

/// <summary>
/// Represents a profile containing multiple keybind entries.
/// Profiles can be saved and loaded from JSON files for persistence.
/// </summary>
public class KeybindProfile
{
    /// <summary>
    /// The name of the profile.
    /// </summary>
    public string Name { get; set; } = "Default";

    /// <summary>
    /// Description of the profile.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// List of keybind entries in this profile.
    /// </summary>
    public List<KeybindEntry> Keybinds { get; set; } = new List<KeybindEntry>();

    /// <summary>
    /// Whether this profile should be auto-loaded on startup.
    /// </summary>
    public bool AutoLoad { get; set; } = false;

    /// <summary>
    /// Gets the default profile file path.
    /// </summary>
    public static string DefaultProfilePath
    {
        get
        {
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MacroForgePlus");
            
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            
            return Path.Combine(appDataPath, "keybinds.json");
        }
    }

    /// <summary>
    /// Saves the profile to a JSON file.
    /// </summary>
    /// <param name="filePath">Optional custom file path. Uses default if not specified.</param>
    public void Save(string? filePath = null)
    {
        filePath ??= DefaultProfilePath;
        
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };
        
        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Loads a profile from a JSON file.
    /// </summary>
    /// <param name="filePath">Optional custom file path. Uses default if not specified.</param>
    /// <returns>The loaded profile, or a new empty profile if the file doesn't exist.</returns>
    public static KeybindProfile Load(string? filePath = null)
    {
        filePath ??= DefaultProfilePath;
        
        if (!File.Exists(filePath))
        {
            return new KeybindProfile();
        }
        
        try
        {
            string json = File.ReadAllText(filePath);
            var profile = JsonSerializer.Deserialize<KeybindProfile>(json);
            return profile ?? new KeybindProfile();
        }
        catch (Exception)
        {
            // If deserialization fails, return a new empty profile
            return new KeybindProfile();
        }
    }

    /// <summary>
    /// Adds a keybind entry to the profile.
    /// </summary>
    public void AddKeybind(KeybindEntry entry)
    {
        Keybinds.Add(entry);
    }

    /// <summary>
    /// Removes a keybind entry from the profile.
    /// </summary>
    public void RemoveKeybind(KeybindEntry entry)
    {
        Keybinds.Remove(entry);
    }

    /// <summary>
    /// Removes a keybind entry by its ID.
    /// </summary>
    public void RemoveKeybind(Guid id)
    {
        var entry = Keybinds.Find(e => e.Id == id);
        if (entry != null)
        {
            Keybinds.Remove(entry);
        }
    }

    /// <summary>
    /// Checks if a key combination is already bound.
    /// </summary>
    /// <param name="entry">The entry to check against (excludes its own ID from the check).</param>
    /// <returns>True if the key combination is already used by another entry.</returns>
    public bool IsKeybindTaken(KeybindEntry entry)
    {
        return Keybinds.Exists(e => 
            e.Id != entry.Id && 
            e.Key == entry.Key && 
            e.Control == entry.Control && 
            e.Alt == entry.Alt && 
            e.Shift == entry.Shift);
    }

    /// <summary>
    /// Enables all keybinds in the profile.
    /// </summary>
    public void EnableAll()
    {
        foreach (var keybind in Keybinds)
        {
            keybind.Enabled = true;
        }
    }

    /// <summary>
    /// Disables all keybinds in the profile.
    /// </summary>
    public void DisableAll()
    {
        foreach (var keybind in Keybinds)
        {
            keybind.Enabled = false;
        }
    }

    /// <summary>
    /// Gets all enabled keybinds.
    /// </summary>
    public IEnumerable<KeybindEntry> GetEnabledKeybinds()
    {
        return Keybinds.FindAll(k => k.Enabled);
    }
}