using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace Parking
{
    public class User : InterfaceChangeUser
    {
        // зміні
        string login;
        string password;
        List<Car> cars;
        List<MotorCycle> moto;
        int money;
        string name;
        // конструктор
        public User(string log)
        {
            string temp = FindUser(log);
            if (temp != null)
            {
                string[] parts = temp.Split(',');
                login = parts[0].Split(':')[1];
                password = parts[1].Split(':')[1];
                cars = new List<Car>();
                string temp_dop1 = parts[2].Split(':')[1];
                if (temp_dop1 == "")
                {

                }
                else
                {
                    string[] temp_dop2 = temp_dop1.Split('|');
                    if (temp_dop2.Length == 1)
                    {
                        cars.Add(new Car(temp_dop2[0]));
                    }
                    else
                    {
                        for (int i = 0; i < temp_dop2.Length; i++)
                        {

                            cars.Add(new Car(temp_dop2[i]));
                        }
                    }
                }
                temp_dop1 = parts[3].Split(':')[1];
                moto = new List<MotorCycle>();
                if (temp_dop1 == "")
                {

                }
                else
                {
                    string[] temp_dop2 = temp_dop1.Split('|');
                    if (temp_dop2.Length == 1)
                    {
                        moto.Add(new MotorCycle(temp_dop2[0]));
                    }
                    else
                    {
                        for (int i = 0; i < temp_dop2.Length; i++)
                        {
                            moto.Add(new MotorCycle(temp_dop2[i]));
                        }
                    }
                }
                if (!(parts[4].Split(':')[1] == ""))
                {
                    money = int.Parse(parts[4].Split(':')[1]);
                }
                else
                {
                    money = 0;
                }
                name = parts[5].Split(':')[1];
            }
        }
        // шлях до файлу
        private static string usersFilePath = "users.txt";

        public static bool RegisterUser(string login, string password, string confirmPassword, string name) // Функція для регістрації
        {
            if (login == "")
            {
                return false;
            }
            if (UserExists(login))
            {
                return false;
            }
            if (password == "")
            {
                return false;
            }
            if (password != confirmPassword)
            {
                return false;
            }
            if (name == "")
            {
                return false;
            }
            // Запис нового користувача до файлу
            using (StreamWriter writer = File.AppendText(usersFilePath))
            {
                writer.WriteLine($"Login:{login},Password:{password},Cars:,Moto:,Money:,Name:{name}");
            }
            return true;
        }
        public static bool LoginUser(string login, string password) // Функція  логіну
        {
            if (!UserExists(login))
            {
                return false;
            }
            // Перевірка пароля згідно до того, що зберігається у файлі
            string storedPassword = GetStoredPassword(login);
            if (password == storedPassword)
            {
                return true;
            }
            return false;
        }

        public static bool UserExists(string login) // функція для перевірки користувача у файлі
        {
            if (File.Exists(usersFilePath))
            {
                string[] lines = File.ReadAllLines(usersFilePath);
                foreach (string line in lines)
                {
                    string pattern = @"Login:(.*?),";
                    Match match = Regex.Match(line, pattern);
                    string log = "qweqweqweqwe";
                    if (match.Success)
                    {
                        log = match.Groups[1].Value;
                    }
                    if (log == login)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string GetStoredPassword(string login) // Функція для перевірки паролю
        {
            if (File.Exists(usersFilePath))
            {
                string[] lines = File.ReadAllLines(usersFilePath);
                foreach (string line in lines)
                {
                    string pattern = @"Login:(.*?),Password:(.*?),";
                    Match match = Regex.Match(line, pattern);
                    string log = "qweqweqweqwe";
                    string pas = "qweqweqweqew";
                    if (match.Success)
                    {
                        log = match.Groups[1].Value;
                        pas = match.Groups[2].Value;
                    }
                    if (log == login)
                    {
                        return pas;
                    }
                }
            }
            return null;
        }
        // функція для пошуку користувача за логіном
        public static string FindUser(string log) 
        {
            if (File.Exists(usersFilePath))
            {
                string[] lines = File.ReadAllLines(usersFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (log == parts[0].Split(':')[1])
                    {
                        return line;
                    }
                }
            }
            return null;
        }
        
        // гетери і сетери
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public List<Car> Cars
        {
            get { return cars; }
            set { cars = value; }
        }

        public List<MotorCycle> Moto
        {
            get { return moto; }
            set { moto = value; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // функція для виводу на екран інформації про користувача
        public string InformationWrite()
        {
            String s = "Login: " + login + "\r\nPassword: " + password + "\r\nCars: ";
            if (!(cars == null))
            {
                foreach (Car item in cars)
                {
                    s += item.Name + "  ";
                }
            }
            s += "\r\nMoto: ";
            if (!(moto == null))
            {
                foreach (MotorCycle item in moto)
                {
                    s += item.Name + "  ";
                }
            }
            s += "\r\nMoney: " + money + "\r\nName: " + name;
            return s;
        }
        // функція для зміни логіна та запису у файл нових даних
        public bool ChangeLogin(string s)
        {
            string old_login = "Login:" + login;
            string new_login = "Login:" + s;
            string[] lines = File.ReadAllLines(usersFilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains(new_login + ","))
                {
                    
                    return false;
                }
            }
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains(login))
                {
                    lines[i] = line.Replace(old_login, new_login);
                    login = s;
                    break;
                }
            }
            File.WriteAllLines(usersFilePath, lines);
            return true;
        }

        // функція для зміни пароля та запису у файл нових даних
        public bool ChangePassword(string s3, string s4)
        {
            string old_password = "Password:" + password;
            string new_password = "Password:" + s4;
            string[] lines = File.ReadAllLines(usersFilePath);
            if (s3 != password)
            {   
                return false;
            }
            if (s4 == "")
            {
                return false;
            }
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Login:" + login))
                {
                    lines[i] = line.Replace(old_password, new_password);
                    password = s4;
                    break;
                }
            }
            File.WriteAllLines(usersFilePath, lines);
            return true;
        }
        // Функція для додавання нової машини користувачу та запис цієї інформації у файл
        public void AddCar(string s)
        {
            string[] lines = File.ReadAllLines(usersFilePath);
            string new_car = "Cars:";
            string old_car = "Cars:";
            foreach (Car item in cars)
            {
                old_car += item.Name + "|";
                new_car += item.Name + "|";
            }
            if (cars.Count == 0)
            {
                old_car = old_car.Substring(0, old_car.Length);
            }
            else
            {
                old_car = old_car.Substring(0, old_car.Length - 1);
            }
            new_car += s;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Login:" + login))
                {
                    lines[i] = line.Replace(old_car, new_car);
                    cars.Add(new Car(s));
                    break;
                }
            }
            File.WriteAllLines(usersFilePath, lines);
        }
        // Функція для видалення машини у користувача та запис цієї інформації до файлу
        public void DeleteCar(string s)
        {
            string[] lines = File.ReadAllLines(usersFilePath);
            string new_car = "Cars:";
            string old_car = "Cars:";
            string first_car = "Cars:" + cars[0].Name;
            foreach (Car item in cars)
            {
                old_car += item.Name + "|";
                new_car += item.Name + "|";
            }
            old_car = old_car.Substring(0, old_car.Length - 1);
            new_car = new_car.Substring(0, new_car.Length - 1);
            if (first_car.Contains("Cars:" + s))
            {
                int index = new_car.IndexOf((s));
                if (cars.Count == 1)
                {
                    if (index != -1)
                    {
                        new_car = new_car.Remove(index, s.Length);
                    }
                }
                else
                {
                    if (index != -1)
                    {
                        new_car = new_car.Remove(index, s.Length + 1);
                    }
                }
            }
            else
            {
                int index = new_car.IndexOf("|" + s);
                if (index != -1)
                {
                    new_car = new_car.Remove(index, s.Length + 1);
                }
            }
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Login:" + login))
                {
                    lines[i] = line.Replace(old_car, new_car);
                    int index = cars.FindIndex(car_ => car_.Name == s);
                    if (index != -1)
                    {
                        cars.RemoveAt(index);
                    }
                    break;
                }
            }
            File.WriteAllLines(usersFilePath, lines);
        }
        // Функція для додавання нового мотоцикла користувачу та запис цієї інформації у файл
        public void AddMoto(string s)
        {
            string[] lines = File.ReadAllLines(usersFilePath);
            string new_moto = "Moto:";
            string old_moto = "Moto:";
            foreach (MotorCycle item in moto)
            {
                old_moto += item.Name + "|";
                new_moto += item.Name + "|";
            }
            if (moto.Count == 0)
            {
                old_moto = old_moto.Substring(0, old_moto.Length);
            }
            else
            {
                old_moto = old_moto.Substring(0, old_moto.Length - 1);
            }
            new_moto += s;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Login:" + login))
                {
                    lines[i] = line.Replace(old_moto, new_moto);
                    moto.Add(new MotorCycle(s));
                    break;
                }
            }
            File.WriteAllLines(usersFilePath, lines);
        }
        // Функція для видалення мотоцикла у користувача та запис цієї інформації до файлу
        public void DeleteMoto(string s)
        {
            string[] lines = File.ReadAllLines(usersFilePath);
            string new_moto = "Moto:";
            string old_moto = "Moto:";
            string first_moto = "Moto:" + moto[0].Name;
            foreach (MotorCycle item in moto)
            {
                old_moto += item.Name + "|";
                new_moto += item.Name + "|";
            }
            old_moto = old_moto.Substring(0, old_moto.Length - 1);
            new_moto = new_moto.Substring(0, new_moto.Length - 1);
            if (first_moto.Contains("Moto:" + s))
            {
                int index = new_moto.IndexOf(s);
                if (moto.Count == 1)
                {
                    if (index != -1)
                    {
                        new_moto = new_moto.Remove(index, s.Length);
                    }
                }
                else
                {
                    if (index != -1)
                    {
                        new_moto = new_moto.Remove(index, s.Length + 1);
                    }
                }
            }
            else
            {
                int index = new_moto.IndexOf("|" + s);
                if (index != -1)
                {
                    new_moto = new_moto.Remove(index, s.Length + 1);
                }
            }
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Login:" + login))
                {
                    lines[i] = line.Replace(old_moto, new_moto);
                    int index = moto.FindIndex(moto_ => moto_.Name == s);
                    if (index != -1)
                    {
                        moto.RemoveAt(index);
                    }
                    break;
                }
            }
            File.WriteAllLines(usersFilePath, lines);
        }
        // Функція для зміни кількості грошей користувача та запис цієї інформації у файл
        public bool ChangeMoney(string s)
        {
            string old_money = "Money:" + money;
            string new_money = "Money:" + s;
            string[] lines = File.ReadAllLines(usersFilePath);
            int amount;
            if (int.TryParse(s, out amount))
            {
                if (amount >= 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];
                        if (line.Contains("Login:" + login))
                        {
                            lines[i] = line.Replace(old_money, new_money);
                            money = int.Parse(s);
                            break;
                        }
                    }
                    File.WriteAllLines(usersFilePath, lines);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        // Функція для зміни ім'я користувача та запис цієї інформації у файл
        public bool ChangeName(string s)
        {
            string old_name = "Name:" + Name;
            string new_name = "Name:" + s;
            string[] lines = File.ReadAllLines(usersFilePath);
            if (s != "")
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("Login:" + login))
                    {
                        lines[i] = line.Replace(old_name, new_name);
                        name = s;
                        break;
                    }
                }
                File.WriteAllLines(usersFilePath, lines);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
