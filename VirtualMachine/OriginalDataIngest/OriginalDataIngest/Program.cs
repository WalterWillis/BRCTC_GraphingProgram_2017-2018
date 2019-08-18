using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseInjection
{
    class Program
    {
        /// <summary>
        /// Works, but isn't very efficient.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //string fileName = "June2018Data00";
            string path = @"C:\Users\Walter\Documents\Data\SplitFiles";
            DirectoryInfo directory = new DirectoryInfo(path);

            List<string> lines = new List<string>();
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
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


                //if (file.Name == fileName)
                lines = File.ReadAllLines(path + @"\" + file.Name).ToList(); // converts all lines to a list


                foreach (string line in lines)
                {
                    if (line.Contains("Supply")) // skip the first line
                        continue;
                    else
                    {
                        string[] data = line.Split(';').ToArray();
                        DataRow row = table.NewRow();
                        row[0] = float.Parse(data[0]);
                        row[1] = float.Parse(data[1]);
                        row[2] = float.Parse(data[2]);
                        row[3] = float.Parse(data[3]);
                        row[4] = int.Parse(data[4]);
                        table.Rows.Add(row);
                    }
                }


                string connectionString = "user id=DataMember;" +
                                          "password=IL0v3Data!;server=10.0.0.56;" +
                                          "Trusted_Connection=no;" +
                                          "database=Main; " +
                                          "connection timeout=2000";
                try
                {
                    SqlConnection myConnection = new SqlConnection(connectionString);
                    if (myConnection.State == ConnectionState.Closed)
                    {
                        myConnection.Open();
                    }
                    Console.WriteLine("It Worked!");


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

                    
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Failure!\n" + e.ToString());
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
