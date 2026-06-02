using System.Threading;
using WindowsInput;
using WindowsInput.Events;

public class KeyboardController
{
    public void KeyDown(KeyCode key)
    {
        Simulate.Events().Hold(key).Invoke();
    }

    public void KeyUp(KeyCode key)
    {
        Simulate.Events().Release(key).Invoke();
    }

    public void KeyPress(KeyCode key)
    {
        Simulate.Events().Click(key).Invoke();
    }


    public void TypeText(string text)
    {
        foreach (char c in text)
        {
            Simulate.Events().Click(c).Invoke();
            Thread.Sleep(30);
        }
    }

    public void TypeText(KeyCode[] keys)
    {
        var eventSimulator = Simulate.Events();
        foreach (KeyCode key in keys)
        {
            eventSimulator.Click(key);
            Thread.Sleep(30);
        }
        eventSimulator.Invoke();
    }

    // Handles combinations like Ctrl+C
    public void ModifiedStroke(KeyCode modifier, KeyCode key)
    {
        Simulate.Events()
            .Hold(modifier)
            .Click(key)
            .Release(modifier)
            .Invoke();
    }
}