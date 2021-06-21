using System;

namespace LogLibrary.Classes
{
    /// <summary>
    /// Responsible for log operations
    /// </summary>
    /// <remarks>
    /// Thread safe singleton class
    /// </remarks>
    public sealed class ApplicationLog
    {
        private static readonly Lazy<ApplicationLog> Lazy = new Lazy<ApplicationLog>(() => new ApplicationLog());
		/// <summary>
        /// Access point to methods and properties
        /// </summary>
        public static ApplicationLog Instance => Lazy.Value;

        /// <summary>
        /// Called first time accessing this class, never again
        /// </summary>
        private ApplicationLog()
        {
            Exceptions.FileName = "";
        }
        /// <summary>
        /// Write to any exception to log file with optional message. Exception file name is
        /// determined by <see cref="ExceptionLogType"/>
        /// </summary>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <param name="text">Message to tag on to exception log</param>
        public  void WriteException(Exception exception, string text = "")
        {
            Exceptions.Write(exception, ExceptionLogType.General, text);
        }
        /// <summary>
        /// Write string to a incrementing log file e.g. Log_1.txt, Log_2.txt where
        /// an algorithm determines the file name in <see cref="Operations"/>
        /// </summary>
        /// <param name="text">Text to write which can be a one liner or several lines</param>
        public void Write(string text)
        {
            Exceptions.FileName = Operations.NextFileName();
            Exceptions.Write(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string LastName() => Operations.GetLast();
        /// <summary>
        /// Write to main log file without an exception
        /// </summary>
        /// <param name="text"></param>
        public void WriteInformation(string text)
        {
            Operations.WriteInformation(text);
        }

    }
}
