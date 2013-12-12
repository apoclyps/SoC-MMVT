using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMVT.CustomerPages
{
    public partial class CustomerBooking : System.Web.UI.Page
    {
        private MySql mysqlconnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            mysqlconnection = new MySql();
            mysqlconnection.OpenConnection();
            header.Text = "<h1>New booking request</h1>";

            if (Session["customerId"] == null)
            {
                Response.Redirect("../LoginForm.aspx");
            }

            Session["customer"] = Customer.selectByCustomerId(Convert.ToInt32(Session["customerId"]), mysqlconnection);

            DropDownList customerVehiclesList = new DropDownList();
            displayVehiclesForCustomerInList(((Customer)Session["customer"]).getCustomerId(),customerVehiclesList);
            customerVehiclesList.ID = "vehiclesList";
            customerVehicles.Controls.Add(customerVehiclesList);
        }


        private void displayVehiclesForCustomerInList(int customerId, DropDownList targetList)
        {   
            Vehicle[] customerVehiclesFromDB = Vehicle.selectByCustomerId(customerId, mysqlconnection);

            if (customerVehiclesFromDB != null)
            {
                for (int i = 0; i < customerVehiclesFromDB.Length; i++)
                {
                    ListItem currentVehicle = new ListItem();
                    currentVehicle.Text = customerVehiclesFromDB[i].getRegistrationNumber();
                    currentVehicle.Value = customerVehiclesFromDB[i].getRegistrationNumber();
                    targetList.Items.Add(currentVehicle);
                }

            }
        }

       protected void Submit_Click(object sender, EventArgs e)
        {
           Booking newBooking = new Booking();
           Job newJob = new Job();

           DropDownList selectVehicle = (DropDownList)this.customerVehicles.FindControl("vehiclesList");

           newBooking.setConfirmed(false);
           newBooking.setDate(SelectCalender.SelectedDate);
           newBooking.setTime(DateTime.ParseExact(time_hours.SelectedValue.ToString() + ":" + time_minutes.SelectedValue.ToString(), "HH:mm", null));
           newBooking.setVehicleRegistrationNumber(selectVehicle.SelectedValue.ToString());
           newJob.setDescription(add_jobDescription.Text);
           
           int bookingIdTemp = Convert.ToInt32(mysqlconnection.Insert(newBooking.InsertBooking()));
           newJob.setBookingID(bookingIdTemp);
           mysqlconnection.Insert(newJob.InsertJob());

           Response.Redirect("MyVehicles.aspx");
        }

    }
}