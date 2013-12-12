using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Manufacturer
/// </summary>
public class Manufacturer
{
    private int manufacturerId;
    private String description;

	public Manufacturer()
	{
        this.manufacturerId = 0;
        this.description = null;
	}

    public Manufacturer(int manufacturerId, String description) 
    {
		this.manufacturerId = manufacturerId;
		this.description = description;
	}

	public int getManufacturerId() {
		return manufacturerId;
	}
    public String getDescription()
    {
		return description;
	}
	public void setManufacturerId(int manufacturerId) {
		this.manufacturerId = manufacturerId;
	}
    public void setDescription(String description)
    {
		this.description = description;
	}

    public string CreateManufacturer()
    {
        string CreateManufacturer = "CREATE  TABLE  Manufacturer ( "
             + "manufacturerId INT PRIMARY KEY IDENTITY,"
             + "description VARCHAR(25) NOT NULL )";

        return CreateManufacturer;
    }

    public string DropManufacturer()
    {
        string dropManufacturerTable = "DROP TABLE Manufacturer";

        return dropManufacturerTable;
    }

    public string InsertManufacturer()
    {
        string insertManufacturer = "INSERT INTO Manufacturer ("
            + "description)"
            + "VALUES ('"
            + this.description + "')";

        return insertManufacturer;
    }

    public static Manufacturer selectByManufacturerId(int manufacturerId, MySql connection)
    {
        string selectStatement = "SELECT * FROM Manufacturer WHERE (manufacturerId='" + manufacturerId + "')";
        SqlDataReader objectFromDatabase = connection.Select(selectStatement);

        if (objectFromDatabase != null)
        {
            if (objectFromDatabase.HasRows)
            {
                objectFromDatabase.Read();
                Manufacturer returnObject = new Manufacturer(Convert.ToInt32(objectFromDatabase["manufacturerId"]),
                                    objectFromDatabase["description"].ToString());
                objectFromDatabase.Close();
                return returnObject;
            }
            objectFromDatabase.Close();
            return null;
        }
        return null;
    }

}