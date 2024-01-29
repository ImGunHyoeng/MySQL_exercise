using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//connection,Command,DataReader등의 기능을 사용하기 위함이다.

namespace MySql
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;Pwd=bitnami"))
            {
                string insertQuery = "INSERT INTO tasks(id,title) VALUES(11,'To infinity… and beyond!')";
                try//예외 처리
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    // 만약에 내가처리한 Mysql에 정상적으로 들어갔다면 메세지를 보여주라는 뜻이다
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("인서트 성공");
                        SelectUsingReader();
                        SelectUsingAdapter();
                    }
                    else
                    {
                        Console.WriteLine("인서트 실패");
                    }
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }

            }
        }

        private static void SelectUsingReader()
        {
            string connStr = "Server=localhost;Port=3306;Database=testdb;Uid=root;Pwd=bitnami";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM tasks WHERE id>=2";

                //ExecuteReader를 이용하여
                //연결 모드로 데이타 가져오기
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.WriteLine("{0}: {1}", rdr["id"], rdr["title"]);
                }
                rdr.Close();
            }
        }

        private static void SelectUsingAdapter()
        {
            DataSet ds = new DataSet();
            string connStr = "Server=localhost;Port=3306;Database=testdb;Uid=root;Pwd=bitnami";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //MySqlDataAdapter 클래스를 이용하여
                //비연결 모드로 데이타 가져오기
                string sql = "SELECT * FROM tasks WHERE id>=2";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "tasks");
            }

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                Console.WriteLine(r["title"]);
            }
        }
        //string strConn = "Server=localhost;Database=test;Uid=root;Pwd=bitnami;";
        //MySqlConnection conn = new MySqlConnection(strConn);
        //conn.Close();

        //    private static void InsertUpdate()
        //{
        //    string strConn = "Server=localhost;Database=test;Uid=root;Pwd=bitnami;";

        //    using (MySqlConnection conn = new MySqlConnection(strConn))
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("INSERT INTO Tab1 VALUES (2, 'Tom')", conn);
        //        cmd.ExecuteNonQuery();

        //        cmd.CommandText = "UPDATE Tab1 SET Name='Tim' WHERE Id=2";
        //        cmd.ExecuteNonQuery();
        //    }
        //    //MySqlConnection
        //    //    connection연걸
        //    //    reader 전에 커맨드를 리더로 읽어옴
        //}
    }
}
