using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMVT.StaffPages
{
    public partial class Requests : System.Web.UI.Page
    {
        MySql mysqlconnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            mysqlconnection = new MySql();
            mysqlconnection.OpenConnection();

            if (Session["account"] == null || !((Account)Session["account"]).gettype().Equals("Admin"))
            {
                Response.Redirect("../LoginForm.aspx");
            }

            Booking[] openBookings = Booking.selectOpenRequests(mysqlconnection);
            if (openBookings != null)
            {
                for (int i = 0; i < openBookings.Length; i++)
                {
                    buildBookingTableForBooking(openBookings[i], todays_bookings);
                }
            }
            else
            {
                Label empty = new Label();
                empty.Text = "There are no unconfirmed booking requests.";
                todays_bookings.Controls.Add(empty);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            mysqlconnection.CloseConnection();
        }

        protected void removeBooking (object sender, CommandEventArgs e)
        {
            Booking bookingToRemove = Booking.selectByBookingId(Convert.ToInt32(e.CommandArgument), mysqlconnection);
            Job jobToRemove = Job.selectByBookingId(bookingToRemove.getId(), mysqlconnection);

            mysqlconnection.Delete(jobToRemove.delete());
            mysqlconnection.Delete(bookingToRemove.delete());

            Response.Redirect("Requests.aspx");
        }

        protected void confirmBooking(object sender, CommandEventArgs e)
        {
            Booking currentBooking = Booking.selectByBookingId(Convert.ToInt32(e.CommandArgument), mysqlconnection);
            currentBooking.setConfirmed(true);
            mysqlconnection.Update(currentBooking.updateConfirmed());
            Response.Redirect("Requests.aspx");
        }

        private void buildBookingTableForBooking(Booking currentBooking, Panel bookingsPanel) 
        {
            Vehicle vehicleForBooking = Vehicle.selectByRegistrationNumber(currentBooking.getVehicleRegistrationNumber(), mysqlconnection);
            Customer customerForBooking = Customer.selectByCustomerId(vehicleForBooking.getCustomerId(), mysqlconnection);
            Job jobForBooking = Job.selectByBookingId(currentBooking.getId(), mysqlconnection);

            Table current_booking = new Table();
            current_booking.CssClass = "entry";

            TableRow booking_r1 = new TableRow();
            booking_r1.CssClass = "entryHeaderRow";
            TableCell customer_name = new TableCell();
            TableCell customer_name_h = new TableCell();
            customer_name_h.Text = "Customer: ";
            customer_name_h.CssClass = "tableHeader";

            customer_name.Text = customerForBooking.getFirstName() + " " + customerForBooking.getSurname();

            booking_r1.Cells.Add(customer_name_h);
            booking_r1.Cells.Add(customer_name);

            TableCell date = new TableCell();
            TableCell date_h = new TableCell();
            date_h.Text = "Time:";
            date_h.CssClass = "tableHeader";

            date.Text = currentBooking.getDate().ToShortDateString();

            booking_r1.Cells.Add(date_h);
            booking_r1.Cells.Add(date);

            TableCell time = new TableCell();
            TableCell time_h = new TableCell();
            time_h.Text = "Priority:";
            time_h.CssClass = "tableHeader";

            time.Text = currentBooking.getTime().ToShortTimeString();

            booking_r1.Cells.Add(time_h);
            booking_r1.Cells.Add(time);
            

            TableRow booking_r2 = new TableRow();
            

            TableCell job_description = new TableCell();
            job_description.ColumnSpan = 6;

            job_description.Text = jobForBooking.getDescription();
            
            booking_r2.Cells.Add(job_description);

            TableRow booking_r3 = new TableRow();
            booking_r3.CssClass = "entryHeaderRow";
            TableCell vehicle_reg = new TableCell();
            TableCell vehicle_reg_h = new TableCell();
            vehicle_reg_h.Text = "Vehicle: ";
            vehicle_reg_h.CssClass = "tableHeader";

            vehicle_reg.Text = vehicleForBooking.getRegistrationNumber();

            booking_r3.Cells.Add(vehicle_reg_h);
            booking_r3.Cells.Add(vehicle_reg);

            TableCell booking_type = new TableCell();
            TableCell booking_type_h = new TableCell();
            booking_type.ColumnSpan = 3;
            booking_type_h.Text = "Type: ";
            booking_type_h.CssClass = "tableHeader";

            booking_type.Text = currentBooking.getBookingTypeId().ToString();

            booking_r3.Cells.Add(booking_type_h);
            booking_r3.Cells.Add(booking_type);

            TableRow booking_r4 = new TableRow();


            // build link buttons to manipulate individual booking
            TableCell booking_controls = new TableCell();
            booking_controls.ColumnSpan = 6;

            LinkButton move = new LinkButton();
            move.Text = "Confirm";
            move.CommandArgument = currentBooking.getId().ToString();
            move.Command += new CommandEventHandler(confirmBooking);

            booking_controls.Controls.Add(move);

            LinkButton remove = new LinkButton();
            remove.Text = "Remove booking";
            remove.CommandArgument = currentBooking.getId().ToString();
            remove.Command += new CommandEventHandler(removeBooking);

            booking_controls.Controls.Add(remove);

            booking_r4.Cells.Add(booking_controls);
            booking_controls.CssClass = "entryControls";

            // add all the rows to the table
            current_booking.Rows.Add(booking_r1);
            current_booking.Rows.Add(booking_r3);
            current_booking.Rows.Add(booking_r2);
            current_booking.Rows.Add(booking_r4);

            bookingsPanel.Controls.Add(current_booking);
        }
    }
}