using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Staff
/// </summary>
public class Staff
{
    private string accountUsername;
    private string firstName;
    private string surname;

	public Staff()
	{
        this.accountUsername = null;
        this.firstName = null;
        this.surname = null;
	}

    public Staff(String accountUsername, String firstName, String surname)
    {
        this.accountUsername = accountUsername;
        this.firstName = firstName;
        this.surname = surname;
    }

    public String getAccountUsername()
    {
        return accountUsername;
    }
    public String getFirstName()
    {
        return firstName;
    }
    public String getSurname()
    {
        return surname;
    }
    public void setAccountUsername(String accountUsername)
    {
        this.accountUsername = accountUsername;
    }
    public void setFirstName(String firstName)
    {
        this.firstName = firstName;
    }
    public void setSurname(String surname)
    {
        this.surname = surname;
    }

    public string CreateStaff()
    {
        string CreateStaff = "CREATE  TABLE  Staff ( "
             + "accountUsername VARCHAR(45) PRIMARY KEY ,"
             + "firstName VARCHAR(20) NOT NULL ,"
             + "surname VARCHAR(30) NOT NULL )";
        return CreateStaff;
    }

    public string DropStaff()
    {
        string dropStaff = "DROP TABLE Staff";

        return dropStaff;
    }

    public string InsertStaff()
    {
        string insertStaff = "INSERT INTO Staff ("
            + "accountUsername,firstName,surname)"
            + "VALUES ('"
            + this.accountUsername + "','"
            + this.firstName + "','"
            + this.surname + "')";

        return insertStaff;
    }

    /// <summary>
    /// Returns a staff object from a search based on the username of that staff member
    /// </summary>
    public static Staff selectByUsername(string username, MySql connection)
    {
        string selectStaff = "SELECT accountUsername, firstName, surname FROM Staff WHERE (accountUsername='" + username + "')";
        SqlDataReader staffFromDb = connection.Select(selectStaff);

        if (staffFromDb != null)
        {
            if (staffFromDb.HasRows)
            {
                staffFromDb.Read();
                return new Staff(staffFromDb["accountUsername"].ToString(), staffFromDb["firstName"].ToString(), staffFromDb["surname"].ToString());
            }
            return null;
        }
        return null;
    }


    
}