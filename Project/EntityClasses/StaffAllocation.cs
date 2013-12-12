using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for StaffAllocation
/// </summary>
public class StaffAllocation
{
    private int jobId;
    private string staffAccountUsername;
    
	public StaffAllocation()
	{
        this.jobId = 0;
        this.staffAccountUsername = null;
	}

    public StaffAllocation(int jobId, String staffAccountUsername)
    {
		this.jobId = jobId;
		this.staffAccountUsername = staffAccountUsername;
	}

    
    public int getjobId() {
		return jobId;
	}
	public String getStaffAccountUsername() {
		return staffAccountUsername;
	}
	public void setjobId(int jobId) {
		this.jobId = jobId;
	}
	public void setStaffAccountUsername(String staffAccountUsername) {
		this.staffAccountUsername = staffAccountUsername;
	}

    public string CreateStaffAllocation()
    {
        string CreateStaffAllocation = "CREATE  TABLE  StaffAllocation ( "
             + "jobId INT NOT NULL,"
             + "staffAccountUsername VARCHAR(45) NOT NULL )";

        return CreateStaffAllocation;
    }

    public string DropStaffAllocation()
    {
        string DropStaffAllocation = "DROP TABLE StaffAllocation";

        return DropStaffAllocation;
    }

    public string InsertStaffAllocation()
    {
        string InsertStaffAllocation = "INSERT INTO StaffAllocation ("
            + "jobId,staffAccountUsername)"
            + "VALUES ('"
            + this.jobId + "','"
            + this.staffAccountUsername + "')";

        return InsertStaffAllocation;
    }

    public string InsertFK_StaffAllocaiton_jobId()
    {
        string InsertFKJob = "ALTER TABLE StaffAllocation ADD CONSTRAINT fk_StaffAllocation_jobId FOREIGN KEY(jobId) REFERENCES Job(jobId)";
        return InsertFKJob;
    }

    public string DropFK_StaffAllocaiton_jobId()
    {
        string DropFKJob = "ALTER TABLE StaffAllocation DROP CONSTRAINT fk_StaffAllocation_jobId";
        return DropFKJob;
    }

    public string InsertFK_StaffAllocaiton_accountUsername()
    {
        string InsertFKJob = "ALTER TABLE StaffAllocation ADD CONSTRAINT fk_StaffAllocation_AccountUsername FOREIGN KEY(staffAccountUsername) REFERENCES Staff(accountUsername)";
        return InsertFKJob;
    }

    public string DropFK_StaffAllocaiton_accountUsername()
    {
        string DropFKJob = "ALTER TABLE StaffAllocation DROP CONSTRAINT fk_StaffAllocation_AccountUsername";
        return DropFKJob;
    }

    /// <summary>
    /// Selects a list of Staffallocations based on job id
    /// </summary>
    public static StaffAllocation[] selectForCustomerId(int jobId, MySql connection)
    {
        string selectVehicles = "SELECT * FROM StaffAllocation WHERE (jobId='" + jobId + "')";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<StaffAllocation> objectList = new List<StaffAllocation>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new StaffAllocation(Convert.ToInt32(objectsFromDB["jobId"]),
                                    objectsFromDB["staffAccountUsername"].ToString()));
                    objectCount++;
                }

                StaffAllocation[] returnValue = new StaffAllocation[objectList.Count];
                objectList.CopyTo(returnValue);
                objectsFromDB.Close();
                return returnValue;
            }
            objectsFromDB.Close();
            return null;
        }
        return null;
    }

    /// <summary>
    /// Selects a list of Staffallocations based on job id
    /// </summary>
    public static StaffAllocation[] selectForStaffAccountUsername(string staffAccountUsername, MySql connection)
    {
        string selectVehicles = "SELECT * FROM StaffAllocation WHERE (staffAccountUsername='" + staffAccountUsername + "')";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<StaffAllocation> objectList = new List<StaffAllocation>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new StaffAllocation(Convert.ToInt32(objectsFromDB["jobId"]),
                                    objectsFromDB["staffAccountUsername"].ToString()));
                    objectCount++;
                }

                StaffAllocation[] returnValue = new StaffAllocation[objectList.Count];
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