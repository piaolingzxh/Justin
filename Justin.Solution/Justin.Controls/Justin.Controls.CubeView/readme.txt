Measure 
Expression
NumericPrecision
NumericScale
UniqueName
Units

PPPP
DEFAULT_FORMAT_STRING
MEASUREGROUP_NAME
MEASURE_UNQUALIFIED_CAPTION
MEASURE_NAME_SQL_COLUMN_NAME
MEASURE_IS_VISIBLE
EXPRESSION
DATA_TYPE
MEASURE_AGGREGATOR
MEASURE_UNIQUE_NAME


Dim


DimensionType
UniqueName

PPPP
DIMENSION_IS_VISIBLE
DIMENSION_MASTER_NAME
DEFAULT_HIERARCHY
DIMENSION_TYPE
DIMENSION_ORDINAL
DIMENSION_UNIQUE_NAME
DIMENSION_NAME



hierarchy

UniqueName
DefaultMember
HierarchyOrigin





Measure:

CATALOG_NAME    System.String     Adventure Works DW 2008
SCHEMA_NAME    System.String     
CUBE_NAME    System.String     Adventure Works
MEASURE_NAME    System.String     Average Rate
MEASURE_UNIQUE_NAME    System.String     [Measures].[Average Rate]
MEASURE_CAPTION    System.String     Average Rate
MEASURE_GUID    System.Guid     
MEASURE_AGGREGATOR    System.Int32     10
DATA_TYPE    System.UInt16     5
NUMERIC_PRECISION    System.UInt16     16
NUMERIC_SCALE    System.Int16     -1
MEASURE_UNITS    System.String     
DESCRIPTION    System.String     
EXPRESSION    System.String     
MEASURE_IS_VISIBLE    System.Boolean     True
LEVELS_LIST    System.String     
MEASURE_NAME_SQL_COLUMN_NAME    System.String     Average Rate
MEASURE_UNQUALIFIED_CAPTION    System.String     Average Rate
MEASUREGROUP_NAME    System.String     Exchange Rates
MEASURE_DISPLAY_FOLDER    System.String     
DEFAULT_FORMAT_STRING    System.String     #,#.00

dimensions

CATALOG_NAME    System.String     Adventure Works DW 2008
SCHEMA_NAME    System.String     
CUBE_NAME    System.String     Adventure Works
DIMENSION_NAME    System.String     Account
DIMENSION_UNIQUE_NAME    System.String     [Account]
DIMENSION_GUID    System.Guid     
DIMENSION_CAPTION    System.String     Account
DIMENSION_ORDINAL    System.UInt32     21
DIMENSION_TYPE    System.Int16     6
DIMENSION_CARDINALITY    System.UInt32     100
DEFAULT_HIERARCHY    System.String     [Account].[Account Number]
DESCRIPTION    System.String     
IS_VIRTUAL    System.Boolean     False
IS_READWRITE    System.Boolean     False
DIMENSION_UNIQUE_SETTINGS    System.Int32     1
DIMENSION_MASTER_NAME    System.String     Account
DIMENSION_IS_VISIBLE    System.Boolean     True


hierarchy

CATALOG_NAME    System.String     Adventure Works DW 2008
SCHEMA_NAME    System.String     
CUBE_NAME    System.String     Adventure Works
DIMENSION_UNIQUE_NAME    System.String     [Account]
HIERARCHY_NAME    System.String     Account Number
HIERARCHY_UNIQUE_NAME    System.String     [Account].[Account Number]
HIERARCHY_GUID    System.Guid     
HIERARCHY_CAPTION    System.String     Account Number
DIMENSION_TYPE    System.Int16     6
HIERARCHY_CARDINALITY    System.UInt32     101
DEFAULT_MEMBER    System.String     [Account].[Account Number].[All Accounts]
ALL_MEMBER    System.String     [Account].[Account Number].[All Accounts]
DESCRIPTION    System.String     
STRUCTURE    System.Int16     0
IS_VIRTUAL    System.Boolean     False
IS_READWRITE    System.Boolean     False
DIMENSION_UNIQUE_SETTINGS    System.Int32     1
DIMENSION_MASTER_UNIQUE_NAME    System.String     
DIMENSION_IS_VISIBLE    System.Boolean     True
HIERARCHY_ORDINAL    System.UInt32     4
DIMENSION_IS_SHARED    System.Boolean     True
HIERARCHY_IS_VISIBLE    System.Boolean     True
HIERARCHY_ORIGIN    System.UInt16     2
HIERARCHY_DISPLAY_FOLDER    System.String     
INSTANCE_SELECTION    System.UInt16     1
GROUPING_BEHAVIOR    System.UInt16     2


Level
CATALOG_NAME    System.String     Adventure Works DW 2008
SCHEMA_NAME    System.String     
CUBE_NAME    System.String     Adventure Works
DIMENSION_UNIQUE_NAME    System.String     [Account]
HIERARCHY_UNIQUE_NAME    System.String     [Account].[Account Number]
LEVEL_NAME    System.String     (All)
LEVEL_UNIQUE_NAME    System.String     [Account].[Account Number].[(All)]
LEVEL_GUID    System.Guid     
LEVEL_CAPTION    System.String     (All)
LEVEL_NUMBER    System.UInt32     0
LEVEL_CARDINALITY    System.UInt32     1
LEVEL_TYPE    System.Int32     1
DESCRIPTION    System.String     
CUSTOM_ROLLUP_SETTINGS    System.Int32     0
LEVEL_UNIQUE_SETTINGS    System.Int32     0
LEVEL_IS_VISIBLE    System.Boolean     True
LEVEL_ORDERING_PROPERTY    System.String     (All)
LEVEL_DBTYPE    System.Int32     3
LEVEL_MASTER_UNIQUE_NAME    System.String     
LEVEL_NAME_SQL_COLUMN_NAME    System.String     
LEVEL_KEY_SQL_COLUMN_NAME    System.String     
LEVEL_UNIQUE_NAME_SQL_COLUMN_NAME    System.String     
LEVEL_ATTRIBUTE_HIERARCHY_NAME    System.String     
LEVEL_KEY_CARDINALITY    System.UInt16     1
LEVEL_ORIGIN    System.UInt16     2



Member

UniqueName
ChildCount
LevelDepth
LevelName



 