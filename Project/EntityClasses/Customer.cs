using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Customer
/// </summary>
public class Customer
{
    private int customerId;
    private string accountUsername;
    private string firstName;
    private string surname;
    private string homePhoneNumber;
    private string mobilePhoneNumber;
    private string emailAddress;

	public Customer()
	{
        this.customerId = 0;
        this.accountUsername = null;
        this.firstName = null;
        this.surname = null;
        this.homePhoneNumber = null;
        this.mobilePhoneNumber = null;
        this.emailAddress = null;
	}

    public Customer(int customerId, string accountUsername, string firstName, string surname, string homePhoneNumber, string mobilePhoneNumber, string emailAddress)
    {
        this.customerId = customerId;
        this.accountUsername = accountUsername;
        this.firstName = firstName;
        this.surname = surname;
        this.homePhoneNumber =  homePhoneNumber ;
        this.mobilePhoneNumber = mobilePhoneNumber;
        this.emailAddress = emailAddress;
    }

    public Customer(int customerId, string firstName, string surname, string homePhoneNumber, string mobilePhoneNumber, string emailAddress)
    {
        this.customerId = customerId;
        this.firstName = firstName;
        this.surname = surname;
        this.homePhoneNumber = homePhoneNumber;
        this.mobilePhoneNumber = mobilePhoneNumber;
        this.emailAddress = emailAddress;
    }

