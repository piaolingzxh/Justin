using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Justin.Controls.Mondrian
{
    #region 特性定义

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class ChildElementAttribute : System.Attribute
    {
        public int Order { get; set; }
        public ChildCategory ChildCategory { get; set; }
        public Type ChildType { get; set; }
        public ChildElementAttribute(int order)
            : this(order, ChildCategory.ChildrenColection)
        {
        }
        public ChildElementAttribute(int order, ChildCategory childCategory)
            : this(order, childCategory, typeof(Element))
        {
        }
        public ChildElementAttribute(int order, ChildCategory childCategory, Type childType)
        {
            this.Order = order;
            this.ChildCategory = childCategory;
            this.ChildType = childType;
        }

    }

    #endregion

    #region  枚举

    [Serializable]
    public enum ElementType
    {
        AggExclude,
        AggName,
        AggFactCount,
        AggIgnoreColumn,
        AggForeignKey,
        AggMeasure,
        AggLevel,
        AggPattern,
        Annotation,

        CalculatedMember,
        CalculatedMemberProperty,
        Cube,
        CubeGrant,
        Closure,

        Dimension,
        DimensionUsage,
        DimensionGrant,

        Element,

        Hierarchy,
        HierarchyGrant,

        InlineTable,

        Join,

        KeyExpression,

        Level,

        Measure,
        MeasureExpression,
        NameExpression,
        MemberGrant,
        MemberFormatter,

        NamedSet,

        OrdinalExpression,

        Property,
        Parameter,
        PropertyFormatter,

        Role,

        Schema,
        SchemaGrant,
        SQL,
        Script,

        Table,

        UserDefinedFunction,

        VirtualCube,
        VirtualCubeDimension,
        VirtualCubeMeasure,
        View,

    }

    [Serializable]
    public enum DimensionType
    {
        StandardDimension,
        TimeDimension
    }
    [Serializable]
    public enum HideMemberIf
    {
        Never,
        IfBlankName,
        IfParentsName,
    }
    [Serializable]
    public enum LevelType
    {
        Regular,
        TimeYears,
        TimeHalfYears,
        TimeHalfYear,
        TimeQuarters,
        TimeMonths,
        TimeWeeks,
        TimeDays,
        TimeHours,
        TimeMinutes,
        TimeSeconds,
        TimeUndefined,
    }
    [Serializable]
    public enum Aggregator
    {
        [XmlEnum("sum")]
        Sum,
        [XmlEnum("count")]
        Count,
        [XmlEnum("min")]
        Min,
        [XmlEnum("max")]
        Max,
        [XmlEnum("avg")]
        AVG,
        [XmlEnum("distinct-count")]
        Distinct_Count,
        [XmlEnum("distinct count")]
        DistinctCount
    }
    [Serializable]
    public enum ColumnType
    {
        Numeric,
        String,
        Integer,
        Boolean,
        Date,
        Time,
        Timestamp,
    }

    public enum SQLDialect
    {
        [XmlEnum("generic")]
        Generic,
        [XmlEnum("access")]
        Access,
        [XmlEnum("db2")]
        Db2,
        [XmlEnum("derby")]
        Derby,
        [XmlEnum("firebird")]
        Firebird,
        [XmlEnum("hsqldb")]
        Hsqldb,
        [XmlEnum("mssql")]
        Mssql,
        [XmlEnum("mysql")]
        Mysql,
        [XmlEnum("oracle")]
        Oracle,
        [XmlEnum("postgres")]
        Postgres,
        [XmlEnum("sysbase")]
        Sysbase,
        [XmlEnum("teradata")]
        Teradata,
        [XmlEnum("ingres")]
        Ingres,
        [XmlEnum("infobright")]
        Infobright,
        [XmlEnum("luciddb")]
        Luciddb,
        [XmlEnum("vertica")]
        Vertica,
        [XmlEnum("neoview")]
        Neoview,
        [XmlEnum("greenplum")]
        Greenplum,
        [XmlEnum("vectorwise")]
        Vectorwise,
        [XmlEnum("hive")]
        Hive,
    }

    public enum InternalType
    {
        [XmlEnum("int")]
        Int,
        [XmlEnum("long")]
        Long,
        [XmlEnum("Object")]
        Object,
        [XmlEnum("String")]
        String,
    }

    public enum FormatString
    {
        [XmlEnum("Standard")]
        Standard,
        [XmlEnum("#,###.00")]
        Decimal2,
        [XmlEnum("#,###")]
        Integer13,
        [XmlEnum("#.0")]
        Decimal1,
        [XmlEnum("Currency")]
        Currency,
        [XmlEnum("#,#")]
        Integer11,

    }

    public enum Access
    {
        [XmlEnum("all")]
        All,
        [XmlEnum("custom")]
        Custom,
        [XmlEnum("none")]
        None,
    }

    public enum ChildCategory
    {
        ChildrenColection,
        ChildrenList,
        Element,
    }

    #endregion

    #region 节点基类

    public abstract partial class Element
    {
        public Element() : this("") { }
        public Element(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.Name = name;
            }

            if (this is IElementList || this is IChildrenCollection)
            {
                if (this is IChildrenCollection)
                {
                    this.items = new List<Element>();
                    this.itemTypes = new List<ElementType>();
                }
                this.InitChildrens();
            }
        }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [Browsable(false)]
        public abstract ElementType ElementType { get; }
        [XmlIgnore()]
        [Browsable(false)]
        public virtual Element[] ChildrenElements { get { return this.Items; } }

        #region 数组子节点

        [XmlIgnore()]
        private List<Element> items;
        [XmlIgnore()]
        private List<ElementType> itemTypes;

        [Browsable(false)]
        [XmlIgnore]
        internal Element[] Items
        {
            get
            {
                if (this.items == null)
                {
                    return null;
                }
                return this.items.ToArray();
            }
            set
            {
                if (value != null)
                {
                    items.Clear();
                    items.AddRange(value);
                }
                else
                {
                    if (items != null)
                        items.Clear();
                }
            }
        }
        [Browsable(false)]
        [XmlIgnore]
        internal ElementType[] ItemTypes
        {
            get
            {
                if (this.itemTypes == null)
                {
                    return null;
                }
                return itemTypes.ToArray();
            }
            set
            {
                if (value != null)
                {
                    itemTypes.Clear();
                    itemTypes.AddRange(value);
                }
                else
                {
                    if (itemTypes != null)
                        itemTypes.Clear();
                }
            }
        }

        #endregion

        internal virtual void InitChildrens() { }
    }
    public abstract class DataSourceElement : Element
    {
        public DataSourceElement() : this("") { }
        public DataSourceElement(string name)
            : base(name)
        {

        }
    }
    /// <summary>
    ///     是否含有List的子节点
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    internal interface IElementList { }
    /// <summary>
    ///     是否含有集合子节点（存储在Items中）
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    internal interface IChildrenCollection { }

    public interface IName
    {
        string Name { get; set; }
    }

    #endregion


    public partial class AggExclude : Element
    {
        public override ElementType ElementType { get { return ElementType.AggExclude; } }

    }
    public partial class AggName : Element, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.AggName; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("AggFactCount", typeof(AggFactCount))]
        [XmlElementAttribute("AggIgnoreColumn", typeof(AggIgnoreColumn))]
        [XmlElementAttribute("AggForeignKey", typeof(AggForeignKey))]
        [XmlElementAttribute("AggMeasure", typeof(AggMeasure))]
        [XmlElementAttribute("AggLevel", typeof(AggLevel))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息
        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(AggFactCount))]
        public List<AggFactCount> AggFactCounts { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(2, ChildCategory.ChildrenColection, ChildType = typeof(AggIgnoreColumn))]
        public List<AggIgnoreColumn> AggIgnoreColumns { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(4, ChildCategory.ChildrenColection, ChildType = typeof(AggForeignKey))]
        public List<AggForeignKey> AggForeignKeies { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(6, ChildCategory.ChildrenColection, ChildType = typeof(AggMeasure))]
        public List<AggMeasure> AggMeasures { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(8, ChildCategory.ChildrenColection, ChildType = typeof(AggLevel))]
        public List<AggLevel> AggLevels { get; set; }


        #endregion
        override internal void InitChildrens()
        {
            this.AggIgnoreColumns = new List<AggIgnoreColumn>();
            this.AggForeignKeies = new List<AggForeignKey>();
            this.AggMeasures = new List<AggMeasure>();
            this.AggLevels = new List<AggLevel>();
            this.AggFactCounts = new List<AggFactCount>();
        }
    }
    public partial class AggPattern : Element, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.AggPattern; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("AggIgnoreColumn", typeof(AggIgnoreColumn))]
        [XmlElementAttribute("AggForeignKey", typeof(AggForeignKey))]
        [XmlElementAttribute("AggMeasure", typeof(AggMeasure))]
        [XmlElementAttribute("AggLevel", typeof(AggLevel))]
        [XmlElementAttribute("AggFactCount", typeof(AggFactCount))]
        [XmlElementAttribute("AggExclude", typeof(AggExclude))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(AggIgnoreColumn))]
        public List<AggIgnoreColumn> AggIgnoreColumns { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(2, ChildCategory.ChildrenColection, ChildType = typeof(AggIgnoreColumn))]

        public List<AggForeignKey> AggForeignKeies { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(4, ChildCategory.ChildrenColection, ChildType = typeof(AggIgnoreColumn))]

        public List<AggMeasure> AggMeasures { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(6, ChildCategory.ChildrenColection, ChildType = typeof(AggIgnoreColumn))]

        public List<AggLevel> AggLevels { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(8, ChildCategory.ChildrenColection, ChildType = typeof(AggFactCount))]

        public List<AggFactCount> AggFactCounts { get; set; }


        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(10, ChildCategory.ChildrenColection, ChildType = typeof(AggExclude))]

        public List<AggExclude> AggExcludes { get; set; }
        #endregion

        override internal void InitChildrens()
        {
            this.AggIgnoreColumns = new List<AggIgnoreColumn>();
            this.AggForeignKeies = new List<AggForeignKey>();
            this.AggMeasures = new List<AggMeasure>();
            this.AggLevels = new List<AggLevel>();
            this.AggFactCounts = new List<AggFactCount>();
            this.AggExcludes = new List<AggExclude>();
        }
    }
    public partial class AggFactCount : Element
    {
        public override ElementType ElementType { get { return ElementType.AggFactCount; } }
    }
    public partial class AggIgnoreColumn : Element
    {
        public override ElementType ElementType { get { return ElementType.AggIgnoreColumn; } }
    }
    public partial class AggForeignKey : Element
    {
        public override ElementType ElementType { get { return ElementType.AggForeignKey; } }
    }
    public partial class AggMeasure : Element
    {
        public override ElementType ElementType { get { return ElementType.AggMeasure; } }
    }
    public partial class AggLevel : Element
    {
        public override ElementType ElementType { get { return ElementType.AggLevel; } }
    }
    public partial class Annotation : Element
    {
        public override ElementType ElementType { get { return ElementType.Annotation; } }
    }

    //+?
    public partial class Cube : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.Cube; } }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        #region DataSource

        private DataSourceElement DataSource { get; set; }
        [ChildElement(-6, ChildCategory.Element)]
        public Table Table
        {
            get
            {
                if (DataSource is Table)
                {
                    return (Table)DataSource;
                }
                else { return null; }
            }
            set
            {
                if (value is Table)
                {
                    DataSource = value;
                }
            }
        }
        [ChildElement(-4, ChildCategory.Element)]
        public Join Join
        {
            get
            {
                if (DataSource is Join)
                {
                    return (Join)DataSource;
                }
                else { return null; }
            }
            set
            {
                if (value is Join)
                {
                    DataSource = value;
                }
            }
        }
        [ChildElement(-2, ChildCategory.Element)]
        public View View
        {
            get
            {
                if (DataSource is View)
                {
                    return (View)DataSource;
                }
                else { return null; }
            }
            set
            {
                if (value is View)
                {
                    DataSource = value;
                }
            }
        }

        #endregion

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("DimensionUsage", typeof(DimensionUsage))]
        [XmlElementAttribute("Dimension", typeof(Dimension))]
        [XmlElementAttribute("Measure", typeof(Measure))]
        [XmlElementAttribute("CalculatedMember", typeof(CalculatedMember))]
        [XmlElementAttribute("NamedSet", typeof(NamedSet))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(2, ChildCategory.ChildrenColection, ChildType = typeof(DimensionUsage))]
        public List<DimensionUsage> DimensionUsages { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(4, ChildCategory.ChildrenColection, ChildType = typeof(Dimension))]
        public List<Dimension> Dimensions { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(6, ChildCategory.ChildrenColection, ChildType = typeof(Measure))]
        public List<Measure> Measures { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(8, ChildCategory.ChildrenColection, ChildType = typeof(CalculatedMember))]
        public List<CalculatedMember> CalculatedMembers { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(10, ChildCategory.ChildrenColection, ChildType = typeof(NamedSet))]
        public List<NamedSet> NamedSets { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.DimensionUsages = new List<DimensionUsage>();
            this.Dimensions = new List<Dimension>();
            this.Measures = new List<Measure>();
            this.CalculatedMembers = new List<CalculatedMember>();
            this.NamedSets = new List<NamedSet>();
        }
    }
    public partial class Closure : Element
    {
        public override ElementType ElementType { get { return ElementType.Closure; } }
        [ChildElement(0, ChildCategory.Element)]
        [XmlElement("Table")]
        public Table Table { get; set; }
    }
    //+?
    public partial class CalculatedMember : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.CalculatedMember; } }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        [XmlElement("Formula")]
        public string FormulaElement { get; set; }
        //public List<Script> CellFormatter { get; set; }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("CalculatedMemberProperty", typeof(CalculatedMemberProperty))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(CalculatedMemberProperty))]
        public List<CalculatedMemberProperty> CalculatedMemberProperties { get; set; }


        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.CalculatedMemberProperties = new List<CalculatedMemberProperty>();
        }

    }
    public partial class CalculatedMemberProperty : Element
    {
        public override ElementType ElementType { get { return ElementType.CalculatedMemberProperty; } }
    }
    public partial class CubeGrant : Element, IChildrenCollection
    {

        public override ElementType ElementType { get { return ElementType.CubeGrant; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("DimensionGrant", typeof(DimensionGrant))]
        [XmlElementAttribute("HierarchyGrant", typeof(HierarchyGrant))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion
        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(DimensionGrant))]
        public List<DimensionGrant> DimensionGrants { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(2, ChildCategory.ChildrenColection, ChildType = typeof(HierarchyGrant))]
        public List<HierarchyGrant> HierarchyGrants { get; set; }

        #endregion
        override internal void InitChildrens()
        {
            this.HierarchyGrants = new List<HierarchyGrant>();
            this.DimensionGrants = new List<DimensionGrant>();
        }
    }

    public partial class DimensionUsage : Element
    {
        public override ElementType ElementType { get { return ElementType.DimensionUsage; } }
    }
    //+?
    public partial class Dimension : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.Dimension; } }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        #region 集合子节点

        [Browsable(false)]
        [XmlElementAttribute("Hierarchy", typeof(Hierarchy))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(Hierarchy))]
        public List<Hierarchy> Hierarchies { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.Hierarchies = new List<Hierarchy>();
        }
    }
    public partial class DimensionGrant : Element
    {
        public override ElementType ElementType { get { return ElementType.DimensionGrant; } }
    }
    //+?
    public partial class Hierarchy : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.Hierarchy; } }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        #region DataSource

        private DataSourceElement DataSource { get; set; }
        [ChildElement(-6, ChildCategory.Element)]
        public Table Table
        {
            get
            {
                if (DataSource is Table)
                {
                    return (Table)DataSource;
                }
                else { return null; }
            }
            set
            {
                if (value is Table)
                {
                    DataSource = value;
                }
            }
        }
        [ChildElement(-4, ChildCategory.Element)]
        public Join Join
        {
            get
            {
                if (DataSource is Join)
                {
                    return (Join)DataSource;
                }
                else { return null; }
            }
            set
            {
                if (value is Join)
                {
                    DataSource = value;
                }
            }
        }
        [ChildElement(-2, ChildCategory.Element)]
        public View View
        {
            get
            {
                if (DataSource is View)
                {
                    return (View)DataSource;
                }
                else { return null; }
            }
            set
            {
                if (value is View)
                {
                    DataSource = value;
                }
            }
        }
        [ChildElement(0, ChildCategory.Element)]
        public InlineTable InlineTable
        {
            get
            {
                if (DataSource is InlineTable)
                {
                    return (InlineTable)DataSource;
                }
                else { return null; }
            }
            set
            {
                if (value is InlineTable)
                {
                    DataSource = value;
                }
            }
        }

        #endregion

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("Level", typeof(Level))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(Level))]
        public List<Level> Levels { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.Levels = new List<Level>();
        }
    }
    public partial class HierarchyGrant : Element, IChildrenCollection
    {

        public override ElementType ElementType { get { return ElementType.HierarchyGrant; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("MemberGrant", typeof(MemberGrant))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(MemberGrant))]
        public List<MemberGrant> MemberGrants { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.MemberGrants = new List<MemberGrant>();
        }
    }

    public partial class InlineTable : DataSourceElement
    {
        public override ElementType ElementType { get { return ElementType.InlineTable; } }
    }

    public partial class Join : DataSourceElement, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.Join; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("Table", typeof(Table))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(Table))]
        public List<Table> Tables { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.Tables = new List<Table>();
        }

    }
    //+?
    public partial class Level : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.Level; } }

        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }
        [ChildElement(-7, ChildCategory.ChildrenList)]
        public List<SQL> KeyExpression { get; set; }
        [ChildElement(-6, ChildCategory.ChildrenList)]
        public List<SQL> NameExpression { get; set; }
        [ChildElement(-5, ChildCategory.ChildrenList)]
        public List<SQL> CaptionExpression { get; set; }
        [ChildElement(-4, ChildCategory.ChildrenList)]
        public List<SQL> OrdinalExpression { get; set; }
        [ChildElement(-3, ChildCategory.ChildrenList)]
        public List<SQL> ParentExpression { get; set; }
        [ChildElement(-2, ChildCategory.Element)]
        public MemberFormatter MemberFormatter { get; set; }
        [ChildElement(-1, ChildCategory.Element)]
        public Closure Closure { get; set; }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("Property", typeof(Property))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(Property))]
        public List<Property> Properties { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.KeyExpression = new List<SQL>();
            this.NameExpression = new List<SQL>();
            this.CaptionExpression = new List<SQL>();
            this.OrdinalExpression = new List<SQL>();
            this.ParentExpression = new List<SQL>();
            this.Properties = new List<Property>();
        }

    }

    public partial class Measure : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.Measure; } }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }
        [ChildElement(-6, ChildCategory.ChildrenList)]
        public List<SQL> MeasureExpression { get; set; }
        //public List<Script> CellFormatter { get; set; }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("CalculatedMemberProperty", typeof(CalculatedMemberProperty))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }
        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(CalculatedMemberProperty))]
        public List<CalculatedMemberProperty> CalculatedMemberProperties { get; set; }


        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.MeasureExpression = new List<SQL>();
            this.CalculatedMemberProperties = new List<CalculatedMemberProperty>();
        }

    }
    public partial class MemberGrant : Element
    {
        public override ElementType ElementType { get { return ElementType.MemberGrant; } }
    }
    public partial class MemberFormatter : Element
    {
        public override ElementType ElementType { get { return ElementType.MemberFormatter; } }
        [ChildElement(-2, ChildCategory.Element)]
        public Script Script { get; set; }
    }

    public partial class NamedSet : Element
    {
        public override ElementType ElementType { get { return ElementType.NamedSet; } }
    }

    public partial class Parameter : Element
    {
        public override ElementType ElementType { get { return ElementType.Parameter; } }
    }
    public partial class Property : Element
    {
        public override ElementType ElementType { get { return ElementType.Property; } }

        public PropertyFormatter PropertyFormatter { get; set; }

    }
    public partial class PropertyFormatter : Element
    {
        public override ElementType ElementType { get { return ElementType.PropertyFormatter; } }
        [ChildElement(-2, ChildCategory.Element)]
        public Script Script { get; set; }

    }

    public partial class Role : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType
        {
            get
            {
                return ElementType.Role;
            }
        }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("SchemaGrant", typeof(SchemaGrant))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(SchemaGrant))]
        public List<SchemaGrant> SchemaGrants { get; set; }


        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.SchemaGrants = new List<SchemaGrant>();
        }

    }

    public partial class Schema : Element, IElementList, IChildrenCollection, IName
    {
        public override ElementType ElementType { get { return ElementType.Schema; } }

        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        #region 集合子节点

        [Browsable(false)]
        [XmlElementAttribute("Parameter", typeof(Parameter))]
        [XmlElementAttribute("Dimension", typeof(Dimension))]
        [XmlElementAttribute("Cube", typeof(Cube))]
        [XmlElementAttribute("VirtualCube", typeof(VirtualCube))]
        [XmlElementAttribute("NamedSet", typeof(NamedSet))]
        [XmlElementAttribute("Role", typeof(Role))]
        [XmlElementAttribute("UserDefinedFunction", typeof(UserDefinedFunction))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory = ChildCategory.ChildrenColection, ChildType = typeof(Parameter))]
        public List<Parameter> Parameters { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(2, ChildCategory = ChildCategory.ChildrenColection, ChildType = typeof(Dimension))]
        public List<Dimension> Dimensions { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(4, ChildCategory = ChildCategory.ChildrenColection, ChildType = typeof(Cube))]
        public List<Cube> Cubes { get; set; }

        [Browsable(false)]
        [ChildElement(6, ChildCategory = ChildCategory.ChildrenColection, ChildType = typeof(VirtualCube))]
        [XmlIgnore()]
        public List<VirtualCube> VirtualCubes { get; set; }

        [Browsable(false)]
        [ChildElement(8, ChildCategory = ChildCategory.ChildrenColection, ChildType = typeof(NamedSet))]
        [XmlIgnore()]
        public List<NamedSet> NamedSets { get; set; }

        [Browsable(false)]
        [ChildElement(10, ChildCategory = ChildCategory.ChildrenColection, ChildType = typeof(Role))]
        [XmlIgnore()]
        public List<Role> Roles { get; set; }

        [Browsable(false)]
        [ChildElement(12, ChildCategory = ChildCategory.ChildrenColection, ChildType = typeof(UserDefinedFunction))]
        [XmlIgnore()]
        public List<UserDefinedFunction> UserDefinedFunctions { get; set; }

        #endregion


        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.Parameters = new List<Parameter>();
            this.Dimensions = new List<Dimension>();
            this.Cubes = new List<Cube>();
            this.VirtualCubes = new List<VirtualCube>();
            this.NamedSets = new List<NamedSet>();
            this.Roles = new List<Role>();
            this.UserDefinedFunctions = new List<UserDefinedFunction>();
        }
    }
    public partial class SQL : Element
    {
        public override ElementType ElementType { get { return ElementType.SQL; } }
    }
    public partial class Script : Element
    {
        public override ElementType ElementType { get { return ElementType.Script; } }
    }
    public partial class SchemaGrant : Element, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.SchemaGrant; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("CubeGrant", typeof(CubeGrant))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(CubeGrant))]
        public List<CubeGrant> CubeGrants { get; set; }


        #endregion

        override internal void InitChildrens()
        {
            this.CubeGrants = new List<CubeGrant>();

        }
    }

    public partial class Table : DataSourceElement, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.Table; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("AggExclude", typeof(AggExclude))]
        [XmlElementAttribute("AggName", typeof(AggName))]
        [XmlElementAttribute("AggPattern", typeof(AggPattern))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(AggExclude))]
        public List<AggExclude> AggExcludes { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(2, ChildCategory.ChildrenColection, ChildType = typeof(AggName))]
        public List<AggName> AggNames { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(4, ChildCategory.ChildrenColection, ChildType = typeof(AggPattern))]
        public List<AggPattern> AggPatterns { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.AggExcludes = new List<AggExclude>();
            this.AggNames = new List<AggName>();
            this.AggPatterns = new List<AggPattern>();
        }

    }

    public partial class UserDefinedFunction : Element
    {
        public override ElementType ElementType { get { return ElementType.UserDefinedFunction; } }
    }

    public partial class View : DataSourceElement, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.View; } }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("SQL", typeof(SQL))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(SQL))]
        public List<SQL> SQLList { get; set; }

        #endregion
        override internal void InitChildrens()
        {
            this.SQLList = new List<SQL>();
        }
    }
    public partial class VirtualCube : Element, IElementList, IChildrenCollection
    {
        public override ElementType ElementType { get { return ElementType.VirtualCube; } }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        #region 集合节点

        [Browsable(false)]
        [XmlElementAttribute("VirtualCubeDimension", typeof(VirtualCubeDimension))]
        [XmlElementAttribute("VirtualCubeMeasure", typeof(VirtualCubeMeasure))]
        [XmlElementAttribute("CalculatedMember", typeof(CalculatedMember))]
        [XmlChoiceIdentifier("ElementTypes")]
        public Element[] Elements
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public ElementType[] ElementTypes
        {
            get { return this.ItemTypes; }
            set { this.ItemTypes = value; }
        }

        #endregion

        #region 集合子节点信息

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(0, ChildCategory.ChildrenColection, ChildType = typeof(VirtualCubeDimension))]
        public List<VirtualCubeDimension> VirtualCubeDimensions { get; set; }

        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(2, ChildCategory.ChildrenColection, ChildType = typeof(VirtualCubeMeasure))]
        public List<VirtualCubeMeasure> VirtualCubeMeasures { get; set; }


        [XmlIgnore()]
        [Browsable(false)]
        [ChildElement(4, ChildCategory.ChildrenColection, ChildType = typeof(CalculatedMember))]
        public List<CalculatedMember> CalculatedMembers { get; set; }

        #endregion

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
            this.VirtualCubeDimensions = new List<VirtualCubeDimension>();
            this.VirtualCubeMeasures = new List<VirtualCubeMeasure>();
            this.CalculatedMembers = new List<CalculatedMember>();
        }
    }
    public partial class VirtualCubeDimension : Element
    {
        public override ElementType ElementType { get { return ElementType.VirtualCubeDimension; } }
    }
    public partial class VirtualCubeMeasure : Element, IElementList
    {
        public override ElementType ElementType { get { return ElementType.VirtualCubeMeasure; } }
        [ChildElement(-8, ChildCategory.ChildrenList, ChildType = typeof(Annotation))]
        public List<Annotation> Annotations { get; set; }

        override internal void InitChildrens()
        {
            this.Annotations = new List<Annotation>();
        }

    }


}
