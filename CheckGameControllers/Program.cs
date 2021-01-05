using System;
using SharpDX.DirectInput;
using System.Collections.Generic;
using CommandLine;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace CheckGameControllers
{
    public class Options
    {
        [Option('c', "controllers", Required = false, HelpText = "List of controllers - separate by commas NO spaces i.e. T.16000M,T.16000M")]
        public string Controllers { get; set; }
        [Option('a', "app", Required = false, HelpText = "App to run if all controllers are found")]
        public string App { get; set; }
    }

    class Program
    {
        static readonly DirectInput input = new DirectInput();

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(opts => RunApplication(opts));
        }

        private static void RunApplication(Options opts)
        {
            List<string> controllerNames;

            if (string.IsNullOrEmpty(opts.Controllers))
                controllerNames = ReadControllerFile();
            else
                controllerNames = opts.Controllers.Split(',').ToList();

            if (controllerNames == null || controllerNames.Count < 1)
                MessageBox.Show($"Specify game controllers to look for by adding --controllers or placing them in the controllers.txt file.");

            foreach (DeviceInstance device in input.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly))
            {
                Joystick stick = new Joystick(input, device.InstanceGuid);
                if (controllerNames.Contains(stick.Properties.ProductName))
                    controllerNames.Remove(stick.Properties.ProductName);
            }

            if (controllerNames.Count > 0)
                MessageBox.Show($"You are missing: {string.Join(", ", controllerNames.ToArray())}");
            else
            {
                if (!string.IsNullOrEmpty(opts.App))
                {
                    try
                    {
                        Process.Start(opts.App);
                    } catch (Win32Exception _) {
                        MessageBox.Show($"{opts.App} not found, did you specify the full path to the file?");
                    }
                }
            }
            Environment.Exit(controllerNames.Count);
        }

        private static List<string> ReadControllerFile()
        {
            List<string> rv = new List<string>();
            FileStream fileStream = new FileStream("controllers.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                    rv.Add(reader.ReadLine());
            }

            return rv;
        }
    }
}