    public int getCustomerId()
    {
        return customerId;
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
    public string getHomePhoneNumber()
    {
        return homePhoneNumber;
    }
    public string getMobilePhoneNumber()
    {
        return mobilePhoneNumber;
    }
    public String getEmailAddress()
    {
        return emailAddress;
    }
    public void setCustomerId(int customerId)
    {
        this.customerId = customerId;
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
    public void setHomePhoneNumber(string homePhoneNumber)
    {
        this.homePhoneNumber = homePhoneNumber;
    }
    public void setMobilePhoneNumber(string mobilePhoneNumber)
    {
        this.mobilePhoneNumber = mobilePhoneNumber;
    }
    public void setEmailAddress(String emailAddress)
    {
        this.emailAddress = emailAddress;
    }

    public string CreateCustomer()
    {
        string createCustomerTable = "CREATE  TABLE  Customer ( "
                + "customerId INT PRIMARY KEY IDENTITY ,"
                + "accountUsername VARCHAR(45) NULL ,"
                + "firstName VARCHAR(20) NOT NULL ,"
                + "surname VARCHAR(30) NOT NULL ,"
                + "homePhoneNumber VARCHAR(15) NULL ,"
                + "mobilePhoneNumber VARCHAR(15 NULL ,"
                + "emailAddress VARCHAR(45) NULL )";
   
        return createCustomerTable;
    }

    public string DropCustomer()
    {
        string dropCustomerTable = "DROP TABLE Customer";

        return dropCustomerTable;
    }

    public string InsertCustomer()
    {
        string insertCustomer = "INSERT INTO Customer ("
            + "accountUsername,firstName,surname,homePhoneNumber,mobilePhoneNumber,emailAddress)"
            + "VALUES ('"
            + this.accountUsername + "','"
            + this.firstName + "','"
            + this.surname + "','"
            + this.homePhoneNumber + "','"
            + this.mobilePhoneNumber + "','"
            + this.emailAddress + "')";

        return insertCustomer;
    }

    public string InsertCustomerWithoutAccount()
    {
        string insertCustomer = "INSERT INTO Customer ("
            + "firstName,surname,homePhoneNumber,mobilePhoneNumber,emailAddress)"
            + "VALUES ('"
            + this.firstName + "','"
            + this.surname + "','"
            + this.homePhoneNumber + "','"
            + this.mobilePhoneNumber + "','"
            + this.emailAddress + "')";

        return insertCustomer;
    }

    public string InsertFK_Customer_AccountUsername()
    {
        string CreateBookingFK = "ALTER TABLE Customer ADD CONSTRAINT fk_accountUsername FOREIGN KEY (accountUsername) REFERENCES Account (username)";
        return CreateBookingFK;
    }

    public string DropFK_Customer_AccountUsername()
    {
        string DropBookingFK = "ALTER TABLE Customer DROP CONSTRAINT fk_accountUsername";
        return DropBookingFK;
    }

    public string update()
    {
        string updateString = "UPDATE Customer SET firstName='" + this.firstName 
                                                + "', surname='" + this.surname 
                                                + "', homePhoneNumber='" + this.homePhoneNumber 
                                                + "', mobilePhoneNumber='" + this.mobilePhoneNumber 
                                                + "', emailAddress='" + this.emailAddress 
                                                + "' WHERE (customerId=" + this.customerId + ")";
        return updateString;
    }

    public static Customer selectByCustomerUsername(string username, MySql connection)
    {
        
        string selectCustomer = "SELECT customerId, accountUsername, firstName, surname, homePhoneNumber, mobilePhoneNumber, emailAddress FROM Customer WHERE (accountUsername='" + username + "')";
        SqlDataReader customerFromDb = connection.Select(selectCustomer);

        if (customerFromDb != null)
        {
            if (customerFromDb.HasRows)
            {
                customerFromDb.Read();
                Customer retVal = new Customer(Convert.ToInt32(customerFromDb["customerId"]), customerFromDb["accountUsername"].ToString(), customerFromDb["firstName"].ToString(), customerFromDb["surname"].ToString(), customerFromDb["homePhoneNumber"].ToString(), customerFromDb["mobilePhoneNumber"].ToString(), customerFromDb["emailAddress"].ToString());
                customerFromDb.Close();
                return retVal;
            }
            customerFromDb.Close();
            return null;
        }
        return null;
    }

    public static Customer selectByCustomerId(int id, MySql connection)
    {
        string selectCustomer = "SELECT customerId, accountUsername, firstName, surname, homePhoneNumber, mobilePhoneNumber, emailAddress FROM Customer WHERE (customerId='" + id + "')";
        SqlDataReader customerFromDb = connection.Select(selectCustomer);

        if (customerFromDb != null)
        {
            if (customerFromDb.HasRows)
            {
                customerFromDb.Read();
                Customer retVal = new Customer(Convert.ToInt32(customerFromDb["customerId"]), customerFromDb["accountUsername"].ToString(), customerFromDb["firstName"].ToString(), customerFromDb["surname"].ToString(), customerFromDb["homePhoneNumber"].ToString(), customerFromDb["mobilePhoneNumber"].ToString(), customerFromDb["emailAddress"].ToString());
                customerFromDb.Close();
                return retVal;
            }
            customerFromDb.Close();
            return null;
        }
        return null;
    }

    public static Customer selectByAccountUsername(string accountUsername, MySql connection)
    {
        string selectCustomer = "SELECT customerId, accountUsername, firstName, surname, homePhoneNumber, mobilePhoneNumber, emailAddress FROM Customer WHERE (accountUsername='" + accountUsername + "')";
        SqlDataReader customerFromDb = connection.Select(selectCustomer);

        if (customerFromDb != null)
        {
            if (customerFromDb.HasRows)
            {
                customerFromDb.Read();
                Customer retVal = new Customer(Convert.ToInt32(customerFromDb["customerId"]), customerFromDb["accountUsername"].ToString(), customerFromDb["firstName"].ToString(), customerFromDb["surname"].ToString(), customerFromDb["homePhoneNumber"].ToString(), customerFromDb["mobilePhoneNumber"].ToString(), customerFromDb["emailAddress"].ToString());
                customerFromDb.Close();
                return retVal;
            }
            customerFromDb.Close();
            return null;
        }
        return null;
    }

    public static Customer[] selectAll(MySql connection)
    {
        string selectCustomer = "SELECT customerId, accountUsername, firstName, surname, homePhoneNumber, mobilePhoneNumber, emailAddress FROM Customer;";
        SqlDataReader customerFromDb = connection.Select(selectCustomer);
        
        List<Customer> customerList = new List<Customer>();
        int customerCount = 0;
        if (customerFromDb != null)
        {
            if (customerFromDb.HasRows)
            {
                while (customerFromDb.Read())
                {
                    customerList.Add(new Customer(Convert.ToInt32(customerFromDb["customerId"]), customerFromDb["accountUsername"].ToString(), customerFromDb["firstName"].ToString(), customerFromDb["surname"].ToString(), customerFromDb["homePhoneNumber"].ToString(), customerFromDb["mobilePhoneNumber"].ToString(), customerFromDb["emailAddress"].ToString()));
                    customerCount++;
                }

                Customer[] customers = new Customer[customerList.Count];
                customerList.CopyTo(customers);
                customerFromDb.Close();
                return customers;
            }
            customerFromDb.Close();
            return null;
        }
        return null;
    }

    public static Customer[] selectSearch(string firstName, string surname, string emailAddress, string vehicleRegistration, MySql connection)
    {
        string selectCustomer;

        if (vehicleRegistration != null && !vehicleRegistration.Equals(""))
        {
            selectCustomer = "SELECT customerId, accountUsername, firstName, surname, homePhoneNumber, mobilePhoneNumber, emailAddress FROM Customer WHERE customerId IN (SELECT customerId FROM Vehicle WHERE registrationNumber = '" + vehicleRegistration + "');";
        }
        else
        {
            selectCustomer = "SELECT customerId, accountUsername, firstName, surname, homePhoneNumber, mobilePhoneNumber, emailAddress FROM Customer WHERE ";
            bool sqlAnd = false;
            if (firstName != null && !firstName.Equals(""))
            {
                selectCustomer += " firstName = '" + firstName + "' ";
                sqlAnd = true;
            }
            if (surname != null && !surname.Equals(""))
            {
                if (sqlAnd)
                    selectCustomer += " AND ";

                selectCustomer += " surname = '" + surname + "' ";

                sqlAnd = true;
            }
            if (emailAddress != null && !emailAddress.Equals(""))
            {
                if (sqlAnd)
                    selectCustomer += " AND ";
                selectCustomer += " emailAddress = '" + emailAddress + "' ";

                sqlAnd = true;
            }
        }

        SqlDataReader customerFromDb = connection.Select(selectCustomer);

        List<Customer> customerList = new List<Customer>();
        int customerCount = 0;
        if (customerFromDb != null)
        {
            if (customerFromDb.HasRows)
            {
                while (customerFromDb.Read())
                {
                    customerList.Add(new Customer(Convert.ToInt32(customerFromDb["customerId"]), customerFromDb["accountUsername"].ToString(), customerFromDb["firstname"].ToString(), customerFromDb["surname"].ToString(), customerFromDb["homePhoneNumber"].ToString(), customerFromDb["mobilePhoneNumber"].ToString(), customerFromDb["emailAddress"].ToString()));
                    customerCount++;
                }

                Customer[] customers = new Customer[customerList.Count];
                customerList.CopyTo(customers);
                customerFromDb.Close();
                return customers;
            }
            customerFromDb.Close();
            return null;
        }
        return null;
    }

    public string delete()
    {
        return "DELETE FROM Customer WHERE (customerId = '" + this.customerId + "');";
    }

}