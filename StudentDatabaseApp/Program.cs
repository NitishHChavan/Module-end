using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentDatabaseApp
{
    class Program
    {
        static SqlConnection connection;
        static string connectionString = "Server=localhost;Database=StudentsDB;User Id=root;Password=Nitish@1996;";

        static void Main(string[] args)
        {
            connection = new SqlConnection(connectionString);

            while (true)
            {
                Console.WriteLine("----- Student Database Application -----");
                Console.WriteLine("1. Insert Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.WriteLine("----------------------------------------");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InsertStudent();
                        break;
                    case "2":
                        ViewAllStudents();
                        break;
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void InsertStudent()
        {
            Console.WriteLine("----- Insert Student -----");
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();
            Console.Write("Enter student roll number: ");
            string rollNumber = Console.ReadLine();
            Console.Write("Enter student marks: ");
            int marks = Convert.ToInt32(Console.ReadLine());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Students (Name, RollNumber, Marks) VALUES (@Name, @RollNumber, @Marks)", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@RollNumber", rollNumber);
                command.Parameters.AddWithValue("@Marks", marks);
                command.ExecuteNonQuery();
                Console.WriteLine("Student inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        static void ViewAllStudents()
        {
            Console.WriteLine("----- View All Students -----");
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Name, RollNumber, Marks FROM Students", connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Name\t\tRoll Number\tMarks");
                Console.WriteLine("----------------------------------------");
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string rollNumber = reader.GetString(1);
                    int marks = reader.GetInt32(2);
                    Console.WriteLine($"{name}\t\t{rollNumber}\t\t{marks}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        static void UpdateStudent()
        {
            Console.WriteLine("----- Update Student -----");
            Console.Write("Enter student roll number: ");
            string rollNumber = Console.ReadLine();
            Console.Write("Enter new marks: ");
            int newMarks = Convert.ToInt32(Console.ReadLine());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Students SET Marks = @Marks WHERE RollNumber = @RollNumber", connection);
                command.Parameters.AddWithValue("@Marks", newMarks);
                command.Parameters.AddWithValue("@RollNumber", rollNumber);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Student updated successfully.");
                else
                    Console.WriteLine("Student not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        static void DeleteStudent()
        {
            Console.WriteLine("----- Delete Student -----");
            Console.Write("Enter student roll number: ");
            string rollNumber = Console.ReadLine();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Students WHERE RollNumber = @RollNumber", connection);
                command.Parameters.AddWithValue("@RollNumber", rollNumber);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Student deleted successfully.");
                else
                    Console.WriteLine("Student not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
