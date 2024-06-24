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

        private void ExecuteNonQuery(string sql, List<SqlParameter> parameters)
        {
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // Log exception
                throw new Exception("Database operation failed", ex);
            }
            finally
            {
                connection.Close();
            }
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

            string sql = "INSERT INTO Orders(orderTime, orderTotal, orderDetails, userID, status) VALUES (@orderTime, @orderTotal, @orderDetails, @userID, @status)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@orderTime", DateTime.Now),
                new SqlParameter("@orderTotal", total),
                new SqlParameter("@orderDetails", orderString),
                new SqlParameter("@userID", UserHolder.loggedInUser.Id),
                new SqlParameter("@status", "open")
            };

            ExecuteNonQuery(sql, parameters);
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
            string sql = "INSERT INTO Products VALUES (@name, @description, @price, @imageURL)";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", item.name);
            command.Parameters.AddWithValue("@description", item.description);
            command.Parameters.AddWithValue("@price", item.price);
            command.Parameters.AddWithValue("@imageSRC", item.imageSRC);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}