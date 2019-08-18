/* In order to get Unity to use System.Data and System.Data.SqlClient, I had to import the System.Data library located in
 * %ProgramFiles(x86)%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.
 * 
 * This uses version 4.6 of .Net. Since the current version of Unity, the latest 2017 version, uses .Net 3.5, I had to enable the experimental 4.6
 * compiler. I did this by going File -> Build Setttings -> PlayerSettings -> Other Settings -> Api Compatibility Level
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class Database{

    private string connectionString = "user id=DataMember;" +
                                      "password=IL0v3Data!;server=192.168.1.10;" +
                                      "Trusted_Connection=no;" +
                                      "database=Main; " +
                                      "connection timeout=20";
 public DataTable GetTable(string tableName, string columnName, string idColumnName, int startIndex, int endIndex, string orderBy)
    {
        DataTable table = new DataTable();
        try
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if(myConnection.State == ConnectionState.Closed)
            {
                myConnection.Open();
            }
          
            SqlCommand cmd = new SqlCommand("GetSegment", myConnection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tableName", tableName);
            cmd.Parameters.AddWithValue("@targetColumnName", columnName);
            cmd.Parameters.AddWithValue("@whereColumnModifier", idColumnName);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@endIndex", endIndex);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(table);

            if(table.Rows.Count > 0)
            {
                Debug.Log("We got " + table.Rows.Count + " rows of data!");
            }
            else
            {
                Debug.Log("Results failure: No data!");
            }

        }
        catch(SqlException ex)
        {
            Debug.Log(ex);
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("Connection Failed! Reverting to dummy data.");
            table.Columns.Add(columnName, typeof(string));

            for (int i = 1; i <= 10000; i++)
            {
                int num;
                if (i % 2 == 0)
                    num = 2;
                else
                    num = 0;
                table.Rows.Add(num.ToString());
            }
        }
        return table;
    }
    
}
