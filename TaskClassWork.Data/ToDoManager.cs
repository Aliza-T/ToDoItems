using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskClassWork.Data
{
    public class ToDoItems
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ToDoManager
    {
        private string _connectionString;
        public ToDoManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<ToDoItems> GetToDoList()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM ToDoItems td
                                JOIN Categories c
                                ON td.CategoryId = c.Id
                                WHERE td.IsCompleted = 0";
            connection.Open();
            List<ToDoItems> results = new List<ToDoItems>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new ToDoItems
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    IsCompleted = (bool)reader["IsCompleted"],
                    CategoryId = (int)reader["CategoryId"],
                    CategoryName = (string)reader["Name"]
                });
            }

            connection.Close();
            connection.Dispose();
            return results;

        }
        public IEnumerable<Category> GetCategories()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * From Categories";
            connection.Open();
            List<Category> categories = new List<Category>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"]
                });
            }
            connection.Close();
            connection.Dispose();
            return categories;

        }

        public void MarkAsCompleted(int Id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Update ToDoItems " +
                "SET IsCompleted = 1 " +
                "Where Id = @Id ";
            cmd.Parameters.AddWithValue("@Id", Id);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }
        public void AddToDoItem(ToDoItems ToDoItem)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO ToDoItems ( Title, DueDate, IsCompleted, CategoryId) " +
                "VALUES(@Title, @Date, 0, @CategoryId) ";
            cmd.Parameters.AddWithValue("@Title", ToDoItem.Title);
            cmd.Parameters.AddWithValue("@Date", ToDoItem.DueDate);
            cmd.Parameters.AddWithValue("@IsCompleted", ToDoItem.IsCompleted);
            cmd.Parameters.AddWithValue("@CategoryId", ToDoItem.CategoryId);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }
        public void AddCategory(Category c)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Categories (Name) " +
                "VALUES(@Name) ";
            cmd.Parameters.AddWithValue("@Name", c.Name);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }
        public Category CatInfo(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Categories WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            Category c = new Category();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                c.Id = (int)reader["Id"];
                c.Name = (string)reader["Name"];

            }
            connection.Close();
            connection.Dispose();
            return c;
        }
        public void UpdateCategory(Category c)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Update Categories" +
                " Set Name= @Name " +
                "Where Id = @Id ";
            cmd.Parameters.AddWithValue("@Name", c.Name);
            cmd.Parameters.AddWithValue("@Id", c.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }
        public IEnumerable<ToDoItems> GetBasedOnCat(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM ToDoItems where CategoryId = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            List<ToDoItems> results = new List<ToDoItems>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new ToDoItems
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    IsCompleted = (bool)reader["IsCompleted"],
                });
            }

            connection.Close();
            connection.Dispose();
            return results;

        }
    }
}
