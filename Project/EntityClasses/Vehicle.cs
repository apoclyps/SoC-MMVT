using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Vehicle
/// </summary>
public class Vehicle
{
    private string registrationNumber;
    private int customerId;
    private int modelId;
    private int mileage;
    private string model;
    private string manufacturer;
    private DateTime year;
    private Boolean modified; 

	public Vehicle()
	{
        this.registrationNumber = null;
        this.customerId = 0;
        this.modelId = 0;
        this.mileage = 0;
        this.year = new DateTime();
        this.modified = false;
	}

    public Vehicle(String registrationNumber, int customerId, int modelId, int mileage, DateTime year, Boolean modified)
    {
		this.registrationNumber = registrationNumber;
        this.customerId = customerId;
		this.modelId = modelId;
		this.mileage = mileage;
		this.year = year;
		this.modified = modified;
	}

    public Vehicle(String registrationNumber, int customerId, string model, string manufacturer, int mileage, DateTime year, Boolean modified)
    {
        this.registrationNumber = registrationNumber;
        this.customerId = customerId;
        this.model = model;
        this.manufacturer = manufacturer;
        this.mileage = mileage;
        this.year = year;
        this.modified = modified;
    }
    
	public String getRegistrationNumber() {
		return registrationNumber;
	}
	public int getCustomerId() {
        return customerId;
	}
	public int getModelId() {
		return modelId;
	}
    public string getModel()
    {
        return model;
    }
    public string getManufacturer()
    {
        return manufacturer;
    }
	public int getMileage() {
		return mileage;
	}
	public DateTime getYear() {
		return year;
	}
	public Boolean getModified() {
		return modified;
	}
	public void setRegistrationNumber(String registrationNumber) {
		this.registrationNumber = registrationNumber;
	}
    public void setCustomerId(int customerId)
    {
        this.customerId = customerId;
	}
	public void setModelId(int modelId) {
		this.modelId = modelId;
	}
    public void setModel(string model)
    {
        this.model = model;
    }
    public void setManufacturer(string manufacturer)
    {
        this.manufacturer = manufacturer;
    }
	public void setMileage(int mileage) {
		this.mileage = mileage;
	}
	public void setYear(DateTime year) {
		this.year = year;
	}
	public void setModified(Boolean modified) {
		this.modified = modified;
	}

    public string CreateVehicle()
    {
        string createVehicleTable = "CREATE  TABLE  Vehicle ( "
             + "registrationNumber VARCHAR(10) NOT NULL ,"
             + "customerId INT NOT NULL ,"
             + "modelId INT NOT NULL ,"
             + "mileage INT NOT NULL ,"
             + "year DATETIME NOT NULL ,"
             + "modified INT NULL ,"
             + "PRIMARY KEY (registrationNumber))";

        return createVehicleTable;
    }

    public string DropVehicle()
    {
        string dropVehicleTable = "DROP TABLE Vehicle";

        return dropVehicleTable;
    }

    public string insertSingleVehicle()
    {
       string insertVehicle = "INSERT INTO vehicle ("
             + "registrationNumber,customerId,modelId,mileage,year,modified)"
             + "VALUES ('"
             + this.registrationNumber + "','"
             + this.customerId + "','"
             + this.mileage + "','"
             + this.modelId + "','"
             + this.year.ToShortDateString() + "','"
             + Convert.ToInt32(this.modified) + "')";

        return insertVehicle;
    }

    public string InsertVehicle()
    {
        string insertVehicle = "";
       /* if (model != null && !model.Equals(""))
        {
            insertVehicle = "DECLARE @MOD_ID int; ";
            insertVehicle += "SET @MOD_ID = (SELECT modelId FROM Model WHERE description = '" + this.model + "'); ";
            insertVehicle += "INSERT INTO vehicle (registrationNumber,customerId,modelId,mileage,year,modified) ";
            insertVehicle += "VALUES ('" + this.registrationNumber 
                                        + "', '" + this.customerId 
                                        + "', @MOD_ID, '" + this.mileage 
                                        + "','" + this.year.ToShortDateString()
                                        + "','" + Convert.ToInt32(this.modified) + "'); ";   
        }
        else
        { */
            insertVehicle = "INSERT INTO vehicle ("
             + "registrationNumber,customerId,modelId,mileage,year,modified)"
             + "VALUES ('"
             + this.registrationNumber + "','"
             + this.customerId + "','"
             + this.modelId + "','"
             + this.mileage + "','"
             + this.year.ToShortDateString() + "','"
             + Convert.ToInt32(this.modified) + "')";
        //}

        return insertVehicle;
    }

