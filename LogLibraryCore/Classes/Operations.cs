using System;
using System.IO;
using System.Linq;
using static System.IO.File;

namespace LogLibraryCore.Classes
{
    /// <summary>
    /// Several choices for unique file names, pick one remove the others.
    /// </summary>
    public class Operations
    {

        #region Two methods produce non Legible names
        /// <summary>
        /// Create a unique file name by date ticks
        /// </summary>
        public static string UniqueFileNameByTicks => $@"{DateTime.Now.Ticks}.txt";

        /// <summary>
        /// Create a unique file name by date ticks and a guid
        /// </summary>
        public static string GenerateFileNameByDateAndGuid() => 
            $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}.txt";
        
        #endregion

        /// <summary>
        /// Write to main/general log file w/o any exception information
        /// </summary>
        /// <param name="text">Text to log</param>
        public static void WriteInformation(string text)
        {
            var fileName = "GeneralUnhandledException.txt";

            if (File.Exists(fileName))
            {
                var contents = ReadAllText(fileName) + Environment.NewLine + text;
                WriteAllText(fileName, contents);
            }
            else
            {
                WriteAllText(fileName, text);
            }
        }

        /// <summary>
        /// Pattern for base file name incrementing together with <see cref="_baseFileName"/>
        /// </summary>
        private static readonly string _pattern = "_{0}";
        /// <summary>
        /// Base file name together with <see cref="_pattern"/>
        /// </summary>
        private static string _baseFileName => "Log";
        /// <summary>
        /// Wrapper for <seealso cref="NextAvailableFilename"/> to
        /// obtain next available file name in a specific folder
        /// </summary>
        /// <returns>Unique ordered file name</returns>
        /// <remarks>
        /// Path is set to main assembly location with a base name of Import.txt
        /// </remarks>
        public static string NextFileName() => NextAvailableFilename(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{_baseFileName}.txt"));

        /// <summary>
        /// Wrapper for <see cref="GetNextFilename"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Next incremented file name</returns>
        public static string NextAvailableFilename(string path)
        {

            if (!File.Exists(path) && Path.GetFileName(path).All(char.IsLetter))
            {
                return path;
            }

            return Path.HasExtension(path) ?
                GetNextFilename(path.Insert(path.LastIndexOf(
                    Path.GetExtension(path), StringComparison.Ordinal), _pattern)) :
                GetNextFilename(path + _pattern);

        }
        /// <summary>
        /// Work horse for <seealso cref="NextFileName"/>
        /// </summary>
        /// <param name="pattern"><see cref="_pattern"/></param>
        /// <returns>Next file name</returns>
        private static string GetNextFilename(string pattern)
        {
            string tmpValue = string.Format(pattern, 1);

            if (tmpValue == pattern)
            {
                throw new ArgumentException("The pattern must include an index place-holder", 
                    nameof(pattern));
            }

            if (!File.Exists(tmpValue))
            {
                return tmpValue;
            }

            int min = 1;
            int max = 2;

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;

                if (File.Exists(string.Format(pattern, pivot)))
                {
                    min = pivot;
                }
                else
                {
                    max = pivot;
                }
            }

            return string.Format(pattern, max);
        }

        /// <summary>
        /// Strip characters from string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string with numbers only</returns>
        public static string GetNumbers(string input) =>
            new(input.Where(char.IsDigit).ToArray());

        /// <summary>
        /// Get all log files
        /// </summary>
        private static string[] LogFiles =>
            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,
                $"{_baseFileName}*.txt");

        /// <summary>
        /// Get last log file by int value
        /// </summary>
        /// <returns>Last log file</returns>
        public static string GetLast()
        {
            var result = LogFiles
                .Select(file => new
                {
                    Name = file, 
                    Number = Convert.ToInt32(GetNumbers(Path.GetFileName(file)))
                })
                .OrderByDescending(x => x.Number).FirstOrDefault();

            return result?.Name;
        }

        /// <summary>
        /// Determine if there are any files
        /// </summary>
        /// <returns></returns>
        public static bool HasAnyFiles() => LogFiles.Length > 0;

        /// <summary>
        /// Remove all files
        /// </summary>
        public static void RemoveAllFiles()
        {
            if (!HasAnyFiles()) return;
            
            foreach (var file in LogFiles)
            {
                File.Delete(file);
            }
        }
    }
}

