using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VSRPP3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Memory;
        private double StackNumber;
        private string CurrentNumber;
        private string LastOperation;
        private bool HasComma;
        private bool Result;

        public MainWindow()
        {
            InitializeComponent();

            CurrentNumber = "";
            LastOperation = "";
            Memory = "";
            HasComma = false;
            Result = false;
        }

        private void WriteNumber(string text)
        {
            if (Result)
            {
                CurrentNumber = "";
                Result = false;
            }

            CurrentNumber += text;
            screen.Text = CurrentNumber.ToString();
        }

        private void MakeOperation(string operation)
        {
            if (LastOperation == "")
            {
                if (CurrentNumber == "")
                {
                    StackNumber = 0;
                }
                else
                {
                    StackNumber = Double.Parse(CurrentNumber);
                }

                LastOperation = operation;
                CurrentNumber = "";
                screen.Text = LastOperation;
                HasComma = false;
            }
        }

        private void MakeEqual()
        {
            Double currentNumber;

            if (CurrentNumber.Length > 0)
            {
                currentNumber = Double.Parse(CurrentNumber);
            }
            else
            {
                currentNumber = 0;
            }

            switch (LastOperation)
            {
                case "/":
                    {
                        screen.Text = (StackNumber / currentNumber).ToString();
                        break;
                    }
                case "*":
                    {
                        screen.Text = (currentNumber * StackNumber).ToString();
                        break;
                    }
                case "+":
                    {
                        screen.Text = (currentNumber + StackNumber).ToString();
                        break;
                    }
                case "-":
                    {
                        screen.Text = (StackNumber - currentNumber).ToString();
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            CurrentNumber = screen.Text;
            StackNumber = 0;
            LastOperation = "";
            Result = true;
        }

        private void MakeDel()
        {
            if (Result)
            {
                CurrentNumber = "";
            }

            if (CurrentNumber.Length > 0)
            {
                CurrentNumber = CurrentNumber.Remove(CurrentNumber.Length - 1);
                screen.Text = CurrentNumber;
            }

            if (CurrentNumber.Length == 0)
            {
                screen.Text = "0";
            }

            Result = false;
        }

        private void WriteComma()
        {
            if (!HasComma)
            {
                CurrentNumber += ",";
                screen.Text = CurrentNumber.ToString();
                HasComma = true;
            }
        }

        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            WriteNumber((sender as Button).Content.ToString());
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeOperation((sender as Button).Content.ToString());
        }

        private void EqualBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeEqual();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeDel();
        }

        private void CommaBtn_Click(object sender, RoutedEventArgs e)
        {
            WriteComma();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                WriteNumber(e.Key.ToString()[6].ToString());
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                WriteNumber(e.Key.ToString()[1].ToString());
            }
            if (e.Key == Key.Decimal)
            {
                WriteComma();
            }
            if (e.Key == Key.Add)
            {
                MakeOperation("+");
            }
            if (e.Key == Key.Subtract)
            {
                MakeOperation("-");
            }
            if (e.Key == Key.Multiply)
            {
                MakeOperation("*");
            }
            if (e.Key == Key.Divide)
            {
                MakeOperation("/");
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                MakeDel();
            }
            if (e.Key == Key.Enter)
            {
                MakeEqual();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Memory = screen.Text;
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            CurrentNumber = Memory;

            if (CurrentNumber != "")
            {
                screen.Text = CurrentNumber;
            } else
            {
                screen.Text = "0";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Memory = "";
        }
    }
}
