using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool verbose = false;
            Console.WriteLine("Smartsheet PO Importer v1");
            Console.WriteLine("Please make sure the PO excel file is in this same folder as this application");
            Console.WriteLine("Be sure to close the excel file if you have it open.");
            Console.WriteLine("Press any key to load the excel file.");
            string initialPrompt = Console.ReadLine().ToUpper();

            if(initialPrompt == "VERB")
            {
                verbose = true;
            }
            


            string[] filePaths = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "*.xls");
            if(filePaths.Count() == 0)
            {
                Console.WriteLine("No excel file detected. Please add the excel file to the directory and reload this application");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if(filePaths.Count() > 1)
            {
                Console.WriteLine("Multiple excel files detected.");
                for(var i = 0;i < filePaths.Count(); i++)
                {
                    Console.WriteLine($"File {i} - {filePaths[i].ToString()}");
                }
            }
            else
            {
                Console.WriteLine($"File Found: {filePaths[0].ToString()}");
                Console.WriteLine("If this is the correct file. Tap any key.");
                Console.ReadLine();

                ExcelReader excelReader = new ExcelReader(filePaths[0].ToString());
                excelReader.ReadExcelFile();
                if (verbose)
                {
                    DataHandler.WritePOToConsole();
                }
            }



            Console.WriteLine("Excel File loaded. Loading Data from smartsheet.");

            RestHandler.Init(verbose);

            Console.Clear();
            Console.WriteLine("Making Comparision");
            DataHandler.MakeComparison();

            Console.Clear();
            DataHandler.ReviewAllChanges();


            Console.WriteLine("Press any key to commit changes");
            Console.ReadLine();
            RestHandler.CommitUpdate();



            Console.ReadLine();


           
            

           


            
            


            

            
        }
    }
}
