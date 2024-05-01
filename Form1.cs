using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace P1_Calculator
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();
        }





        private void AppendToInput(string value)
        {
            textBox1.Text += value;
        }
        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            AppendToInput(button.Text);
        }





        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

        }



        List<double> Numbers = new List<double>();
        List<char> Operators = new List<char>();

        void GetNumbers()
        {
            // 32+2-1 

            List<string> tokens = new List<string>(textBox1.Text.ToString().Trim().Split('*', '/', '+', '-', '%'));
            foreach (string token in tokens)
            {
                Numbers.Add(double.Parse(token));
            }
        }



        void GetOperators()
        {
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] == '-' || textBox1.Text[i] == '+' ||
                    textBox1.Text[i] == '*' || textBox1.Text[i] == '/' || textBox1.Text[i] == '%')
                {
                    Operators.Add(textBox1.Text[i]);
                }
            }
        }


        private void btnResult_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                GetNumbers();
                GetOperators();
                CalculateResult();
                textBox1.Text = Numbers[0].ToString();
                Operators.Clear();
                Numbers.Clear();
            }
        }
        private double CalculateTwoNumbers(double Number1, double Number2, char Operator)
        {
            switch (Operator)
            {
                case '+':
                    return Number1 + Number2;
                case '-':
                    return Number1 - Number2;
                case '%':
                    return Number1 % Number2;
                case '*':
                    return Number1 * Number2;
                case '/':
                    return Number1 / Number2;
            }
            return 0;
        }


        void CalculateResult()
        {
            double Result = 0;
            for (int i = 0; i < Operators.Count;)
            {
                if (Operators[i] == '/' || Operators[i] == '*')
                {
                    Numbers[i] = CalculateTwoNumbers(Numbers[i], Numbers[i + 1], Operators[i]);  //--
                    Numbers.RemoveAt(i + 1);
                    Operators.RemoveAt(i);
                    i--;
                }
                i++;
            }
            while (Operators.Count > 0)
            {
                for (int i = 0; i < Operators.Count;)
                {
                    Numbers[0] = CalculateTwoNumbers(Numbers[0], Numbers[1], Operators[0]);  //--
                    Numbers.RemoveAt(1);
                    Operators.RemoveAt(0);

                    i++;
                }
            }

        }



        private void btnReturn_Click(object sender, EventArgs e)
        {

            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
        }






    }
}
