using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyAdomd;
using Microsoft.AnalysisServices.AdomdClient;
using Justin.FrameWork.Helper;
/*
 日期：2008-7-13
 作者博客号:http://xuanfeng.cnblogs.com
 
 */
namespace CubesViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connString;
        MyAdomd.CubesViewer cubesViews = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            //provider=msolap ;Integrated Security =SSPI ;Data Source= localhost ;Catalog =AdventureWorksDW;
            var connElement=ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["ConnStr"]];
            txtConnStr.Text = String.Format("provider={0} ;{1}",connElement.ProviderName,connElement.ConnectionString);
                
        }
        void   InitTreeView()
        {
            //try
            //{
                InitCube();
                InitDimensions();
                InitKpiName();
                InitNameSet();
            //}
            //catch (Exception ex)
            //{
            //    txtMessage.Text = PrintException(ex);
            
            //}
        }
        void InitDim()
        {
            DataTable table = MyAdomd.CubesViewer.CubesDimensions(cubesViews.Connection.Cubes);

        }
    
        void InitCube()
        {
            this.treeView1.Nodes.Add("Cubes_","多维数据集") ;
            DataTable table = MyAdomd.CubesViewer.CubesCube(cubesViews.Connection.Cubes);
            for (int i = 0; i < table.Rows.Count;i++ )
            { 
                treeView1.Nodes["Cubes_"].Nodes.Add(table.Rows[i]["Name"].ToString(),table.Rows[i]["Caption"].ToString());
                treeView1.Nodes["Cubes_"].Nodes[table.Rows[i]["Name"].ToString()].Nodes.Add("Measures_","度量值");               
            }
            InitMeasures();
           
            
        }
        void InitMeasures()
        {
            DataTable table = MyAdomd.CubesViewer.CubesMeasures(cubesViews.Connection.Cubes);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string cubeName = table.Rows[i]["CubeName"].ToString();
                string name = table.Rows[i]["Name"].ToString();
                string caption = table.Rows[i]["Caption"].ToString();
                treeView1.Nodes["Cubes_"].Nodes[cubeName].Nodes["Measures_"].Nodes.Add(name, caption);
                
            }
        }
        void InitDimensions()
        {
            this.treeView1.Nodes.Add("Dimensions_","维度");
            DataTable table = MyAdomd.CubesViewer.CubesDimensions(cubesViews.Connection.Cubes);
            for (int i = 0; i < table.Rows.Count;i++ )
            {
               string name=table.Rows[i]["Name"].ToString() ;
               string caption = table.Rows[i]["Caption"].ToString();
               this.treeView1.Nodes["Dimensions_"].Nodes.Add(name,caption);
               this.treeView1.Nodes["Dimensions_"].Nodes[name].Nodes.Add("Hiberarchies_", "层次结构");
               
            }
        }
        void InitHiberarchies(string dimName)
        {

           
            DataTable table = MyAdomd.CubesViewer.DimHiberarchies(MyAdomd.CubesViewer.GetDimByName(cubesViews.Connection.Cubes,dimName));
            this.dataGridView1.DataSource = table;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i]["Name"].ToString();
                string caption = table.Rows[i]["Caption"].ToString();
                this.treeView1.Nodes["Dimensions_"].Nodes[dimName].Nodes["Hiberarchies_"].Nodes.Add(name,caption);
                this.treeView1.Nodes["Dimensions_"].Nodes[dimName].Nodes["Hiberarchies_"].Nodes[name].Nodes.Add("Levels_", "层次结构级别");
            }
        }
        void InitLevels(string dimName,string hiberarchyName)
        {

            DataTable table = MyAdomd.CubesViewer.HierarchyLevels(MyAdomd.CubesViewer.GetHierarchyByName(cubesViews.Connection.Cubes, dimName,hiberarchyName));
            this.dataGridView1.DataSource = table;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i]["Name"].ToString();
                string caption = table.Rows[i]["Caption"].ToString();
                this.treeView1.Nodes["Dimensions_"].Nodes[dimName].Nodes["Hiberarchies_"].Nodes[hiberarchyName].Nodes["Levels_"].Nodes.Add(name, caption);
                this.treeView1.Nodes["Dimensions_"].Nodes[dimName].Nodes["Hiberarchies_"].Nodes[hiberarchyName].Nodes["Levels_"].Nodes[name].Nodes.Add("Members_", "成员");
            }

        }
        void InitMembers(string dimName, string hiberarchyName,string levelName)
        {

            DataTable table = MyAdomd.CubesViewer.LevelMembers(MyAdomd.CubesViewer.GetLevelByName(cubesViews.Connection.Cubes, dimName, hiberarchyName,levelName));
            this.dataGridView1.DataSource = table;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i]["Name"].ToString();
                string caption = table.Rows[i]["Caption"].ToString();
                this.treeView1.Nodes["Dimensions_"].Nodes[dimName].Nodes["Hiberarchies_"].Nodes[hiberarchyName].Nodes["Levels_"].Nodes[levelName].Nodes.Add(name, caption);           
            }

        }
        void InitKpiName()
        {
            this.treeView1.Nodes.Add("KpiName_", "KPI");
            DataTable table = MyAdomd.CubesViewer.CubesKpiNames(cubesViews.Connection.Cubes);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i]["Name"].ToString();
                string caption = table.Rows[i]["Caption"].ToString();
                this.treeView1.Nodes["KpiName_"].Nodes.Add(name, caption);
            }
        }
        void InitNameSet()
        {
            this.treeView1.Nodes.Add("NameSet_", "命名集");
            DataTable table = MyAdomd.CubesViewer.CubesNameSet(cubesViews.Connection.Cubes);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i]["Name"].ToString();
              
                this.treeView1.Nodes["NameSet_"].Nodes.Add(name, name);
            }
        }
        void InitGvNameSet()
        {
            DataTable table = MyAdomd.CubesViewer.CubesNameSet(cubesViews.Connection.Cubes);
            this.dataGridView1.DataSource = table;
        }
        void InitGvKpiName()
        {
            DataTable table = MyAdomd.CubesViewer.CubesKpiNames(cubesViews.Connection.Cubes);
            this.dataGridView1.DataSource = table;
        }
        void InitGvDimension()
        {
            DataTable table = MyAdomd.CubesViewer.CubesDimensions(cubesViews.Connection.Cubes);
            this.dataGridView1.DataSource = table;
        }
        void InitGvCubes()
        {
            DataTable table = MyAdomd.CubesViewer.CubesCube(cubesViews.Connection.Cubes);
            this.dataGridView1.DataSource = table;
        }
        void InitGvMeasures()
        {
            DataTable table = MyAdomd.CubesViewer.CubesMeasures(cubesViews.Connection.Cubes);
            this.dataGridView1.DataSource = table;
        }


        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
               switch (e.Node.Parent.Name)
               {
                   case "Dimensions_":
                       InitHiberarchies(e.Node.Name);
                       break;
                   case "Hiberarchies_":
                       InitLevels(e.Node.Parent.Parent.Name,e.Node.Name);
                       break;
                   case "Levels_":
                       InitMembers(e.Node.Parent.Parent.Parent.Parent.Name,e.Node.Parent.Parent.Name,e.Node.Name);
                       break;
                   default:
                       break;

                       
               
               }
            }
            switch (e.Node.Name)
            {
                case "NameSet_":
                    InitGvNameSet();
                    break;
                case "Cubes_":
                    InitGvCubes();
                    break;
                case "Measures_":
                    InitGvMeasures();
                    break;
                case "Dimensions_":
                    InitGvDimension();
                    break;
                case "KpiName_":
                    InitGvKpiName();
                    break;
                default:
                    break;


            
            }
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            cubesViews = new MyAdomd.CubesViewer(txtConnStr.Text);
            cubesViews.OpenConnection();
            InitTreeView();
        }

        private void btnDemoMdx_Click(object sender, EventArgs e)
        {

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            using (var conn = MdxHelper.GetConnection(txtConnStr.Text))
            {
                DataTable dt = MdxHelper.ExecuteDataTable(conn, txtMdx.Text);
                dgvQueryResult.DataSource = dt;
            }
        }


        private string PrintException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            Exception exc = ex;
            sb.Append(exc.ToString()).AppendLine();
            while (exc.InnerException != null)
            {
                sb.Insert(0, exc.InnerException.ToString());
                exc = exc.InnerException;
            }
            return sb.ToString();
        }
     
    }
}