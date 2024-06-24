using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudDev_POE
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        DataAccess da = new DataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CartHolder.cart.Count == 0)
            {
                lblEmpty.Visible = true;
                dgCart.Visible = false;
                btnClearCart.Visible = false;
                btnSubmitOrder.Visible = false;
            }
            else
            {
                dgCart.DataSource = CartHolder.cart;
                dgCart.DataBind();

                // Ensure there are enough columns before removing
                if (dgCart.Columns.Count > 0)
                {
                    dgCart.Columns.RemoveAt(0); // Remove first column if it exists
                }

                if (dgCart.Columns.Count > 3)
                {
                    dgCart.Columns.RemoveAt(3); // Remove fourth column if it exists
                }

                lblEmpty.Visible = false;
                dgCart.Visible = true;
                btnClearCart.Visible = true;
                btnSubmitOrder.Visible = true;

            }
        }
        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            da.SubmitOrder(CartHolder.cart);
        }

        protected void btnClearCart_Click(object sender, EventArgs e)
        {
            CartHolder.cart.Clear();
        }
    }
}