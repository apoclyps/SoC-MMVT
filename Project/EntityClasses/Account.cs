using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Account
/// </summary>
public class Account
{
    private int accountId;
    private string username;
    private string password;
    private string type;

    // Constructors
	public Account()
	{
        accountId = 0;
        username = null;
        password = null;
        type = null;
	}

    public Account(int accountId ,string username, string password, string type)
    {
        this.accountId = accountId;
        this.username = username;
        this.password = password;
        this.type = type;
    }

    public Account(string username, string password, string type)
    {
        this.username = username;
        this.password = password;
        this.type = type;
    }


    // SETTERS
    public void setAccountId(int accountId)
    {
        this.accountId = accountId;
    }

    public void setusername(string username)
    {
        this.username = username;
    }

    public void setpassword(string password)
    {
        this.password = password;
    }

    public void settype(string type)
    {
        this.type = type;
    }

    // GETTERS

    public int getAccountId() {
		return accountId;
	}
    public string getusername()
    {
        return this.username;
    }

    public string getpassword()
    {
        return this.password;
    }

    public string gettype()
    {
        return this.type;
    }

    public string CreateAccount()
    {
        string createAccountTable = "CREATE  TABLE  Account ( "
            + "accountId INT PRIMARY KEY IDENTITY,"
             + "username VARCHAR(45) NOT NULL UNIQUE,"
             + "password VARCHAR(512) NOT NULL ,"
             + "type VARCHAR(8) NOT NULL )";
    
        return createAccountTable;
    }

    public string DropAccount()
    {
        string dropAccountTable = "DROP TABLE Account";
        return dropAccountTable;
    }

    public string InsertAccount()
    {
        string insertAccount = "INSERT INTO Account ("
            + "username,password,type)"
            + "VALUES ('"
            + this.username + "','"
            + hashPassword(this.password) + "','"
            + this.type + "')";

        return insertAccount;
    }

    // Overloaded method to take in parameters and insert directly into database
    public string InsertAccount(string username, string password, string type)
    {
        string insertAccount = "INSERT INTO Account ("
            + "username,password,type)"
            + "VALUES ('"
            + username + "','"
            + hashPassword(password) + "','"
            + type + "')";

        return insertAccount;
    }

    public static Account selectByUsername(string username, MySql connection)
    {
        string selectAccount = "SELECT accountId, username, password, type FROM Account WHERE (username='" + username + "')";
        SqlDataReader accountFromDb = connection.Select(selectAccount);

        if (accountFromDb != null)
        {
            if (accountFromDb.HasRows)
            {
                accountFromDb.Read();
                Account retVal = new Account(Convert.ToInt32(accountFromDb["accountId"]), accountFromDb["username"].ToString(), accountFromDb["password"].ToString(), accountFromDb["type"].ToString());
                accountFromDb.Close();
                return retVal;
            }
            accountFromDb.Close();
            return null;
        }
        return null;
    }

    // checks the given password matches the password stored in member fields
    public bool checkPassword(string password)
    {
        string hashedInput = hashPassword(password);
        if (hashedInput.Equals(this.password))
            return true;
        else
            return false;
    }

    // Searches the table to check if the username is already used and returns true or false
    public Boolean findValidUserName()
    {

        // Search table and find if name is used
       // if name in table then exit and false

        return false;
    }

    public string updatePassword()
    {
        return "Update Account SET password='" + hashPassword(password) + "';" ;
    }

    /*
     * Createa a hashed version of the pasword string and returns it.
     * 
     * Argument 1: string - password to hash
     * Returns: string - hashed version of argument 1
     */
    public string hashPassword(string myPassword)
    {
        SHA512 encrypter = SHA512.Create();
        byte[] hashedBytes = encrypter.ComputeHash(Encoding.ASCII.GetBytes(myPassword));
        string hashedPassword = Convert.ToBase64String(hashedBytes);

        return hashedPassword;

    }

    public string InsertIndex_Account_accountId()
    {
        string InsertIndex = "CREATE UNIQUE INDEX INDEX_Account_Username ON Account (username)";
        return InsertIndex;
    }

    public string DropIndex_Account_accountId()
    {
        string InsertIndex = "DROP INDEX Account.INDEX_Account_Username";
        return InsertIndex;
    }

}