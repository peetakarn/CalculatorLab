﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class CalculatorEngine
    {
        public string DecimalManage(double result)
        {
            string[] parts;
            int remainLength;
            string returnTo = String.Format("{0:0.######}", result);
            parts = returnTo.Split('.');
            // if integer part length is already break max output, return error
            if (parts[0].Length > 8)
            {
                return "Error";
            }
            // calculate remaining space for fractional part.
            remainLength = 8 - parts[0].Length - 1;
            //trim the fractional part gracefully. =
            returnTo = Convert.ToDouble(returnTo).ToString("N" + remainLength);
            if (returnTo.IndexOf(".") != -1)
            {
                for (;;)
                {
                    if (returnTo[(returnTo.Length) - 1] == '0')
                    {
                        returnTo = returnTo.Substring(0, returnTo.Length - 1);
                    }
                    else if (returnTo[(returnTo.Length) - 1] == '.')
                    {
                        returnTo = returnTo.Substring(0, returnTo.Length - 1);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (returnTo.Length > 8)
            {
                return "Error";
            }
            return returnTo;
        }
        public string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            switch (operate)
            {
                case "+":
                    return DecimalManage((Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)));
                case "-":
                    return DecimalManage((Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)));
                case "X":
                    return DecimalManage((Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)));
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != "0")
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return DecimalManage(result);
                    }
                    break;
                case "%":
                    //your code here
                    break;
            }
            return "E";
        }
    }
}
