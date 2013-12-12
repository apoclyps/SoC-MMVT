using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Photograph
/// </summary>
public class Photograph
{
    private int photographId;
    private int jobId;
    private DateTime timeStamp;
    private string photoURL; // Changed from blob to string as no benefit to storing images within the database when file name can be used isntead

	public Photograph()
	{
        this.photographId = 0;
        this.jobId = 0;
        this.timeStamp = new DateTime();
        this.photoURL = null;
	}

    public Photograph(int photographId, int jobId, DateTime timeStamp, String photoURL)
    {
        this.photographId = photographId;
        this.jobId = jobId;
        this.timeStamp = timeStamp;
        this.photoURL = photoURL;
    }

    public int getPhotographId()
    {
        return photographId;
    }
    public int getJobId()
    {
        return jobId;
    }
    public DateTime getTimeStamp()
    {
        return timeStamp;
    }
    public String getPhotoURL()
    {
        return photoURL;
    }
    public void setPhotographId(int photographId)
    {
        this.photographId = photographId;
    }
    public void setJobId(int jobId)
    {
        this.jobId = jobId;
    }
    public void setTimeStamp(DateTime timeStamp)
    {
        this.timeStamp = timeStamp;
    }
    public void setPhotoURL(String photoURL)
    {
        this.photoURL = photoURL;
    }

    public string CreatePhotograph()
    {
        string createPhotographTable = "CREATE  TABLE  Photograph ( "
                + "photographId INT PRIMARY KEY IDENTITY,"
                + "jobId INT NULL,"
                + "timeStamp DATETIME NOT NULL ,"
                + "photoURL VARCHAR(70) NULL )";
        return createPhotographTable;
    }

    public string DropPhotograph()
    {
        string dropPhotographTable = "DROP TABLE Photograph";

        return dropPhotographTable;
    }

    public string InsertPhotograph()
    {
        string insertPhotograph = "INSERT INTO Photograph ("
            + "jobId,timeStamp,photoURL)"
            + "VALUES ('"
            + this.jobId + "','"
            + this.timeStamp.ToShortDateString() + "','"
            + this.photoURL + "')";

        return insertPhotograph;
    }

    public string InsertFK_Photograph_jobId()
    {
        string InsertFKJob = "ALTER TABLE Photograph ADD CONSTRAINT fk_Photograph_jobId FOREIGN KEY(jobId) REFERENCES Job(jobId)";
        return InsertFKJob;
    }

    public string DropFK_Photograph_jobId()
    {
        string DropFKJob = "ALTER TABLE Photograph DROP CONSTRAINT fk_Photograph_jobId";
        return DropFKJob;
    }

    public static Photograph selectByphotographId(int photographId, MySql connection)
    {
        string selectStatement = "SELECT * FROM Photograph WHERE (photographId='" + photographId + "')";
        SqlDataReader objectFromDatabase = connection.Select(selectStatement);

        if (objectFromDatabase != null)
        {
            if (objectFromDatabase.HasRows)
            {
                objectFromDatabase.Read();
                Photograph returnObject = new Photograph(Convert.ToInt32(objectFromDatabase["photographId"]),
                                    Convert.ToInt32(objectFromDatabase["jobId"]),
                                    Convert.ToDateTime(objectFromDatabase["timeStamp"]),
                                    objectFromDatabase["photoURL"].ToString());
                objectFromDatabase.Close();
                return returnObject;
            }
            objectFromDatabase.Close();
            return null;
        }
        return null;
    }

}