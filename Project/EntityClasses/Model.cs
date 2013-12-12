using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Model
/// </summary>
public class Model
{
    private int modelId;
    private int manufacturerId;
    private string description;
    
	public Model()
	{
        this.modelId = 0;
        this.manufacturerId = 0;
        this.description = null;
	}

    public Model(int modelId, int manufacturerId, String description)
    {
        this.modelId = modelId;
        this.manufacturerId = manufacturerId;
        this.description = description;
    }

    public int getModelId()
    {
        return modelId;
    }
    public int getManufacturerId()
    {
        return manufacturerId;
    }
    public String getDescription()
    {
        return description;
    }
    public void setModelId(int modelId)
    {
        this.modelId = modelId;
    }
    public void setManufacturerId(int manufacturerId)
    {
        this.manufacturerId = manufacturerId;
    }
    public void setDescription(String description)
    {
        this.description = description;
    }


    public string CreateModel()
    {
        string CreateModel = "CREATE  TABLE  Model ( "
             + "modelId INT PRIMARY KEY IDENTITY,"
             + "manufacturerId INT NOT NULL,"
             + "description VARCHAR(25) NOT NULL )";
        return CreateModel;
    }

    public string DropModel()    {      
        string dropModelTable = "DROP TABLE Model";
        return dropModelTable;
    }

    public string InsertModel()    {
        string insertModel = "INSERT INTO Model ("
            + "manufacturerId,description)"
            + "VALUES ('"
            + this.manufacturerId + "','"
            + this.description + "')";
        return insertModel;
    }

    public string InsertFK_Model_bookingId()   {
        string InsertFKJob = "ALTER TABLE Model ADD CONSTRAINT fk_manufacturerId FOREIGN KEY(manufacturerId) REFERENCES Manufacturer(manufacturerId)";
        return InsertFKJob;
    }

    public string DropFK_Model_bookingId()    {
        string DropFKJob = "ALTER TABLE Model DROP CONSTRAINT fk_manufacturerId";
        return DropFKJob;
    }

    public static Model selectByModelId(int modelId, MySql connection)
    {
        string selectStatement = "SELECT * FROM Model WHERE (modelId='" + modelId + "')";
        SqlDataReader objectFromDatabase = connection.Select(selectStatement);

        if (objectFromDatabase != null)
        {
            if (objectFromDatabase.HasRows)
            {
                objectFromDatabase.Read();
                Model returnObject = new Model(Convert.ToInt32(objectFromDatabase["modelId"]),
                                    Convert.ToInt32(objectFromDatabase["manufacturerId"]),
                                    objectFromDatabase["description"].ToString());
                objectFromDatabase.Close();
                return returnObject;
            }
            objectFromDatabase.Close();
            return null;
        }
        return null;
    }

    /// <summary>
    /// Returns a list of model objects based upon manufacturerId
    /// </summary>
    public static Model[] selectForManufacturerId(int manufacturerId, MySql connection)
    {
        string selectVehicles = "SELECT * FROM Model WHERE (manufacturerId='" + manufacturerId + "')";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<Model> objectList = new List<Model>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new Model(Convert.ToInt32(objectsFromDB["modelId"]),
                                    Convert.ToInt32(objectsFromDB["manufacturerId"]),
                                    objectsFromDB["description"].ToString()));
                    objectCount++;
                }

                Model[] returnValue = new Model[objectList.Count];
                objectList.CopyTo(returnValue);
                objectsFromDB.Close();
                return returnValue;
            }
            objectsFromDB.Close();
            return null;
        }
        return null;
    }

    public static Model[] selectAll(MySql connection)
    {
        string selectVehicles = "SELECT * FROM Model;";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<Model> objectList = new List<Model>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new Model(Convert.ToInt32(objectsFromDB["modelId"]),
                                    Convert.ToInt32(objectsFromDB["manufacturerId"]),
                                    objectsFromDB["description"].ToString()));
                    objectCount++;
                }

                Model[] returnValue = new Model[objectList.Count];
                objectList.CopyTo(returnValue);
                objectsFromDB.Close();
                return returnValue;
            }
            objectsFromDB.Close();
            return null;
        }
        return null;
    }
}