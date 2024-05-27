using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudDev_POE
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        DataAccess da = new DataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserHolder.loggedInUser.Staff)
                {
                    divAdd.Visible = true;
                    lblWarning.Visible = false;
                }
                else
                {
                    divAdd.Visible = false;
                    lblWarning.Visible = true;
                }
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Product temp = new Product
            {
                name = txtName.Text.ToString(),
                description = txtDescription.Text.ToString(),
                imageSRC = txtURL.Text.ToString(),
                price = Convert.ToDouble(txtPrice.Text.ToString())
            };
            da.AddNewProduct(temp);
        }
    }
}