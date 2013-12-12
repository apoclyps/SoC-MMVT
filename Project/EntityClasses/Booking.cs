using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Booking
/// </summary>
public class Booking
{
    private int bookingId;
    private string vehicleRegistrationNumber;
    private int bookingTypeId;
    private int invoiceId;
    private DateTime date;
    private DateTime time;
    private int priority;
    private Boolean confirmed;

	public Booking()
	{
        this.bookingId = 0;
        this.vehicleRegistrationNumber = null;
        this.bookingTypeId = 0;
        this.invoiceId = 0;
        this.date = new DateTime();
        this.time = new DateTime();
        this.priority = 0;
        this.confirmed = false;
	}

    public Booking(int id, string vehicleRegistrationNumber, int bookingTypeId, int invoiceId, DateTime date, DateTime time, int priority, Boolean confirmed)
    {
        this.bookingId = id;
        this.vehicleRegistrationNumber = vehicleRegistrationNumber;
        this.bookingTypeId = bookingTypeId;
        this.invoiceId = invoiceId;
        this.date = date;
        this.time = time;
        this.priority = priority;
        this.confirmed = confirmed;
    }

    public void setId(int id)
    {
        this.bookingId = id;
    }

    public void setVehicleRegistrationNumber(string vehicleRegistrationNumber)
    {
        this.vehicleRegistrationNumber = vehicleRegistrationNumber;
    }

    public void setBookingTypeId(int bookingTypeId)
    {
        this.bookingTypeId = bookingTypeId;
    }

    public void setInvoiceId(int invoiceId)
    {
        this.invoiceId = invoiceId;
    }

    public void setDate(DateTime date)
    {
        this.date = date;
    }

    public void setTime(DateTime time)
    {
        this.time = time;
    }

    public void setPriority(int priority)
    {
        this.priority = priority;
    }

    public void setConfirmed(Boolean confirmed)
    {
        this.confirmed = confirmed;
    }
    
    // Getters

    public int getId()
    {
        return this.bookingId;
    }

    public string getVehicleRegistrationNumber()
    {
        return this.vehicleRegistrationNumber;
    }

    public int getBookingTypeId()
    {
        return this.bookingTypeId;
    }

    public int getInvoiceId()
    {
        return this.invoiceId;
    }

    public DateTime getDate()
    {
        return this.date;
    }

    public DateTime getTime()
    {
        return this.time;
    }

    public int getPriority()
    {
        return this.priority;
    }

    public Boolean getConfirmed()
    {
        return this.confirmed;
    }

    // Methods

    public string CreateBooking()
    {
        string createBookingTable = "CREATE  TABLE  Booking ( "
             + "bookingId INT PRIMARY KEY IDENTITY,"
             + "vehicleRegistrationNumber VARCHAR(10) NOT NULL  ,"
             + "bookingTypeId INT NULL ,"
             + "invoiceId INT NULL ,"
             + "date DATETIME NOT NULL ,"
             + "time DATETIME NOT NULL ,"
             + "priority INT NOT NULL ,"
             + "confirmed INT NOT NULL )";
        return createBookingTable;
    }

    /*
     * public string CreateBooking()
    {
        string createBookingTable = "CREATE  TABLE  Booking ( "
             + "bookingId INT PRIMARY KEY IDENTITY,"
             + "vehicleRegistrationNumber VARCHAR(10) NOT NULL  ,"
             + "bookingTypeId INT NOT NULL ,"
             + "invoiceId INT NOT NULL ,"
             + "date DATETIME NOT NULL ,"
             + "time DATETIME NOT NULL ,"
             + "priority INT NOT NULL ,"
             + "confirmed INT NOT NULL )";
        return createBookingTable;
    }
     */

    public string InsertFKvehicleRegistrationNumber()
    {
        string CreateBookingFK = "ALTER TABLE Booking ADD CONSTRAINT fk_vehicleRegistrationNumber FOREIGN KEY (vehicleRegistrationNumber) REFERENCES Vehicle(registrationNumber)";
        return CreateBookingFK;
    }

    public string InsertFKBookingId()
    {
        string CreateBookingFK = "ALTER TABLE Booking ADD CONSTRAINT fk_bookingTypeId FOREIGN KEY (bookingTypeId) REFERENCES BookingType(bookingTypeId)";
        return CreateBookingFK;
    }

    public string InsertFKInvoiceId()
    {
        string CreateBookingFK = "ALTER TABLE Booking ADD CONSTRAINT fk_invoiceId FOREIGN KEY (invoiceId) REFERENCES Invoice(invoiceId)";
        return CreateBookingFK;
    }


    public string DropFKVehicleRegistrationNumber()
    {
        string DropBookingFK = "ALTER TABLE Booking DROP CONSTRAINT fk_vehicleRegistrationNumber";
        return DropBookingFK;
    }


    public string DropFKBookingTypeId()
    {
        string DropBookingFK = "ALTER TABLE Booking DROP CONSTRAINT fk_bookingTypeId";
        return DropBookingFK;
    }


    public string DropFKInvoiceId()
    {
        string DropBookingFK = "ALTER TABLE Booking DROP CONSTRAINT fk_invoiceId";
        return DropBookingFK;
    }
    

    public string DropBooking()
    {
        string dropBookingTable = "DROP TABLE Booking";
        return dropBookingTable;
    }

