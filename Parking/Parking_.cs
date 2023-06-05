using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parking
{
    public class Parking_ : InterfaceChangeParking
    {
        Random random = new Random();
        int id;
        int numberOfColumns;
        int numberOfRows;
        int totalNumberOfSpots;
        int freeParkingSpaces;
        Spot[,] spots;
        static string parkingFilePath = "parking.txt";
        // конструктор
        public Parking_(int id, int rows, int columns)
        {
            this.id = id;
            numberOfRows = rows;
            numberOfColumns = columns;
            totalNumberOfSpots = columns * rows;
            spots = new Spot[rows, columns];
            freeParkingSpaces = 0;
            if (File.Exists(parkingFilePath))
            {
                string[] lines = File.ReadAllLines(parkingFilePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("Parking:" + id))
                    {
                        i += 1;
                        int c = -1;
                        int r = 0;
                        for(int k = i; k < i + numberOfRows * numberOfColumns; k++)
                        {
                            c += 1;
                            if (c == numberOfColumns)
                            {
                                c = 0;
                                r += 1;
                            }
                            string[] parts1 = lines[k].Split(',');
                            string[] parts2 = parts1[0].Split(':');
                            int new_id = int.Parse(parts2[1]);
                            parts2 = parts1[1].Split(':');
                            int new_cost = int.Parse(parts2[1]);
                            parts2 = parts1[2].Split(':');
                            string new_state = parts2[1];
                            if (new_state != "false")
                            {
                                freeParkingSpaces++;
                            }
                            parts2 = parts1[3].Split(':');
                            int new_time = int.Parse(parts2[1]);
                            parts2 = parts1[6].Split(':');
                            bool new_isCar;
                            if (parts2[1] == "true")
                            {
                                new_isCar = true;
                            }
                            else
                            {
                                new_isCar = false;
                            }
                            spots[r, c] = new Spot(new_id, new_cost, new_state, new_time, new_isCar);
                            parts2 = parts1[4].Split(':');
                            if (parts2[1] != "null")
                            {
                                Vehicle new_carInSpot = new Car(parts2[1]);
                                spots[r, c].VehicleInSpot = new_carInSpot;
                            }
                            parts2 = parts1[5].Split(':');
                            if (parts2[1] != "null")
                            {
                                spots[r, c].UserInSpot = parts2[1];
                            }
                        }
                        break;
                    }
                }
            }
        }
        // гетери та сетери
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int NumberOfColumns
        {
            get { return numberOfColumns; }
            set { numberOfColumns = value; TotalNumberOfSootsRecount(); }
        }

        public int NumberOfRows
        {
            get { return numberOfRows; }
            set { numberOfRows = value; TotalNumberOfSootsRecount(); }
        }

        public int TotalNumberOfSpots
        {
            get { return totalNumberOfSpots; }
        }
        public int FreeParkingSpaces
        {
            get { return freeParkingSpaces; }
        }
        public Spot[,] Spots
        {
            get { return spots; }
        }
        // Функція для підрахування нового розміру паркування
        public int TotalNumberOfSootsRecount ()
        {
            totalNumberOfSpots = numberOfColumns * numberOfRows;
            return totalNumberOfSpots;
        }
        // Функція для виведення назв паркувань 
        public string ParkingNameWrite(string name)
        {
            string result_name;
            result_name = name + "\r\n" + numberOfRows + " x " + numberOfColumns + "\r\nFree: " + freeParkingSpaces;
            return result_name;
        }
        // Функція для зміну стану паркувального місця
        public void ChangeState(string new_state, int row, int column)
        {
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[2].Split(':');
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + new_state);
                    spots[row, column].State = new_state;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
        }
        // Функція для зміни часу паркувального місця
        public void ChangeTime(int new_time, int row, int column)
        {
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[3].Split(':');
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + new_time.ToString());
                    spots[row, column].Time = new_time;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
        }
        // Функція для зміну типу паркувального місця
        public void ChangeIsCar(bool isCar, int row, int column)
        {
            string isCar_text = isCar.ToString();
            isCar_text = isCar_text.ToLower();
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[6].Split(':');
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + isCar_text);
                    spots[row, column].IsCar = isCar;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
        }
        // Функція для додавання авто до паркувального місця
        public void AddCarInSpot(string new_car_name, int row, int column)
        {
            Car new_car = new Car(new_car_name);
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[4].Split(':');
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + new_car.Name);
                    spots[row, column].VehicleInSpot = new_car;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
        }
        // Функція для видалення авто з паркувального місця
        public string DeleteCarInSpot(int row, int column)
        {
            string car_name = "";
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[4].Split(':');
                    car_name = parts2[1];
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + "null");
                    spots[row, column].VehicleInSpot = null;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
            return car_name;
        }
        // Функція для додавання мото до паркувального місця
        public void AddMotoInSpot(string new_moto_name, int row, int column)
        {
            MotorCycle new_moto = new MotorCycle(new_moto_name);
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[4].Split(':');
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + new_moto.Name);
                    spots[row, column].VehicleInSpot = new_moto;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
        }
        // Функція для видалення мото з паркувального місця
        public string DeleteMotoInSpot(int row, int column)
        {
            string moto_name = "";
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[4].Split(':');
                    moto_name = parts2[1];
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + "null");
                    spots[row, column].VehicleInSpot = null;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
            return moto_name;
        }
        // Функція для додавання інформації про користувача до паркувального місця
        public void AddUserInSpot(string new_user_login, int row, int column)
        {
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[5].Split(':');
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + new_user_login);
                    spots[row, column].UserInSpot = new_user_login;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
        }
        // Функція для видалення інформації про користувача з паркувального місця
        public string DeleteUserInSpot(int row, int column)
        {
            string user_login = "";
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    i += 1;
                    i += row * numberOfColumns + column;
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[5].Split(':');
                    user_login = parts2[1];
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts2[1], parts2[0] + ":" + "null");
                    spots[row, column].UserInSpot = null;
                    File.WriteAllLines(parkingFilePath, lines);
                    break;
                }
            }
            return user_login;
        }
        // Додаткова функція для коректної зміни state з wait на true
        public bool ParkingViborSpotCorrect()
        {
            if (File.Exists(parkingFilePath))
            {
                string[] lines = File.ReadAllLines(parkingFilePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("Parking:" + id))
                    {
                        i += 1;
                        int c = -1;
                        int r = 0;
                        for (int k = i; k < i + numberOfRows * numberOfColumns; k++)
                        {
                            c += 1;
                            if (c == numberOfColumns)
                            {
                                c = 0;
                                r += 1;
                            }
                            if (lines[k].Contains("wait"))
                            {
                                Console.WriteLine(lines[k]);
                                lines[k] = lines[k].Replace("wait", "true");
                                Console.WriteLine(lines[k]);
                                spots[r, c].State = "true";
                                File.WriteAllLines(parkingFilePath, lines);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        // Функція для виконання всіх потрібних дій для додавання авто до паркувального місця
        public void CarInParking(string nameAvto,int priceInParking, int hoursInParking, int row, int column, User user, bool isCar)
        {
            user.ChangeMoney((user.Money - priceInParking).ToString());
            ChangeTime(hoursInParking, row, column);
            AddUserInSpot(user.Login, row, column);
            freeParkingSpaces--;
            if (isCar)
            {
                AddCarInSpot(nameAvto, row, column);
                ChangeIsCar(true,row,column);
                user.DeleteCar(nameAvto);
            }
            else
            {
                AddMotoInSpot(nameAvto, row, column);
                ChangeIsCar(false, row, column);
                user.DeleteMoto(nameAvto);
            }
            ChangeState("false", row, column);
        }
        // Функція для виконання всіх потрібних дій для видалення авто з паркувального місця
        public void ReturnCarInParking(int row, int column)
        {
            ChangeTime(0, row, column);
            ChangeState("true", row, column);
            string avto_name = "";
            string user_login = DeleteUserInSpot(row, column);
            freeParkingSpaces++;
            User user_temp = new User(user_login);
            if (spots[row,column].IsCar)
            {
                avto_name = DeleteCarInSpot(row, column);
                user_temp.AddCar(avto_name);
            }
            else
            {
                avto_name = DeleteMotoInSpot(row, column);
                user_temp.AddMoto(avto_name);
            }
            user_temp = null;
        }
        // Додаткова функція для таймеру для того, щоб в реальному часі слідкувати за залишеним часом паркування авто
        public void TimeMinusAllParking()
        {
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (spots[i,j].Time != 0)
                    {
                        spots[i, j].Time--;
                        if (spots[i,j].Time == 0)
                        {
                            ReturnCarInParking(i, j);
                        }
                        else
                        {
                            ChangeTime(spots[i, j].Time, i, j);
                        }
                    }
                }
            }
        }
        // Функція для зміни розміру Parking
        public void ParkingSizeChange(int new_row_count, int new_column_count)
        {
            string[] lines = File.ReadAllLines(parkingFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Parking:" + id))
                {
                    string[] parts1 = lines[i].Split(',');
                    string[] parts2 = parts1[1].Split(':');
                    string[] parts3 = parts2[1].Split('|');
                    lines[i] = lines[i].Replace(parts2[0] + ":" + parts3[0] + "|" + parts3[1], parts2[0] + ":" + new_row_count + "|" + new_column_count);
                    int start_index = i + 1;
                    int count_delete_lines = totalNumberOfSpots;
                    if (start_index >= 0 && start_index + count_delete_lines <= lines.Length)
                    {
                        lines = lines.Where((line, index) => index < start_index || index >= start_index + count_delete_lines).ToArray();
                    }
                    NumberOfRows = new_row_count;
                    NumberOfColumns = new_column_count;
                    totalNumberOfSpots = new_row_count * new_column_count;
                    freeParkingSpaces = totalNumberOfSpots;
                    spots = new Spot[new_row_count, new_column_count];
                    string[] newLines = new string[new_row_count * new_column_count];
                    int j = 0;
                    for (int m = 0; m < new_row_count; m++)
                    {
                        for (int n = 0; n < new_column_count; n++)
                        {
                            spots[m, n] = new Spot((m * new_column_count + n), random.Next(100, 201), "true", 0, true);
                            newLines[j] = "ID:" + spots[m, n].Id + ",Cost:" + spots[m, n].Cost + ",State:true,Time:0,CarInSpot:null,UserInSpot:null,isCar:true";
                            Console.WriteLine(newLines[j]);
                            j++;
                        }
                    }
                    var result = lines.Take(start_index).Concat(newLines).Concat(lines.Skip(start_index));
                    File.WriteAllLines(parkingFilePath, result);
                    break;
                }
            }
        }
    }
}
