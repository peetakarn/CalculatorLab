﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private String mResult;
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand;
        private string operate;
        private bool secondOperandClicked = false;
        private CalculatorEngine cal = new CalculatorEngine();

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            
        }

       

        public MainForm()
        {
            InitializeComponent();

            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if(secondOperandClicked && isAfterOperater==false)
            {
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                string secondOperand = lblDisplay.Text;
                string result = cal.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
                secondOperandClicked = false;
            }
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            operate = ((Button)sender).Text;
            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    secondOperandClicked = true;
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    hasDot = false;
                    break;
                case "%":
                    // your code here
                    break;
            }
            isAllowBack = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = cal.calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
            secondOperandClicked = false;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Double oneOverX = (1 / Convert.ToDouble(lblDisplay.Text));
            oneOverX = Math.Round(oneOverX, 7);
            lblDisplay.Text = oneOverX.ToString();
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            string mOperate = ((Button)sender).Text;
            switch (mOperate)
            {
                case "MC":
                    mResult = "0";
                    lblDisplay.Text = "0";
                    lblM.Text = mResult; break;
                case "MR":
                    lblDisplay.Text = mResult;
                    break;
                case "MS":
                    mResult = lblDisplay.Text;
                    lblM.Text = mResult;
                    break;
                case "M+":
                    if(mResult=="0")mResult = lblDisplay.Text;
                    mResult =(Convert.ToDouble(mResult) + Convert.ToDouble(lblDisplay.Text)).ToString();
                    lblM.Text = mResult; break;
                case "M-":
                    mResult = (Convert.ToDouble(mResult) - Convert.ToDouble(lblDisplay.Text)).ToString();
                    lblM.Text = mResult; break;
            }
        }

        private void root_Click(object sender, EventArgs e)
        {
            Double sqrt = (Math.Sqrt(Convert.ToDouble(lblDisplay.Text)));
            
            lblDisplay.Text = cal.DecimalManage(sqrt);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double percent;
            //percent = (Convert.ToDouble(lblDisplay.Text)) *
            percent = Convert.ToDouble(firstOperand) * (Convert.ToDouble(lblDisplay.Text)/100);
            lblDisplay.Text = percent.ToString();
        }
    }
}
