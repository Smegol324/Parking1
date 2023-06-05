using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parking
{
    public partial class EditSizeParking_Form : Form
    {
        User user;
        City city;
        public EditSizeParking_Form(User user, City city)
        {
            this.user = user;
            this.city = city;
            InitializeComponent();
        }

        private void EditSizeParking_Form_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            CorrectStartPosition();
            textBox1.Text = city.ParkingSizeWrite();
        }
        public void CorrectStartPosition()
        {
            int formWidth = this.Width;
            int formHeight = this.Height;
            button1.Width = formWidth / 10;
            button1.Height = formHeight / 20;
            button1.Left = formWidth - formWidth / 70 - button1.Width;
            button1.Top = formHeight / 40;
            label1.Left = (formWidth - label1.Width) / 7;
            label1.Top = (formHeight - textBox1.Height) / 3;
            comboBox1.Left = label1.Left + label1.Width + 20;
            comboBox1.Top = label1.Top - 3;
            textBox1.Left = formWidth / 2 + (formWidth - textBox1.Width) / 4;
            textBox1.Top = (formHeight - textBox1.Height) / 2 - formHeight / 10;
            label2.Left = textBox1.Left + textBox1.Width / 2 - label2.Width / 2;
            label2.Top = textBox1.Top - label2.Height - 20;
            button2.Width = formWidth / 10;
            button2.Height = formHeight / 20;
            button2.Top = formHeight / 2 + formHeight / 3;
            button2.Left = (formWidth - button2.Width) / 2;
            label3.Left = label1.Left;
            label3.Top = label1.Top + label1.Height + 20;
            label4.Left = label1.Left;
            label4.Top = label3.Top + label3.Height + 20;
            label5.Left = label1.Left;
            label5.Top = label4.Top + label4.Height + 20;
            label6.Left = label1.Left;
            label6.Top = label5.Top + label5.Height + 20;
            numericUpDown2.Left = label6.Left + label6.Width + 5;
            numericUpDown2.Top = label6.Top - 3;
            numericUpDown1.Left = numericUpDown2.Left;
            numericUpDown1.Top = label5.Top - 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu_Form menu = new Menu_Form(user, city);
            menu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            int new_row = (int)numericUpDown1.Value;
            int new_column = (int)numericUpDown2.Value;
            switch (selectedItem)
            {
                case "Parking 1":
                    if (city.getParking1.TotalNumberOfSpots == city.getParking1.FreeParkingSpaces)
                    {
                        if (new_row != 0 && new_column != 0)
                        {
                            city.getParking1.ParkingSizeChange(new_row, new_column);
                            textBox1.Text = city.ParkingSizeWrite();
                        }
                        else
                        {
                            MessageBox.Show("значення кількості рядків або кількості стовпців не може бути рівне 0");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Parking 1 не ввільний, тому його розмір неможливо змінити");
                    }
                    break;
                case "Parking 2":
                    if (city.getParking2.TotalNumberOfSpots == city.getParking2.FreeParkingSpaces)
                    {
                        if (new_row != 0 && new_column != 0)
                        {
                            city.getParking2.ParkingSizeChange(new_row, new_column);
                            textBox1.Text = city.ParkingSizeWrite();
                        }
                        else
                        {
                            MessageBox.Show("значення кількості рядків або кількості стовпців не може бути рівне 0");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Parking 2 не ввільний, тому його розмір неможливо змінити");
                    }
                    break;
                case "Parking 3":
                    if (city.getParking3.TotalNumberOfSpots == city.getParking3.FreeParkingSpaces)
                    {
                        if (new_row != 0 && new_column != 0)
                        {
                            city.getParking3.ParkingSizeChange(new_row, new_column);
                            textBox1.Text = city.ParkingSizeWrite();
                        }
                        else
                        {
                            MessageBox.Show("значення кількості рядків або кількості стовпців не може бути рівне 0");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Parking 3 не ввільний, тому його розмір неможливо змінити");
                    }
                    break;
                default:
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            switch (selectedItem)
            {
                case "Parking 1":
                    label3.Text = "Попередній розмір: " + city.getParking1.NumberOfRows + "x" + city.getParking1.NumberOfColumns;
                    break;
                case "Parking 2":
                    label3.Text = "Попередній розмір: " + city.getParking2.NumberOfRows + "x" + city.getParking2.NumberOfColumns;
                    break;
                case "Parking 3":
                    label3.Text = "Попередній розмір: " + city.getParking3.NumberOfRows + "x" + city.getParking3.NumberOfColumns;
                    break;
                default:
                    break;
            }
        }
    }
}
