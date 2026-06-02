using System;
using System.Threading;
using WindowsInput.Events;

public class InputController
{
    private MouseController mouse;
    private KeyboardController keyboard;
    private Random rnd;

    public InputController()
    {
        mouse = new MouseController();
        keyboard = new KeyboardController();
        rnd = new Random();
    }
    
    public void Wait(int delay)
    {
        Thread.Sleep(delay);
    }

    public void RandomWait(int lowerBound, int upperBound)
    {
        if (lowerBound > upperBound) (lowerBound, upperBound) = (upperBound, lowerBound);
        Thread.Sleep(rnd.Next(lowerBound, upperBound + 1));
    }

    public void MouseMoveTo(int x, int y) => mouse.MoveTo(x, y);
    public void MouseMoveBy(int x, int y) => mouse.MoveBy(x, y);
    public void MouseScroll(ButtonCode direction, int clicks) => mouse.Scroll(direction, clicks);

    public void MouseButtonDown(ButtonCode button) => mouse.ButtonDown(button);
    public void MouseButtonUp(ButtonCode button) => mouse.ButtonUp(button);
    public void MouseButtonPress(ButtonCode button) => mouse.ButtonPress(button);
    public void MouseDelayedButtonDown(ButtonCode button, int delay)
    {
        Thread.Sleep(delay);
        MouseButtonDown(button);
    }

    public void RandomDelayedMouseButtonDown(ButtonCode button, int lowerBound, int upperBound)
    {
        RandomWait(lowerBound, upperBound);
        MouseButtonDown(button);
    }

    public void MouseDelayedButtonUp(ButtonCode button, int delay)
    {
        Thread.Sleep(delay);
        MouseButtonUp(button);
    }

    public void RandomDelayedMouseButtonUp(ButtonCode button, int lowerBound, int upperBound)
    {
        RandomWait(lowerBound, upperBound);
        MouseButtonUp(button);
    }

    public void DelayedMouseButtonPress(ButtonCode button, int delay)
    {
        Thread.Sleep(delay);
        MouseButtonPress(button);
    }
    
    public void RandomDelayedMouseButtonPress(ButtonCode button, int lowerBound, int upperBound)
    {
        RandomWait(lowerBound, upperBound);
        MouseButtonPress(button);
    }
    public void KeyboardKeyDown(KeyCode key) => keyboard.KeyDown(key);
    public void KeyboardKeyUp(KeyCode key) => keyboard.KeyUp(key);
    public void KeyboardKeyPress(KeyCode key) => keyboard.KeyPress(key);
    
    public void KeyboardModifiedStroke(KeyCode modifier, KeyCode key) => keyboard.ModifiedStroke(modifier, key);
    
    public void KeyboardTypeText(string text) => keyboard.TypeText(text);
    public void KeyboardTypeText(KeyCode[] text) => keyboard.TypeText(text);

    public void KeyboardDelayedKeyPress(KeyCode key, int delay)
    {
        Thread.Sleep(delay);
        KeyboardKeyPress(key);
    }

    public void RandomDelayedKeyboardKeyPress(KeyCode key, int lowerBound, int upperBound)
    {
        RandomWait(lowerBound, upperBound);
        KeyboardKeyPress(key);
    }
}