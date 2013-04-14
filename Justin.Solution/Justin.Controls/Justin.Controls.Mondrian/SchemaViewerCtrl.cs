using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.FormUI.PropertyGrid;
using Justin.FrameWork.WinForm.FormUI.SharpCodeTextEditor;
using Justin.FrameWork.WinForm.Models;

namespace Justin.Controls.Mondrian
{
    public partial class SchemaViewerCtrl : JUserControl, IFile
    {
        public SchemaViewerCtrl()
        {
            InitializeComponent();
        }

        private void SchemaViewerCtrl_Load(object sender, EventArgs e)
        {
            ObjectDescriptionProvider provider = new ObjectDescriptionProvider();
            TypeDescriptor.AddProvider(provider, typeof(Table));
            txtSchemaXML.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
            txtSchemaXML.Encoding = Encoding.GetEncoding("GB2312");
        }

        private void treeViewSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Element el = e.Node.Tag as Element;
            lbPropertyGridSource.Text = string.Format("{0}：{1}", el.GetType().Name, el.Name);

            propertyGrid.SelectedObject = e.Node.Tag;


        }

        #region 变量

        public string SchemaFileName
        {
            get
            {
                return txtFileName.Text;
            }
            set
            {
                txtFileName.Text = value;
            }
        }
        public string SaveSchemaFileName
        {
            get
            {
                return txtDstFileName.Text;
            }
            set
            {
                txtDstFileName.Text = value;
            }
        }

        Schema schema;
        Dictionary<TextEditorControl, HighlightGroup> _highlightGroups = new Dictionary<TextEditorControl, HighlightGroup>();

        #endregion

        #region 按钮

        private void btnBrowerFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string folder = Constants.ConfigFileFolder;
            if (!string.IsNullOrEmpty(txtFileName.Text))
            {
                var dic = new DirectoryInfo(Path.GetDirectoryName(txtFileName.Text));
                if (dic.Exists)
                {
                    folder = Path.GetDirectoryName(txtFileName.Text);
                }
            }

            fileDialog.InitialDirectory = folder;
            fileDialog.RestoreDirectory = true;

