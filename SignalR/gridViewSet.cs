using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SignalR
{
    public class gridViewSet
    {
        public static int version = 2;
        
        public static void setGridView(GridView GridView1,string[][] table,string[] columnName)
        {
            DataTable dataTable = new DataTable();
            int column = 0;
            for (int i = 0; i < table.Length;i++ )
            {
                dataTable.Rows.Add();
                column = Math.Max(column,table[i].Length);
            }
            for (int i = 0; i < column; i++)
            {
                dataTable.Columns.Add(columnName[i], Type.GetType("System.String"));
            }


            GridView1.DataSource = dataTable;

            GridView1.DataBind();

            for (int k = 0; k < GridView1.Rows.Count; k++)
            {
                for (int j = 0; j < GridView1.Rows[k].Cells.Count; j++)
                {
                    
                        Label temp = new Label();
                        temp.ID = k + "plus" + j;
                        temp.Text = table[k][j];// k + "plus" + j + question[k % 4][j / 6];
                        //temp.CssClass = "";
                        GridView1.Rows[k].Cells[j].Controls.Add(temp);
                    
                    
                }
            }


        }
        public static int setGridView(GridView GridView1, string[][] table)
        {

            DataTable dataTable = new DataTable();
            int column = 0;
            for (int i = 0; i < table.Length; i++)
            {
                dataTable.Rows.Add();
                column = Math.Max(column, table[i].Length);
            }
            for (int i = 0; i < column; i++)
            {
                dataTable.Columns.Add("", Type.GetType("System.String"));
            }

            GridView1.DataSource = dataTable;

            GridView1.DataBind();

            for (int k = 0; k < GridView1.Rows.Count; k++)
            {
                for (int j = 0; j < GridView1.Rows[k].Cells.Count; j++)
                {


                    Label temp = new Label();
                    temp.ID = k + "plus" + j;
                    temp.Text = table[k][j];// k + "plus" + j + question[k % 4][j / 6];
                    //temp.CssClass = "";
                    GridView1.Rows[k].Cells[j].Controls.Add(temp);


                }
            }

            return column;
        }

        public static void setGridView(GridView GridView1,string[][] question,string[][][] answer)
        {
            int rows = 4;
            int column = 6;
            DataTable dataTable = new DataTable();
            for (int i = 0; i < rows * question.Length; i++)
            {
                dataTable.Rows.Add();

            }

            int q = 0;//製作列數
            for (int i = 0; i < question.Length; i++)
            {
                q = Math.Max(question[i].Length, q);
            }
            for (int i = 0; i < column * q; i++)
            {
                dataTable.Columns.Add("", Type.GetType("System.String"));
            }


            GridView1.DataSource = dataTable;

            GridView1.DataBind();

            for (int k = 0; k < GridView1.Rows.Count; k++)
            {
                for (int j = 0; j < GridView1.Rows[k].Cells.Count; j++)
                {
                    if (j % 6 == 0 && k % 4 == 0)
                    {

                        GridView1.Rows[k].Cells[j].RowSpan = 4;
                        GridView1.Rows[k].Cells[j].ColumnSpan = 4;
                        GridView1.Rows[k].Cells[j].Width = 100;
                        GridView1.Rows[k].Cells[j].Height = 25;
                        Label temp = new Label();
                        temp.ID = k + "plus" + j;
                        temp.Text = "";// k + "plus" + j + question[k % 4][j / 6];
                        //temp.CssClass = "";

                        GridView1.Rows[k].Cells[j].Controls.Add(temp);
                    }
                    else if (j % 6 == 4 && !answer[k / 4][j / 6][k % 4].Equals("non"))
                    {
                        TextBox temp = new TextBox();
                        temp.ID = k + "plus" + j;
                        temp.Width = 25;
                        temp.Text = "";// k + "plus" + j + answer[k / 4][j / 6][k % 4];
                        temp.Attributes.Add("onclick", "init(this.id)");
                        string[] secondClass = new string[] { "first", "second", "third", "forth" };
                        temp.CssClass = "" + secondClass[k % 4];
                        temp.ReadOnly = true;
                        GridView1.Rows[k].Cells[j].BorderWidth = 0;
                        GridView1.Rows[k].Cells[j].Controls.Add(temp);

                    }

                    else if (k % 4 == 2 && j % 6 == 5 && !answer[k / 4][j / 6][4].Equals("non"))
                    {
                        TextBox temp = new TextBox();
                        temp.ID = k + "plus" + j;
                        temp.Width = 25;
                        temp.Text = "";//k + "plus" + j + answer[k / 4][j / 6][4];
                        temp.Attributes.Add("onclick", "init(this.id)");
                        temp.CssClass = "fifth";
                        temp.ReadOnly = true;
                        GridView1.Rows[k].Cells[j].BorderWidth = 0;
                        GridView1.Rows[k].Cells[j].Controls.Add(temp);

                    }
                    else if (k % 4 == 2 && j % 6 == 5 && answer[k / 4][j / 6][4].Equals("non"))
                    {
                        GridView1.Rows[k - 2].Cells[j].Visible = false;
                        GridView1.Rows[k - 1].Cells[j].Visible = false;
                        GridView1.Rows[k].Cells[j].Visible = false;
                        GridView1.Rows[k + 1].Cells[j].Visible = false;
                        TextBox temp = new TextBox();
                        temp.ID = k + "plus" + j;
                        temp.Width = 0;
                        temp.Text = "non";//k + "plus" + j + answer[k / 4][j / 6][4];
                        temp.CssClass = "fifth";
                        temp.ReadOnly = true;
                    }
                    else if (j % 6 < 4)
                    {

                        /*
                        Label temp = new Label();
                        temp.Text = k+"plus"+j;
                        GridView1.Rows[k].Cells[j].Controls.Add(temp);
                            
                         */
                        GridView1.Rows[k].Cells[j].Visible = false;

                    }
                }
            }


        }


    }
}