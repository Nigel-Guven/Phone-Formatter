using System;
using System.IO;
using System.Text.RegularExpressions;


namespace PhoneFormatterProject
{
    class Program
    {
        // Format Mobile Number -- 
        //  861234567 becomes +353861234567 
        // 0861234567 becomes +353861234567
        static string FormatIrishMobile(string number)
        {
            if(number.Length == 10)
            {
                return "+353" + number.Substring(1);
            }
            else if(number.Length == 9)
            {
                return number.Replace(number, "+353" + number);
            }
            else
                return "";
        }

        static void Main(string[] args)
        {
            string path = "";

            try
            {
                if(args.Length==0)
                {
                    // Filename is retrieved from variable
                    string filename = "implement_user-centric_paradigms.csv";
                    path = @".\\csv\" + filename;
                }
                else if(args.Length==1)
                {
                    // If one argument has been provided, run specified name
                    path = @".\\csv\" + args[0];
                }
                
                

                using (StreamReader csv_reader = new StreamReader(path))
                {
                    //Match only digits
                    Regex numbers_only_regex = new Regex("^[0-9]+$");

                    //Change filename
                    string updated_filename = path.Substring(7).Replace(".csv", "") + "_updated";
                    Console.WriteLine("\nFile will be updated as: " + updated_filename + ".csv\n");
                    
                    //Scan the CSV File
                    while(csv_reader.Peek() != -1)
                    {
                        string user = csv_reader.ReadLine();
                        string [] user_data = user.Split(",");

                        //iterate through a single users data
                        for(int i = 0;i < user_data.Length; i++) 
                        {
                            //Match only phone numbers
                            if (numbers_only_regex.IsMatch(user_data[i]))
                            { 
                                //If number contains white spaces, remove these
                                user_data[i] = user_data[i].Replace(" ", string.Empty);

                                //Format to International Standard for Irish Numbers
                                user = user.Replace(user_data[i], FormatIrishMobile(user_data[i]));
                            }

                        }
                        //Append Updated Data to new file
                        File.AppendAllText(@".\\csv\" + updated_filename + ".csv", user + "\n");              
                    }

                }
            }
            catch(FileNotFoundException exception)
            {
                Console.WriteLine("\nFile not found in path: \n" + exception);
            }
            catch(ArgumentOutOfRangeException exception)
            {
                Console.WriteLine("\nArgument Parameter is out of range: \n" + exception);
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine("\nIllegal Path Name found in arguments: \n" + exception);
            }
            catch(DirectoryNotFoundException exception)
            {
                Console.WriteLine("\nFilePath Links to a directory which does not exist: \n" + exception);
            }
        }
    }
}
