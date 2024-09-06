﻿using Microsoft.Data.SqlClient;
using System.Collections;
using System.Globalization;

partial class Program
{
    private static void ConfigureConsole(string culture = "en-US", bool useComputerCulture = false) 
    {
        // To enable Unicode characters like Euro symbol in the console.
        OutputEncoding = System.Text.Encoding.UTF8;

        if (!useComputerCulture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture); 
        }
        WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
    }

    private static void WriteLineInColor(string value, ConsoleColor color = ConsoleColor.White)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = color;
        WriteLine(value);
        ForegroundColor = previousColor;
    }

    private static void OutputStatistics(SqlConnection connection)
    {
        // Remove all the string valus to see all the statistics
        string[] includeKeys = { "BytesSent", "BytesReceived", "ExecutionTime", "ConnectionTime", "SelectRows" };
        IDictionary statistics = connection.RetrieveStatistics();
        foreach (object? key in statistics.Keys) 
        {
            if (!includeKeys.Any() || includeKeys.Contains(key))
            {
                if (int.TryParse(statistics[key]?.ToString(), out int value)) 
                {
                    WriteLineInColor($"{key}: {value:N0}", ConsoleColor.Cyan);
                }
            }
        }
    }
}
