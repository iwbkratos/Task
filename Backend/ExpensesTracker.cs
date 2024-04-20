using Microsoft.VisualBasic;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Xml.Linq;


namespace Task
{

     class Expenses {

        private int id;
        private string start_date;
        private string end_date;
        private decimal amount;
        private string description;
        private int cid;

      
        public int Id {
            get { return id; }
            set { id = value; }
        }

        public string Start_date {
            get { return start_date; }
            set { start_date = value; }
        }

        public string End_date {
            get { return end_date; }
            set { end_date = value; }
        }

        public decimal Amount {

            get { return amount; }
            set { amount = value; }
        }

        public string Description {

            get { return description; }
            set { description = value; }
        }

        public int Cid {
            get { return cid; }
            set { cid = value; }
        }

        public override string ToString()
        {

            return $"Id: {Id}, StartDate: {start_date}, EndDate: {end_date}, Amount: {amount}, Description{description}, CategoryId: {cid}";
        }
    }

    public class Category {


        private int id;
        private string CategoryType;

        public Category() { }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string categoryName
        {
            get { return CategoryType; }
            set { CategoryType = value; }
        
        }

        public override string ToString()
        {
            return $"CategoryId : {id}, CategoryType : {CategoryType}";
        }
    }

       public class Driver {
      
