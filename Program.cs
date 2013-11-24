using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ListExtensions {
    /// <summary>
    /// The main program
    /// </summary>
    class Program {
        /// <summary>
        /// The main point of entry of the application
        /// </summary>
        /// <param name="args">The application arguments.</param>
        static void Main (string[] args) {
            #region Parse options
            string directory;

            if (args.Length == 0) {
                directory = Environment.CurrentDirectory;
            } else {
                directory = args[0];
                if (!Directory.Exists(directory)) {
                    Console.Error.WriteLine("Directory not found: {0}", directory);
                    return;
                }
            }
            #endregion

            #region Procedural code
            string[] extensions = GetExtensionsInDirectory(directory, true);
            Console.Write(String.Join(" ", extensions));
            #endregion
        }

        /// <summary>
        /// Gets the extensions in directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="lookInSubDirectories">if set to <c>true</c>, looks in subdirectories.</param>
        /// <returns>
        /// An array containing the extensions of files found in directory.
        /// </returns>
        public static string[] GetExtensionsInDirectory (string directory, bool lookInSubDirectories) {
            HashSet<string> extensions = new HashSet<string>();
            DirectoryInfo di = new DirectoryInfo(directory);
            
            //Look the files in the current directories, and get an unique list of extensions
            foreach (FileInfo fi in di.GetFiles()) {
                extensions.Add(fi.Extension);
            }

            //In recursive, looks also in subdirectories
            if (lookInSubDirectories) {
                foreach (DirectoryInfo subDi in di.GetDirectories()) {
                    extensions.UnionWith(GetExtensionsInDirectory(subDi.FullName, true));
                }
            }

            string[] result = new string[extensions.Count];
            extensions.CopyTo(result);
            return result;
        }
    }
}
