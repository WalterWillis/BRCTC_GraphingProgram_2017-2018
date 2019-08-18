using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DataBaseInjection
{
    class Program
    {

        static FileInfo dataFile;
        static SortedList<Int64, string> lines;
      
        /// <summary>
        /// Much more efficient way to ingest data.
        /// Targets a specific file and then stream reads the data up to specific size before
        /// parsing and inserting the data into the database.
        /// The table must exist in the database before injection.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string fileName = "Sat Jun 16 19-00-16 2018_pihat.txt";
            string path = @"C:\Users\Walter\Documents\Data";
            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.GetFiles())
            {
                if (file.Name == fileName)
                    dataFile = file;
            }

            Int64 totalLines = 0;
            using (StreamReader dataStream = new StreamReader(dataFile.OpenRead()))
            {
                string line;
                while ((line = dataStream.ReadLine()) != null)
                {
                    totalLines++;
                }
            }
            Console.WriteLine("Total rows: " + totalLines);
           // timer.Start();
           // timer.AutoReset = true;

            using (StreamReader dataStream = new StreamReader(dataFile.OpenRead()))
            {
                lines = new SortedList<long, string>();
                string line;
                Int64 count = 0;
                Int64 lastCount = 0;
                while ((line = dataStream.ReadLine()) != null)
                {
                    lines.Add(count, line);
                    if (count >= 5000000)
                    {
                        lastCount += count;
                        Console.WriteLine("Current Progress: " + (float)lastCount / totalLines * 100 + "%.\nAmount: " + lastCount);
                        InsertLines();
                        lines = new SortedList<long, string>();
                
                        count = 0;
                    }
                    else
                    {
                        count++;
                    }
                }

                if (lastCount != count)
                {
                    InsertLines();
                }


            }
        }

        static void InsertLines()
        {

            //Console.WriteLine(lines.Count + " lines to copy!");

            int dataLoopSize = 100000;
            bool tblFinished = false;
            int lastDataElement = 0;

            while (!tblFinished)
            {
                DataColumn SupplyVolts = new DataColumn("SupplyVolts")
                {
                    DataType = typeof(float)
                };
                DataColumn PiezoX = new DataColumn("PiezoX")
                {
                    DataType = typeof(float)
                };
                DataColumn PiezoY = new DataColumn("PiezoY")
                {
                    DataType = typeof(float)
                };
                DataColumn PiezoZ = new DataColumn("PiezoZ")
                {
                    DataType = typeof(float)
                };
                DataColumn Seconds = new DataColumn("Seconds")
                {
                    DataType = typeof(int)
                };

                DataTable table = new DataTable("Main");
                table.Columns.Add(SupplyVolts);
                table.Columns.Add(PiezoX);
                table.Columns.Add(PiezoY);
                table.Columns.Add(PiezoZ);
                table.Columns.Add(Seconds);


                foreach (var line in lines)
                {
                    if (line.Value.Contains("Supply"))
                    { // skip the first line
                        lastDataElement++;
                        continue;
                    }
                    else if (table.Rows.Count > dataLoopSize)
                    {
                        break;
                    }
                    else
                    {
                        string[] data = line.Value.Split(';').ToArray();
                        DataRow row = table.NewRow();
                        row[0] = float.Parse(data[0]);
                        row[1] = float.Parse(data[1]);
                        row[2] = float.Parse(data[2]);
                        row[3] = float.Parse(data[3]);
                        row[4] = int.Parse(data[4]);
                        table.Rows.Add(row);
                        lastDataElement++;
                    }
                }
                if (lastDataElement >= lines.Count)
                    tblFinished = true;

                Console.WriteLine("\tCurrent Inner Progress: " + (float)lastDataElement / lines.Count * 100 + "%");

                string connectionString = "user id=DataMember;" +
                                          "password=IL0v3Data!;server=10.0.0.180;" +
                                          "Trusted_Connection=no;" +
                                          "database=Main; " +
                                          "connection timeout=2000";
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                {
                    try
                    {

                        if (myConnection.State == ConnectionState.Closed)
                        {
                            myConnection.Open();
                        }
                        //Console.WriteLine("It Worked!");


                        SqlBulkCopy bulkCopy = new SqlBulkCopy(myConnection);
                        // column mappings
                        bulkCopy.ColumnMappings.Add("SupplyVolts", "SupplyVolts");
                        bulkCopy.ColumnMappings.Add("PiezoX", "PiezoX");
                        bulkCopy.ColumnMappings.Add("PiezoY", "PiezoY");
                        bulkCopy.ColumnMappings.Add("PiezoZ", "PiezoZ");
                        bulkCopy.ColumnMappings.Add("Seconds", "Seconds");
                        bulkCopy.DestinationTableName = "[Main].[dbo].[June21LaunchData]";
                        bulkCopy.WriteToServer(table);


                        //SqlCommand sqlComm = new SqlCommand("InsertImport", myConnection);

                        //sqlComm.Parameters.AddWithValue("@Data", table);

                        //sqlComm.CommandType = CommandType.StoredProcedure;

                        //sqlComm.ExecuteNonQuery();


                        //Console.WriteLine("Copied " + lastDataElement + " rows so far!");
                        // Console.ReadKey();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Failure!\n" + e.ToString());
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