        static SqlConnection conn;
        static string constr = @"Data Source=DESKTOP-70IGP51\SQLEXPRESS;Initial Catalog=ExpenseTracker;Integrated Security=True";
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("******************Expense Tractor**************");
                Console.WriteLine(" Enter '1' to show your record \n Enter '2' to display all record \n Enter '3'  to delete a record \n Enter '4' to create a record \n Enter '5' to edit ");
                Console.WriteLine("------------------------------------------------");
                int operation = Convert.ToInt32(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        showOne();
                        break;
                    case 2:
                        displayAll();
                        break;
                    case 3:
                        deleteExpenses();
                        break;
                    case 4:
                        create();
                        break;
                    case 5:
                        edit();
                        break;
                    default:
                        Console.WriteLine("Enter valid input!");
                        break;
                }
            }
        }

        static void create()
        {
            Console.WriteLine("-----------------Catagories------------------");
            displayCategory();
            Console.WriteLine();
            insertExpenses();
            Console.WriteLine("------------------------------------------------");

        }


        static void insertExpenses()
        {
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                Expenses expenses = new Expenses();
                Console.WriteLine("Select the category id for your expense : ");
                //Console.WriteLine("Enter Category id : ");
                int cid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Id : ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter StartDate (yyyy-mm-dd): ");
                string sdate = Console.ReadLine();
                Console.WriteLine("Enter Enddate (yyyy-mm-dd): ");
                string edate = Console.ReadLine();
                Console.WriteLine("Enter Money :");
                decimal money = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Write your description : ");
                string desc = Console.ReadLine();
               

                expenses.Id = id;
                expenses.Start_date = sdate;
                expenses.End_date = edate;
                expenses.Amount = money;
                expenses.Description = desc;
                expenses.Cid = cid;
                if(expenses!=null)
                {
                    string insert = $"insert into Expenses values({expenses.Id},'{expenses.Start_date}','{expenses.End_date}',{expenses.Amount},'{expenses.Description}',{expenses.Cid})";
                    SqlCommand cmd = new SqlCommand(insert, conn);
                  int row=  cmd.ExecuteNonQuery();
                    Console.WriteLine($"{row} row affected");
                    if (row > 0)
                    {
                        Console.WriteLine("----Successfully Edited----");
                    }
                    else
                    {

                        Console.WriteLine("----Failed----");
                    }
                }
                else {
                    Console.WriteLine("Entry is empty");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void deleteExpenses()
        {

            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
               // Expenses expenses = new Expenses();
                Console.WriteLine("Enter Id of the data that you want to delete : ");
                int id = Convert.ToInt32(Console.ReadLine());
               

                string insert = $"delete from Expenses where Eid={id}";
                SqlCommand cmd = new SqlCommand(insert, conn);
                int row= cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    Console.WriteLine("----Successfully Edited----");
                }
                else
                {

                    Console.WriteLine("----Failed----");
                }
                Console.WriteLine("------------------------------------------------");

            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
            }


        }

        static void displayAll()
        {
            List<Expenses> expensesList = new List<Expenses>();
            List<Category> categorylist = Category();
            try {
                conn = new SqlConnection(constr);
                conn.Open();
                String query = $"select * from Expenses";
                SqlCommand cmd = new SqlCommand(query,conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Expenses expenses = new Expenses();
                    expenses.Id = reader.GetInt32(0);
                    expenses.Start_date = Convert.ToString(reader.GetDateTime(1));
                    expenses.End_date = Convert.ToString(reader.GetDateTime(2));
                    expenses.Amount = reader.GetDecimal(3);
                    expenses.Description = reader.GetString(4);
                    expenses.Cid = reader.GetInt32(5);
                    expensesList.Add(expenses);
                }

                var result = from Category in categorylist
                             join Expenses in expensesList on Category.Id equals Expenses.Cid
                             group Expenses by Category.categoryName into groupOrders
                             select groupOrders;
                foreach (var group in result)
                {
                    Console.WriteLine($"CategoryType : {group.Key}");
                    Console.WriteLine($" ----------------");
                    foreach (var expense in group)
                    {
                        
                        Console.Write($"    {expense.Id,-4}   {expense.Start_date,-11}    {expense.End_date,-12}    {expense.Amount,-12}    {expense.Description,-12}     {expense.Cid,-12}\n");
                    }
                }
            }
            catch (Exception e)
            { 
                  Console.WriteLine($"{e.Message}");            
            }
        }
        static void showOne()
        {
            
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                Console.WriteLine("Enter id of the record : ");
                int input = Convert.ToInt32(Console.ReadLine());
                string query = $"select * from Expenses where Eid={input}";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Expenses expenses = new Expenses();
                while (reader.Read())
                {
                    expenses.Id = reader.GetInt32(0);
                    expenses.Start_date = Convert.ToString(reader.GetDateTime(1));
                    expenses.End_date = Convert.ToString(reader.GetDateTime(2));
                    expenses.Amount = reader.GetDecimal(3);
                    expenses.Description = reader.GetString(4);
                    expenses.Cid = reader.GetInt32(5);
                }


                Console.WriteLine($"Expense-Id : {expenses.Id}, StartDate : {expenses.Start_date}, EndDate : {expenses.End_date}, Amount : {expenses.Amount}, Description : {expenses.Description}, CategoryID : {expenses.Cid})");
                Console.WriteLine("------------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static List<Category> Category()
        {
            List<Category> CategoryList = new List<Category>();
 

            try {
                conn = new SqlConnection(constr);
                conn.Open();
                string query = "select * from Category";
                SqlCommand cmd = new SqlCommand(query,conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Category category = new Category();
                    category.Id = reader.GetInt32(0);
                    category.categoryName = reader.GetString(1);
                    CategoryList.Add(category);
                
                }
                reader.Close();
               
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return CategoryList;
        }

        static void displayCategory()
        {
            Console.WriteLine($" ________________");
            Console.WriteLine($"|  ID  |  Type  |");
            Console.WriteLine($" ----------------");

            foreach (var cat in Category())
                {
                
                Console.WriteLine($"   {cat.Id}   | {cat.categoryName}");
                }
            Console.WriteLine($" ----------------");
        }

        static void edit()
        {
            displayAll();
            Console.WriteLine("Enter id of the record that you want to update : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount : ");
            decimal amnt= Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Description : ");
            string desc = Console.ReadLine();

            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                string query = $"update Expenses set Amount={amnt},Description='{desc}'  where Eid={id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                int row= cmd.ExecuteNonQuery();
                Console.WriteLine($"{row} row affected");

                if (row > 0)
                {
                    Console.WriteLine("----Successfully Edited----");
                }
                else {

                    Console.WriteLine("----Failed----");
                }
                Console.WriteLine("------------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
       }
}
