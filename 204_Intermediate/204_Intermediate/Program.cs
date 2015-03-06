using System;
using System.Collections.Generic;

namespace _204_Intermediate
{
    internal class Program
    {
        #region Properties

        private static int MaxPosition;
        private static int IntToConvertToBinary;
        private static List<string> HyperBinaryList;

        #endregion Properties

        #region Main

        private static void Main(string[] args)
        {
            var running = true;

            while (running)
            {
                string userInput = GetUserInput();

                if (string.IsNullOrEmpty(userInput))
                {
                    running = false;
                    break;
                }

                HyperBinaryList = new List<string>();
                IntToConvertToBinary = Int32.Parse(userInput);

                GetMaxPosition();
                GetAllHyperBinaryRepresentations();

                PrintResults();
            }
        }

        #endregion Main

        #region Private Helpers

        private static void GetMaxPosition()
        {
            int i = 0;
            var maxFound = false;

            while (!maxFound)
            {
                if ((2 * Math.Pow(2, i)) > IntToConvertToBinary)
                    maxFound = true;

                i++;
            }

            MaxPosition = i;
        }

        private static void GetAllHyperBinaryRepresentations()
        {
            List<int> binary = new List<int>();

            for (int i = 0; i < MaxPosition; i++)
            {
                binary.Add(0);
            }

            int numberOfCombinations = (int)Math.Pow(3, MaxPosition);

            for (int i = 0; i < numberOfCombinations; i++)
            {
                if (GetValueFromBinaryList(binary) == IntToConvertToBinary)
                {
                    HyperBinaryList.Add(GetStringFromBinaryList(binary));
                }

                IncreaseBinaryList(binary, 0);
            }
        }

        private static string GetStringFromBinaryList(List<int> binary)
        {
            string str = "";

            binary.ForEach(x => str = x.ToString() + str);

            return str;
        }

        private static int GetValueFromBinaryList(List<int> binary)
        {
            int value = 0;

            for (int i = 0; i < binary.Count; i++)
            {
                value += binary[i] * (int)Math.Pow(2, i);
            }

            return value;
        }

        private static List<int> IncreaseBinaryList(List<int> binary, int position)
        {
            if (position >= MaxPosition)
                return null;

            binary[position]++;

            if (binary[position] == 3)
            {
                binary[position] = 0;
                IncreaseBinaryList(binary, position + 1);
            }

            return binary;
        }

        private static string GetUserInput()
        {
            var validInput = false;
            int number = -int.MaxValue;

            while (!validInput)
            {
                Console.Write("Enter a number to convert to hyper-binary (Q to quit): ");
                var inputString = Console.ReadLine().Trim();

                if (inputString.ToLower().Equals("q"))
                {
                    return null;
                }
                else if (Int32.TryParse(inputString, out number))
                {
                    if (number < 0)
                    {
                        Console.WriteLine("Please select a positive number.");
                    }
                    else
                    {
                        validInput = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            return number.ToString();
        }

        private static void PrintResults()
        {
            Console.WriteLine(Environment.NewLine + HyperBinaryList.Count + " hyper-binary representations found for " + IntToConvertToBinary + "..." + Environment.NewLine);

            HyperBinaryList.ForEach(x => Console.WriteLine(">>" + x));

            Console.WriteLine();
        }

        #endregion Private Helpers
    }
}