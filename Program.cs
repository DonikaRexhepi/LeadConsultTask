using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeadConsultTask
{
    class Program
    {
        #region Private properties

        private static int IndexOfOpenParanth(string input) => input.IndexOf('(');
        private static int IndexOfComma(string input) => input.IndexOf(',');
        private static int IndexOfClosingParanth(string input) => input.IndexOf(')');

        private static List<Coordinate> Coordinates = new List<Coordinate>();
        #endregion
        static void Main(string[] args)
        {
            //default value for testing: C:/Users/Admin/Desktop/input.txt
            try
            {
                bool appRunning = true;
                while (appRunning)
                {
                    Console.WriteLine("Please enter the file path for inputs: ");

                    string userInputFilePath = Console.ReadLine();
                    // Read all lines from the file, ignore empty lines
                    string[] lines = File.ReadAllLines(userInputFilePath).Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (ValidateFileLine(lines[i]))
                        {
                            Coordinates.Add(new Coordinate(GetValueOfX(lines[i]) ?? 0, GetValueOfY(lines[i]) ?? 0));
                        }
                        else
                        {
                            Console.WriteLine($"Line on index {i} is an invalid input");
                        }
                    }

                    var furtherestPoint = Coordinates.OrderByDescending(e => e.DistanceFromCenter).FirstOrDefault();

                    Console.WriteLine("\n\n");
                    Console.WriteLine($"The furtherest point from the center is line with coordinates ({furtherestPoint.X},{furtherestPoint.Y})");
                    Console.WriteLine($"The furtherest point is in the {furtherestPoint.QuadrantOfTheCoordinate}");
                    Console.WriteLine("\n\n");
                    Console.Write("Do you want to try again? (y/n): ");
                    string userInput = Console.ReadLine();
                    if (userInput.ToLower() != "y")
                    {
                        appRunning = false;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        

        public static bool ValidateFileLine(string input)
        {
            var indexOfOpenParanth = IndexOfOpenParanth(input);
            var indexOfComma = IndexOfComma(input);
            var indexOfClosingParanth = IndexOfClosingParanth(input);

            //check if the line number contains ( , and ), otherwise it is invalid template
            if(indexOfOpenParanth == -1 || indexOfComma == -1 || indexOfClosingParanth == -1)
            {
                return false;
            }
            //we start with Point#(#.#,#.#) where # is int and #.# is a double number

            var keyword = "point";
            //check if the line starts with keyword Point, otherwise it is invalid
            if (!input.ToLower().StartsWith(keyword))
            {
                //the text does not start with word 'Point', invalid input from the file
                return false;
            }
            var lineNumber = input.Substring(keyword.Length, indexOfOpenParanth - keyword.Length);

            if (!int.TryParse(lineNumber, out int number))
            {
                //the text after Point##, is not a number, an invalid input from the file
                return false;
            }

            //now we check if X coordinate input is a double number
            if (GetValueOfX(input) == default)
            {
                //the number on the X coordinate is not a double number
                return false;
            }

            //now we check if Y coordinate input is a double number
            if (GetValueOfY(input) == default)
            {
                //the number on the Y coordinate is not a double number
                return false;
            }

            //the input line is of correct format
            return true;
        }
        public static double? GetValueOfX(string input)
        {
            var indexOfOpenParanth = IndexOfOpenParanth(input);
            var indexOfComma = IndexOfComma(input);

            //now we check if X coordinate input is a double number
            var xCoordinate = input.Substring(indexOfOpenParanth + 1, indexOfComma - indexOfOpenParanth - 1);
            if (double.TryParse(xCoordinate, out double xNumber))
            {
                return xNumber;
            }
            //the number on the X coordinate is not a double number
            return default;
        }
        public static double? GetValueOfY(string input)
        {
            var indexOfComma = IndexOfComma(input);
            var indexOfClosingParanth = IndexOfClosingParanth(input);

            //now we check if Y coordinate input is a double number
            var yCoordinate = input.Substring(indexOfComma + 1, indexOfClosingParanth - indexOfComma - 1);
            if (double.TryParse(yCoordinate, out double yNumber))
            {
                return yNumber;
            }
            //the number on the Y coordinate is not a double number
            return default;
        }

    }
}
