using System;
using System.Threading;
using WindowsInput; // Ensure InputSimulator is installed

class Program
{
    private static bool _isRunning = false;
    private static Thread _clickThread;

    static void Main(string[] args)
    {
        var sim = new InputSimulator();

        Console.WriteLine("Press F1 to start/stop the autoclicker. Press Esc to exit.");

        while (true)
        {
            if (sim.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.F1))
            {
                ToggleAutoclicker();
                Thread.Sleep(500); // Prevent repeated toggle due to key hold
            }

            if (sim.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.ESCAPE))
            {
                StopAutoclicker();
                break;
            }

            Thread.Sleep(50); // Reduce CPU usage
        }
    }

    private static void ToggleAutoclicker()
    {
        if (_isRunning)
        {
            StopAutoclicker();
        }
        else
        {
            _isRunning = true;
            _clickThread = new Thread(Autoclick);
            _clickThread.Start();
            Console.WriteLine("Autoclicker started.");
        }
    }

    private static void StopAutoclicker()
    {
        if (_isRunning)
        {
            _isRunning = false;
            _clickThread?.Join();
            Console.WriteLine("Autoclicker stopped.");
        }
    }

    private static void Autoclick()
    {
        var sim = new InputSimulator();
        while (_isRunning)
        {
            sim.Mouse.LeftButtonClick(); // Simulates a left mouse button click
            Thread.Sleep(100); // Adjust click interval as needed
        }
    }
}
