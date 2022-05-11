using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //for generating a MessageBox upon encountering an error


namespace Group24_assignment3
{
    
    abstract class studentGroup
    {
        //If including error reporting within this class (as this sample does) then you'll need a way
        //to control whether the errors are actually shown or silently ignored, since once you have
        //connected the GUI to the Boss object then the GUI designer will execute its code, which
        //will try to connect to the database to load live data into the GUI at design time.
        private static bool reportingErrors = false;

        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "gmis";
        private const string user = "gmis";
        private const string pass = "gmis";
        private const string server = "alacritas.cis.utas.edu.au";

       private static MySqlConnection conn = null;

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }


        //For step 2.2 in Week 8 tutorial
        public static List<Student> LoadAllStudents()
        {
            List <Student> students = new List<Student>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select student_id, given_name, family_name from student", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Note that in your assignment you will need to inspect the *type* of the
                    //employee/researcher before deciding which kind of concrete class to create.
                    students.Add(new Student { student_id = rdr.GetInt32(0), given_name = rdr.GetString(1), family_name = rdr.GetString(2) });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading student", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return students;
        }

        //For step 2.3 in Week 8 tutorial
      
        public static List<Group> LoadAllGroups(int group_id)
        {
            List<Group> groups = new List<Group>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select group_id, group_name from studentGroup", conn);

                cmd.Parameters.AddWithValue("id", group_id);
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    groups.Add(new Group
                    {
                        group_id = rdr.GetInt32(0),
                        //group_name = rdr.GetInt32(1)
                    });
                
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading all groups", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return groups;
        }
        
        /// <summary>
        /// In a more complete application this error would be logged to a file
        /// and the error reported back to the original caller, who is closer
        /// to the GUI and hence better able to produce the error message box
        /// (which would not show the actual error details like this does).
        /// </summary>
        private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