            fileDialog.Filter = "XML Files (.txt)|*.xml|All Files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fileDialog.FileName;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //GenerateSchema(); 
                GenerateSchemaFromFile();
                BindTreeview(schema);
                propertyGrid.SelectedObject = schema;
                PriviewSchema();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }

        private void btnPriview_Click(object sender, EventArgs e)
        {
            try
            {
                PriviewSchema();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }

        private void btnSerch_Click(object sender, EventArgs e)
        {
            var _editor = txtSchemaXML;
            if (!_highlightGroups.ContainsKey(_editor))
                _highlightGroups[_editor] = new HighlightGroup(_editor);
            HighlightGroup group = _highlightGroups[_editor];

            if (string.IsNullOrEmpty(txtLookFor.Text))
                // Clear highlights
                group.ClearMarkers();
            else
            {
                TextEditorSearcher _search = new TextEditorSearcher();
                _search.LookFor = txtLookFor.Text;
                _search.MatchCase = false;
                _search.MatchWholeWordOnly = false;
                _search.Document = _editor.Document;

                bool looped = false;
                int offset = 0, count = 0;
                for (; ; )
                {
                    TextRange range = _search.FindNext(offset, false, out looped);
                    if (range == null || looped)
                        break;
                    offset = range.Offset + range.Length;
                    count++;

                    TextMarker m = new TextMarker(range.Offset, range.Length,
                            TextMarkerType.SolidBlock, Color.Yellow, Color.Black);
                    group.AddMarker(m);
                }
                if (count == 0)
                    MessageBox.Show("没有找到你要查找的内容", "提示");
                else
                {
                    //Close();
                }
                Folding();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //ShowSchema();
                string fileName = Path.Combine(Application.StartupPath, txtDstFileName.Text);
                StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8);
                writer.Write(txtSchemaXML.Text);
                writer.Close();
                this.ShowMessage("OK");
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }
        #endregion

        #region 函数

        public void BindTreeview(Schema schema)
        {
            treeViewSchema.Nodes.Clear();
            string text = string.IsNullOrEmpty(schema.Name) ? schema.GetType().Name : schema.Name;
            var node = new TreeNode(text);
            node.Tag = schema;
            treeViewSchema.Nodes.Add(node);
            BindChildElement(node, schema);
            treeViewSchema.Nodes[0].Expand();
        }
        public void BindChildElement(TreeNode node, Element element)
        {
            if (element.ChildrenElements != null && element.ChildrenElements.Count() > 0)
            {
                foreach (var childElement in element.ChildrenElements)
                {
                    string text = string.IsNullOrEmpty(childElement.Name) ? childElement.GetType().Name : childElement.Name;
                    var childNode = new TreeNode(text);
                    childNode.Tag = childElement;
                    node.Nodes.Add(childNode);
                    BindChildElement(childNode, childElement);
                }
            }
            ChangeTreeNodesIcon(this.treeViewSchema.Nodes);
        }
        public void PriviewSchema()
        {

            string fileName = Path.Combine(Application.StartupPath, txtFileName.Text);
            string content = schema.Serializer(); // //SerializeHelper.XmlSerialize<Schema>(schema, true);    
            txtSchemaXML.SetText(content);
            Folding();
            this.ShowMessage("OK");
        }
        private void GenerateSchema()
        {
            //#region Schema
            //var dimStore = new Dimension("Store");
            //var dimTime = new Dimension("Time");
            //var dimProduct = new Dimension("Product");

            //var cubeSales = new Cube("Sales");
            //var cubeSalesRagged = new Cube("Sales Ragged");

            //var vCube = new VirtualCube("Warehouse and Sales");

            //var role1 = new Role("California manager");
            //var role2 = new Role("No HR Cube");

            //#region dimStore

            //var hierarchyStore = new Hierarchy("");
            //hierarchyStore.HasAll = true;
            //hierarchyStore.PrimaryKey = "store_id";
            //hierarchyStore.Table = new Justin.BI.DBLibrary.Mondrian.Table("store");

            //hierarchyStore.Add(new Level("Store Country", "store_country", true));
            //hierarchyStore.Add(new Level("Store State", "store_state", true));
            //hierarchyStore.Add(new Level("Store City", "store_city", false));
            //var level = new Level("Store Name", "store_name", true);
            //Property p1 = new Property("Store Type", "store_type", ColumnType.String);
            //Property p2 = new Property("Store Manager", "store_manager", ColumnType.String);
            //Property p3 = new Property("Store Sqft", "store_sqft", ColumnType.Numeric);
            //Property p4 = new Property("Grocery Sqft", "grocery_sqft", ColumnType.Numeric);
            //Property p5 = new Property("Frozen Sqft", "frozen_sqft", ColumnType.Numeric);
            //Property p6 = new Property("Meat Sqft", "meat_sqft", ColumnType.Numeric);
            //Property p7 = new Property("Has coffee bar", "coffee_bar", ColumnType.Boolean);
            //Property p8 = new Property("Street address", "store_street_address", ColumnType.String);

            //level.Add(new Element[] { p1, p2, p3, p4, p5, p6, p7, p8 });
            //hierarchyStore.Add(level);

            //dimStore.Add(hierarchyStore);

            //#endregion
            //#region dimTime

            //dimTime.Type = DimensionType.TimeDimension;

            //#region month

            //var hierarchyMonth = new Hierarchy("");
            //hierarchyMonth.HasAll = false;
            //hierarchyMonth.PrimaryKey = "time_id";
            //hierarchyMonth.Table = new Justin.BI.DBLibrary.Mondrian.Table("time_by_day");

            //hierarchyMonth.Add(new Level("Year", "the_year", true, ColumnType.Numeric, LevelType.TimeYears));
            //hierarchyMonth.Add(new Level("Quarter", "quarter", true, ColumnType.String, LevelType.TimeQuarters));
            //hierarchyMonth.Add(new Level("Month", "month_of_year", false, ColumnType.String, LevelType.TimeMonths));

            //dimTime.Add(hierarchyMonth);
            //#endregion

            //#region week

            //var hierarchyWeek = new Hierarchy("Weekly");
            //hierarchyWeek.HasAll = true;
            //hierarchyWeek.PrimaryKey = "time_id";
            //hierarchyWeek.Table = new Justin.BI.DBLibrary.Mondrian.Table("time_by_day");

            //hierarchyWeek.Add(new Level("Year", "the_year", true, ColumnType.Numeric, LevelType.TimeYears));
            //hierarchyWeek.Add(new Level("Week", "week_of_year", false, ColumnType.Numeric, LevelType.TimeWeeks));
            //hierarchyWeek.Add(new Level("Day", "day_of_month", false, ColumnType.Numeric, LevelType.TimeDays));

            //dimTime.Add(hierarchyWeek);

            //#endregion

            //#endregion

            //#region dimProduct

            //var hierarchyProduct = new Hierarchy("");
            //hierarchyProduct.HasAll = true;
            //hierarchyProduct.PrimaryKey = "product_id";
            //hierarchyProduct.PrimaryKeyTable = "product";
            //hierarchyProduct.Join = new Justin.BI.DBLibrary.Mondrian.Join("");
            //hierarchyProduct.Join.LeftKey = "product_class_id";
            //hierarchyProduct.Join.RightKey = "product_class_id";

            //hierarchyProduct.Join.Add(new Justin.BI.DBLibrary.Mondrian.Table("product"));
            //hierarchyProduct.Join.Add(new Justin.BI.DBLibrary.Mondrian.Table("product_class"));

            //hierarchyProduct.Add(new Level("Product Family", table: "product_class", column: "product_family", uniqueMembers: true));
            //hierarchyProduct.Add(new Level("Product Department", table: "product_class", column: "product_department", uniqueMembers: false));
            //dimProduct.Add(hierarchyProduct);
            //#endregion

            //#region Employees

            //var dimEmployees = new Dimension("Employees") { ForeignKey = "employee_id" };
            //var hierarchyEmployees = new Hierarchy("")
            //{
            //    HasAll = true,
            //    AllMemberName = "All Employees",
            //    PrimaryKey = "employee_id",
            //};
            //hierarchyEmployees.Table = new Justin.BI.DBLibrary.Mondrian.Table("employee");
            //var levelEmploye = new Level("Employee Id")
            //{
            //    ColumnType = ColumnType.Numeric,
            //    UniqueMembers = true,
            //    Column = "employee_id",
            //    ParentColumn = "supervisor_id",
            //    NameColumn = "full_name",
            //    NullParentValue = "0",
            //};
            //levelEmploye.Closure = new Closure() { ChildColumn = "employee_id", ParentColumn = "supervisor_id" };
            //levelEmploye.Closure.Table = new Justin.BI.DBLibrary.Mondrian.Table("employee_closure");



            //Property p1Employees = new Property("Marital Status") { Column = "marital_status" };
            //Property p2Employees = new Property("Store Manager") { Column = "position_title" };

            //levelEmploye.Add(new Property[] { p1Employees, p2Employees });
            //hierarchyEmployees.Add(levelEmploye);

            //dimEmployees.Add(hierarchyEmployees);
            //cubeSales.Add(dimEmployees);
            //#endregion

            //#region  cubeSales

            //cubeSales.DefaultMeasure = "Unit Sales";

            //#region Annotations

            //cubeSales.Annotations = new List<Annotation>();
            //cubeSales.Annotations.Add(new Annotation("caption.de_DE") { Value = "Verkaufen" });
            //cubeSales.Annotations.Add(new Annotation("caption.fr_FR") { Value = "Ventes" });
            //cubeSales.Annotations.Add(new Annotation("description.fr_FR") { Value = "Cube des ventes" });
            //cubeSales.Annotations.Add(new Annotation("description.de") { Value = "Cube Verkaufen" });
            //cubeSales.Annotations.Add(new Annotation("description.de_AT") { Value = "Cube den Verkaufen" });

            //#endregion

            //#region Table

            //cubeSales.Table = new Justin.BI.DBLibrary.Mondrian.Table("sales_fact_1997");
            //cubeSales.Table.Add(new AggExclude("agg_c_special_sales_fact_1997"));
            //cubeSales.Table.Add(new AggExclude("agg_lc_100_sales_fact_1997"));
            //cubeSales.Table.Add(new AggExclude("agg_lc_10_sales_fact_1997"));
            //cubeSales.Table.Add(new AggExclude("agg_pc_10_sales_fact_1997"));
            //var aggName = new AggName("agg_c_special_sales_fact_1997");

            //aggName.Add(new AggFactCount() { Column = "FACT_COUNT" });
            //aggName.Add(new AggIgnoreColumn() { Column = "foo" });
            //aggName.Add(new AggIgnoreColumn() { Column = "bar" });

            //aggName.Add(new AggForeignKey() { FactColumn = "product_id", AggColumn = "product_id" });
            //aggName.Add(new AggForeignKey() { FactColumn = "customer_id", AggColumn = "customer_id" });
            //aggName.Add(new AggForeignKey() { FactColumn = "promotion_id", AggColumn = "promotion_id" });
            //aggName.Add(new AggForeignKey() { FactColumn = "store_id", AggColumn = "store_id" });

            //aggName.Add(new AggMeasure("[Measures].[Unit Sales]") { Column = "UNIT_SALES_SUM" });
            //aggName.Add(new AggMeasure("[Measures].[Store Cost]") { Column = "STORE_COST_SUM" });
            //aggName.Add(new AggMeasure("[Measures].[Store Sales]") { Column = "STORE_SALES_SUM" });

            //aggName.Add(new AggLevel("[Time].[Year]") { Column = "TIME_YEAR" });
            //aggName.Add(new AggLevel("[Time].[Quarter]") { Column = "TIME_QUARTER" });
            //aggName.Add(new AggLevel("[Time].[Month]") { Column = "TIME_MONTH" });

            //cubeSales.Table.Add(aggName);
            //#endregion

            //#region Dimension

            ////cubeSales.Add(new DimensionUsage("Store") { Source = "Store", ForeignKey = "store_id" });
            ////cubeSales.Add(new DimensionUsage("Store Size in SQFT") { Source = "Store Size in SQFT", ForeignKey = "store_id" });
            ////cubeSales.Add(new DimensionUsage("Store Type") { Source = "Store Type", ForeignKey = "store_id" });
            ////cubeSales.Add(new DimensionUsage("Time") { Source = "Time", ForeignKey = "time_id" });
            ////cubeSales.Add(new DimensionUsage("Product") { Source = "Product", ForeignKey = "product_id" });

            ////Dimension dimCustomers = new Dimension("Customers") { ForeignKey = "customer_id" };

            ////Hierarchy hieCustomers = new Hierarchy() { HasAll = true, AllMemberName = "All Customers", PrimaryKey = "customer_id", };

            ////hieCustomers.Table = new Justin.BI.DBLibrary.Mondrian.Table("customer");

            ////hieCustomers.Add(new Level("Country") { Column = "Country", UniqueMembers = true });
            ////hieCustomers.Add(new Level("State Province") { Column = "state_province", UniqueMembers = true, });
            ////hieCustomers.Add(new Level("City") { Column = "city", UniqueMembers = false, });

            ////Level levelCustomerName = new Level("Name") { Column = "customer_id", ColumnType = ColumnType.Numeric, UniqueMembers = true };
            ////levelCustomerName.NameExpression = new List<SQL>();
            ////levelCustomerName.NameExpression.Add(new SQL() { Dialect = "oracle", Text = "\"fname\" || ' ' || \"lname\"" });
            ////levelCustomerName.NameExpression.Add(new SQL() { Dialect = "mysql", Text = "CONCAT(`customer`.`fname`, ' ', `customer`.`lname`)" });
            ////levelCustomerName.NameExpression.Add(new SQL() { Dialect = "mssql", Text = "fname + ' ' + lname" });
            ////levelCustomerName.NameExpression.Add(new SQL() { Dialect = "generic", Text = "fullname" });

            ////levelCustomerName.OrdinalExpression = new List<SQL>();
            ////levelCustomerName.OrdinalExpression.Add(new SQL() { Dialect = "oracle", Text = "\"fname\" || ' ' || \"lname\"" });
            ////levelCustomerName.OrdinalExpression.Add(new SQL() { Dialect = "mysql", Text = "CONCAT(`customer`.`fname`, ' ', `customer`.`lname`)" });
            ////levelCustomerName.OrdinalExpression.Add(new SQL() { Dialect = "mssql", Text = "fname + ' ' + lname" });
            ////levelCustomerName.OrdinalExpression.Add(new SQL() { Dialect = "generic", Text = "fullname" });

            ////levelCustomerName.Add(new Property("Gender") { Column = "Gender" });
            ////levelCustomerName.Add(new Property("Marital Status") { Column = "marital_status" });
            ////levelCustomerName.Add(new Property("Education") { Column = "education" });
            ////levelCustomerName.Add(new Property("Yearly Income") { Column = "yearly_income" });

            ////hieCustomers.Add(levelCustomerName);


            ////dimCustomers.Add(hieCustomers);
            ////cubeSales.Add(dimCustomers);

            //#endregion

            //#region Measure

            //cubeSales.Add(new Measure("Unit Sales") { Column = "unit_sales", Aggregator = Aggregator.Sum, FormatString = "Standard" });
            //cubeSales.Add(new Measure("Sales Count") { Column = "product_id", Aggregator = Aggregator.Count, FormatString = "#,###" });
            //cubeSales.Add(new Measure("Customer Count") { Column = "customer_id", Aggregator = Aggregator.DistinctCount, FormatString = "#,###" });

            //var measurePromotionSales = new Measure("Promotion Sales") { Aggregator = Aggregator.Sum, FormatString = "#,###.00" };

            //List<SQL> measureExpression = new List<SQL>();
            //measureExpression.Add(new SQL() { Dialect = "oracle", Text = "(case when \"sales_fact_1997\".\"promotion_id\" = 0 then 0 else \"sales_fact_1997\".\"store_sales\" end)" });
            //measureExpression.Add(new SQL() { Dialect = "mysql", Text = " (case when `sales_fact_1997`.`promotion_id` = 0 then 0 else `sales_fact_1997`.`store_sales` end)" });
            //measureExpression.Add(new SQL() { Dialect = "generic", Text = "(case when sales_fact_1997.promotion_id = 0 then 0 else sales_fact_1997.store_sales end)" });
            //measurePromotionSales.MeasureExpression = measureExpression.ToArray();
            //cubeSales.Add(measurePromotionSales);
            //var measureCalMerber1 = new CalculatedMember("Profit last Period")
            //{
            //    Dimension = "Measures",
            //    Formula = "COALESCEEMPTY((Measures.[Profit], [Time].[Time].PREVMEMBER),    Measures.[Profit])",
            //    Visible = false,

            //};
            //measureCalMerber1.Add(new CalculatedMemberProperty("FORMAT_STRING") { Value = "$#,##0.00" });
            //measureCalMerber1.Add(new CalculatedMemberProperty("MEMBER_ORDINAL") { Value = "18" });

            //var measureCalMerber2 = new CalculatedMember("Profit Growth")
            //{
            //    Dimension = "Measures",
            //    Formula = "([Measures].[Profit] - [Measures].[Profit last Period]) / [Measures].[Profit last Period]",
            //    Visible = true,
            //    Caption = "Gewinn-Wachstum"
            //};

            //measureCalMerber2.Add(new CalculatedMemberProperty("FORMAT_STRING") { Value = "0.0%" });

            //cubeSales.Add(measureCalMerber1);
            //cubeSales.Add(measureCalMerber2);

            //cubeSales.Add(new NamedSet("Top Sellers") { Formula = "TopCount([Warehouse].[Warehouse Name].MEMBERS, 5, [Measures].[Warehouse Sales])" });



            //#endregion

            //#endregion

            //Schema schema = new Schema("foodmart");
            //schema.Add(dimStore);
            //schema.Add(dimTime);
            //schema.Add(dimProduct);

            //schema.Add(cubeSales);
            //schema.Add(cubeSalesRagged);

            //schema.Add(vCube);
            //var mG1 = new MemberGrant() { Member = "[Store].[USA].[CA]", Access = "all" };
            //var mG2 = new MemberGrant() { Member = "[Store].[USA].[CA].[Los Angeles]", Access = "none" };
            //var mG3 = new MemberGrant() { Member = "[Customers].[USA].[CA]", Access = "all" };
            //var mG4 = new MemberGrant() { Member = "[Customers].[USA].[CA].[Los Angeles]", Access = "none" };

            //var hieG1 = new HierarchyGrant() { Hierarchy = "[Store]", Access = "all", TopLevel = "[Store].[Store Country]" };
            //hieG1.Add(new MemberGrant[] { mG1, mG2 });
            //var hieG2 = new HierarchyGrant() { Hierarchy = "[Customers]", Access = "custom", BottomLevel = "[Customers].[City]", TopLevel = "[Customers].[State Province]" };
            //hieG2.Add(new MemberGrant[] { mG3, mG4 });


            //var cubeG = new CubeGrant() { Access = "all", Cube = "Sales" };
            //cubeG.Add(new HierarchyGrant[] { hieG1, hieG2 });
            //var schemaG = new SchemaGrant() { Access = "none" };
            //schemaG.Add(cubeG);
            //role1.Add(schemaG);
            //schema.Add(role1);
            //schema.Add(role2);



            //#endregion
        }
        public Schema GenerateSchemaFromFile()
        {
            //string fileName = Path.Combine(Application.StartupPath, txtFileName.Text);
            schema = Schema.Deserialize(txtFileName.Text); //SerializeHelper.XmlDeserializeFromFile<Schema>(txtFileName.Text);
            this.ShowMessage("OK");
            return schema;
        }

        #endregion

        #region 功能无关

        public void Folding()
        {

            if (!(this.txtSchemaXML.Document.FoldingManager.FoldingStrategy is XmlFoldingStrategy))
            {
                this.txtSchemaXML.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
            }

            this.txtSchemaXML.Document.FoldingManager.UpdateFoldings(String.Empty, null);
            this.txtSchemaXML.ActiveTextAreaControl.TextArea.Refresh();
        }

        #endregion



        private void ChangeTreeNodesIcon(TreeNodeCollection nodes)
        {
            if (nodes != null && nodes.Count > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    var data = node.Tag;
                    if (data is Cube || data is VirtualCube)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 0;
                    }
                    else if (data is Dimension || data is VirtualCubeDimension || data is DimensionGrant)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 1;
                    }
                    else if (data is Measure || data is VirtualCubeMeasure)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 2;
                    }
                    else if (data is CalculatedMember)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 3;
                    }
                    else if (data is Hierarchy || data is HierarchyGrant)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 4;
                    }
                    else if (data is Level)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 5;
                    }
                    else if (data is Property || data is CalculatedMemberProperty || data is MemberGrant)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 6;
                    }

                    else if (data is DimensionUsage)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 7;
                    }
                    else if (data is NamedSet)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 8;
                    }
                    else if (data is SQL)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 9;
                    }
                    else if (data is Table)
                    {

                    }
                    else if (data is View)
                    {

                    }
                    else if (data is Join)
                    {
                    }
                    else if (data is Role)
                    {
                        node.ImageIndex = node.SelectedImageIndex = 10;
                    }

                    ChangeTreeNodesIcon(node.Nodes);
                }
            }
        }

        private void priviewSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PriviewSchema();
        }

        #region override

        public override string FileName
        {
            get
            {
                return txtFileName.Text;
            }
            set
            {
                txtFileName.Text = value;
            }
        }

        public override void SaveFile(string fileName)
        {
            base.SaveFile(fileName);
            this.schema.Serializer(fileName);
        }
        public override void LoadFile(string fileName)
        {
            base.LoadFile(fileName);
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                txtFileName.Text = fileName;
                if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                {
                    try
                    {
                        GenerateSchemaFromFile();
                        BindTreeview(schema);
                        propertyGrid.SelectedObject = schema;
                        PriviewSchema();
                    }
                    catch (Exception ex)
                    {
                        this.ShowMessage(ex);
                    }
                }
            }
        }

        #endregion

    }
}
