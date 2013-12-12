using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMVT
{
    public partial class CustomerList : System.Web.UI.Page
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
            Response.Redirect("CustomerList.aspx");
        }

        public void add_click(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer();
            newCustomer.setEmailAddress(add_email.Text);
            newCustomer.setFirstName(add_firstName.Text);
            newCustomer.setSurname(add_surname.Text);
            newCustomer.setHomePhoneNumber(add_homePhone.Text);
            newCustomer.setMobilePhoneNumber(add_mobilePhone.Text);

            string customerInsertId = mysqlconnection.Insert(newCustomer.InsertCustomerWithoutAccount());
            int customerInsertIdInt;
            if (customerInsertId != null)
            {
                customerInsertIdInt = Convert.ToInt32(customerInsertId);
                newCustomer.setCustomerId(customerInsertIdInt);
                Address newCustomerAddress = new Address(customerInsertIdInt, add_addressL1.Text, add_addressL2.Text, add_town.Text, add_postcode.Text);
                mysqlconnection.Insert(newCustomerAddress.InsertAddress());
                buildCustomerListForSet(Customer.selectAll(mysqlconnection));
            }
            else
            {
                // handle insert error
                Response.Write("some kind of db insert error happened");
            }

            add_firstName.Text = "";
            add_surname.Text = "";
            add_addressL1.Text = "";
            add_addressL2.Text = "";
            add_town.Text = "";
            add_postcode.Text = "";
            add_homePhone.Text = "";
            add_mobilePhone.Text = "";
            add_email.Text = "";
        }

        public void viewDetails_click(object sender, CommandEventArgs e)
        {
            Session["customerId"] = ((LinkButton)sender).CommandArgument;
            Response.Redirect("CustomerDetails.aspx");
        }

        private void buildCustomerListForSet(Customer[] customerList)
        {
            CustomersPanel.Controls.Clear();
            if (customerList == null)
            {
                Customer.selectAll(mysqlconnection);
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

            LinkButton viewDetails = new LinkButton();
            viewDetails.Text = "View details";
            viewDetails.CommandArgument = Convert.ToString(thisCustomer.getCustomerId());
            viewDetails.Command += new CommandEventHandler(viewDetails_click);
            
            TableCell customerFName = new TableCell();
            TableCell customerSurname = new TableCell();
            TableCell customerEmail = new TableCell();
            TableCell customerPhone = new TableCell();
            TableCell customerDetails = new TableCell();
            customerDetails.Controls.Add(viewDetails);
            customerDetails.ColumnSpan = 2;

            customerFName.Text = thisCustomer.getFirstName();
            customerFName.Width = 300;
            customerSurname.Text = thisCustomer.getSurname();
            customerEmail.Text = thisCustomer.getEmailAddress();
            customerPhone.Text = thisCustomer.getHomePhoneNumber();


            TableRow customerNameR = new TableRow();
            customerNameR.CssClass = "entryHeaderRow";

            TableRow customerContactR = new TableRow();
            TableRow customerOptions = new TableRow();
            customerOptions.CssClass = "entryControls";

            customerNameR.Cells.Add(customerFName);
            customerNameR.Cells.Add(customerSurname);


            customerContactR.Cells.Add(customerEmail);
            customerContactR.Cells.Add(customerPhone);

            customerOptions.Cells.Add(customerDetails);

            current_customer.Rows.Add(customerNameR);
            current_customer.Rows.Add(customerContactR);
            current_customer.Rows.Add(customerOptions);

            customerPanel.Controls.Add(current_customer);
        } 
    }
}