    public string InsertBooking()
    {
        string insertBooking = "INSERT INTO Booking ("
            + "vehicleRegistrationNumber,date,time,priority,confirmed)"
            + "VALUES ('"
            + this.vehicleRegistrationNumber + "','"
            + this.date.ToLongDateString() + "','"
            + this.time.ToLongTimeString() + "','"
            + this.priority + "','"
            + Convert.ToInt32(this.confirmed) + "')";
        return insertBooking;
    }

    public static Booking selectByBookingId(int bookingId, MySql connection)
    {
        string selectBooking = "SELECT * FROM Booking WHERE (bookingId='" + bookingId + "')";
        SqlDataReader bookingFromDb = connection.Select(selectBooking);

        if (bookingFromDb != null)
        {
            if (bookingFromDb.HasRows)
            {
                bookingFromDb.Read();
                Booking returnBooking = new Booking(Convert.ToInt32(bookingFromDb["bookingId"]), 
                                   bookingFromDb["vehicleRegistrationNumber"].ToString(),
                                   0,0,
                                   Convert.ToDateTime(bookingFromDb["date"]),
                                   Convert.ToDateTime(bookingFromDb["time"].ToString()),
                                   Convert.ToInt32(bookingFromDb["priority"].ToString()),
                                   Convert.ToBoolean(bookingFromDb["confirmed"]));
                bookingFromDb.Close();
                return returnBooking;
            }
            bookingFromDb.Close();
            return null;
        }
        return null;
    }

    /// <summary>
    /// Returns a list of model objects based upon manufacturerId
    /// </summary>
    public static Booking[] selectForVehicleRegistrationNo(string vehicleRegistrationNumber, MySql connection)
    {
        string selectVehicles = "SELECT * FROM Booking WHERE (vehicleRegistrationNumber='" + vehicleRegistrationNumber + "')";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<Booking> objectList = new List<Booking>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new Booking(Convert.ToInt32(objectsFromDB["bookingId"]), 
                                   objectsFromDB["vehicleRegistrationNumber"].ToString(),
                                   0,0,
                                   Convert.ToDateTime(objectsFromDB["date"]),
                                   Convert.ToDateTime(objectsFromDB["time"].ToString()),
                                   Convert.ToInt32(objectsFromDB["priority"].ToString()),
                                   Convert.ToBoolean(objectsFromDB["confirmed"])));
                    objectCount++;
                }

                Booking[] returnValue = new Booking[objectList.Count];
                objectList.CopyTo(returnValue);
                objectsFromDB.Close();
                return returnValue;
            }
            objectsFromDB.Close();
            return null;
        }
        return null;
    }

    public static Booking[] selectForDate(DateTime selectedDate, MySql connection)
    {
        string selectVehicles = "SELECT * FROM Booking WHERE (date = '" + selectedDate.ToLongDateString() + "' AND confirmed=1)";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<Booking> objectList = new List<Booking>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new Booking(Convert.ToInt32(objectsFromDB["bookingId"]),
                                   objectsFromDB["vehicleRegistrationNumber"].ToString(),
                                   0,
                                   0,
                                   Convert.ToDateTime(objectsFromDB["date"]),
                                   Convert.ToDateTime(objectsFromDB["time"].ToString()),
                                   Convert.ToInt32(objectsFromDB["priority"].ToString()),
                                   Convert.ToBoolean(objectsFromDB["confirmed"])));
                    objectCount++;
                }

                Booking[] returnValue = new Booking[objectList.Count];
                objectList.CopyTo(returnValue);
                objectsFromDB.Close();
                return returnValue;
            }
            objectsFromDB.Close();
            return null;
        }
        return null;
    }

    public static Booking[] selectOpenRequests(MySql connection)
    {
        string selectVehicles = "SELECT * FROM Booking WHERE (confirmed=0)";
        SqlDataReader objectsFromDB = connection.Select(selectVehicles);

        List<Booking> objectList = new List<Booking>();
        int objectCount = 0;
        if (objectsFromDB != null)
        {
            if (objectsFromDB.HasRows)
            {
                while (objectsFromDB.Read())
                {
                    objectList.Add(new Booking(Convert.ToInt32(objectsFromDB["bookingId"]),
                                   objectsFromDB["vehicleRegistrationNumber"].ToString(),
                                   0,
                                   0,
                                   Convert.ToDateTime(objectsFromDB["date"]),
                                   Convert.ToDateTime(objectsFromDB["time"].ToString()),
                                   Convert.ToInt32(objectsFromDB["priority"].ToString()),
                                   Convert.ToBoolean(objectsFromDB["confirmed"])));
                    objectCount++;
                }

                Booking[] returnValue = new Booking[objectList.Count];
                objectList.CopyTo(returnValue);
                objectsFromDB.Close();
                return returnValue;
            }
            objectsFromDB.Close();
            return null;
        }
        return null;
    }

    public string updateDate()
    {
        string updateDate = "UPDATE Booking SET date = '" + this.date.ToLongDateString() + "' WHERE bookingId = '" + this.bookingId + "';";
        return updateDate;
    }

    public string updateConfirmed()
    {
        string updateConfirmed = "UPDATE Booking SET confirmed='" + Convert.ToInt32(this.confirmed) + "' WHERE bookingId='" + this.bookingId + "';";
        return updateConfirmed;
    }

    public string update()
    {
        string update = "UPDATE Booking SET time='" + this.time.ToLongTimeString()
                                                + ", priority='" + this.priority + "' WHERE bookingId = '" + this.bookingId + "';";
        return update;
    }

    public string delete()
    {
        return "DELETE FROM Booking WHERE (bookingId = '" + this.bookingId + "');";
    }

}