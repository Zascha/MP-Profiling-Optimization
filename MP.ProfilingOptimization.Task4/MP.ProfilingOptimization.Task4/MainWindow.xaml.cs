// Decompiled with JetBrains decompiler
// Type: MyCalculatorv1.MainWindow
// Assembly: MyCalculatorv1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E4247A5-25E4-47A6-84F4-A414933F7536
// Assembly location: C:\Users\Aliaksandra_Zakharav\Documents\MP\Profiling&Optimization\MP.ProfilingOptimization\MP.ProfilingOptimization.Task4\MyCalculator.exe

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MyCalculatorv1
{
    public partial class MainWindow : Window, IComponentConnector
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tb.Text += ((Button)sender).Content.ToString();
        }

        private void Result_click(object sender, RoutedEventArgs e)
        {
            Result();
        }

        private readonly Regex _numberRegex = new Regex("[0-9]+", RegexOptions.Compiled);
        private readonly Regex _symbolRegex = new Regex(@"(\+|-|\*|\/)+", RegexOptions.Compiled);

        private void Result()
        {
            if (tb.Text.Contains('='))
            {
                return;
            }

            var symbol = _symbolRegex.Match(tb.Text).Value.First();
            var num1 = double.Parse(_numberRegex.Matches(tb.Text)[0].Value);
            var num2 = double.Parse(_numberRegex.Matches(tb.Text)[1].Value);

            if (symbol == '+')
            {
                var result = num1 + num2;
                tb.Text = $"{tb.Text}={result}";
            }
            else if (symbol == '-')
            {
                var result = num1 - num2;
                tb.Text = $"{tb.Text}={result}";
            }
            else if (symbol == '*')
            {
                var result = num1 * num2;
                tb.Text = $"{tb.Text}={result}";
            }
            else
            {
                var result = num1 / num2;
                tb.Text = $"{tb.Text}={result}";
            }
        }

        private void Off_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "";
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text.Length <= 0)
                return;
            tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
        }
    }
}
