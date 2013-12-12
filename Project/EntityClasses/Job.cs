using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Job
/// </summary>
public class Job
{
    private int jobId;
    private int bookingId;
    private string description;
    private DateTime startDate;
    private DateTime endDate;
    private int status;
    private int numberOfManHours;

	public Job()
	{
        this.jobId = 0;
        this.bookingId = 0;
        this.description = null;
        this.startDate = new DateTime();
        this.endDate = new DateTime();
        this.status = 0;
        this.numberOfManHours = 0;
	}

    public Job(int jobId, int bookingId, string description, DateTime startDate, DateTime endDate, int status, int numberOfManHours) 
    {
		this.jobId = jobId;
		this.bookingId = bookingId;
		this.description = description;
		this.startDate = startDate;
		this.endDate = endDate;
		this.status = status;
		this.numberOfManHours = numberOfManHours;
	}


    public Job(int jobId, int bookingId, string description, int status, int numberOfManHours)
    {
        this.jobId = jobId;
        this.bookingId = bookingId;
        this.description = description;
        this.status = status;
        this.numberOfManHours = numberOfManHours;
    }


    public int getjobId()
    {
        return jobId;
    }
    public int getBookingID()
    {
        return bookingId;
    }
    public string getDescription()
    {
        return description;
    }
    public DateTime getStartDate()
    {
        return startDate;
    }
    public DateTime getEndDate()
    {
        return endDate;
    }
    public int getStatus()
    {
        return status;
    }
    public int getNumberOfManHours()
    {
        return numberOfManHours;
    }
    public void setJobId(int jobId)
    {
        this.jobId = jobId;
    }
    public void setBookingID(int bookingId)
    {
        this.bookingId = bookingId;
    }
    public void setDescription(string description)
    {
        this.description = description;
    }
    public void setStartDate(DateTime startDate)
    {
        this.startDate = startDate;
    }
    public void setEndDate(DateTime endDate)
    {
        this.endDate = endDate;
    }
    public void setStatus(int status)
    {
        this.status = status;
    }
    public void setNumberOfManHours(int numberOfManHours)
    {
        this.numberOfManHours = numberOfManHours;
    }

    public string CreateJob()    {
        string createJobTable = "CREATE  TABLE  Job ( "
                + "jobId INT PRIMARY KEY IDENTITY,"
                + "bookingId INT NULL,"
                + "description VARCHAR(3000) NULL ,"
                + "startDate DATETIME NULL ,"
                + "endDate DATETIME NULL ,"
                + "status INT NULL ,"
                + "numberOfManHours INT NULL )";
        return createJobTable;
    }

    public string DropJob()    {
        string dropJobTable = "DROP TABLE Job";
        return dropJobTable;
    }

    public string InsertJob()
    {
        string insertJob = "INSERT INTO Job ("
            + "bookingId,description,status,numberOfManHours)"
            + "VALUES ('"
            + this.bookingId + "','"
            + this.description + "','"
            + this.status + "','"
            + this.numberOfManHours + "')";

        return insertJob;
    }

    public string InsertFKJob()
    {
        string InsertFKJob = "ALTER TABLE Job ADD CONSTRAINT fk_bookingId FOREIGN KEY(bookingId) REFERENCES Booking(bookingId)";

        return InsertFKJob;
    }

    public string DropFKJob()
    {
        string DropFKJob = "ALTER TABLE Job DROP CONSTRAINT fk_bookingId";

        return DropFKJob;
    }

    public static Job selectByJobId(int jobId, MySql connection)
    {
        string selectJob = "SELECT * FROM Job WHERE (jobId='" + jobId + "')";
        SqlDataReader jobFromDb = connection.Select(selectJob);

        if (jobFromDb != null)
        {
            if (jobFromDb.HasRows)
            {
                jobFromDb.Read();
                Job returnJob = new Job(Convert.ToInt32(jobFromDb["jobId"]),
                                   Convert.ToInt32(jobFromDb["bookingId"]),
                                   jobFromDb["description"].ToString(),
                                   Convert.ToDateTime(jobFromDb["startDate"]),
                                   Convert.ToDateTime(jobFromDb["endDate"]),
                                   Convert.ToInt32(jobFromDb["status"]),
                                   Convert.ToInt32(jobFromDb["numberOfManHours"]));
                jobFromDb.Close();
                return returnJob;
            }
            jobFromDb.Close();
            return null;
        }
        return null;
    }

    public static Job selectByBookingId(int bookingId, MySql connection)
    {
        string selectJob = "SELECT * FROM Job WHERE (bookingId='" + bookingId + "')";
        SqlDataReader jobFromDb = connection.Select(selectJob);

        if (jobFromDb != null)
        {
            if (jobFromDb.HasRows)
            {
                jobFromDb.Read();
                Job returnJob = new Job(Convert.ToInt32(jobFromDb["jobId"]),
                                   Convert.ToInt32(jobFromDb["bookingId"]),
                                   jobFromDb["description"].ToString(),
                                   Convert.ToInt32(jobFromDb["status"]),
                                   Convert.ToInt32(jobFromDb["numberOfManHours"]));
                jobFromDb.Close();
                return returnJob;
            }
            jobFromDb.Close();
            return null;
        }
        return null;
    }

    public string delete()
    {
        return "DELETE FROM Job WHERE (jobId = '" + this.jobId + "');";
    }

    public string update()
    {
        string updateString = "UPDATE Job SET description='" + this.description
                                                + "', status='" + this.status
                                                + "' WHERE (jobId=" + this.jobId + ");";

        return updateString;
    }
}