    public string InsertFKVehicleRegistrationNumber()
    {
        string CreateBookingFK = "ALTER TABLE Vehicle ADD CONSTRAINT fk_Vehicle_customerId FOREIGN KEY (customerId) REFERENCES Customer(customerId)";
        return CreateBookingFK;
    }
    
    public string DropFKVehicleRegistrationNumber()
    {
        string DropBookingFK = "ALTER TABLE Vehicle DROP CONSTRAINT fk_Vehicle_customerId";
        return DropBookingFK;
    }



    public static Vehicle selectByRegistrationNumber(string registrationNumber, MySql connection)
    {
        string selectVehicle = "SELECT v.registrationNumber, v.customerId, v.mileage, v.year, v.modified, mo.description AS model, mu.description AS manufacturer FROM Vehicle AS v INNER JOIN Model AS mo ON v.modelId = mo.modelId INNER JOIN Manufacturer AS mu ON mo.manufacturerId = mu.manufacturerId WHERE registrationNumber = '" + registrationNumber + "';";
        SqlDataReader objectFromDB = connection.Select(selectVehicle);

        if (objectFromDB != null)
        {
            if (objectFromDB.HasRows)
            {
                objectFromDB.Read();
                Vehicle returnObject = new Vehicle(objectFromDB["registrationNumber"].ToString(),
                                   Convert.ToInt32(objectFromDB["customerId"]),
                                   objectFromDB["model"].ToString(),
                                   objectFromDB["manufacturer"].ToString(),
                                   Convert.ToInt32(objectFromDB["mileage"]),
                                   Convert.ToDateTime(objectFromDB["year"]),
                                   Convert.ToBoolean(objectFromDB["modified"]));
                objectFromDB.Close();
                return returnObject;
            }
            objectFromDB.Close();
            return null;
        }
        return null;
    }

    public static Vehicle[] selectByCustomerId(int customerId, MySql connection)
    {
        string selectVehicles = "SELECT v.registrationNumber, v.mileage, v.year, v.modified, mo.description AS model, mu.description AS manufacturer FROM Vehicle AS v INNER JOIN Model AS mo ON v.modelId = mo.modelId INNER JOIN Manufacturer AS mu ON mo.manufacturerId = mu.manufacturerId WHERE customerId = '" + customerId + "';";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<Vehicle> objectList = new List<Vehicle>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new Vehicle(objectsFromDB["registrationNumber"].ToString(),
                                   customerId,
                                   objectsFromDB["model"].ToString(),
                                   objectsFromDB["manufacturer"].ToString(),
                                   Convert.ToInt32(objectsFromDB["mileage"]),
                                   Convert.ToDateTime(objectsFromDB["year"]),
                                   Convert.ToBoolean(objectsFromDB["modified"])));
                    objectCount++;
                }

                Vehicle[] returnValue = new Vehicle[objectList.Count];
                objectList.CopyTo(returnValue);
                objectsFromDB.Close();
                return returnValue;
            }
            objectsFromDB.Close();
            return null;
        }
        return null;
    }

    public string delete()
    {
        return "DELETE FROM Vehicle WHERE (registrationNumber = '" + this.registrationNumber + "');";
    }

    public string update()
    {

        string updateString;

        if (!model.Equals(""))
        {
            updateString = "DECLARE @MOD_ID int; ";
            updateString += "SET @MOD_ID = (SELECT modelId FROM Model WHERE description = '" + this.model + "'); ";
            updateString += "Update Vehicle SET modelId=@MOD_ID, mileage='" + this.mileage 
                                                                + "', year='" + this.year.ToShortDateString() 
                                                                + "', modified='" + Convert.ToInt32(this.modified) 
                                                                + "' WHERE (registrationNumber='" + this.registrationNumber + "');";
        }
        else
        {
           updateString = "Update Vehicle SET modelId='" + this.modelId 
                                          + "', mileage='" + this.mileage 
                                          + "', year='" + this.year.ToShortDateString() 
                                          + "', modified='" + Convert.ToInt32(this.modified) 
                                          + "' WHERE (registrationNumber='"
                                          + this.registrationNumber + "');";
        }

        return updateString;
    }
}