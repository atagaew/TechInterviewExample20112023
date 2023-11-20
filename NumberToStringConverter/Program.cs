// See https://aka.ms/new-console-template for more information


using System.Globalization;
using System.Runtime.InteropServices.ComTypes;

class Humanizer
{
    private string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    private string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    private string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
    static string[]  thousands = { "", "Thousand" };
    public string NumberToWords(int number)
    {
        var i = 0;
        var words = "";

        while (number > 0)
        {
            if (number % 1000 != 0)
            {
                words = $"{ConvertLessThanOneThousand((int)(number % 1000))} {thousands[i]} {words}";
            }

            number /= 1000;
            i++;
        }

        return words.Trim();
    }

    string ConvertLessThanOneThousand(int smallValue)
    {
        string result;

        if (smallValue % 100 < 10)
        {
            result = ones[smallValue % 100];
        }
        else if (smallValue % 100 < 20)
        {
            result = teens[smallValue % 10];
        }
        else
        {
            result = $"{tens[smallValue % 100 / 10]} {ones[smallValue % 10]}";
        }

        if (smallValue < 100)
        {
            return result;
        }
        
        return ones[smallValue / 100] + " Hundred " + result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int userInput;

        while (true)
        {
            do
            {
                Console.Write("Enter a number between 1 and 100,000: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 1 || userInput > 100000);

            string resultString = new Humanizer().NumberToWords(userInput);

            Console.WriteLine("You entered: " + resultString);
        }
    }
}

