using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parking
{
    public partial class Parkovka_Form : Form
    {
        static string parkingFilePath = "parking.txt";
        User user;
        City city;
        Spot[,] cells;
        Panel[,] cellPanels;
        System.Windows.Forms.Timer timer;
        int AllParkingFreeSpotPrevious;

        public Parkovka_Form(User user, City city)
        {
            InitializeComponent();
            this.user = user;
            this.city = city;
            AllParkingFreeSpotPrevious = city.getParking1.FreeParkingSpaces + city.getParking2.FreeParkingSpaces + city.getParking3.FreeParkingSpaces;
        }
        int cellPreviousRow = 0;
        int cellPreviousColumn = 0;
        // Вибір першого паркування
        private void button1_Click(object sender, EventArgs e)
        {
            ParkingVisibleCorrect1();
            HideCells();
            label2.Text = "Parking 1";
            ParkingWrite(city.getParking1);          
        }
        // Вибір другого паркування
        private void button2_Click(object sender, EventArgs e)
        {
            ParkingVisibleCorrect1();
            HideCells();
            label2.Text = "Parking 2";
            ParkingWrite(city.getParking2);  
        }
        // Вибір третього паркування
        private void button3_Click(object sender, EventArgs e)
        {
            ParkingVisibleCorrect1();
            HideCells();
            label2.Text = "Parking 3";
            ParkingWrite(city.getParking3);
        }
        // функція для виведення паркування на екран
        private void ParkingWrite(Parking_ parking)
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
            // Масив елементів
            cells = parking.Spots;
            cellPanels = new Panel[parking.NumberOfRows, parking.NumberOfColumns];
            // Розміри клітинки і відступу
            int cellSize = 120;
            int cellSpacing = 10;

            // Початкові координати для розміщення клітинок, мітки та фону
            int formWidth = this.Width;
            int formHeight = this.Height;
            
            int startX = (formWidth - (cellSize * parking.NumberOfColumns) - (cellSpacing * (parking.NumberOfColumns - 1))) / 2;
            int startY = (formHeight - (cellSize * parking.NumberOfRows) - (cellSpacing * (parking.NumberOfRows - 1))) / 2;

            label2.Left = (formWidth - label2.Width) / 2;
            label2.Top = startY - 50;

            textBox1.Width = (cellSize * parking.NumberOfColumns) + (cellSpacing * (parking.NumberOfColumns - 1)) + 40;
            textBox1.Height = (cellSize * parking.NumberOfRows) + (cellSpacing * (parking.NumberOfRows - 1)) + 90;
            textBox1.Top = startY - 70;
            textBox1.Left = startX - 20;
            // Створення графічних елементів на формі та обробники подій
            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int col = 0; col < cells.GetLength(1); col++)
                {
                    Panel cellPanel = new Panel();
                    cellPanels[row, col] = cellPanel;
                    cellPanel.Width = cellSize;
                    cellPanel.Height = cellSize;

                    // Розрахунок положення клітинки з урахуванням відступу та початкових координат
                    int left = startX + col * (cellSize + cellSpacing);
                    int top = startY + row * (cellSize + cellSpacing);
                    cellPanel.Left = left;
                    cellPanel.Top = top;

                    // Створення и налаштування label
                    Label cellLabel = new Label();
                    cellLabel.Text = "ID: " + cells[row, col].Id + "\r\nCost: " + cells[row, col].Cost + "\r\n";
                    cellLabel.Dock = DockStyle.Fill;
                    cellLabel.TextAlign = ContentAlignment.MiddleCenter;

                    // Додавання Label на форму
                    this.Controls.Add(cellLabel);

                    // Встановлення Parent для Label як cellPanel
                    cellLabel.Parent = cellPanel;

                    // Встановлення кольору клітинки в залежності від її стану
                    if (cells[row, col].State == "true")
                    {
                        cellLabel.Text = "ID: " + cells[row, col].Id + "\r\nCost: " + cells[row, col].Cost + "\r\nState: true";
                        cellPanel.BackColor = Color.Green;
                    }
                    else if (cells[row, col].State == "false")
                    {
                        cellLabel.Text = "ID: " + cells[row, col].Id + "\r\nCost: " + cells[row, col].Cost + "\r\nState: false\r\nTime: " + cells[row, col].Time + "\r\nCarInSpot: " + cells[row, col].VehicleInSpot.Name + "\r\nUserInSpot: " + cells[row,col].UserInSpot;
                        cellPanel.BackColor = Color.Red;
                    }
                    else
                    {
                        cellLabel.Text = "ID: " + cells[row, col].Id + "\r\nCost: " + cells[row, col].Cost + "\r\nState: wait";
                        cellPanel.BackColor = Color.Yellow;
                    }

                    // Додавання клітинки до форми
                    this.Controls.Add(cellPanel);

                    // Додавання обробника події Click для кожної клітинки
                    int currentRow = row;
                    int currentCol = col;
                    cellLabel.Click += (clickSender, clickEvent) =>
                    {
                        // Зміна кольору клітинки при натисканні
                        if (cellPanel.BackColor == Color.Red)
                        {
                            if (user.Login == "admin")
                            {
                                parking.ReturnCarInParking(currentRow, currentCol);
                            }
                        }
                        else if (cellPanel.BackColor == Color.Green)
                        {
                            if (user.Login != "guest" && user.Login != "admin")
                            {
                                checkBox1.Checked = false;
                                checkBox1.Checked = true;
                                cellLabel.Text = cellLabel.Text.Substring(0, cellLabel.Text.Length - 4) + "wait";
                                CorrectStartPositionVibor();
                                cellPanel.BackColor = Color.Yellow;
                                if (parking.ParkingViborSpotCorrect())
                                {
                                    cellPanels[cellPreviousRow, cellPreviousColumn].BackColor = Color.Green;
                                }
                                ParkingViborSpotVisibleTrue();
                                cells[currentRow, currentCol].State = "wait";
                                parking.ChangeState("wait", currentRow, currentCol);
                                cellPreviousRow = currentRow;
                                cellPreviousColumn = currentCol;
                            }
                        }
                        else if (cellPanel.BackColor == Color.Yellow)
                        {
                            cellLabel.Text = cellLabel.Text.Substring(0, cellLabel.Text.Length - 4) + "true";
                            cellPanel.BackColor = Color.Green;
                            ParkingViborSpotVisibleFalse();
                            cells[currentRow, currentCol].State = "true";
                            parking.ChangeState("true", currentRow, currentCol);
                        }
                    };
                }
            }
            textBox1.SendToBack();
            textBox1.Enabled = false;
        }
        // Функція для таймеру
        private void Timer_Tick(object sender, EventArgs e)
        {
            int AllParkingFreeSpot = city.getParking1.FreeParkingSpaces + city.getParking2.FreeParkingSpaces + city.getParking3.FreeParkingSpaces;
            if (AllParkingFreeSpot != AllParkingFreeSpotPrevious)
            {
                user = new User(user.Login);
                AllParkingFreeSpotPrevious = AllParkingFreeSpot;
                ComboBoxReload();
            }
            button1.Text = city.getParking1.ParkingNameWrite("Parking 1");
            button2.Text = city.getParking2.ParkingNameWrite("Parking 2");
            button3.Text = city.getParking3.ParkingNameWrite("Parking 3");
            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int col = 0; col < cells.GetLength(1); col++)
                {
                    Label cellLabel = cellPanels[row, col].Controls[0] as Label;

                    if (cells[row, col].State == "true")
                    {
                        cellLabel.Text = "ID: " + cells[row, col].Id + "\r\nCost: " + cells[row, col].Cost + "\r\nState: true";
                        cellPanels[row,col].BackColor = Color.Green;
                    }
                    else if (cells[row, col].State == "false")
                    {
                        cellLabel.Text = "ID: " + cells[row, col].Id + "\r\nCost: " + cells[row, col].Cost + "\r\nState: false\r\nTime: " + cells[row, col].Time + "\r\nCarInSpot: " + cells[row, col].VehicleInSpot.Name + "\r\nUserInSpot: " + cells[row, col].UserInSpot;
                        cellPanels[row, col].BackColor = Color.Red;
                    }
                    else
                    {
                        cellLabel.Text = "ID: " + cells[row, col].Id + "\r\nCost: " + cells[row, col].Cost + "\r\nState: wait";
                        cellPanels[row, col].BackColor = Color.Yellow;
                    }
                }
            }
        }
        // Функція для прибирання клітинок паркування
        void HideCells()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel cellPanel)
                {
                    cellPanel.Visible = false;
                }
            }
        }
        // Функція яка виконується при завантаженні форми
        private void Parkovka_Form_Load(object sender, EventArgs e)
        {
            CorrectStartPosition();
            city.getParking1.ParkingViborSpotCorrect();
            city.getParking2.ParkingViborSpotCorrect();
            city.getParking3.ParkingViborSpotCorrect();
            button1.Text = city.getParking1.ParkingNameWrite("Parking 1");
            button2.Text = city.getParking2.ParkingNameWrite("Parking 2");
            button3.Text = city.getParking3.ParkingNameWrite("Parking 3");
        }

        // Функція для повернення до головного меню
        private void button4_Click(object sender, EventArgs e)
        {
            Menu_Form menu = new Menu_Form(user, city);
            menu.Show();
            this.Hide();
        }
        // Функція для переходу до інтерфейсу вибора паркувального місця
        public void ParkingVisibleCorrect1()
        {
            label1.Visible = false;
            label2.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = true;
            textBox1.Visible = true;
        }
        // Функція для повернення з інтерфейсу вибора паркувальних місць до початкового інтерфейсу форми
        public void ParkingVisibleCorrect2()
        {
            label1.Visible = true;
            label2.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button5.Visible = false;
            textBox1.Visible = false;
        }
        // функція для виведення інтерфейсу оплати паркувального місця
        public void ParkingViborSpotVisibleTrue()
        {
            comboBox1.Visible = true;
            checkBox1.Visible = true;
            checkBox1.Checked = true;
            textBox2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            trackBar1.Visible = true;
            button6.Visible = true;
            textBox2.SendToBack();
            textBox2.Enabled = false;
        }
        // функція для прибирання інтерфейсу оплати паркувального місця
        public void ParkingViborSpotVisibleFalse()
        {
            comboBox1.Visible = false;
            checkBox1.Visible = false;
            textBox2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            trackBar1.Visible = false;
            button6.Visible = false;
        }
        // Функція для повернення до початкового інтерфейсу форми
        private void button5_Click(object sender, EventArgs e)
        {
            HideCells();
            city.getParking1.ParkingViborSpotCorrect();
            city.getParking2.ParkingViborSpotCorrect();
            city.getParking3.ParkingViborSpotCorrect();
            ParkingVisibleCorrect2();
            ParkingViborSpotVisibleFalse();
        }
        // Функція для паркування авто на паркування
        private void button6_Click(object sender, EventArgs e)
        {
            if (TryToParking())
            {
                city.getParking1.ParkingViborSpotCorrect();
                city.getParking2.ParkingViborSpotCorrect();
                city.getParking3.ParkingViborSpotCorrect();
                ParkingViborSpotVisibleFalse();
                button1.Text = city.getParking1.ParkingNameWrite("Parking 1");
                button2.Text = city.getParking2.ParkingNameWrite("Parking 2");
                button3.Text = city.getParking3.ParkingNameWrite("Parking 3");
                ComboBoxReload();
            }
        }
        // Функція для перевірки змоги припаркувати авто на паркування
        public bool TryToParking()
        {
            string name_vehicle = "";
            if (comboBox1.Text != "")
            {
                name_vehicle = comboBox1.Text;
            }
            else
            {
                MessageBox.Show("Ви не вибрали транспорт");
                return false;
            }
            if (trackBar1.Value == 0)
            {
                MessageBox.Show("Ви не обрали час паркування");
                return false;
            }
            string str = label2.Text;
            int priceInParking = 0;
            int parkingId = 0;
            if (str[str.Length - 1] == '1')
            {
                parkingId = 1;
                priceInParking = city.getParking1.Spots[cellPreviousRow, cellPreviousColumn].Cost;
            }
            else if (str[str.Length - 1] == '2')
            {
                parkingId = 2;
                priceInParking = city.getParking2.Spots[cellPreviousRow, cellPreviousColumn].Cost;
            }
            else if (str[str.Length - 1] == '3')
            {
                parkingId = 3;
                priceInParking = city.getParking3.Spots[cellPreviousRow, cellPreviousColumn].Cost;
            }
            else
            {
                MessageBox.Show("error");
                return false;
            }
            priceInParking *= trackBar1.Value;
            if (user.Money < priceInParking)
            {
                MessageBox.Show("Недостатньо коштів для паркування на такий час");
                return false;
            }
            bool isCar;
            if (checkBox1.Checked == true)
            {
                isCar = true;
            }
            else
            {
                isCar = false;
            }
            if (parkingId == 1)
            {
                city.getParking1.CarInParking(name_vehicle,priceInParking, trackBar1.Value,cellPreviousRow,cellPreviousColumn, user, isCar);

                return true;
            }
            else if (parkingId == 2)
            {
                city.getParking2.CarInParking(name_vehicle, priceInParking, trackBar1.Value, cellPreviousRow, cellPreviousColumn, user, isCar);
                return true;
            }
            else
            {
                city.getParking3.CarInParking(name_vehicle, priceInParking, trackBar1.Value, cellPreviousRow, cellPreviousColumn, user, isCar);
                return true;
            }
        }
        // Функція для правильного динамічного розміщення об'єктів на формі
        public void CorrectStartPosition()
        {
            int formWidth = this.Width;
            int formHeight = this.Height;
            button4.Width = formWidth / 10;
            button4.Height = formHeight / 20;
            button4.Left = formWidth - formWidth / 70 - button4.Width;
            button4.Top = formHeight / 40;
            button1.Left = formWidth / 10;
            button1.Top = formHeight / 4;
            button2.Left = formWidth / 2 + formWidth / 4;
            button2.Top = formHeight / 5;
            button3.Left = formWidth / 2;
            button3.Top = formHeight - formHeight / 6;
            label1.Left = (formWidth - label1.Width) / 2;
            label1.Top = formHeight / 30;
            button5.Height = button4.Height;
            button5.Width = button4.Width;
            button5.Left = button4.Left;
            button5.Top = formHeight - button5.Height - button4.Top;
        }
        // Функція для правильного динамічного розміщення об'єктів на формі
        public void CorrectStartPositionVibor()
        {
            int formWidth = this.Width;
            int formHeight = this.Height;
            textBox2.Width = label3.Width + trackBar1.Width + 60;
            textBox2.Height = label3.Height + trackBar1.Height + button6.Height + 80;
            textBox2.Top = textBox1.Top + textBox1.Height + 20;
            textBox2.Left = (formWidth - textBox2.Width) / 2;
            label3.Top = textBox2.Top + 20;
            label3.Left = textBox2.Left + 20;
            label4.Left = label3.Left + label3.Width + 20;
            label4.Top = label3.Top;
            checkBox1.Top = label3.Top + label3.Height + 10;
            comboBox1.Top = checkBox1.Top + checkBox1.Height + 10;
            checkBox1.Left = label3.Left;
            comboBox1.Left = label3.Left;
            trackBar1.Left = label4.Left;
            trackBar1.Top = label4.Top + label4.Height + 10;
            button6.Left = (formWidth - button6.Width) / 2;
            button6.Top = trackBar1.Top + trackBar1.Height + 40;
            label5.Left = trackBar1.Left + trackBar1.Width / 2;
            label5.Top = trackBar1.Top + trackBar1.Height + 10;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ComboBoxReload();
        }
        // функція для створення елементів та їх коректного виводу у comboBox1
        public void ComboBoxReload()
        {
            comboBox1.Text = "";
            System.Windows.Forms.ComboBox.ObjectCollection items = comboBox1.Items;
            items.Clear();
            if (checkBox1.Checked == true)
            {
                if (!(user.Cars == null))
                {
                    foreach (Car item in user.Cars)
                    {
                        items.Add(item.Name);
                    }
                }
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar1.Value.ToString();
        }
    }
}
