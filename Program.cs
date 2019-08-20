using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using Topshelf;

namespace lighttower_core
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>                                  
            {
                x.Service<Interface>(s =>                                  
                {
                    s.ConstructUsing(name => new Interface());             
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.StartManually();
                x.RunAsLocalSystem();                                      

                x.SetDescription("Sample Topshelf Host");                  
                x.SetDisplayName("Stuff");                                 
                x.SetServiceName("Stuff");                                 
            });                                                            
                                                                                                                  
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  
            Environment.ExitCode = exitCode;
        }
    }

        
    public class Interface
    {
        public void Start()
        {
            SerialPort _serialPort;
            bool _continue;
            // string name;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            //Thread readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM4"; //SetPortName(_serialPort.PortName);
            _serialPort.BaudRate = 9600; //SetPortBaudRate(_serialPort.BaudRate);
            _serialPort.Parity = Parity.None; //SetPortParity(_serialPort.Parity);
            _serialPort.DataBits = 8;//SetPortDataBits(_serialPort.DataBits);
            _serialPort.StopBits = StopBits.One;//SetPortStopBits(_serialPort.StopBits);
            _serialPort.Handshake = Handshake.None; //SetPortHandshake(_serialPort.Handshake);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();
            _continue = true;
            //readThread.Start();

            // Console.Write("Name: ");
            // name = Console.ReadLine();

            Console.WriteLine("Type QUIT to exit");
            var commands = new[] {
                "WR10000",
                "WR01000",
                "WR00100",
                "WR22200",
            };

            int i = 0;
            while (_continue)
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));

                //Console.WriteLine("Enter command:");
                //var message = Console.ReadLine();

                //if (stringComparer.Equals("quit", message))
                //{
                //    _continue = false;
                //}

                _serialPort.WriteLine(commands[i]);
                i = i + 1 < commands.Length ? i + 1 : 0;
            }

            // readThread.Join();
            _serialPort.Close();
        }

        public void Stop()
        {
            SerialPort _serialPort;
            bool _continue;
            // string name;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            //Thread readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM4"; //SetPortName(_serialPort.PortName);
            _serialPort.BaudRate = 9600; //SetPortBaudRate(_serialPort.BaudRate);
            _serialPort.Parity = Parity.None; //SetPortParity(_serialPort.Parity);
            _serialPort.DataBits = 8;//SetPortDataBits(_serialPort.DataBits);
            _serialPort.StopBits = StopBits.One;//SetPortStopBits(_serialPort.StopBits);
            _serialPort.Handshake = Handshake.None; //SetPortHandshake(_serialPort.Handshake);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();
            _continue = true;
            //readThread.Start();

            // Console.Write("Name: ");
            // name = Console.ReadLine();
            
            _serialPort.WriteLine("WR00000");

            // readThread.Join();
            _serialPort.Close();
        }
    }


}

