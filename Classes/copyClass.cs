﻿using HtmlAgilityPack;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace testInstallServer.Classes
{
    public class copyClass
    {
        private bool flag;

        private loggingClass loggingClass = new loggingClass();

        #region copying code

        //Mobile copy
        //this will copy a file from one location to another location as sent by PreReqSearch above
        public async void mobileCopy(string FileNamePath)
        {
            try
            {
                string filename = Path.GetFileName(FileNamePath);

                string replace = Path.Combine(configValues.preReqRun, filename);

                File.Copy(FileNamePath, replace, true);

                string logEntry = filename + " has been copied.";

                loggingClass.logEntryWriter(logEntry, "info");
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "info");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        //Mobile copy
        //this will copy all files located at the NWSHoldPath.txt to the MobileInstaller folder within C:\Temp
        public void mobileCopy1(string SourcePath, string TargetLocation)
        {
            string TargetPath = TargetLocation;
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, TargetPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, TargetPath), true);
        }

        public async Task webDownloadAsync(string URL1)
        {
            HtmlWeb client = new HtmlWeb();
            HtmlDocument doc = client.Load(URL1);
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            try
            {
                Directory.CreateDirectory(@"C:\Temp\_Client-Installation");
                Directory.CreateDirectory(@"C:\Temp\_Client-Installation\DecompressionHolder");

                foreach (var link in Nodes)
                {
                    var newURL = link.Attributes["href"].Value;

                    if (newURL.Equals("#mobile") || newURL.Equals("#cad"))
                    {
                        //nothing
                    }
                    else
                    {
                        WebClient webClient1 = new WebClient();

                        char[] seperators = new char[] { '/' };

                        string[] speratedURL = newURL.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        webClient1.DownloadFile(newURL, Path.Combine(@"C:\Temp\_Client-Installation", speratedURL.Last()));

                        loggingClass.logEntryWriter($"{speratedURL.Last()} copied to temp directory from HMS", "info");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(404) Not Found."))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        #endregion copying code

        #region pre req search/rename/copy

        //this is designed to relabel the SQL compact 4.0 64bit and 32bit components.
        //this is so that CAD and the other applications will be able to have the correct pre reqs
        public async void preReqRename(string FileName, string NewName, string SubFolderSearch, string folderPath)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(folderPath))
                {
                    foreach (var filename in Directory.GetFiles(directory))
                    {
                        string sourcepath = Path.GetDirectoryName(filename);

                        //if the directory of a found file contains the text of a searched term/s
                        //then a specific file is renamed to a desired name.
                        if (sourcepath.ToString().Contains(SubFolderSearch))
                        {
                            File.Move(Path.Combine(sourcepath, FileName), Path.Combine(sourcepath, NewName));

                            string logEntry = FileName + " has been renamed to" + NewName;

                            loggingClass.logEntryWriter(logEntry, "info");
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                loggingClass.nLogLogger(ex.ToString(), "error");
            }
            catch (Exception ex)
            {
                //if the exception error contains the text file not found (system.IO.FileNotFound)
                //then specific text is entered into the log file.
                if (ex.ToString().Contains("FileNotFound"))
                {
                }
                else if (ex.Message.Contains("Could not find a part of the path"))
                {
                }
                else if (ex.Message.Contains("The UNC path should be of the form"))
                {
                    string logEntry = $"Please verify {folderPath} it may be incorrect.";

                    loggingClass.logEntryWriter(logEntry, "debug");
                }
                else if (ex.Message.Contains("Access to the path is denied."))
                {
                    string logEntry = $"You're user is unable to rename a file. Please ensure your user account has full right to {folderPath}";

                    loggingClass.logEntryWriter(logEntry, "debug");
                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("The given path's format is not supported"))
                {
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        //this searches through a user entered directory/subdirectories for pre reqs
        public async void preReqSearchCopy(string sDir)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(sDir))
                {
                    foreach (var filename in Directory.GetFiles(directory))
                    {
                        mobileCopy(filename);
                    }

                    //this is so that a folder that has a subdirectory will also be searched
                    preReqSearchCopy(directory);
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "info");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        //will search for different versions of applications
        //Primarily for .net
        public int preReqSearch(string sDir, string preReqName)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(sDir))
                {
                    foreach (var filename in Directory.GetFiles(directory))
                    {
                        if (Path.GetFileName(filename) == preReqName)
                        {
                            //if the file name of the found file within the folders/subdirs matches the one file we are searching for
                            /// the flag is set to true and we exit this level of recursion
                            flag = true;
                            break;
                        }
                    }
                    //due to the multi leveled recursion we must check to see if the bool flag has the value of true (or doesn't have the value of false)
                    //IF the flag is false then the recursion is called again to check the subdir that returned false
                    //if the flag is set to not false then the recursion escapes to the next level
                    if (flag == false)
                    {
                        preReqSearch(directory, preReqName);
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The network path was not found"))
                {
                    loggingClass.logEntryWriter(ex.Message, "error");

                    loggingClass.nLogLogger($"Unable to locate {preReqName} at {sDir}, please ensure the path is correct.", "debug");
                }
                else if (ex.Message.Contains("The UNC path should be of the form"))
                {
                    string logEntry = $"Please verify {sDir} it may be incorrect.";

                    loggingClass.logEntryWriter(logEntry, "debug");
                }
                else if (ex.Message.Contains("The user name or password is incorrect"))
                {
                    loggingClass.logEntryWriter("There was an error searching for" + preReqName + " over the network. Please verify your user can, or download pre reqs locally.", "error");

                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("The given path's format is not supported"))
                {
                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("Could not find a part of the path"))
                {
                    loggingClass.logEntryWriter(ex.Message, "error");

                    loggingClass.nLogLogger($"Unable to locate {sDir}, please ensure the path is correct.", "debug");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "info");

                    //loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), logEntry, "Automated Error Reported by " + Environment.UserName);
                }
            }

            //this code block is so that a band aide escape of the multi leveled recursion
            //if the bool has a value we want to maintain that value.
            //   False = 0 and not false = 1
            if (flag == false)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        #endregion pre req search/rename/copy

        #region local Working Dir manipulate

        //Temp file Creation, working storage folder Creation, Temp file cleaning on button click
        public void temp()
        {
            Directory.CreateDirectory(@"C:\Temp");

            Directory.CreateDirectory(configValues.preReqRun);
            Directory.CreateDirectory(configValues.nwsAddonLocalRun);
            Directory.CreateDirectory(configValues.clientRun);
        }

        //Deletes folders by path and recursively deletes sub folders
        public async void deleteDir(string dir)
        {
            try
            {
                Directory.Delete(dir, true);

                string logEntry = dir + " has been deleted.";

                loggingClass.logEntryWriter(logEntry, "info");
            }
            catch (IOException)
            {
                string logEntry = dir + " was not found and therefore could not deleted.";

                loggingClass.logEntryWriter(logEntry, "error");
            }
            catch (UnauthorizedAccessException ex)
            {
                string logEntry = $"User is unable to modify {dir} please run the Set Permissions triage or install step and try clearing folders again.";

                loggingClass.logEntryWriter(logEntry, "error");

                loggingClass.nLogLogger(ex.ToString(), "error");
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        public async void decompressionFinder(string sDir)
        {
            string[] compressedArray = { "SSCERuntime_x64-ENU.zip", "SSCERuntime_x86-ENU.zip" };

            foreach (var filename in Directory.GetFiles(sDir))
            {
                string[] path = filename.Split('\\');

                if (path.Last().Equals(compressedArray[0]) || path.Last().Equals(compressedArray[1]))
                {
                    await decompressAsync(filename, path.Last());
                }
            }

            preReqSearchCopy(@"C:\Temp\_Client-Installation\DecompressionHolder");
        }

        public async Task decompressAsync(string originalFilePath, string newFilePath)
        {
            string decompressionHolder = @"C:\Temp\_Client-Installation\DecompressionHolder";

            try
            {
                ZipFile.ExtractToDirectory(originalFilePath, Path.Combine(decompressionHolder, newFilePath));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "info");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        #endregion local Working Dir manipulate
    }
}