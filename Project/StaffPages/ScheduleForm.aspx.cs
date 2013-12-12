using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMVT
{
    public partial class ScheduleForm : System.Web.UI.Page
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

            DateTime selectedDate;

            if (Session["selectedDate"] == null)
            {
                selectedDate = DateTime.Now.Date;
            }
            else
            {
                selectedDate = (DateTime)Session["selectedDate"];
            }
            
                
            Session["selectedDate"] = selectedDate;
            SelectCalender.SelectedDate = selectedDate;

            Booking[] bookingsForSelectedDate = Booking.selectForDate(selectedDate, mysqlconnection);
            if (bookingsForSelectedDate != null)
            {
                for (int i = 0; i < bookingsForSelectedDate.Length; i++)
                {
                    buildBookingTableForBooking(bookingsForSelectedDate[i], todays_bookings);
                }
            }
            else
            {
                Label empty = new Label();
                empty.Text = "There are no bookings on this day.";
                todays_bookings.Controls.Add(empty);
            }
            calenderHeader.Text = "Schedule for:";


            
           
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            mysqlconnection.CloseConnection();
        }

        protected void moveBooking (object sender, CommandEventArgs e)
        {
            Session["moveBookingFlag"] = "true";
            Session["moveBookingId"] = e.CommandArgument.ToString();
            calenderHeader.Text = "Move to:";
        }

        protected void removeBooking(object sender, CommandEventArgs e)
        {
            Booking bookingToRemove = Booking.selectByBookingId(Convert.ToInt32(e.CommandArgument),mysqlconnection);
            Job jobToRemove = Job.selectByBookingId(bookingToRemove.getId(), mysqlconnection);

            mysqlconnection.Delete(jobToRemove.delete());
            mysqlconnection.Delete(bookingToRemove.delete());

            Response.Redirect("ScheduleForm.aspx");
        }

        public void add_jobSubmitClick(object sender, EventArgs e)
        {
            Booking newBooking = new Booking();
            Job newJob = new Job();

            newBooking.setDate((DateTime)Session["selectedDate"]);
            
            /* !!!!!!!!!!!!!!!!! Really really nasty time retreval !!!!!!!!!!!!!!!!!!!!!!!!!!
                                       FIX IN REAL VERSION
             */
            newBooking.setTime(DateTime.ParseExact(add_jobTime.Text, "HH:mm", null));

            newBooking.setPriority(Convert.ToInt32(add_jobPriority.SelectedItem.Text));
            newJob.setDescription(add_jobDescription.Text);
            newBooking.setConfirmed(true);
            Session["bookingToInsert"] = newBooking;
            Session["jobToInsert"] = newJob;
            Response.Redirect("CustomerSelect.aspx");
        }

        public void showAddBooking_click(object sender, EventArgs e)
        {
            add_jobDescription.Text = "";
            add_jobPriority.SelectedIndex = 0;
            add_jobTime.Text = "";

            AddJobPanel.Visible = true;
            JobDetailsPanel.Visible = false;

        }

        protected void updateSelectedDate(object sender, EventArgs e)
        {
            if (Session["moveBookingFlag"] != null && Session["moveBookingFlag"].Equals("true") && Session["moveBookingId"] != null)
            {
                Booking seletedBooking = Booking.selectByBookingId(Convert.ToInt32(Session["moveBookingId"].ToString()), mysqlconnection);

                seletedBooking.setDate(SelectCalender.SelectedDate);
                mysqlconnection.Update(seletedBooking.updateDate());

                Session["moveBookingFlag"] = "false";
                Session["moveBookingId"] = null;
                Session["selectedDate"] = SelectCalender.SelectedDate;
               
                calenderHeader.Text = "Schedule for:";
            }
            else
            {
                DateTime currentSelection = SelectCalender.SelectedDate;
                Session["selectedDate"] = SelectCalender.SelectedDate;
                todays_bookings.Controls.Clear();

                Booking[] bookingsForSelectedDate = Booking.selectForDate(currentSelection, mysqlconnection);
                if (bookingsForSelectedDate != null)
                {
                    for (int i = 0; i < bookingsForSelectedDate.Length; i++)
                    {
                        buildBookingTableForBooking(bookingsForSelectedDate[i], todays_bookings);
                    }
                }
                else
                {
                    Label empty = new Label();
                    empty.Text = "There are no bookings on this day.";
                    todays_bookings.Controls.Add(empty);
                }
            }
        }

        private void buildBookingTableForBooking(Booking currentBooking, Panel bookingsPanel) // replase string id with booking object might also require customer object
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

            TableCell booking_time = new TableCell();
            TableCell booking_time_h = new TableCell();
            booking_time_h.Text = "Time:";
            booking_time_h.CssClass = "tableHeader";

            booking_time.Text = currentBooking.getTime().ToShortTimeString();

            booking_r1.Cells.Add(booking_time_h);
            booking_r1.Cells.Add(booking_time);

            TableCell priority = new TableCell();
            TableCell priority_h = new TableCell();
            priority_h.Text = "Priority:";
            priority_h.CssClass = "tableHeader";

            priority.Text = currentBooking.getPriority().ToString() ;

            booking_r1.Cells.Add(priority_h);
            booking_r1.Cells.Add(priority);
            

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

            // replace with methodcall to customer/booking object
            booking_type.Text = currentBooking.getBookingTypeId().ToString();

            booking_r3.Cells.Add(booking_type_h);
            booking_r3.Cells.Add(booking_type);

            TableRow booking_r4 = new TableRow();


            // build link buttons to manipulate individual booking
            TableCell booking_controls = new TableCell();
            booking_controls.ColumnSpan = 6;

            LinkButton move = new LinkButton();
            move.Text = "Move booking";
            move.CommandArgument = currentBooking.getId().ToString();
            move.Command += new CommandEventHandler(moveBooking);

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