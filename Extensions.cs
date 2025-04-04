using System;
using static BikeDB2024.Helpers;

namespace ExtensionMethods
{
    public static class Extensions
    {
        /// <summary>
        /// Simple Word Count Method as extension to strings.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Word Count.</returns>
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?', '!', ';', ',' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// http://www.csharphelper.com/howtos/howto_file_size_in_words.html
        /// </summary>
        /// <param name="value"></param>
        /// <returns>File Size as string.</returns>
        public static string ToFileSize(this double value)
        {
            string[] suffixes = { "bytes", "KB", "MB", "GB",
                "TB", "PB", "EB", "ZB", "YB"};
            for (int i = 0; i < suffixes.Length; i++)
            {
                if (value <= (Math.Pow(1024, i + 1)))
                {
                    return ThreeNonZeroDigits(value /
                        Math.Pow(1024, i)) +
                        " " + suffixes[i];
                }
            }

            return ThreeNonZeroDigits(value /
                Math.Pow(1024, suffixes.Length - 1)) +
                " " + suffixes[suffixes.Length - 1];
        }
    }
}
