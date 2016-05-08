using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameUtils
{
    public static string ReplaceTail(string count, string text)
    {
        int number1 = Int32.Parse(count.Substring(count.Length - 1, 1));
        int number2 = count.Length > 1 ? Int32.Parse(count.Substring(0)) : 0;
        string pattern = @"{.+}";
        Regex regex = new Regex(pattern);
        var match = regex.Match(text);
        string totalString = text.Substring(0, match.Index);
        var result = match.Value.Substring(1, match.Length - 2);
        string[] data = result.Split('|');

        if (number2 == 11 || number2 == 12 || number2 == 13 || number2 == 14)
        {
            totalString += data[2];
        }
        else if (number1 == 1)
        {
            totalString += data[0];
        }
        else if (number1 == 2 || number1 == 3 || number1 == 4)
        {
            totalString += data[1];
        }
        else
        {
            totalString += data[2];
        }
        return count + " " + totalString;
    }
}
