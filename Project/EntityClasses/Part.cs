using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Part
/// </summary>
public class Part
{
    private int partId;
    private int jobId;
    private String partNumber;
    private int quantityUsed;
    private String description;
    private double unitPrice;

	public Part()
	{
        this.partId = 0;
        this.jobId = 0;
        this.partNumber = null;
        this.quantityUsed = 0;
        this.description = null;
        this.unitPrice = 0;
	}

    public Part(int partId, int jobId, String partNumber, int quantityUsed, String description, double unitPrice)
    {
        this.partId = partId;
        this.jobId = jobId;
        this.partNumber = partNumber;
        this.quantityUsed = quantityUsed;
        this.description = description;
        this.unitPrice = unitPrice;
    }

    public int getPartId()
    {
        return partId;
    }
    public int getJobId()
    {
        return jobId;
    }
    public String getPartNumber()
    {
        return partNumber;
    }
    public int getQuantityUsed()
    {
        return quantityUsed;
    }
    public String getDescription()
    {
        return description;
    }
    public double getUnitPrice()
    {
        return unitPrice;
    }
    public void setPartId(int partId)
    {
        this.partId = partId;
    }
    public void setJobId(int jobId)
    {
        this.jobId = jobId;
    }
    public void setPartNumber(String partNumber)
    {
        this.partNumber = partNumber;
    }
    public void setQuantityUsed(int quantityUsed)
    {
        this.quantityUsed = quantityUsed;
    }
    public void setDescription(String description)
    {
        this.description = description;
    }
    public void setUnitPrice(double unitPrice)
    {
        this.unitPrice = unitPrice;
    }

    public string CreatePart()
    {
        string createPartTable = "CREATE  TABLE  Part ( "
             + "partId INT PRIMARY KEY IDENTITY,"
             + "jobId INT NOT NULL ,"
             + "partNumber VARCHAR(45) NULL ,"
             + "quantityUsed INT NOT NULL ,"
             + "description VARCHAR(300) NULL ,"
             + "unitPrice FLOAT NOT NULL )";

        return createPartTable;
    }

    public string DropPart()
    {
        string dropPartTable = "DROP TABLE Part";

        return dropPartTable;
    }

    public string InsertPart()
    {
        string insertPart = "INSERT INTO Part ("
            + "jobId,partNumber,quantityUsed,description,unitPrice)"
            + "VALUES ('"
            + this.jobId + "','"
            + this.partNumber + "','"
            + this.quantityUsed + "','"
            + this.description + "','"
            + this.unitPrice + "')";

        return insertPart;
    }

    public string InsertFK_Part_jobId()    {
        string InsertFKJob = "ALTER TABLE Part ADD CONSTRAINT fk_Part_jobId FOREIGN KEY(jobId) REFERENCES Job(jobId)";
        return InsertFKJob;
    }

    public string DropFK_Part_jobId()   {
        string DropFKJob = "ALTER TABLE Part DROP CONSTRAINT fk_Part_jobId";
        return DropFKJob;
    }

    public static Part selectByPartId(int partId, MySql connection)
    {
        string selectStatement = "SELECT * FROM Part WHERE (partId='" + partId + "')";
        SqlDataReader objectFromDatabase = connection.Select(selectStatement);

        if (objectFromDatabase != null)
        {
            if (objectFromDatabase.HasRows)
            {
                objectFromDatabase.Read();
                Part returnObject = new Part(Convert.ToInt32(objectFromDatabase["partId"]),
                                    Convert.ToInt32(objectFromDatabase["jobId"]),
                                    objectFromDatabase["partNumber"].ToString(),
                                    Convert.ToInt32(objectFromDatabase["quantityUsed"]),
                                    objectFromDatabase["description"].ToString(),
                                    Convert.ToDouble(objectFromDatabase["unitPrice"]));        
                objectFromDatabase.Close();
                return returnObject;
            }
            objectFromDatabase.Close();
            return null;
        }
        return null;
    }
}