using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using System.Reflection;
namespace ConsoleApplicationDemo
{
    public class Customer
    {
        public int id { get; set; }
        public String name { get; set; }

        public Customer()
        {
            id = 0;
            name = "Bla";
        }

        public void addName(String name)
        {
            Console.WriteLine("adding the name" + name);
        }
        public void addName(int name)
        {
            Console.WriteLine("add the name of int type " + name);
        }
    }


    public partial class RegistrationDBContext
    {
        public void Display()
        {
            Console.WriteLine("Testing");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Class Part
            
            Customer c = new Customer();
            c.addName("Kla");
            c.addName(2);

            Type typeclass = Type.GetType("ConsoleApplicationDemo.Customer");
            Console.WriteLine( "Class Name " +  typeclass.Name);

            List<PropertyInfo> classProperties = typeclass.GetProperties().ToList();
            foreach (var classProperty in classProperties)
            {
                Console.WriteLine("Class Property Value: " + classProperty.GetValue(c,null));
                Console.WriteLine("Class Property Name: " + classProperty.Name);
                Console.WriteLine("Class Property Type: " + classProperty.PropertyType.Name  + "\n");
            }

            List<MethodInfo> classMethods = typeclass.GetMethods().ToList();
            foreach (var classMethod in classMethods)
            {
                Console.WriteLine("Class Method Name: " + classMethod.Name);
                Console.WriteLine("Class Method Type: " + classMethod.ReturnType.Name + "\n");
            }


            //Creating Class and then getting its property and setting those from console
            object classInstance = Activator.CreateInstance(typeclass);
            String property;
            Console.WriteLine("Enter the property which you want to set --> id or name: ");
            property = Console.ReadLine();
            PropertyInfo idname = typeclass.GetProperty(property);
            Console.WriteLine("Enter the " + property + " which you want to set: ");
            var input = Console.ReadLine();
            idname.SetValue(classInstance, input, null);  //so it saves us from if else. no hardcoding..

            Console.WriteLine ( idname.Name + ": " + idname.GetValue(classInstance,null)) ;

            Console.WriteLine("\nTesting\n");

            //Database part


            RegistrationDBContext db = new RegistrationDBContext();
            db.Connection.Open();
            IDbTransaction transaction = db.Connection.BeginTransaction();
            try
            {
                RegistrationDBContext db1 = new RegistrationDBContext();
                db1.Display();
                RegistrationTable1 t = new RegistrationTable1();
                t.Country = "Pakistan";
                t.Id = 1;
                t.Username = "Daniyal";
                t.Password = "dsa";
                t.Email = "fdsf";
                

                RegistrationTable1 t1 = new RegistrationTable1();
                t1.Country = "Pakistan";
                t1.Id = 223;
                t1.Username = "Daniyal";
                t1.Password = "dsa";
                t1.Email = "fdsf";


                List <PropertyInfo> typeDb = db.RegistrationTable1.Where(x => x.Id == 223).First().GetType().GetProperties().ToList();

                Console.WriteLine(typeDb[0].Name);
                Console.WriteLine(typeDb[2].PropertyType.Name);
                Console.WriteLine(typeDb[3]);
                Console.WriteLine(typeDb[1]);

                //db.RegistrationTable1.AddObject(t);
                //db.RegistrationTable1.AddObject(t1);
                //db.SaveChanges();

                //RegistrationTable1 change = db.RegistrationTable1.Where(x => x.Id == 223).First();
                //change.Country = "Australia";
                //db.SaveChanges();

                //RegistrationTable1 tRemove = db.RegistrationTable1.First();
                //Console.WriteLine(tRemove.Id);
                //db.RegistrationTable1.DeleteObject(tRemove);

                //db.SaveChanges();


                //int a = 0;
                //int d = 64 / a;


                //RegistrationTable1 t2 = new RegistrationTable1();
                //t2.Country = "Pakistan";
                //t2.Id = 333;
                //t2.Username = "Daniyal";
                //t2.Password = "dsa";
                //t2.Email = "fdsf";
                //db.RegistrationTable1.AddObject(t2);
                //db.SaveChanges();
                transaction.Commit();
                db.Connection.Close();
            
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }



          /*  
            Random rand = new Random();
            string connectionString = "Data Source=.\\SQL2k8;Initial Catalog=Registration;User ID=sa;Password=Rolustech99";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into RegistrationTable1(Id,UserName,Password,Country,Email) values(@id,@UserName,@Password,@Country,@Email)", con);
            cmd.Parameters.AddWithValue("@id", rand.Next(1000));
            cmd.Parameters.AddWithValue("@UserName", "Daniyal");
            cmd.Parameters.AddWithValue("@Password", "1234");
            cmd.Parameters.AddWithValue("@Country", "Pakistan");
            cmd.Parameters.AddWithValue("@Email", "daniyal313@gmail.com");
            int result = cmd.ExecuteNonQuery();
            con.Close();
            */
        }
    }
}
