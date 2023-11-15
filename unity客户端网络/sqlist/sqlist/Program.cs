using MySql;
using MySql.Data.MySqlClient;
using System;

namespace sqlist
{

    internal class Program
    {
        //server=127.0.0.1;port=3306;user=root;password=; database=studymyql;CharSet=utf8;
        static MySqlConnection con = null;
        static string connerstr = "server=127.0.0.1;port=3306;user=root;password=; database=studymysql;Charset=utf8;";
        static void Main(string[] args)
        {
            con = new MySqlConnection(connerstr);
            con.Open();

            //增加
            // add();
            //删
         //   delete();
            //改
            update();



            //查找
         //   quary();
            Console.ReadKey();
            con.Close();
        }
        static void add()
        {
            MySqlCommand cmd = new MySqlCommand("insert into userinfo (Id,name,age) values (3,'oo',128); ", con);
            cmd.ExecuteNonQuery();
            Console.WriteLine((int)cmd.LastInsertedId);
        }
        static void delete() 
        {
            MySqlCommand cmd = new MySqlCommand("delete from userinfo where Id=3",con);
            cmd.ExecuteNonQuery();
        }
        static void update()
        {
            MySqlCommand cmd = new MySqlCommand("update userinfo set name=@name ,age=@age where Id=@Id",con);
            cmd.Parameters.AddWithValue("name", "xoxo");
            cmd.Parameters.AddWithValue("age", 100);
            cmd.Parameters.AddWithValue("Id", 1);

            cmd.ExecuteNonQuery();
        }
        static void quary()
        {
            MySqlCommand cmd = new MySqlCommand("select *from userinfo", con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int age = reader.GetInt32(2);
                Console.WriteLine("id{0}.name{1},age{2}", id, name, age);
            }
        }
    }
}
