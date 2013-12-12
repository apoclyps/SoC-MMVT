using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMVT
{
    public partial class CustomerSelect : System.Web.UI.Page
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

            
            if (Session["searchResults"] != null && Session["searchResults"].Equals("true"))
            {
                buildCustomerListForSet(Customer.selectSearch(Session["searchFName"].ToString(), Session["searchSurname"].ToString(), Session["searchEmail"].ToString(), Session["searchVehicle"].ToString(), mysqlconnection));
                Session["searchResults"] = false;
            }
            else
            {
                buildCustomerListForSet(Customer.selectAll(mysqlconnection));
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            mysqlconnection.CloseConnection();
        }

        public void search_click(object sender, EventArgs e)
        {
            // session hack to work around dynamic controls / page reload issue
            Session["searchResults"] = "true";
            Session["searchFName"] = search_firstName.Text;
            Session["searchSurname"] = search_surname.Text;
            Session["searchVehicle"] = search_vehicleReg.Text;
            Session["searchEmail"] = search_emailAddress.Text;
            Response.Redirect("CustomerSelect.aspx");
        }

        public void selectVehicle_click(object sender, CommandEventArgs e)
        {
            if (Session["jobToInsert"] != null && Session["bookingToInsert"] != null)
            {
                Job currentJob = (Job)Session["jobToInsert"];
                Booking currentBooking = (Booking)Session["bookingToInsert"];

                currentBooking.setVehicleRegistrationNumber(e.CommandArgument.ToString());

                int bookingId = Convert.ToInt32(mysqlconnection.Insert(currentBooking.InsertBooking()));
                currentJob.setBookingID(bookingId);
                mysqlconnection.Insert(currentJob.InsertJob());

                Response.Redirect("ScheduleForm.aspx");
            }
        }

        private void buildCustomerListForSet(Customer[] customerList)
        {
            CustomersPanel.Controls.Clear();
            if (customerList == null)
            {
                customerList = Customer.selectAll(mysqlconnection);
            }

            for (int i = 0; customerList != null && i < customerList.Length; i++)
            {
                buildCustomerTableForCustomer(customerList[i], CustomersPanel);
            }
        }

        private void buildCustomerTableForCustomer(Customer thisCustomer, Panel customerPanel)
        {
            Table current_customer = new Table();
            current_customer.CssClass = "entry";

            TableCell customerFName = new TableCell();
            TableCell customerSurname = new TableCell();
            TableCell customerEmail = new TableCell();
            TableCell customerPhone = new TableCell();

            customerFName.Text = thisCustomer.getFirstName();
            customerSurname.Text = thisCustomer.getSurname();
            customerEmail.Text = thisCustomer.getEmailAddress();
            customerPhone.Text = thisCustomer.getHomePhoneNumber();


            TableRow customerNameR = new TableRow();
            customerNameR.CssClass = "entryHeaderRow";

            TableRow customerContactR = new TableRow();
            customerContactR.CssClass = "entryHeaderRow";

            customerNameR.Cells.Add(customerFName);
            customerNameR.Cells.Add(customerSurname);

            customerContactR.Cells.Add(customerEmail);
            customerContactR.Cells.Add(customerPhone);

            current_customer.Rows.Add(customerNameR);
            current_customer.Rows.Add(customerContactR);


            Vehicle[] vehiclesForCustomer = Vehicle.selectByCustomerId(thisCustomer.getCustomerId(), mysqlconnection);
            if (vehiclesForCustomer != null)
            {
                for (int i = 0; i < vehiclesForCustomer.Length; i++)
                {
                    TableRow customerVehicle = new TableRow();
                    TableCell vehicleRegistration = new TableCell();
                    vehicleRegistration.Text = vehiclesForCustomer[i].getRegistrationNumber();
                    vehicleRegistration.CssClass = "customerSlectRegstrationL";
                    

                    TableCell vehicleSelect = new TableCell();
                    LinkButton selectVehicle = new LinkButton();
                    vehicleSelect.Controls.Add(selectVehicle);
                    vehicleSelect.CssClass = "customerSlectRegstrationR";
                    selectVehicle.Text = "Select";
            
                    selectVehicle.CommandArgument = vehiclesForCustomer[i].getRegistrationNumber();
                    selectVehicle.Command += new CommandEventHandler(selectVehicle_click);

                    customerVehicle.Cells.Add(vehicleRegistration);
                    customerVehicle.Cells.Add(vehicleSelect);
                    current_customer.Rows.Add(customerVehicle);
                }
            }

            customerPanel.Controls.Add(current_customer);
        } 
    }
}