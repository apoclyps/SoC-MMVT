using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace MMVT
{
    public partial class MyVehicles : System.Web.UI.Page
    {
        private MySql mysqlconnection;
        private bool vehicleAdded = false;
        private Customer thisCustomer;
        private Address thisAddress;
        private Vehicle thisVehicle;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["customerId"] == null)
            {
                Response.Redirect("../LoginForm.aspx");
            }

            mysqlconnection = new MySql();
            mysqlconnection.OpenConnection();

            thisCustomer = Customer.selectByCustomerId(Convert.ToInt32(Session["customerId"]), mysqlconnection);
            thisAddress = Address.selectByCustomerId(Convert.ToInt32(Session["customerId"]), mysqlconnection);

            lblUpdated.Visible = false;
            txtAddRegistrationNumber.MaxLength = 10;
            txtRegistartionNumber.MaxLength = 10;
            txtYear.MaxLength = 4;
            txtAddYear.MaxLength = 4;

            if (vehicleAdded)
            {
                btnAddNewVehicle.Visible = true;
                AddVehiclePanel.Visible = false;
                CustomerPanel.Visible = false;
                editVehiclePanel.Visible = false;
                displayListOfVehicles();     
            }
            else
            {
                btnAddVehicle.Visible = false;
                editVehiclePanel.Visible = false;
                AddVehiclePanel.Visible = false;

                displayPersonalDetails();
            }

            Model[] allModels = Model.selectAll(mysqlconnection);
            for (int i = 0; allModels != null && i < allModels.Length; i++)
            {
                AddModel.Items.Add(new ListItem(allModels[i].getDescription(), Convert.ToString(allModels[i].getModelId())));
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            mysqlconnection.CloseConnection();
        }

        public void manageDetails_onClick(object sender, EventArgs e)
        {
            btnAddVehicle.Visible = false;
            CustomerPanel.Visible = true;
            editVehiclePanel.Visible = false;

            displayPersonalDetails();
        }

        protected void displayPersonalDetails()
        {
            // Select from Customer and Address and populate Personal details

            txtFirstName.Text = thisCustomer.getFirstName();
            txtSurname.Text = thisCustomer.getSurname();
            txtAddressLine1.Text = thisAddress.getAddressLine();
            txtAddressLine2.Text = thisAddress.getAddressLine2();
            txtTown.Text = thisAddress.getTown();
            txtPostcode.Text = thisAddress.getPostcode();
            txtPhone.Text = thisCustomer.getHomePhoneNumber();
            txtMobile.Text = thisCustomer.getMobilePhoneNumber();
            txtEmailAddress.Text = thisCustomer.getEmailAddress();

        }

        public void manageVehicles_onClick(object sender, EventArgs e)
        {
            btnAddVehicle.Visible = true;
            CustomerPanel.Visible = false;
            editVehiclePanel.Visible = false;

            displayListOfVehicles();
        }

        protected void UpdatePersonalDetails_onClick(object sender, EventArgs e)
        {
            //Response.Write(txtFirstName.Text);
            thisCustomer.setFirstName(txtFirstName.Text);
            thisCustomer.setSurname(txtSurname.Text);
            thisAddress.setAddressLine1(txtAddressLine1.Text);
            thisAddress.setAddressLine2(txtAddressLine2.Text);
            thisAddress.setTown(txtTown.Text);
            thisAddress.setPostcode(txtPostcode.Text);
            thisCustomer.setHomePhoneNumber(txtPhone.Text);
            thisCustomer.setMobilePhoneNumber(txtMobile.Text);
            thisCustomer.setEmailAddress(txtEmailAddress.Text);

            mysqlconnection.Update(thisCustomer.update());
            mysqlconnection.Update(thisAddress.update());

            // Some sort of feed back about updating
        }

        protected void displayListOfVehicles()
        {
            int customerId = Convert.ToInt32(Session["customerId"]);

            Vehicle[] selectedVehicles = Vehicle.selectByCustomerId(customerId, mysqlconnection);

            // Vehicle Details Label
            Label lblVehicle = new Label();
            lblVehicle.Text = "<h1>Vehicle Details</h1>";
            vehicleListPanel.Controls.Add(lblVehicle);

            if (selectedVehicles != null)
            {
                for (int i = 0; i < selectedVehicles.Length; i++)
                {
                    buildVehicleTable(selectedVehicles[i]);
                }
            }
        }


        protected void SaveVehicle_onClick(object sender, EventArgs e)
        {
           // btnSaveVehicle.Visible = true;
            btnAddNewVehicle.Visible = true;
            AddVehiclePanel.Visible = false;
            CustomerPanel.Visible = false;
            editVehiclePanel.Visible = false;
        }

        public void addVehicle_onClick(object sender, EventArgs e)
        {
           // btnSaveVehicle.Visible = false;
            btnAddNewVehicle.Visible = true;
            AddVehiclePanel.Visible = true;
            CustomerPanel.Visible = false;
            editVehiclePanel.Visible = false;
            AddVehiclePanel.Visible = true;

            displayListOfVehicles();

            // empties add vehicle text fields
            txtAddManufacturer.Text = "";
            txtAddMilage.Text = "";
            txtAddRegistrationNumber.Text = "";
            txtAddYear.Text = "";

            txtAddRegistrationNumber.Text = "";
        }

        public void addNewVehicle_onClick(object sender, EventArgs e)
        {

          //  int year = Convert.ToInt32(txtAddYear.Text);
            DateTime newYear = new DateTime(2012, 1, 1);
            // Insert new vehicle
            //String registrationNumber, int customerId, int modelId, int mileage, DateTime year, Boolean modified)
            thisVehicle = new Vehicle();

            thisVehicle.setRegistrationNumber(txtAddRegistrationNumber.Text);
            thisVehicle.setCustomerId(Convert.ToInt32(Session["customerId"]));
            thisVehicle.setModelId(Convert.ToInt32(AddModel.SelectedValue));
            thisVehicle.setManufacturer(txtAddManufacturer.Text);
            thisVehicle.setMileage(Convert.ToInt32(txtAddMilage.Text));
            
            thisVehicle.setYear(newYear);
            
            if (ddlAddModified.SelectedValue.Equals("No"))
            {
                thisVehicle.setModified(false);
              //  Response.Write(ddlAddModified.Text);
            }
            else
            {
                thisVehicle.setModified(true);
              //  Response.Write(ddlAddModified.Text);
            }
            

            mysqlconnection.Insert(thisVehicle.InsertVehicle());
          //  Response.Write("ERROR" + errorID);

            vehicleAdded = true;
         }

        protected void viewEditVehicle(object sender, CommandEventArgs e)
        {
            editVehiclePanel.Visible = true;
            Response.Write("Editing");
        }

        protected void deleteVehicle(object sender, CommandEventArgs e)
        {
            // if panel is display current vehicle then hide
            editVehiclePanel.Visible = false;

            Vehicle vehicleToRemove = Vehicle.selectByRegistrationNumber(Convert.ToString(e.CommandArgument), mysqlconnection);
            mysqlconnection.Delete(vehicleToRemove.delete());

          //  Response.Redirect("ScheduleForm.aspc");          
        }

        private void buildVehicleTable(Vehicle currentVehicle)
        {
            // Getting Vehicle Details
            Vehicle vehicleForBooking = Vehicle.selectByRegistrationNumber(currentVehicle.getRegistrationNumber(), mysqlconnection);

            // Creating new Table
            Table current_booking = new Table();
            current_booking.CssClass = "entry";

        // Vehicle Reg
            // New Table Row
            TableRow booking_r1 = new TableRow();
            booking_r1.CssClass = "entryHeaderRow";
            TableCell vehicleRegistrationNumber = new TableCell();
            TableCell vehicleRegistrationNumber_h = new TableCell();
            vehicleRegistrationNumber_h.Text = "Registration Number : ";
            vehicleRegistrationNumber_h.CssClass = "tableHeader";

            // Get Row ID
            vehicleRegistrationNumber.Text = currentVehicle.getRegistrationNumber().ToString();


            // build link buttons to manipulate individual booking
            TableCell booking_controls = new TableCell();
            booking_controls.ColumnSpan = 2;

            LinkButton viewEdit = new LinkButton();
            viewEdit.Text = "View/Edit";
            viewEdit.CommandArgument = currentVehicle.getRegistrationNumber().ToString();
            viewEdit.Command += new CommandEventHandler(viewEditVehicle);;



            booking_controls.Controls.Add(viewEdit);
            TableRow booking_r4 = new TableRow();
            booking_r4.CssClass = "entryHeaderRow";
            booking_r4.Cells.Add(booking_controls);

            // Add to Booking
            booking_r1.Cells.Add(vehicleRegistrationNumber_h);
            booking_r1.Cells.Add(vehicleRegistrationNumber);
            


       // TYPE
            // New Table Row
            TableRow booking_r2 = new TableRow();

            TableCell vehicleType = new TableCell();
            TableCell vehicleType_h = new TableCell();
            vehicleType_h.Text = "Vehicle Type : ";
            vehicleType_h.CssClass = "tableHeader";

            // Get Row ID
            vehicleType.Text = currentVehicle.GetType().ToString();

            // Add to Booking
            booking_r2.Cells.Add(vehicleType_h);
            booking_r2.Cells.Add(vehicleType);
        

      // Manufacturer
            // New Table Row
            TableRow booking_r3 = new TableRow();
           // booking_r3.CssClass = "entryHeaderRow";
            TableCell vehicleManufacturer = new TableCell();
            TableCell vehicleManufacturer_h = new TableCell();
            vehicleManufacturer_h.Text = "Manufacturer : ";
            vehicleManufacturer_h.CssClass = "tableHeader";
            vehicleManufacturer_h.Width = 140;

            // Get Row ID
            vehicleManufacturer.Text = currentVehicle.getManufacturer().ToString();


            LinkButton delete = new LinkButton();
            delete.Text = "Delete";
            delete.CommandArgument = currentVehicle.getRegistrationNumber().ToString();
            delete.Command += new CommandEventHandler(deleteVehicle);

            booking_controls.Controls.Add(delete);
            booking_r4.CssClass = "entryControls";

            // Add to Booking
            booking_r3.Cells.Add(vehicleManufacturer_h);
            booking_r3.Cells.Add(vehicleManufacturer);
            
            // add all the rows to the table
            current_booking.Rows.Add(booking_r1);
            current_booking.Rows.Add(booking_r2);
            current_booking.Rows.Add(booking_r3);
            current_booking.Rows.Add(booking_r4);
            vehicleListPanel.Controls.Add(current_booking);
        }
        
    }
}