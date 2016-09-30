// Declare used namespaces instead of full qualification
using System;
using System.Data;
using System.Data.Odbc;

// Declare namespace the program is part of
// Must not match directory structure
namespace ma.metl.sirh.Model.Oracle {

  // Class name must not match file name
    public class GipeOrdConnexion
    {

    /*
     * Main program
    */
    public static void Main() {

        GipeOrdConnexion test = new GipeOrdConnexion();

        // Specify Data Source Name DSN along with user and password
        OdbcConnection connection = test.Connect("DSN=Connexion11g;UID=GIPEORD;PWD=GipeOrd");
        
     
        if (connection != null) {
            try {
                  //test.SelectTable("SELECT * FROM AGENT a WHERE a.COD_AG = '1986';", connection);
                test.SelectTable("SELECT SYSDATE FROM DUAL;", connection);
                }
            finally {
                // Close DB connection and free resources
                connection.Dispose();
            }
        }
    } // Main()

    /*
     * Connects to the specified ODBC source and returns the connection reference
     */
    protected OdbcConnection Connect(string connectString) {
     
        OdbcConnection connection = null;
        // Open a connection
        try {
        Console.WriteLine("Connect to " + connectString + " ...");
        connection = new OdbcConnection(connectString);
        connection.Open();
        }
        catch (OdbcException ex) {
        Console.WriteLine("\n" + ex.Message + "\n" + ex.StackTrace);
        return null;
        }
        return connection;

    } // Connect()

    /*
     * Single table select for demonstration purposes
    */
    protected void SelectTable(string selectStatement, OdbcConnection connection)
    {
        try
        {

            //Create a dataset
            DataSet dataSet = new DataSet();
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter();
            OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(dataAdapter);
            dataAdapter.SelectCommand = new OdbcCommand(selectStatement, connection);
            dataAdapter.Fill(dataSet);

            //Iterate through all the rows
            Console.WriteLine();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                foreach (DataColumn column in dataSet.Tables[0].Columns)
                {
                    Console.WriteLine(dataSet.Tables[0].Rows[i][column.ColumnName]);
                }
            }

            // Display the record count
            Console.WriteLine("\n{0} rows selected\n",
            dataSet.Tables[0].Rows.Count);
        }

        catch (OdbcException ex)
        {
            Console.WriteLine("\nSQL Statement:\n" + selectStatement + "\n" + ex.Message);
        }
    } // SelectTable()
  } // ODBCTest
} // namespace
