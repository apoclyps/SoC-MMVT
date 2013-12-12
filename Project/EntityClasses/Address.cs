using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


/// <summary>
/// Summary description for Address
/// </summary>
public class Address
{
    private int customerId;
    private string addressLine1;
    private string addressLine2;
    private string town;
    private string postcode;

    // Constructors
	public Address()
	{
        this.customerId = 0;
        this.addressLine1 = null;
        this.addressLine2 = null;
        this.town = null;
        this.postcode = null;
    }

    public Address(int customerid, string addressLine1, string addressLine2, string town, string postcode)
    {
        this.customerId = customerid;
        this.addressLine1 = addressLine1;
        this.addressLine2 = addressLine2;
        this.town = town;
        this.postcode = postcode;
    }

    // setters

    public void setCustomerId(int customerId)
    {
        this.customerId = customerId;
    }

    public void setAddressLine1(string addressLine1)
    {
        this.addressLine1 = addressLine1;
    }

    public void setAddressLine2(string addressLine2)
    {
        this.addressLine2 = addressLine2;
    }

    public void setTown(string town)
    {
        this.town = town;
    }

    public void setPostcode(string postcode)
    {
        this.postcode = postcode;
    }


    // Getters

    public int getCustomerId()
    {
        return this.customerId;
    }

    public string getAddressLine()
    {
        return this.addressLine1;
    }

    public string getAddressLine2()
    {
        return this.addressLine2;
    }

    public string getTown()
    {
        return this.town;
    }

    public string getPostcode()
    {
        return this.postcode;
    }

    // Statements

    public string CreateAddress()
    {
        string createAddressTable = "CREATE  TABLE  Address ( "
                + "customerId INT NOT NULL ,"
                + "addressLine1 VARCHAR(70) NULL ,"
                + "addressLine2 VARCHAR(70) NULL ,"
                + "town VARCHAR(45) NULL ,"
                + "postCode VARCHAR(10) NULL)";

        return createAddressTable;
    }

    public string DropAddress()
    {
        string dropAddressTable = "DROP TABLE Address";

        return dropAddressTable;
    }
    
    public string InsertAddress()
    {
        string insertAddress = "INSERT INTO Address ("
            + "customerId,addressLine1,addressLine2,town,postCode)"
            + "VALUES ('"
            + this.customerId + "','"
            + this.addressLine1 + "','"
            + this.addressLine2 + "','"
            + this.town + "','"
            + this.postcode + "')";

        return insertAddress;
    }

    public string InsertFK_Address_customerID()
    {
        string CreateBookingFK = "ALTER TABLE Address ADD CONSTRAINT fk_Address_customerId FOREIGN KEY (customerId) REFERENCES Customer (customerId)";
        return CreateBookingFK;
    }

    public string DropFK_Address_customerID()
    {
        string DropBookingFK = "ALTER TABLE Address DROP CONSTRAINT fk_Address_customerId";
        return DropBookingFK;
    }


    public static Address selectByCustomerId(int id, MySql connection)
    {
        string selectAddress = "SELECT customerId, addressLine1, addressLine2, town, postCode FROM Address WHERE (customerId='" + id + "')";
        SqlDataReader addressFromDb = connection.Select(selectAddress);

        if (addressFromDb != null)
        {
            if (addressFromDb.HasRows)
            {
                addressFromDb.Read();
                Address retVal = new Address(Convert.ToInt32(addressFromDb["customerId"]), addressFromDb["addressLine1"].ToString(), addressFromDb["addressLine2"].ToString(), addressFromDb["town"].ToString(), addressFromDb["postCode"].ToString());
                addressFromDb.Close();
                return retVal;
            }
            addressFromDb.Close();
            return null;
        }
        return null;
    }

    public string update()
    {
        string updateString = "UPDATE Address SET addressLine1='" + this.addressLine1
                                                + "', addressLine2='" + this.addressLine2
                                                + "', town='" + this.town
                                                + "', postCode='" + this.postcode
                                                + "' WHERE (customerId=" + this.customerId + ");";

        return updateString;
    }

    public string delete()
    {
        return "DELETE FROM Address WHERE (customerId = '" + this.customerId + "');";
    }
}