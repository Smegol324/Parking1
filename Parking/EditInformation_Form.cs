using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace Parking
{
    public partial class EditInformation_Form : Form
    {
        User user;
        City city;
        System.Windows.Forms.Timer timer;
        int AllParkingFreeSpotPrevious;
        private static string usersFilePath = "users.txt";
        public EditInformation_Form(User user, City city)
        {
            InitializeComponent();
            this.user = user;
            this.city = city;
            AllParkingFreeSpotPrevious = city.getParking1.FreeParkingSpaces + city.getParking2.FreeParkingSpaces + city.getParking3.FreeParkingSpaces;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Кнопка для повернення до форми перегляду інформації
        private void button1_Click(object sender, EventArgs e)
        {
            UserInformation_Form info = new UserInformation_Form(user, city);
            info.Show();
            this.Hide();
        }
        // Функція яка виконується при запуску форми. Вона виставляє правильні динамічні позиції об'єктів та виводить інформацію на екран про користувача
        private void EditInformation_Form_Load(object sender, EventArgs e)
        {
            CorrectStartPosition();
            textBox1.Text = user.InformationWrite();
        }
        // Функція для правильного динамічного розміщення об'єктів на формі
        public void CorrectStartPosition()
        {
            int formWidth = this.Width;
            int formHeight = this.Height;
            button1.Width = formWidth / 10;
            button1.Height = formHeight / 20;
            button1.Left = formWidth - formWidth / 70 - button1.Width;
            button1.Top = formHeight / 40;
            textBox1.Left = formWidth / 2 + (formWidth - textBox1.Width) / 4;
            textBox1.Top = (formHeight - textBox1.Height) / 2 - formHeight / 10;
            label2.Left = textBox1.Left + textBox1.Width / 2 - label2.Width / 2;
            label2.Top = textBox1.Top - label2.Height - 20;
            button2.Width = formWidth / 10;
            button2.Height = formHeight / 20;
            button2.Top = formHeight / 2 + formHeight / 3;
            button2.Left = (formWidth - button2.Width) / 2;
            label1.Left = (formWidth - label1.Width) / 7;
            label1.Top = (formHeight - textBox1.Height) / 3;
            comboBox1.Left = label1.Left + label1.Width + 20;
            comboBox1.Top = label1.Top - 3;
            label3.Left = label1.Left;
            label3.Top = label1.Top + label1.Height + 20;
            label4.Left = label3.Left;
            label4.Top = label3.Top + label3.Height + 20;
            label5.Left = label4.Left;
            label5.Top = label4.Top + label4.Height + 20;
            textBox2.Left = label5.Left + label5.Width + 5;
            textBox2.Top = label5.Top - 3;
            label6.Left = label4.Left;
            label6.Top = label4.Top;
            label7.Left = label5.Left;
            label7.Top = label5.Top;
            textBox3.Left = label6.Left + label6.Width + 5;
            textBox3.Top = label6.Top - 3;
            textBox4.Left = textBox3.Left;
            textBox4.Top = label7.Top - 3;
            checkBox1.Left = label6.Left;
            checkBox1.Top = label6.Top;
            label8.Left = checkBox1.Left;
            label8.Top = checkBox1.Top + checkBox1.Height + 20;
            comboBox2.Left = label8.Left + label8.Width + 5;
            comboBox2.Top = label8.Top - 3;
            checkBox2.Left = checkBox1.Left;
            checkBox2.Top = checkBox1.Top;
            label9.Left = label8.Left;
            label9.Top = label8.Top;
            comboBox3.Left = label9.Left + label9.Width + 5;
            comboBox3.Top = label9.Top - 3;
            label11.Left = label4.Left;
            label11.Top = label4.Top;
            label12.Left = label5.Left;
            label12.Top = label5.Top;
            textBox5.Left = label12.Left + label12.Width + 5;
            textBox5.Top = label12.Top - 3;
            label13.Left = label4.Left;
            label13.Top = label4.Top;
            label14.Left = label5.Left;
            label14.Top = label5.Top;
            textBox6.Left = label14.Left + label14.Width + 5;
            textBox6.Top = label14.Top - 3;
        }
        // Функція, яка спрацьовує при зміні головного комбобокса. При виборі параметра виводить відповідну інформацію на екран
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            switch (selectedValue)
            {
                case "Login":
                    AllVisibleFalse();
                    label3.Visible = true;
                    label3.Text = "Login:";
                    label4.Visible = true;
                    label5.Visible = true;
                    textBox2.Visible = true;
                    label4.Text = "Старий логін: " + user.Login;
                    break;
                case "Password":
                    AllVisibleFalse();
                    label3.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    label3.Text = "Password:";
                    break;
                case "Cars":          
                    AllVisibleFalse();
                    checkBox1.Visible = true;
                    label8.Visible = true;
                    comboBox2.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Cars:";
                    checkBox1.Checked = true;
                    comboBox2.SelectedIndex = 0;
                    break;
                case "Moto":  
                    AllVisibleFalse();
                    checkBox2.Visible = true;
                    label9.Visible = true;
                    comboBox3.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Moto:";
                    checkBox2.Checked = true;
                    comboBox3.SelectedIndex = 0;
                    break;
                case "Money":
                    AllVisibleFalse();
                    label11.Visible = true;
                    label12.Visible = true;
                    textBox5.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Money:";
                    label11.Text = "Попередня кількість грошей: " + user.Money;
                    break;
                case "Name":
                    AllVisibleFalse();
                    label13.Visible = true;
                    label14.Visible = true;
                    textBox6.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Name:";
                    label13.Text = "Попереднє ім'я: " + user.Name;
                    break;
                default:
                    MessageBox.Show("Выбран неизвестный параметр");
                    break;
            }
        }
        // Функція для застосування нових змін при натисненні на відповідну кнопку
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                label3.Visible = true;
                switch (selectedValue)
                {
                    case "Login":
                        if (textBox2.Text != "")
                        {
                            if (!user.ChangeLogin(textBox2.Text))
                            {
                                MessageBox.Show("Заданий логін вже існує");
                            }
                            else
                            {
                                textBox1.Text = user.InformationWrite();
                                label4.Text = "Старий логін: " + user.Login;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ви не ввели новий логін");
                        }
                        break;
                    case "Password":
                        if (user.ChangePassword(textBox3.Text, textBox4.Text))
                        {
                            textBox1.Text = user.InformationWrite();
                        }
                        else
                        {
                            MessageBox.Show("Невірно введений старий пароль або не введено новий");
                        }
                        break;
                    case "Cars":
                        if (checkBox1.Checked)
                        {
                            if (comboBox2.SelectedItem != null)
                            {
                                user.AddCar(comboBox2.SelectedItem.ToString());
                                MessageBox.Show("Успішне додавання авто до облікового запису користувача");
                            }
                            else
                            {
                                MessageBox.Show("Ви не вибрали авто для додавання");
                            }
                        }
                        else
                        {
                            if (comboBox2.SelectedItem != null)
                            {
                                user.DeleteCar(comboBox2.SelectedItem.ToString());
                                MessageBox.Show("Успішне видалення авто з облікового запису користувача");
                            }
                            else
                            {
                                MessageBox.Show("Ви не вибрали авто для видалення");
                            }
                        }
                        textBox1.Text = user.InformationWrite();
                        ComboBox2Reload();
                        break;
                    case "Moto":
                        if (checkBox2.Checked)
                        {
                            if (comboBox3.SelectedItem != null)
                            {
                                user.AddMoto(comboBox3.SelectedItem.ToString());
                                MessageBox.Show("Успішне додавання мотоцикла до облікового запису користувача");
                            }
                            else
                            {
                                MessageBox.Show("Ви не вибрали мотоцикл для додавання");
                            }
                            
                        }
                        else
                        {
                            if (comboBox2.SelectedItem != null)
                            {
                                user.DeleteMoto(comboBox3.SelectedItem.ToString());
                                MessageBox.Show("Успішне видалення мотоцикла з облікового запису користувача");
                            }
                            else
                            {
                                MessageBox.Show("Ви не вибрали мотоцикл для видалення");
                            }
                        }
                        textBox1.Text = user.InformationWrite();
                        ComboBox3Reload();
                        break;
                    case "Money":
                        if (user.ChangeMoney(textBox5.Text))
                        {
                            textBox1.Text = user.InformationWrite();
                            label11.Text = "Попередня кількість грошей: " + user.Money;
                            textBox5.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Невірно введений формат");
                        }
                        break;
                    case "Name":
                        if (user.ChangeName(textBox6.Text))
                        {
                            textBox1.Text = user.InformationWrite();
                            label13.Text = "Попереднє ім'я: " + user.Name;
                            textBox6.Text = "";
                            MessageBox.Show("Успішна зміна ім'я користувача");
                        }
                        else
                        {
                            MessageBox.Show("Ви не ввели нове ім'я"); 
                        }
                        break;
                    default:
                        MessageBox.Show("Ви вибрали некоректний параметр");
                        break;
                }
            }
        }
        // допоміжна функція для зміни елементів комбобокса при зміні параметра Checked checkBox1
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ComboBox2Reload();
            comboBox2.SelectedIndex = 0;
        }
        // функція для зміни елементів comboBox2
        public void ComboBox2Reload()
        {
            comboBox2.Text = "";
            System.Windows.Forms.ComboBox.ObjectCollection items = comboBox2.Items;
            items.Clear();
            if (checkBox1.Checked == true)
            {
                items.Add("BMW 3 Series");
                items.Add("Mersedes - Benz C - Class");
                items.Add("Toyota Camry");
                items.Add("Ford Mustang");
                items.Add("Chevrolet Silverado");
                items.Add("Honda Civic");
                items.Add("Volkswagen Golf");
                items.Add("Audi A4");
                items.Add("Nissan Altima");
                items.Add("Tesla Model S");
                items.Add("Subaru Forester");
                items.Add("Hyundai Sonata");
                items.Add("Kia Optima");
                items.Add("Volvo XC60");
                items.Add("Porsche 911");
                items.Add("Jaguar F-Pace");
                items.Add("Land Rover Discovery");
                items.Add("Fiat 500");
                items.Add("Peugeot 208");
                items.Add("Mazda CX-5");
            }
            else
            {
                if (!(user.Cars == null))
                {
                    foreach (Car item in user.Cars)
                    {
                        items.Add(item.Name);
                    }
                }
            }
        }
        // допоміжна функція для зміни елементів комбобокса при зміні параметра Checked checkBox2
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ComboBox3Reload();
            comboBox3.SelectedIndex = 0;
        }
        // функція для зміни елементів comboBox3
        public void ComboBox3Reload()
        {
            comboBox3.Text = "";
            System.Windows.Forms.ComboBox.ObjectCollection items = comboBox3.Items;
            items.Clear();
            if (checkBox2.Checked == true)
            {
                items.Add("Honda CBR1000RR");
                items.Add("Yamaha YZF-R6");
                items.Add("Suzuki GSX-R1000");
                items.Add("Kawasaki Ninja ZX-10R");
                items.Add("BMW S1000RR");
                items.Add("Ducati Panigale V4");
                items.Add("Harley-Davidson Street Glide");
                items.Add("Triumph Street Triple");
                items.Add("KTM 1290 Super Duke R");
                items.Add("Aprilia RSV4");
            }
            else
            {
                if (!(user.Moto == null))
                {
                    foreach (MotorCycle item in user.Moto)
                    {
                        items.Add(item.Name);
                    }
                }
            }
        }
        // Функція для прибирання видимості всіх непотрібних об'єктів
        public void AllVisibleFalse()
        {
            label4.Visible = false;
            label5.Visible = false;
            textBox2.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            checkBox1.Visible = false;
            label8.Visible = false;
            comboBox2.Visible = false;
            checkBox1.Checked = false;
            checkBox2.Visible = false;
            label9.Visible = false;
            comboBox3.Visible = false;
            checkBox2.Checked = false;
            label11.Visible = false;
            label12.Visible = false;
            textBox5.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            textBox6.Visible = false;
        }
        // Функція для таймеру
        private void Timer_Tick(object sender, EventArgs e)
        {
            int AllParkingFreeSpot = city.getParking1.FreeParkingSpaces + city.getParking2.FreeParkingSpaces + city.getParking3.FreeParkingSpaces;
            if (AllParkingFreeSpot != AllParkingFreeSpotPrevious)
            {
                user = new User(user.Login);
                AllParkingFreeSpotPrevious = AllParkingFreeSpot;
                textBox1.Text = user.InformationWrite();
            }
        }
    }
}