﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudDev_POE
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataAccess da = new DataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            da.SignUp(edtName.Text, edtSignUpEmail.Text, edtSignUpPassword.Text);
            Response.Redirect("Login.aspx");
        }

    }
}