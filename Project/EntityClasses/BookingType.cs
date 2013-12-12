using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BookingType
/// 
/// Booking Type has auto generate bookingTypeId so it is not needed
/// </summary>
public class BookingType
{

    private int bookingTypeId;
    private String description;

	public BookingType()
	{
        bookingTypeId = 0;
        description = null;
	}

    public BookingType(String description)
    {
        this.bookingTypeId = 0;
        this.description = description;
    }

    public BookingType(int bookingTypeId, String description)
    {
        this.bookingTypeId = bookingTypeId;
        this.description = description;
    }

    public string getDescription()
    {
        return description;
    }
    public void setDescription(String description)
    {
        this.description = description;
    }
    public int getbookingTypeId()
    {
        return bookingTypeId;
    }
    public void setbookingTypeId(int bookingTypeId)
    {
        this.bookingTypeId = bookingTypeId;
    }

    public string CreateBookingType()
    {
        string CreateBookingType = "CREATE  TABLE  BookingType ( "
             + "bookingTypeId INT PRIMARY KEY IDENTITY,"
             + "description VARCHAR(25) NOT NULL )";

        return CreateBookingType;
    }

    public string DropBookingType()
    {
        string dropBookingType = "DROP TABLE BookingType";

        return dropBookingType;
    }

    public string InsertBookingType()
    {
        string insertBookingType = "INSERT INTO BookingType ("
            + "description)"
            + "VALUES ('"
            + this.description +  "')";

        return insertBookingType;
    }

    public static BookingType selectByBookingTypeId(int bookingTypeId, MySql connection)
    {
        string selectBookingTypeId = "SELECT * FROM BookingType WHERE (bookingTypeId='" + bookingTypeId + "')";
        SqlDataReader bookingTypeIdFromDb = connection.Select(selectBookingTypeId);

        if (bookingTypeIdFromDb != null)
        {
            if (bookingTypeIdFromDb.HasRows)
            {
                bookingTypeIdFromDb.Read();
                BookingType returnBookingType = new BookingType(Convert.ToInt32(bookingTypeIdFromDb["bookingTypeId"]),
                                    bookingTypeIdFromDb["description"].ToString());
                bookingTypeIdFromDb.Close();
                return returnBookingType;
            }
            bookingTypeIdFromDb.Close();
            return null;
        }
        return null;
    }
}