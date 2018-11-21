using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICMarkets.Cameras.Common;
using ICMarkets.Cameras.Common.Commands;
using ICMarkets.Cameras.Common.Communication;
using ICMarkets.Cameras.Lg;
using ICMarkets.Cameras.Samsung;
using ICMarkets.Commands;
using ICMarkets.Devices.Common.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ICMarkets.Console
{
    public class Program
    {
        // Keep console app as simple as posible
        // No need to create pseudo-real structure for demo purposes.

        private static IServiceProvider _serviceProvider;
        private static readonly List<ICamera> Cameras = new List<ICamera>();
        private static readonly List<Type> Commands = new List<Type>();

        public static async Task Main(string[] args)
        {
            // Configure DI container
            ConfigureServices();

            // Add some cameras to show independence from a particular camera
            AddCameras();

            // Add available commands, just iterate it, no reflection or smth like that, keep it simple.
            AddCommands();


            var originalColor = System.Console.ForegroundColor;
            System.Console.WriteLine("To exit press Ctrl+C");
            while (true)
            {
                try
                {
                    await Task.Delay(10);

                    var camera = SelectCameraDialog();
                    var command = SelectCommandDialog(camera);
                    await command.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(ex);
                    System.Console.ForegroundColor = originalColor;
                }
            }

            // Infinite loop, supress warning, to exit press Ctrl + C
            // ReSharper disable once FunctionNeverReturns
        }

        #region ### Helper Methods ###

        private static void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<ICommunicationChannel, RtspCommunicationChannel>();
            serviceCollection.AddTransient<SamsungCamera>();
            serviceCollection.AddTransient<LgCamera>();

            serviceCollection.AddLogging(configure => configure.AddConsole());
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void AddCommands()
        {
            Commands.Add(typeof(TakePhotoAsyncCommand));
            Commands.Add(typeof(ZoomInAsyncCommand));
            Commands.Add(typeof(ZoomOutAsyncCommand));
            Commands.Add(typeof(SwitchOnAsyncCommand));
            Commands.Add(typeof(SwitchOffAsyncCommand));
        }

        private static void AddCameras()
        {
            Cameras.Add(_serviceProvider.GetService<SamsungCamera>());
            Cameras.Add(_serviceProvider.GetService<LgCamera>());
            Cameras.Add(_serviceProvider.GetService<SamsungCamera>());
        }

        private static ICamera SelectCameraDialog()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Available cameras:");
            for (var i = 0; i < Cameras.Count; i++)
            {
                System.Console.WriteLine($"{i}: {Cameras[i]}");
            }

            // I dont handle exceptions here, because it out of scope, but of course I remember about it =)))

            System.Console.WriteLine("Select a camera index you want to use:");
            var cameraIndex = Convert.ToInt32(System.Console.ReadLine());
            return Cameras[cameraIndex];
        }

        private static IAsyncCommand SelectCommandDialog(ICamera camera)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Available commands:");
            for (var i = 0; i < Commands.Count; i++)
            {
                System.Console.WriteLine($"{i}: {Commands[i].Name}");
            }

            // I dont handle exceptions here, because it out of scope, but of course I remember about it =)))

            System.Console.WriteLine($"Which command send to camera {camera}?");
            var commandIndex = Convert.ToInt32(System.Console.ReadLine());
            var commandType = Commands[commandIndex];
            return (IAsyncCommand)Activator.CreateInstance(commandType, camera);
        }

        #endregion
    }
}
