using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Net.NetworkInformation;

namespace Proyecto3_TDBD_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqliteConnection();
        }

        public void SqliteConnection()
        {
            string createQuery = @"CREATE TABLE IF NOT EXISTS
                                 [TABLE1] (
                                 [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT)";

            System.Data.SQLite.SQLiteConnection.CreateFile("testFile.txt");
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=testFile.txt"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();
                    cmd.ExecuteReader();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        [SQLiteFunction(Name = "PING", Arguments = 1, FuncType = FunctionType.Scalar)]
        class PING : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                Ping ping = new Ping();
                string ip = args[0].ToString();

                try
                {
                    PingReply reply = ping.Send(ip);
                    if(reply.Status == IPStatus.Success)
                    {
                        return 1;
                    }
                }
                catch (PingException e)
                {

                }

                return 0;
            }
        }


        [SQLiteFunction(Name = "F2C", Arguments = 1, FuncType = FunctionType.Scalar)]
        class F2C : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int fahrenheit = Convert.ToInt32(args[0].ToString());
                int celsius = (fahrenheit - 32) * 5 / 9;

                return celsius;
            }
        }

        [SQLiteFunction(Name = "BIN2DEC", Arguments = 1, FuncType = FunctionType.Scalar)]
        class BIN2DEC : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {

                string valor_binario = args[0].ToString();
                return Convert.ToInt64(valor_binario, 2);
            }
        }

        [SQLiteFunction(Name = "DEC2BIN", Arguments = 1, FuncType = FunctionType.Scalar)]
        class DEC2BIN : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                return Convert.ToString(Convert.ToInt32(args[0].ToString()), 2);
            }
        }

        [SQLiteFunction(Name = "C2F", Arguments = 1, FuncType = FunctionType.Scalar)]
        class C2F : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int fahrenheit = Convert.ToInt32(args[0].ToString());
                fahrenheit = (fahrenheit * 9) / 5 + 32;
                return fahrenheit;
            }
        }

        [SQLiteFunction(Name = "Factorial", Arguments = 1, FuncType = FunctionType.Scalar)]
        class Factorial : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                int resultado = 1;
                int numero = Convert.ToInt32(args[0].ToString());

                if (numero < 0)
                    return 0;
                else if (numero == 0)
                    return 1;
                else
                {
                    for (int i = 0; i < numero; numero--)
                    {
                        resultado = resultado * numero;
                    }
                }

                return resultado;
            }

        }

        [SQLiteFunction(Name = "DEC2HEX", Arguments = 1, FuncType = FunctionType.Scalar)]
        class DEC2HEX : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                int decValue = Convert.ToInt32(args[0].ToString());
                string hexValue = decValue.ToString("X");
                return hexValue;
            }

        }

        [SQLiteFunction(Name = "HEX2DEC", Arguments = 1, FuncType = FunctionType.Scalar)]
        class HEX2DEC : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                string hexValue = args[0].ToString();
                int decValue = Convert.ToInt32(hexValue, 16);
                return decValue;
            }
        }

        [SQLiteFunction(Name = "COMPARESTRING", Arguments = 2, FuncType = FunctionType.Scalar)]
        class COMPARESTRING : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                string string1 = args[0].ToString();
                string string2 = args[1].ToString();
                int a = string.Compare(string1, string2);
                return a;
            }
        }


            private void button2_Click(object sender, EventArgs e)
        {
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=testFile.txt"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = textBox1.Text;
                    cmd.ExecuteNonQuery();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            textBox2.Text = (reader[0].ToString());
                    }
                }
            }

        }
    }
}



