using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Justin.BI.OLAP.Entity.Mondrian
{

    public partial class AggExclude : Element
    {
        public AggExclude() : this("") { }
        public AggExclude(string name)
            : base(name)
        {

        }

        #region 属性

        [XmlAttribute("ignorecase")]
        [DefaultValue("")]
        public string Ignorecase { get; set; }
        [XmlAttribute("pattern")]
        [DefaultValue("")]
        public string Pattern { get; set; }
        #endregion
    }
    public partial class AggName : Element
    {
        public AggName() : this("") { }
        public AggName(string name)
            : base(name)
        {
            this.Ignorecase = true;
        }

        #region 属性
        [XmlAttribute("approxRowCount")]
        [DefaultValue(0)]
        public int ApproxRowCount { get; set; }
        [DefaultValue(true)]
        [XmlAttribute("ignorecase")]
        public bool Ignorecase { get; set; }

        #endregion

    }
    public partial class AggPattern : Element
    {
        public AggPattern() : this("") { }
        public AggPattern(string name)
            : base(name)
        {
            this.Ignorecase = true;
        }

        #region 属性
        [XmlAttribute("pattern")]
        [DefaultValue("")]
        public string Pattern { get; set; }
        [DefaultValue(true)]
        [XmlAttribute("ignorecase")]
        public bool Ignorecase { get; set; }

        #endregion

    }
    public partial class AggFactCount : Element
    {
        public AggFactCount() : this("") { }
        public AggFactCount(string name)
            : base(name)
        {

        }
        #region 属性

        [XmlAttribute("column")]
        [DefaultValue("")]
        public string Column { get; set; }

        #endregion


    }
    public partial class AggIgnoreColumn : Element
    {
        public AggIgnoreColumn() : this("") { }
        public AggIgnoreColumn(string name)
            : base(name)
        {

        }
        #region 属性

        [XmlAttribute("column")]
        [DefaultValue("")]
        public string Column { get; set; }

        #endregion

    }
    public partial class AggForeignKey : Element
    {
        public AggForeignKey() : this("") { }
        public AggForeignKey(string name)
            : base(name)
        {

        }
        #region 属性

        [XmlAttribute("factColumn")]
        [DefaultValue("")]
        public string FactColumn { get; set; }
        [XmlAttribute("aggColumn")]
        [DefaultValue("")]
        public string AggColumn { get; set; }

        #endregion

    }
    public partial class AggMeasure : Element
    {
        public AggMeasure() : this("") { }
        public AggMeasure(string name)
            : base(name)
        {

        }
        #region 属性

        [XmlAttribute("column")]
        [DefaultValue("")]
        public string Column { get; set; }

        #endregion

    }
    public partial class AggLevel : Element
    {
        public AggLevel() : this("") { }
        public AggLevel(string name)
            : base(name)
        {

        }
        #region 属性

        [XmlAttribute("column")]
        [DefaultValue("")]
        public string Column { get; set; }

        #endregion

    }
    public partial class Annotation : Element
    {
        public Annotation() : this("") { }
        public Annotation(string name) : base(name) { }
        [XmlText()]
        [DefaultValue("")]
        public string Value { get; set; }

    }

    public partial class Cube : Element
    {
        public Cube() : this("") { }
        public Cube(string name)
            : base(name)
        {
            this.Cache = true;
            this.Enable = true;
            this.Visible = true;
        }


        #region 属性
        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [DefaultValue(true)]
        [XmlAttribute("cache")]
        public bool Cache { get; set; }
        [DefaultValue(true)]
        [XmlAttribute("enable")]
        public bool Enable { get; set; }
        [DefaultValue(true)]
        [XmlAttribute("visible")]
        public bool Visible { get; set; }
        //++????
        [XmlAttribute("defaultMeasure")]
        [DefaultValue("")]
        public string DefaultMeasure { get; set; }

        #endregion

    }
    public partial class Closure : Element
    {
        public Closure() : this("") { }
        public Closure(string name)
            : base(name)
        {

        }

        #region 属性

        [XmlAttribute("parentColumn")]
        [DefaultValue("")]
        public string ParentColumn { get; set; }
        [XmlAttribute("childColumn")]
        [DefaultValue("")]
        public string ChildColumn { get; set; }

        #endregion

    }
    public partial class CalculatedMember : Element
    {
        public CalculatedMember() : this("") { }
        public CalculatedMember(string name)
            : base(name)
        {
            this.Visible = true;
            this.Dimension = "Measures";
        }
        #region 属性
        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [XmlAttribute("dimension")]
        [DefaultValue("")]
        public string Dimension { get; set; }
        [XmlAttribute("hierarchy")]
        [DefaultValue("")]
        public string Hierarchy { get; set; }
        [XmlAttribute("parent")]
        [DefaultValue("")]
        public string Parent { get; set; }

        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }
        [XmlAttribute("formatString")]
        [DefaultValue("")]
        public string FormatString { get; set; }

        [XmlAttribute("formula")]
        [DefaultValue("")]
        public string Formula { get; set; }


        #endregion

    }
    public partial class CalculatedMemberProperty : Element
    {
        public CalculatedMemberProperty() : this("") { }
        public CalculatedMemberProperty(string name)
            : base(name)
        {

        }
        #region 属性
        [XmlAttribute("value")]
        [DefaultValue("")]
        public string Value { get; set; }

        #endregion

    }
    public partial class CubeGrant : Element
    {
        public CubeGrant() : this("") { }
        public CubeGrant(string name) : base(name) { }

        #region 属性
        [XmlAttribute("cube")]
        [DefaultValue("")]
        public string Cube { get; set; }
        [XmlAttribute("access")]
        public Access Access { get; set; }
        #endregion
    }

    public partial class DimensionUsage : Element
    {
        public DimensionUsage() : this("") { }
        public DimensionUsage(string name)
            : base(name)
        {
            this.Visible = true;
        }

        #region 属性
        [XmlAttribute("source")]
        [DefaultValue("")]
        public string Source { get; set; }
        [XmlAttribute("foreignKey")]
        [DefaultValue("")]
        public string ForeignKey { get; set; }


        [XmlAttribute("level")]
        [DefaultValue("")]
        public string Level { get; set; }
        [XmlAttribute("usagePrefix")]
        [DefaultValue("")]
        public string UsagePrefix { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }
        //++???
        [XmlAttribute("highCardinality")]
        [DefaultValue(false)]
        public bool HighCardinality { get; set; }

        #endregion
    }
    public partial class Dimension : Element
    {

        public Dimension() : this("") { }
        public Dimension(string name)
            : base(name)
        {
            this.Visible = true;
            this.Type = DimensionType.StandardDimension;
        }

        #region 属性
        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        [XmlAttribute("foreignKey")]
        [DefaultValue("")]
        public string ForeignKey { get; set; }
        [DefaultValue(DimensionType.StandardDimension)]
        [XmlAttribute("type")]
        public DimensionType Type { get; set; }
        [XmlAttribute("usagePrefix")]
        [DefaultValue("")]
        public string UsagePrefix { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }
        #endregion
    }
    public partial class DimensionGrant : Element
    {
        public DimensionGrant() : this("") { }
        public DimensionGrant(string name) : base(name) { }

        #region 属性
        [XmlAttribute("dimension")]
        [DefaultValue("")]
        public string Dimension { get; set; }
        [XmlAttribute("access")]
        public Access Access { get; set; }
        #endregion
    }

    public partial class Hierarchy : Element
    {
        public Hierarchy() : this("") { }
        public Hierarchy(string name)
            : base(name)
        {
            this.HasAll = true;
            this.Visible = true;
        }

        #region 属性
        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        //[DefaultValue(true)]
        [XmlAttribute("hasAll")]
        public bool HasAll { get; set; }
        [XmlAttribute("allMemberName")]
        [DefaultValue("")]
        public string AllMemberName { get; set; }
        [XmlAttribute("allMemberCaption")]
        [DefaultValue("")]
        public string allMemberCaption { get; set; }
        [XmlAttribute("allLevelName")]
        [DefaultValue("")]
        public string allLevelName { get; set; }

        [XmlAttribute("defaultMember")]
        [DefaultValue("")]
        public string DefaultMember { get; set; }
        [XmlAttribute("memberReaderClass")]
        [DefaultValue("")]
        public string MemberReaderClass { get; set; }
        [XmlAttribute("primaryKey")]
        [DefaultValue("")]
        public string PrimaryKey { get; set; }
        [XmlAttribute("primaryKeyTable")]
        [DefaultValue("")]
        public string PrimaryKeyTable { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }

        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }

        #endregion
    }
    public partial class HierarchyGrant : Element
    {
        public HierarchyGrant() : this("") { }
        public HierarchyGrant(string name) : base(name) { }

        #region 属性
        [XmlAttribute("hierarchy")]
        [DefaultValue("")]
        public string Hierarchy { get; set; }
        [XmlAttribute("access")]
        public Access Access { get; set; }
        [XmlAttribute("topLevel")]
        [DefaultValue("")]
        public string TopLevel { get; set; }
        [XmlAttribute("bottomLevel")]
        [DefaultValue("")]
        public string BottomLevel { get; set; }
        #endregion
    }

    public partial class InlineTable : DataSourceElement
    {
        public InlineTable() : this("") { }
        public InlineTable(string name)
            : base(name)
        {
        }

        #region 属性
        [XmlAttribute("language")]
        [DefaultValue("")]
        public string Language { get; set; }
        [XmlText]
        [DefaultValue("")]
        public string Value { get; set; }

        #endregion

    }

    public partial class Join : DataSourceElement
    {
        public Join() : this("") { }
        public Join(string name)
            : base(name)
        {

        }

        #region 属性

        [XmlAttribute("leftKey")]
        [DefaultValue("")]
        public string LeftKey { get; set; }
        [XmlAttribute("rightKey")]
        [DefaultValue("")]
        public string RightKey { get; set; }

        #endregion

    }

    public partial class Level : Element
    {
        public Level() : this("") { }

        public Level(string name)
            : base(name)
        {
            this.Visible = true;
            this.UniqueMembers = false;
            this.LevelType = LevelType.Regular;
            this.ColumnType = ColumnType.String;
            this.HideMemberIf = HideMemberIf.Never;
        }

        #region 属性

        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        [XmlAttribute("table")]
        [DefaultValue("")]
        public string Table { get; set; }
        [XmlAttribute("column")]
        [DefaultValue("")]
        public string Column { get; set; }
        [XmlAttribute("nameColumn")]
        [DefaultValue("")]
        public string NameColumn { get; set; }
        [XmlAttribute("parentColumn")]
        [DefaultValue("")]
        public string ParentColumn { get; set; }

        [XmlAttribute("nullParentValue")]
        [DefaultValue("")]
        public string NullParentValue { get; set; }
        [XmlAttribute("ordinalColumn")]
        [DefaultValue("")]
        public string OrdinalColumn { get; set; }
        [DefaultValue(ColumnType.String)]
        [XmlAttribute("type")]
        public ColumnType ColumnType { get; set; }
        [XmlAttribute("internalType")]
        public InternalType InternalType { get; set; }
        [DefaultValue(false)]
        [XmlAttribute("uniqueMembers")]
        public bool UniqueMembers { get; set; }
        [DefaultValue(LevelType.Regular)]
        [XmlAttribute("levelType")]
        public LevelType LevelType { get; set; }
        [DefaultValue(HideMemberIf.Never)]
        [XmlAttribute("hideMemberIf")]
        public HideMemberIf HideMemberIf { get; set; }
        [XmlAttribute("approxRowCount")]
        [DefaultValue("")]
        public string approxRowCount { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [XmlAttribute("captionColumn")]
        [DefaultValue("")]
        public string CaptionColumn { get; set; }

        [XmlAttribute("formatter")]
        [DefaultValue("")]
        public string Formatter { get; set; }
        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }

        #endregion
    }

    public partial class Measure : Element
    {
        public Measure() : this("") { }
        public Measure(string name)
            : base(name)
        {
            this.Visible = true;
            this.Aggregator = Aggregator.Sum;
            this.DataType = ColumnType.Numeric;
        }

        #region 属性
        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        //[DefaultValue(Aggregator.Sum)]
        [XmlAttribute("aggregator")]
        public Aggregator Aggregator { get; set; }
        [XmlAttribute("column")]
        [DefaultValue("")]
        public string Column { get; set; }
        [XmlAttribute("formatString")]
        public FormatString FormatString { get; set; }
        [DefaultValue(ColumnType.Numeric)]
        [XmlAttribute("datatype")]
        public ColumnType DataType { get; set; }

        [XmlAttribute("formatter")]
        [DefaultValue("")]
        public string Formatter { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }

        #endregion

    }
    public partial class MemberGrant : Element
    {
        public MemberGrant() : this("") { }
        public MemberGrant(string name) : base(name) { }

        #region 属性
        [XmlAttribute("member")]
        [DefaultValue("")]
        public string Member { get; set; }
        [XmlAttribute("access")]
        public Access Access { get; set; }
        #endregion
    }
    public partial class MemberFormatter : Element
    {
        public MemberFormatter() : this("") { }
        public MemberFormatter(string name)
            : base(name)
        {
        }

        #region 属性

        #endregion

    }

    public partial class NamedSet : Element
    {
        public NamedSet() : this("") { }
        public NamedSet(string name)
            : base(name)
        {

        }
        #region 属性
        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        [XmlElement("Formula")]
        [DefaultValue("")]
        public string Formula { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }

        #endregion

    }

    public partial class Property : Element
    {

        public Property() : this("") { }
        public Property(string name)
            : base(name)
        {
            this.ColumnType = ColumnType.String;
        }
        #region 属性

        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }
        [XmlAttribute("column")]
        [DefaultValue("")]
        public string Column { get; set; }
        [DefaultValue(ColumnType.String)]
        [XmlAttribute("type")]
        public ColumnType ColumnType { get; set; }
        [XmlAttribute("formatter")]
        [DefaultValue("")]
        public string Formatter { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }

        #endregion

    }
    public partial class Parameter : Element
    {
        public Parameter() : this("") { }
        public Parameter(string name)
            : base(name)
        {
        }

        #region 属性
        [XmlAttribute("language")]
        [DefaultValue("")]
        public string Language { get; set; }
        [XmlText]
        [DefaultValue("")]
        public string Value { get; set; }

        #endregion

    }
    public partial class PropertyFormatter : Element
    {
        public PropertyFormatter() : this("") { }
        public PropertyFormatter(string name)
            : base(name)
        {
        }

        #region 属性
        [XmlAttribute("className")]
        [DefaultValue("")]
        public string ClassName { get; set; }

        #endregion

    }

    public partial class Role : Element
    {
        public Role() : this("") { }
        public Role(string name) : base(name) { }
    }

    public partial class Script : Element
    {
        public Script() : this("") { }
        public Script(string name)
            : base(name)
        {
        }

        #region 属性
        [XmlAttribute("language")]
        [DefaultValue("")]
        public string Language { get; set; }
        [XmlText]
        [DefaultValue("")]
        public string Value { get; set; }

        #endregion

    }
    public partial class Schema : Element
    {
        public Schema() : this("") { }
        public Schema(string name)
            : base(name)
        {
            this.Parameters = new List<Parameter>();
            this.Cubes = new List<Cube>();
        }

        #region 属性
        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }

        [XmlAttribute("measuresCaption")]
        [DefaultValue("")]
        public string MeasuresCaption { get; set; }

        [XmlAttribute("defaultRole")]
        [DefaultValue("")]
        public string DefaultRole { get; set; }

        #endregion

    }
    public partial class SchemaGrant : Element
    {
        public SchemaGrant() : this("") { }
        public SchemaGrant(string name) : base(name) { }

        #region 属性

        [XmlAttribute("access")]
        public Access Access { get; set; }
        #endregion
    }
    public partial class SQL : Element
    {
        public SQL() : this("") { }
        public SQL(string name)
            : base(name)
        {

        }
        #region 属性
        [XmlAttribute("dialect")]
        public SQLDialect Dialect { get; set; }
        [XmlIgnore]
        //[XmlText()]
        [DefaultValue("")]
        public string Text { get; set; }

        #endregion

        private string dialectAlias = "dialect";
    }

    public partial class Table : DataSourceElement
    {
        public Table() : this("") { }
        public Table(string name)
            : base(name)
        {

        }

        #region 属性

        [XmlAttribute("schema")]
        [DefaultValue("")]
        public string Schema { get; set; }
        [XmlAttribute("alias")]
        [DefaultValue("")]
        public string Alias { get; set; }

        #endregion

    }

    public partial class UserDefinedFunction : Element
    {
        public UserDefinedFunction() : this("") { }
        public UserDefinedFunction(string name)
            : base(name)
        {
        }

        #region 属性
        [XmlAttribute("className")]
        [DefaultValue("")]
        public string ClassName { get; set; }

        #endregion


    }

    public partial class View : DataSourceElement
    {
        public View() : this("") { }
        public View(string name)
            : base(name)
        {

        }

        #region 属性

        [XmlAttribute("alias")]
        [DefaultValue("")]
        public string Alias { get; set; }

        #endregion

    }
    public partial class VirtualCube : Element
    {
        public VirtualCube() : this("") { }
        public VirtualCube(string name)
            : base(name)
        {
            this.Enable = true;
            this.Visible = true;
        }

        #region 属性

        [XmlAttribute("description")]
        [DefaultValue("")]
        public string Description { get; set; }

        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [DefaultValue(true)]
        [XmlAttribute("enable")]
        public bool Enable { get; set; }
        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }

        #endregion

    }
    public partial class VirtualCubeDimension : Element
    {
        public VirtualCubeDimension() : this("") { }
        public VirtualCubeDimension(string name)
            : base(name)
        {
        }

        #region 属性
        [XmlAttribute("cubeName")]
        [DefaultValue("")]
        public string CubeName { get; set; }
        [XmlAttribute("caption")]
        [DefaultValue("")]
        public string Caption { get; set; }
        [XmlAttribute("foreignKey")]
        [DefaultValue("")]
        public string ForeignKey { get; set; }

        #endregion
    }
    public partial class VirtualCubeMeasure : Element
    {
        public VirtualCubeMeasure() : this("") { }
        public VirtualCubeMeasure(string name)
            : base(name)
        {
            this.Visible = true;
        }

        #region 属性
        [XmlAttribute("cubeName")]
        [DefaultValue("")]
        public string CubeName { get; set; }
        [XmlAttribute("visible")]
        [DefaultValue(true)]
        public bool Visible { get; set; }

        #endregion
    }


}

