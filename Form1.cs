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


        private void UpdateButtonState()
        {
            string input = textBox1.Text;
            bool isLastCharOperator = input.Length > 0 && (input[input.Length - 1] == '+' || input[input.Length - 1] == '-' ||
                                                           input[input.Length - 1] == '*' || input[input.Length - 1] == '/' ||
                                                           input[input.Length - 1] == '%');


            // check Decimal Button 
            bool LastNumberContainsDecimal = false;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == '.')
                {
                    LastNumberContainsDecimal = true;
                    break;
                }
                else if (char.IsDigit(input[i]))
                {
                    // Found a digit, so this is part of the last number
                    break;
                }
            }



            // Disable all operator buttons if the last character is an operator
            btnAdd.Enabled = !isLastCharOperator && input[input.Length - 1] != '.';
            btnSubtract.Enabled = !isLastCharOperator && input[input.Length - 1] != '.';
            btnMultiply.Enabled = !isLastCharOperator && input[input.Length - 1] != '.';
            btnDivide.Enabled = !isLastCharOperator && input[input.Length - 1] != '.';
            btnModulus.Enabled = !isLastCharOperator && input[input.Length - 1] != '.';

            // Disable equal button if there are no numbers or if the last character is an operator
            btnResult.Enabled = input.Length > 0 && !isLastCharOperator && !LastNumberContainsDecimal;
            // Disable the decimal button if the last character is an operator or if there's already a decimal in the current number
            btnDecimal.Enabled = !isLastCharOperator && input[input.Length - 1] != '.' && !LastNumberContainsDecimal;



            // Disable the decimal button if the last character is an operator or if there's already a decimal in the current number
            btnDecimal.Enabled = !isLastCharOperator && input[input.Length - 1] != '.' && !LastNumberContainsDecimal;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }
    }
}
