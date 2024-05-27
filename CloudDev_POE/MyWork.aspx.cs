using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudDev_POE
{
    public partial class MyWork : System.Web.UI.Page
    {
        DataAccess da = new DataAccess();
        static List<Product> product = new List<Product>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                product = da.GetAllProduct();
                productRepeater.DataSource = product;
                productRepeater.DataBind();
            }
        }

        protected void btnAddToCart_Command(object sender, CommandEventArgs e)
        {


        }


        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productID = Convert.ToInt32(btn.CommandArgument);

            foreach (Product product in product)
            {
                if (product.productID == productID)
                {
                    CartHolder.cart.Add(product);
                    break;
                }
            }
        }
    }
}