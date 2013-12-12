using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMVT.StaffPages
{
    public partial class WebForm1 : System.Web.UI.Page
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
            

            if (Session["customerId"] != null)
            {
                int customerId = Convert.ToInt32((string)Session["customerId"]);
                displayCustomerDetailsInPanel(customerId, customerDetails); 
                displayVehiclesForCustomerInPanel(customerId, customerVehicles);
            }

            Model[] allModels = Model.selectAll(mysqlconnection);
            for (int i = 0; allModels != null && i < allModels.Length; i++)
            {
                vehicleEditModel.Items.Add(new ListItem(allModels[i].getDescription(), Convert.ToString(allModels[i].getModelId())));
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            mysqlconnection.CloseConnection();
        }

        private void displayVehiclesForCustomerInPanel(int customerId, Panel targetPanel)
        {
            targetPanel.Controls.Clear();
            Table customerVehicles = new Table();

            Vehicle[] customerVehiclesFromDB = Vehicle.selectByCustomerId(customerId, mysqlconnection);

            if (customerVehiclesFromDB != null)
            {
                for (int i = 0; i < customerVehiclesFromDB.Length; i++)
                {
                    TableRow currentVehicle = new TableRow();
                    TableCell registration = new TableCell();
                    TableCell model = new TableCell();
                    TableCell viewDetails = new TableCell();
                    viewDetails.CssClass = "entryControls";

                    LinkButton vdLink = new LinkButton();
                    vdLink.Text = "View details";
                    vdLink.CommandArgument = customerVehiclesFromDB[i].getRegistrationNumber();
                    vdLink.Command += new CommandEventHandler(vehicleDetails_click);

                    viewDetails.Controls.Add(vdLink);

                    registration.Text = customerVehiclesFromDB[i].getRegistrationNumber();
                    model.Text = customerVehiclesFromDB[i].getModel();

                    currentVehicle.Cells.Add(registration);
                    currentVehicle.Cells.Add(model);
                    currentVehicle.Cells.Add(viewDetails);

                    customerVehicles.Rows.Add(currentVehicle);
                }
            }
            TableRow controlsR = new TableRow();
            TableCell controlsC = new TableCell();
            LinkButton addV = new LinkButton();
            controlsC.CssClass = "right";
            controlsC.ColumnSpan = 3;
            addV.Text = "+ Add a vehicle";
            addV.Click += new EventHandler(addVehicle_click);

            controlsC.Controls.Add(addV);
            controlsR.Cells.Add(controlsC);
            controlsR.CssClass = "entryControls";
            customerVehicles.Rows.Add(controlsR);

            targetPanel.Controls.Add(customerVehicles);
        }

        private void addVehicle_click(object sender, EventArgs e)
        {
            vehicleHistory.Controls.Clear();
            vehicleDetails.Visible = true;

            vehicleEditRegistration.Text = "";
            vehicleEditManufacturer.Text = "";
            vehicleEditMiles.Text = "";
            vehicleEditYear.Text = "";
            //vehicleEditType.SelectedIndex = 0;
            vehicleEditModel.SelectedIndex = 0;

            Session["saveVehicleMode"] = "new";
        }

        private void vehicleDetails_click(object sender, CommandEventArgs e)
        {
            vehicleDetails.Visible = true;
            vehicleHistory.Visible = true;

            vehicleHistory.Controls.Clear();

            Vehicle currentVehicle = Vehicle.selectByRegistrationNumber(e.CommandArgument.ToString(), mysqlconnection);
            displayDetailsForVehicleInPanel(currentVehicle, vehicleDetails);
        }

        public void saveVehicle_click(object sender, EventArgs e)
        {
            // if new vehicle
            if (Session["saveVehicleMode"] != null && Session["saveVehicleMode"].Equals("new"))
            {
                Vehicle newVehicle = new Vehicle();
                newVehicle.setCustomerId(Convert.ToInt32(Session["customerId"]));
                newVehicle.setManufacturer(vehicleEditManufacturer.Text);
                newVehicle.setModelId(Convert.ToInt32(vehicleEditModel.SelectedValue));
                newVehicle.setRegistrationNumber(vehicleEditRegistration.Text);
                newVehicle.setYear(new DateTime(Convert.ToInt32(vehicleEditYear.Text), 1,1 ));
                newVehicle.setMileage(Convert.ToInt32(vehicleEditMiles.Text));

                mysqlconnection.Insert(newVehicle.InsertVehicle());

                Session["saveVehicleMode"] = "edit";

                displayVehiclesForCustomerInPanel(Convert.ToInt32(Session["customerId"]), customerVehicles);
            } 
            else // if existing vehicle
            {
                // update
            }
        }

        private void displayDetailsForVehicleInPanel(Vehicle displayVehicle, Panel targetPanel)
        {
            // set fields to object info on form
            vehicleEditRegistration.Text = displayVehicle.getRegistrationNumber();
            vehicleEditManufacturer.Text = displayVehicle.getManufacturer();
            vehicleEditMiles.Text = Convert.ToString(displayVehicle.getMileage());
            //vehicleEditModel.SelectedValue = displayVehicle.getModel();
            vehicleEditYear.Text = Convert.ToDateTime(displayVehicle.getYear()).ToShortDateString();

            // select jobs for vehicle from database

            // for each job
                //addJobEntryToPanel("1", vehicleHistory);
                //addJobEntryToPanel("2", vehicleHistory);
                //addJobEntryToPanel("3", vehicleHistory);
            //end loop

        }

        private void addJobEntryToPanel(string id, Panel targetPanel) // amend to take job oject instead of string id
        {
            Table currentJob = new Table();
            currentJob.CssClass = "entry";

            TableRow jobR1 = new TableRow();
            jobR1.CssClass = "entryHeaderRow";
            TableRow jobR2 = new TableRow();
            TableRow jobR3 = new TableRow();
            jobR3.CssClass = "entryHeaderRow";

            TableCell job_date = new TableCell();
            TableCell job_description = new TableCell();
            TableCell hours_worked = new TableCell();

            job_date.Text = "Date: " + "12/03/2012"; // amend to include method call for actual date
            job_date.CssClass = "tableHeader";
            job_description.Text = "Description: <br />" + "description....."; // amend to include method call for actual description
            hours_worked.Text = "Hours worked: " + "5"; // replace to include object method call for number
            hours_worked.CssClass = "tableHeader";

            jobR1.Cells.Add(job_date);
            jobR2.Cells.Add(job_description);
            jobR3.Cells.Add(hours_worked);

            currentJob.Rows.Add(jobR1);
            currentJob.Rows.Add(jobR2);
            currentJob.Rows.Add(jobR3);

            targetPanel.Controls.Add(currentJob);


        }

        public void editCustomerClick(object sender, CommandEventArgs e)
        {
            Customer customerFromDb = Customer.selectByCustomerId(Convert.ToInt32(e.CommandArgument), mysqlconnection);
            Address customerAddress = Address.selectByCustomerId(Convert.ToInt32(e.CommandArgument), mysqlconnection);

            edit_firstName.Text = customerFromDb.getFirstName();
            edit_surname.Text = customerFromDb.getSurname();
            edit_addressL1.Text = customerAddress.getAddressLine();
            edit_addressL2.Text = customerAddress.getAddressLine2();
            edit_town.Text = customerAddress.getTown();
            edit_postcode.Text = customerAddress.getPostcode();
            edit_email.Text = customerFromDb.getEmailAddress();
            edit_homePhone.Text = customerFromDb.getHomePhoneNumber();
            edit_mobilePhone.Text = customerFromDb.getMobilePhoneNumber();

            customerDetails.Visible = false; // swaps between the customer details table and customer details edit form
            editCustomerDetails.Visible = true;
        }

        public void saveCustomerClick(object sender, EventArgs e)
        {

            Customer customerToSave = Customer.selectByCustomerId(Convert.ToInt32(Session["customerId"]), mysqlconnection);
            Address addressToSave = Address.selectByCustomerId(Convert.ToInt32(Session["customerId"]), mysqlconnection);

            customerToSave.setEmailAddress(edit_email.Text);
            customerToSave.setFirstName(edit_firstName.Text);
            customerToSave.setHomePhoneNumber(edit_homePhone.Text);
            customerToSave.setMobilePhoneNumber(edit_mobilePhone.Text);
            customerToSave.setSurname(edit_surname.Text);
            addressToSave.setAddressLine1(edit_addressL1.Text);
            addressToSave.setAddressLine2(edit_addressL2.Text);
            addressToSave.setPostcode(edit_postcode.Text);
            addressToSave.setTown(edit_town.Text);

            mysqlconnection.Update(addressToSave.update());
            mysqlconnection.Update(customerToSave.update());
            customerDetails.Visible = true;  // swap back to customer details table
            displayCustomerDetailsInPanel(customerToSave.getCustomerId(), customerDetails);
            editCustomerDetails.Visible = false;
        }

    
        private void displayCustomerDetailsInPanel(int customerId, Panel targetPanel)
        {
            targetPanel.Controls.Clear();

            Customer customerFromDb = Customer.selectByCustomerId(customerId, mysqlconnection);
            Address customerAddress = Address.selectByCustomerId(customerId, mysqlconnection);

            Table current_customer = new Table();

            TableCell customerName = new TableCell();
            TableCell customeraddressL1 = new TableCell();
            TableCell customeraddressL2 = new TableCell();
            TableCell customerTown = new TableCell();
            TableCell customerPostCode = new TableCell();
            TableCell customerEmail = new TableCell();
            TableCell customerMPhone = new TableCell();
            TableCell customerHPhone = new TableCell();

            customerName.Text =  customerFromDb.getFirstName() + " " + customerFromDb.getSurname();
            customerName.ColumnSpan = 2;
            customeraddressL1.Text = customerAddress.getAddressLine();
            customeraddressL2.Text = customerAddress.getAddressLine2();
            customeraddressL1.ColumnSpan = 2;
            customeraddressL2.ColumnSpan = 2;
            customerEmail.Text = customerFromDb.getEmailAddress();
            customerMPhone.Text = customerFromDb.getMobilePhoneNumber();
            customerMPhone.ColumnSpan = 2;
            customerHPhone.Text = customerFromDb.getHomePhoneNumber();
            customerHPhone.ColumnSpan = 2;

            TableRow nameRow = new TableRow();
            TableRow addressRow1 = new TableRow();
            TableRow addressRow2 = new TableRow();
            TableRow townCodeRow = new TableRow();
            TableRow emailRow = new TableRow();
            TableRow mPhoneRow = new TableRow();
            TableRow hPhoneRow = new TableRow();

            nameRow.Cells.Add(customerName);
            addressRow1.Cells.Add(customeraddressL1);
            addressRow2.Cells.Add(customeraddressL2);
            townCodeRow.Cells.Add(customerTown);
            townCodeRow.Cells.Add(customerPostCode);
            emailRow.Cells.Add(customerEmail);
            mPhoneRow.Cells.Add(customerMPhone);
            hPhoneRow.Cells.Add(customerHPhone);


            LinkButton editBtn = new LinkButton();
            editBtn.Text = "Edit";
            editBtn.CommandArgument = Convert.ToString(customerFromDb.getCustomerId());
            editBtn.Command += new CommandEventHandler(editCustomerClick);

            TableCell controls = new TableCell();
            controls.ColumnSpan = 2;
            controls.CssClass = "entryControls";
            controls.Controls.Add(editBtn);

            TableRow controlRow = new TableRow();
            controlRow.Cells.Add(controls);


            current_customer.Rows.Add(nameRow);
            current_customer.Rows.Add(addressRow1);
            current_customer.Rows.Add(addressRow2);
            current_customer.Rows.Add(townCodeRow);
            current_customer.Rows.Add(emailRow);
            current_customer.Rows.Add(hPhoneRow);
            current_customer.Rows.Add(mPhoneRow);
            current_customer.Rows.Add(controlRow);
            

            targetPanel.Controls.Add(current_customer);
        }
    }
}