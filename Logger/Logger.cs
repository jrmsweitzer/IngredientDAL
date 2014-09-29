using System;
using System.Diagnostics;

namespace Logger
{
    public static class Logger
    {
        public static string OPENINGDATABASE =
            "Opening the production database...";

        public static string OPENINGMOCKDATABASE =
            "Opening a new in-memory database...";

        public static void Error(string message)
        {
            WriteEntry(message, "error");
        }

        public static void Error(Exception ex)
        {
            WriteEntry(ex.Message, "error");
        }

        public static void Warning(string message)
        {
            WriteEntry(message, "warning");
        }

        public static void Info(string message)
        {
            WriteEntry(message, "info");
        }

        private static void WriteEntry(string message, string type)
        {
            Trace.WriteLine(
                    string.Format("{0},{1},{2}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  type,
                                  message));
        }
    }
}
