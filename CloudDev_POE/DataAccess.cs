using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CloudDev_POE
{
    public class DataAccess
    {
        static string connectionString = "Server=tcp:serverst10374462.database.windows.net,1433;Initial Catalog=KhumaloCraftDBAS;Persist Security Info=False;User ID=st10374462;Password=Yang-1209man;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        static SqlConnection connection = new SqlConnection(connectionString);
        static SqlCommand command;

        public void ConnectionToDatabase() { connection.Open(); }
        public void DisconnectionFromDatabase() { connection.Close(); }
        public void SignUp(string name, string email, string password)
        {
            connection.Open();
            string sql = "INSERT INTO Users(name, email, password, staff) VALUES (@name, @email, @password, @staff)";
            command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@email", email));
            command.Parameters.Add(new SqlParameter("@password", password));
            command.Parameters.Add(new SqlParameter("@staff", "no"));
            command.ExecuteNonQuery();
            connection.Close();

        }
        public User LoginUser(string email, string password)
        {
            connection.Open();
            string sql = "SELECT * FROM Users WHERE email = @email AND password = @password";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("Password", password);
            command.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                User temp = new User
                {
                    Email = dt.Rows[0]["email"].ToString(),
                    Name = dt.Rows[0]["name"].ToString(),
                    Staff = dt.Rows[0]["staff"].ToString().Equals("yes"),
                    Id = Convert.ToInt32(dt.Rows[0]["userId"].ToString())
                };
                return temp;
            }
        }

        public List<Product> GetAllProduct()
        {
            List<Product> art = new List<Product>();
            connection.Open();
            string sql = "SELECT * FROM Products";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            connection.Close();

            foreach (DataRow row in dataTable.Rows)
            {
                art.Add(new Product
                {
                    productID = Convert.ToInt32(row["productID"].ToString()),
                    name = row["name"].ToString(),
                    description = row["description"].ToString(),
                    price = Convert.ToDouble(row["price"].ToString()),
                    imageSRC = row["imageSRC"].ToString()
                });
            }
            return art;
        }

        public void SubmitOrder(List<Product> cart)
        {
            double total = 0;
            string orderString = "";
            foreach (Product item in cart)
            {
                total += item.price;
                orderString += $"{item.name},";
            }


            connection.Open();
            string sql = "INSERT INTO Orders VALUES (@orderTime, @orderTotal, @orderDetails, @userId, @status)";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@orderTime", DateTime.Now);
            command.Parameters.AddWithValue("@orderTotal", total);
            command.Parameters.AddWithValue("@orderDetails", orderString);
            command.Parameters.AddWithValue("@status", "open");
            command.Parameters.AddWithValue("@userId", UserHolder.loggedInUser.Id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            connection.Open();
            string sql = "SELECT * FROM Orders";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order
                {
                    orderID = Convert.ToInt32(row["orderID"].ToString()),
                    orderTime = DateTime.Parse(row["orderTime"].ToString()),
                    orderTotal = Double.Parse(row["orderTotal"].ToString()),
                    orderDetails = row["orderDetails"].ToString(),
                    userID = Convert.ToInt32(row["userID"].ToString()),
                    status = row["status"].ToString()

                });
            }
            return orders;
        }

        public List<Order> GetUserOrders(int userID)
        {
            List<Order> orders = new List<Order>();
            connection.Open();
            string sql = "SELECT * FROM Orders WHERE userID = @userID";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@userID", userID);
            command.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order
                {
                    orderID = Convert.ToInt32(row["orderID"].ToString()),
                    orderTime = DateTime.Parse(row["orderTime"].ToString()),
                    orderTotal = Double.Parse(row["orderTotal"].ToString()),
                    orderDetails = row["orderDetails"].ToString(),
                    userID = Convert.ToInt32(row["userID"].ToString()),
                    status = row["status"].ToString()

                });
            }
            return orders;
        }

        public void UpdateOrder(int orderID, string updatedStatus)
        {
            connection.Open();
            string sql = "UPDATE Orders SET status = @updatedStatus WHERE orderid = @orderID";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@updatedStatus", updatedStatus);
            command.Parameters.AddWithValue("@orderID", orderID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddNewProduct(Product item)
        {
            connection.Open();
            string sql = "INSERT INTO Menu VALUES (@name, @description, @price, @imageURL)";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", item.name);
            command.Parameters.AddWithValue("@description", item.description);
            command.Parameters.AddWithValue("@price", item.price);
            command.Parameters.AddWithValue("@imageURl", item.imageSRC);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}