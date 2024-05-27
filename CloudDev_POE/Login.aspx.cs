using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudDev_POE
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        DataAccess da = new DataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            User temp = da.LoginUser(edtLoginEmail.Text.ToString(), edtLoginPassword.Text.ToString());
            if (temp != null)
            {
                Response.Redirect("~/Home");
            }
            else
            {
                //https://www.bing.com/ck/a?!&&p=9cd484036165ea24JmltdHM9MTcxNjc2ODAwMCZpZ3VpZD0yNzhlYWMyNy1jZTliLTYxZTMtMGM1ZS1iODBjY2ZlMjYwMDkmaW5zaWQ9NTUxMA&ptn=3&ver=2&hsh=3&fclid=278eac27-ce9b-61e3-0c5e-b80ccfe26009&psq=switching+web+forms+on+button+click&u=a1aHR0cHM6Ly9zdGFja292ZXJmbG93LmNvbS9xdWVzdGlvbnMvMjM5NzY2ODMvYXNwLW5ldC1idXR0b24tdG8tcmVkaXJlY3QtdG8tYW5vdGhlci1wYWdlIzp-OnRleHQ9WW91JTIwY2FuJTIwZWl0aGVyJTIwZG8lMjBhJTIwUmVzcG9uc2UuUmVkaXJlY3QlMjglMjJZb3VyUGFnZS5hc3B4JTIyJTI5JTNCJTIwb3IsYSUyMFNlcnZlci5UcmFuc2ZlciUyOCUyMllvdXJQYWdlLmFzcHglMjIlMjklM0IlMjBvbiUyMHlvdXIlMjBidXR0b24lMjBjbGljayUyMGV2ZW50Lg&ntb=1
                Response.Redirect("SignUp.aspx");
            }
        }
    }
}