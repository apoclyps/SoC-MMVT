using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Invoice
/// </summary>
public class Invoice
{
    private int invoiceId;  
    private DateTime date;
    private double totalAmount;
    private DateTime estimatedTime;
    private double estimatedPrice;
    private Boolean paid;
    
	public Invoice()
	{
        this.invoiceId = 0;
        this.date = new DateTime();
        this.totalAmount = 0;
        this.estimatedTime = new DateTime();
        this.estimatedPrice = 0.00;
        this.paid = false;

	}


    public Invoice(int invoiceId, double totalAmount, double estimatedPrice, Boolean paid)
    {
        this.invoiceId = invoiceId;
        this.date = new DateTime();
        this.totalAmount = totalAmount;
        this.estimatedTime = new DateTime();
        this.estimatedPrice = estimatedPrice;
        this.paid = paid;

    }

    public Invoice(int invoiceId, DateTime date, double totalAmount, DateTime estimatedTime, double estimatedPrice, Boolean paid)
    {
        this.invoiceId = invoiceId;
        this.date = date;
        this.totalAmount = totalAmount;
        this.estimatedTime = estimatedTime;
        this.estimatedPrice = estimatedPrice;
        this.paid = paid;

    }

    public int getInvoiceId()
    {
        return invoiceId;
    }
    public DateTime getDate()
    {
        return date;
    }
    public double getTotalAmount()
    {
        return totalAmount;
    }
    public DateTime getEstimatedTime()
    {
        return estimatedTime;
    }
    public double getEstimatedPrice()
    {
        return estimatedPrice;
    }
    public Boolean getPaid()
    {
        return paid;
    }
    public void setInvoiceId(int invoiceId)
    {
        this.invoiceId = invoiceId;
    }
    public void setDate(DateTime date)
    {
        this.date = date;
    }
    public void setTotalAmount(double totalAmount)
    {
        this.totalAmount = totalAmount;
    }
    public void setEstimatedTime(DateTime estimatedTime)
    {
        this.estimatedTime = estimatedTime;
    }
    public void setEstimatedPrice(double estimatedPrice)
    {
        this.estimatedPrice = estimatedPrice;
    }
    public void setPaid(Boolean paid)
    {
        this.paid = paid;
    }

    public string CreateInvoice()
    {
        string createInvoiceTable = "CREATE  TABLE  Invoice ( "
             + "invoiceId INT PRIMARY KEY IDENTITY,"
             + "date DATETIME NOT NULL ,"
             + "totalAmount FLOAT NOT NULL ,"
             + "estimatedTime DATETIME NOT NULL ,"
             + "estimatedPrice FLOAT NOT NULL ,"
             + "paid INT NOT NULL )";

        return createInvoiceTable;
    }

    public string DropInvoice()
    {
        string dropInvoiceTable = "DROP TABLE Invoice";

        return dropInvoiceTable;
    }

    public string InsertInvoice()
    {
        string insertInvoice = "INSERT INTO Invoice ("
            + "date,totalAmount,estimatedTime,estimatedPrice,paid)"
            + "VALUES ('"
            + this.date.ToShortDateString() + "','"
            + this.totalAmount + "','"
            + this.estimatedTime.ToShortDateString() + "','"
            + this.estimatedPrice + "','"
            + Convert.ToInt32(this.paid) + "')";

         return insertInvoice;
    }

    public static Invoice selectByInvoiceId(int InvoiceId, MySql connection)
    {
        string selectInvoice = "SELECT * FROM Invoice WHERE (invoiceId='" + InvoiceId + "')";
        SqlDataReader invoiceFromDb = connection.Select(selectInvoice);

        if (invoiceFromDb != null)
        {
            if (invoiceFromDb.HasRows)
            {
                invoiceFromDb.Read();
                Invoice returnInvoice = new Invoice(Convert.ToInt32(invoiceFromDb["invoiceId"]),
                                   Convert.ToDateTime(invoiceFromDb["date"]),
                                   Convert.ToDouble(invoiceFromDb["totalAmount"]),
                                   Convert.ToDateTime(invoiceFromDb["estimatedTime"]),
                                   Convert.ToDouble(invoiceFromDb["estimatedPrice"]),
                                   Convert.ToBoolean(invoiceFromDb["paid"].ToString()));
                invoiceFromDb.Close();
                return returnInvoice;
            }
            invoiceFromDb.Close();
            return null;
        }
        return null;
    }

}