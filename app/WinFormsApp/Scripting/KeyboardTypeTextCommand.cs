using WindowsInput;
using WindowsInput.Events;

public class KeyboardTypeTextCommand : IMacroCommand
{
    public KeyCode[] TextKeys { get; }
    public string TextString { get; }

    // Standard constructor for pre-mapped arrays (special key combinations)
    public KeyboardTypeTextCommand(KeyCode[] textKeys)
    {
        TextKeys = textKeys;
        TextString = null;
    }

    // Constructor for direct string input - uses WindowsInput's built-in text typing
    public KeyboardTypeTextCommand(string text)
    {
        TextString = text;
        TextKeys = null;
    }

    public void Execute(InputController controller)
    {
        if (!string.IsNullOrEmpty(TextString))
        {
            // Use direct string input for proper text typing
            controller.KeyboardTypeText(TextString);
        }
        else if (TextKeys != null && TextKeys.Length > 0)
        {
            // Use KeyCode array for special key combinations
            controller.KeyboardTypeText(TextKeys);
        }
    }
}