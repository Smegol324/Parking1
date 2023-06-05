using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Parking
{
    internal static class Program
    {
        private static string parkingFilePath = "parking.txt";
        static System.Timers.Timer timer;
        static City city;
        [STAThread]
        static void Main()
        {
            
            if (File.Exists(parkingFilePath))
            {
                int[,] array = new int[3, 3];
                int z = -1;
                string[] lines = File.ReadAllLines(parkingFilePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("Parking"))
                    {
                        z += 1;
                        string[] parts1 = line.Split(',');
                        string[] parts2 = parts1[0].Split(':');
                        array[z, 0] = int.Parse(parts2[1]);
                        parts2 = parts1[1].Split(':');
                        string[] parts3 = parts2[1].Split('|');
                        array[z, 1] = int.Parse(parts3[0]);
                        array[z, 2] = int.Parse(parts3[1]);
                    }
                }
                city = new City(array[0, 0], array[0, 1], array[0, 2], array[1, 0], array[1, 1], array[1, 2], array[2, 0], array[2, 1], array[2, 2]);
                timer = new System.Timers.Timer(1000);
                timer.Elapsed += TimerElapsed;
                timer.Start();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Login_Form(city));
                timer.Stop();
                timer.Dispose();
            }
        }
        static void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            city.getParking1.TimeMinusAllParking();
            city.getParking2.TimeMinusAllParking();
            city.getParking3.TimeMinusAllParking();
        }
    }
}
