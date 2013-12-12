using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMVT
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            password.TextMode = TextBoxMode.Password;

            Session["account"] = null;

            

        }

        public void loginClick(object sender, EventArgs e)
        {
            MySql connection = new MySql();
            try
            {
                connection.CreateConnection();
                connection.OpenConnection();

                if ((Session["account"] = Account.selectByUsername(username.Text, connection)) != null && ((Account)Session["account"]).checkPassword(password.Text))
                {
                    if (((Account)Session["account"]).gettype().Equals("Admin"))
                    {
                        Session["staffMember"] = Staff.selectByUsername(username.Text, connection);
                        Response.Redirect("~/StaffPages/ScheduleForm.aspx");
                    }

                    else 
                    {
                        // Customer object
                        Customer currentcustomer = Customer.selectByAccountUsername(username.Text, connection);
                        Session["customerId"] = currentcustomer.getCustomerId();
                        Response.Redirect("~/CustomerPages/MyVehicles.aspx");
                    }
                }
                else
                {
                    Response.Write("Incorrect login.");

                }
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public void signupClick(object sender, EventArgs e)
        {
            MySql connection = new MySql();
                connection.CreateConnection();
                connection.OpenConnection();
            Account thisAcc = Account.selectByUsername(username.Text, connection);
            thisAcc.setpassword(password.Text);
            connection.Update(thisAcc.updatePassword());
            connection.CloseConnection();
        }
    }
}