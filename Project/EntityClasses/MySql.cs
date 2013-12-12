using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MySql
/// </summary>
public class MySql
{
    private String Connection, User, Password, Database;
    private String ConnectionString;
    private SqlConnection MySQLConnection;

    // Constructor
	public MySql()
	{
        this.Connection = null;
        this.User = null;
        this.Password = null;
        this.Database = null;

        ConnectionString = ConfigurationManager.ConnectionStrings["MainConnStr"].ToString();
        this.MySQLConnection = new SqlConnection(getConnectionString());
	}
     
    public MySql(string Connection, String User, String Password)
    {
        this.Connection = Connection;
        this.User = User;
        this.Password = Password;
        this.Database = null;
        this.MySQLConnection = new SqlConnection(getConnectionString());
    }

    public MySql(string Connection, String User, String Password, String Database)
    {
        this.Connection = Connection;
        this.User = User;
        this.Password = Password;
        this.Database = Database;
        this.MySQLConnection = new SqlConnection(getConnectionString());
    }



    public void CreateConnection()
    {
        this.MySQLConnection = new SqlConnection(getConnectionString());
    }

    public string buildConnectionString()
    {
       // NEEDS CODED
        this.ConnectionString = Connection + User + Password + Database;

        return ConnectionString;
    }


    public void OpenConnection()
    {
        try
        {
            MySQLConnection.Open();
        }
        catch (Exception)
        {
            MySQLConnection.Close();
            
        }
        Console.WriteLine("Connection Opened");
    }

    public void CloseConnection()
    {
        MySQLConnection.Close();
        Console.WriteLine("Connection Closed");
    }


    // Sets connection string from web.config file
    public void setConnectionString()
    {
        ConnectionString = ConfigurationManager.ConnectionStrings["MainConnStr"].ToString();
    }

    // Sets connection string from string parameter passed to method
    public void setConnectionString(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    // Getters
    public String getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["MainConnStr"].ToString();
    }

  

 
    // Table Methods

    public SqlDataReader Select(string selectStatement)
    {
        if (this.MySQLConnection.State == System.Data.ConnectionState.Open)
        {
            try
            {
                SqlCommand selectCmd = new SqlCommand(selectStatement, MySQLConnection);
                SqlDataReader results = selectCmd.ExecuteReader();
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MySQLConnection.Close();
                return null;
            }
        }
        return null;
    }

    // returns insert ID of row inserted or null on error
    public string Insert(string insertStatement)
    {
        if (this.MySQLConnection.State == System.Data.ConnectionState.Open)
        {
            try
            {
                //create command
                SqlCommand myCommand = new SqlCommand(insertStatement + "; SELECT SCOPE_IDENTITY() AS insert_id;", MySQLConnection);

                //send command
                SqlDataReader myResults = myCommand.ExecuteReader();
                myResults.Read();

                string insertId = myResults["insert_id"].ToString();
                myResults.Close();
                return insertId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MySQLConnection.Close();
                return null;
            }
        }
        return null;
    }

    // returns number of rows affected
    public int Update(string updateStatement)
    {
        if (this.MySQLConnection.State == System.Data.ConnectionState.Open)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(updateStatement, MySQLConnection);
                return myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MySQLConnection.Close();
                return 0;
            }
        }
        return 0;
    }

    // returns number of rows affected
    public int Delete(string deleteStatement)
    {
        if (this.MySQLConnection.State == System.Data.ConnectionState.Open)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(deleteStatement, MySQLConnection);
                return myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MySQLConnection.Close();
                return 0;
            }
        }
        return 0;
    }

    public void CreateTable()
    {

    }


    public SqlConnection getConnection()
    {
        return this.MySQLConnection;
    }

    public static void createAllTables()
    {

    }

    public static void dropAllTableS()
    {

    }

    

 
}