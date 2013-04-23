using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justin.FrameWork.Settings;
using Justin.FrameWork.Extensions;
using Justin.Controls.TestDataGenerator.DAL;
using Justin.Controls.TestDataGenerator.Utility;
using System.IO;
using Justin.FrameWork.Helper;
namespace Justin.Controls.TestDataGenerator.Entities
{
    public delegate void SQLProcess(StringBuilder sqlBuilder, JTable table);


    public class JTable
    {
        #region JTable

        public JTable(DBTable dbTable, string connStr)
            : this()
        {
            this.TableName = dbTable.TableName;
            this.Fields = new List<JField>();
            foreach (var item in dbTable.Columns)
            {
                JField field = new JField(item);
                this.Fields.Add(field);
            }
            if (dbTable.PrimaryKey != null)
                this.Fields.Add(new JField(dbTable.PrimaryKey));

            foreach (var fk in dbTable.ForeignKeys)
            {
                this.Fields.Add(new JField(fk));
            }
            this.ConnStr = connStr;
        }
        public string TableName { get; set; }
        public int DataCount { get; set; }
        public string BeforeSQL { get; set; }
        public string AfterSQL { get; set; }
        public List<JField> Fields { get; set; }
        public int Order = 1000;
        public string ConnStr { get; set; }
        public JTable()
        {
        }
        public static string NumericFieldValueFormat = "{0}";
        public static string StringFieldValueFormat = "'{0}'";
        public static string DateTimeFieldValueFormat = "{{ts'{0}'}}";
        private string GetFileValueFormat(JValueType valueType)
        {
            switch (valueType)
            {
                case JValueType.Numeric: return NumericFieldValueFormat + ",";
                case JValueType.String: return StringFieldValueFormat + ",";
                case JValueType.DateTime: return DateTimeFieldValueFormat + ",";
                default: return StringFieldValueFormat;
            }
        }


        public static SQLProcess SqlProcess { get; set; }

        public void Process(string connStr = "")
        {
            if (string.IsNullOrEmpty(connStr))
            {
                connStr = this.ConnStr;
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {

                JTable table = this;
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(table.BeforeSQL))
                {
                    builder.Append(Constants.SQLParagraphStartFlag).AppendLine();
                    builder.Append(table.BeforeSQL).AppendLine();
                    builder.Append(Constants.SQLParagraphEndFlag).AppendLine();
                }
                for (int i = 0; i < table.DataCount; i++)
                {
                    #region 生成每一条数据
                    string format = "insert into {0}({1}) values({2});";
                    StringBuilder fieldNameBuilder = new StringBuilder();
                    StringBuilder fieldValueBuilder = new StringBuilder();
                    Dictionary<string, object> fieldValuesOfCurrentRow = new Dictionary<string, object>();

                    JField[] fields = table.Fields.Where(row => row.Visible == true).OrderBy(row => row.Order).ToArray();
                    for (int f = 0; f < fields.Count(); f++)
                    {
                        JField field = fields[f];
                        try
                        {

                            fieldNameBuilder.AppendFormat("{0},", field.FieldName);

                            object value1 = field.FirstOperand.GetValue(conn, fieldValuesOfCurrentRow);
                            object value2 = field.SecondOperand == null ? null : field.SecondOperand.GetValue(conn, fieldValuesOfCurrentRow);
                            object value = value1;

                            if (field.Operator != null && field.SecondOperand != null && field.SecondOperand.ValueType == JValueType.Numeric)
                            {
                                switch (field.FirstOperand.ValueType)
                                {
                                    case JValueType.DateTime:
                                        DateTime dtValue1 = DateTime.Parse(value1.ToJString(DateTime.Now.ToString()));
                                        double dtParameter = double.Parse(value2.ToJString("0"));

                                        switch (field.Operator)
                                        {
                                            case "+": value = dtValue1.AddDays(dtParameter); break;
                                            case "-": value = dtValue1.AddDays(-dtParameter); break;
                                            case "*": throw new FieldValueTypeNotSupportOperatorException(field.FieldName, field.ValueType, "*");
                                            case "/": throw new FieldValueTypeNotSupportOperatorException(field.FieldName, field.ValueType, "/");
                                            case "%": throw new FieldValueTypeNotSupportOperatorException(field.FieldName, field.ValueType, "%");
                                        }
                                        break;
                                    case JValueType.Numeric:
                                        double numericParameter1 = double.Parse(value1.ToJString("0"));
                                        double numericParameter2 = double.Parse(value2.ToJString("0"));
                                        switch (field.Operator)
                                        {
                                            case "+": value = numericParameter1 + numericParameter2; break;
                                            case "-": value = numericParameter1 - numericParameter2; break;
                                            case "*": value = numericParameter1 * numericParameter2; break;
                                            case "/": value = numericParameter1 / numericParameter2; break;
                                            case "%": value = (int)numericParameter1 % (int)numericParameter2; break;
                                        }
                                        break;
                                    case JValueType.String:
                                        string strValue1 = value1.ToJString("");
                                        string strValue2 = value2.ToJString("");

                                        switch (field.Operator)
                                        {
                                            case "+": value = strValue1 + strValue2; break;
                                            case "-": throw new FieldValueTypeNotSupportOperatorException(field.FieldName, field.ValueType, "-");
                                            case "*": throw new FieldValueTypeNotSupportOperatorException(field.FieldName, field.ValueType, "*");
                                            case "/": throw new FieldValueTypeNotSupportOperatorException(field.FieldName, field.ValueType, "/");
                                            case "%": throw new FieldValueTypeNotSupportOperatorException(field.FieldName, field.ValueType, "%");
                                        }
                                        break;
                                }
                            }

                            fieldValueBuilder.AppendFormat(
                             GetFileValueFormat(field.ValueType)
                             , field.ValueType == JValueType.DateTime ? DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") : value
                                );
                            fieldValuesOfCurrentRow.Add(field.FieldName, value);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Data：{0}-{1},Filed:{2} ErrorMessage:{3} ", field.ValueType.ToString(), field.FirstOperand.ValueCategroy.ToString(), field.FieldName, ex.ToString()));
                        }
                    }
                    #endregion

                    builder.AppendFormat(format,
                        table.TableName,
                        fieldNameBuilder.ToString(0, fieldNameBuilder.Length - 1),
                        fieldValueBuilder.ToString(0, fieldValueBuilder.Length - 1)).AppendLine();

                    if (builder.Length > Constants.SqlBufferSize && SqlProcess != null)
                    {
                        SqlProcess(builder, this);
                        builder.Clear();
                    }

                }
                if (!string.IsNullOrEmpty(table.AfterSQL))
                {
                    builder.Append(Constants.SQLParagraphStartFlag).AppendLine();
                    builder.Append(table.AfterSQL).AppendLine();
                    builder.Append(Constants.SQLParagraphEndFlag).AppendLine();
                }
                if (builder.Length > 0 && SqlProcess != null)
                {
                    SqlProcess(builder, this);
                    builder.Clear();
                }
            }
        }
        private NotSupportedException GetException(string filedName, JValueType valueType, JValueCategroy sourceValueCategroy)
        {
            throw new NotSupportedException(string.Format("{0}不支持{1},Filed:{2}", valueType.ToString(), sourceValueCategroy.ToString(), filedName));
        }

        public void SaveSettings(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            SerializeHelper.XmlSerializeToFile<JTable>(this, fileName);
        }
        public bool Modified { get; set; }

        #endregion
    }

    public class JField
    {
        #region JField

        public JField()
        {
            this.Visible = false;
            this.Order = 0;
        }

        public JField(DBColumn column)
        {
            this.FieldName = column.ColumnName;
            this.ValueType = JField.GetJValueType(column.DbType);
            this.FirstOperand = new JOperateNum(this.FieldName, this.ValueType);
            this.AllowNull = column.AllowNull;
            if (column is DBPrimaryKey)
            {
                DBPrimaryKey pk = column as DBPrimaryKey;
                this.FirstOperand.ValueCategroy = JValueCategroy.Sequence;
                this.FirstOperand.MinValue = pk.CurrentValue + pk.Step;
                this.FirstOperand.Step = pk.Step;
            }
            if (column is DBForeignKey)
            {
                DBForeignKey fk = column as DBForeignKey;
                this.FirstOperand.ValueCategroy = JValueCategroy.FromTable;
                this.FirstOperand.ReferenceTableName = fk.ReferenceTableName;
                this.FirstOperand.ReferenceColumnName = fk.ReferenceColumnName;
            }
            this.Visible = false;
            this.Order = 0;

        }
        public string FieldName { get; set; }

        public bool Visible { get; set; }
        public bool AllowNull { get; set; }
        public int Order { get; set; }

        public JValueType ValueType { get; set; }
        public JOperateNum FirstOperand { get; set; }
        public JOperateNum SecondOperand { get; set; }
        public string Operator { get; set; }

        public void SetVisible(bool value)
        {
            if (AllowNull)
            {
                Visible = value;
            }
            else if (!AllowNull && !value)
            {
                throw new Exception(string.Format("字段{0}必须生成数据", FieldName));
            }
            else
            {
                Visible = true;
            }
        }
        private static JValueType GetJValueType(SqlDbType dbType)
        {

            switch (dbType)
            {
                case SqlDbType.BigInt: return JValueType.Numeric;
                case SqlDbType.Binary: return JValueType.String;
                case SqlDbType.Bit: return JValueType.String;
                case SqlDbType.Char: return JValueType.String;
                case SqlDbType.DateTime: return JValueType.DateTime;
                case SqlDbType.Decimal: return JValueType.Numeric;
                case SqlDbType.Float: return JValueType.Numeric;
                case SqlDbType.Image: return JValueType.String;
                case SqlDbType.Int: return JValueType.Numeric;
                case SqlDbType.Money: return JValueType.Numeric;
                case SqlDbType.NChar: return JValueType.String;
                case SqlDbType.NText: return JValueType.String;
                case SqlDbType.NVarChar: return JValueType.String;
                case SqlDbType.Real: return JValueType.String;
                case SqlDbType.UniqueIdentifier: return JValueType.String;
                case SqlDbType.SmallDateTime: return JValueType.DateTime;
                case SqlDbType.SmallInt: return JValueType.Numeric;
                case SqlDbType.SmallMoney: return JValueType.Numeric;
                case SqlDbType.Text: return JValueType.String;
                case SqlDbType.Timestamp: return JValueType.DateTime;
                case SqlDbType.TinyInt: return JValueType.Numeric;
                case SqlDbType.VarBinary: return JValueType.String;
                case SqlDbType.VarChar: return JValueType.String;
                case SqlDbType.Variant: return JValueType.String;
                case SqlDbType.Xml: return JValueType.String;
                case SqlDbType.Udt: return JValueType.String;
                case SqlDbType.Structured: return JValueType.String;
                case SqlDbType.Date: return JValueType.DateTime;
                case SqlDbType.Time: return JValueType.DateTime;
                case SqlDbType.DateTime2: return JValueType.DateTime;
                case SqlDbType.DateTimeOffset: return JValueType.DateTime;
            }
            throw new NotSupportedException();
        }

        #endregion
    }

    public class SourceFieldData
    {
        public string TableName { get; set; }
        public string fieldName { get; set; }
        public object[] Values { get; set; }
    }
    public class JOperateNum
    {
        #region JOperateNum

        public JOperateNum()
        {
            this.Values = new List<object>();
            this.ValueType = JValueType.Numeric;
        }
        public JOperateNum(string fieldName, JValueType valueType)
            : this()
        {
            this.FieldName = fieldName;
            this.ValueType = valueType;
            this.Values = new List<object>();
        }
        private string FieldName { get; set; }
        public JValueType ValueType { get; set; }
        public JValueCategroy ValueCategroy { get; set; }

        //Range或Sequence
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public object Step { get; set; }
        public string Format { get; set; }

        //列表
        public List<object> Values { get; set; }

        //引用其他表字段
        public string ReferenceTableName { get; set; }
        public string ReferenceColumnName { get; set; }
        public string RefFilter { get; set; }

        //引用本表其他字段
        public string OtherFiledName { get; set; }


        public object GetValue(SqlConnection conn, Dictionary<string, object> fieldValuesOfCurrentRow)
        {
            if (this == null)
            {
                return null;
            }
            Random rd = new Random();
            object value = "";
            switch (this.ValueCategroy)
            {
                case JValueCategroy.List:

                    #region List不管是DateTime、Numeric、String处理方式都一样

                    object[] values = this.Values.ToArray();
                    int listCount = this.Values.Count();
                    int randNum = rd.Next(0, listCount);
                    value = values[randNum];
                    break;

                    #endregion

                case JValueCategroy.Range:

                    #region Range

                    switch (this.ValueType)
                    {
                        case JValueType.DateTime:

                            #region Range

                            DateTime minDate = DateTime.Parse(this.MinValue.ToJString());
                            DateTime maxDate = DateTime.Parse(this.MaxValue.ToJString());
                            int minutesDiff = (int)(maxDate - minDate).TotalMinutes;
                            int randMinutes = rd.Next(0, minutesDiff);
                            value = minDate.AddMinutes(randMinutes);

                            break;

                            #endregion

                        case JValueType.Numeric:

                            #region Range

                            decimal maxSeed = decimal.Parse(this.MaxValue.ToJString());
                            decimal minSeed = decimal.Parse(this.MinValue.ToJString());
                            if (maxSeed <= 1)
                            {
                                maxSeed = maxSeed * 10000;
                                minSeed = minSeed * 10000;
                                value = (decimal)rd.Next((int)minSeed, (int)maxSeed) / 10000;
                            }
                            else
                            {
                                value = rd.Next((int)minSeed, (int)maxSeed);
                            }

                            break;

                            #endregion

                        case JValueType.String:

                            value = String.Format(this.Format, rd.Next((Int32)this.MinValue, (Int32)this.MaxValue));

                            break;

                    }
                    break;

                    #endregion

                case JValueCategroy.Sequence:

                    #region Sequence

                    switch (this.ValueType)
                    {
                        case JValueType.DateTime:
                            #region Sequence

                            throw new FieldValueTypeNotSupportValueCategroyException(this.FieldName, this.ValueType, this.ValueCategroy);

                            #endregion

                        case JValueType.Numeric:
                            #region Sequence

                            value = this.MinValue;
                            this.MinValue = int.Parse(this.MinValue.ToJString("0")) + int.Parse(this.Step.ToJString("0"));
                            break;

                            #endregion

                        case JValueType.String:

                            value = String.Format(this.Format, this.MinValue);
                            this.MinValue = int.Parse(this.MinValue.ToJString("0")) + int.Parse(this.Step.ToJString("0"));
                            break;

                    }
                    break;

                    #endregion

                case JValueCategroy.FromTable:

                    #region FromTable

                    string refTableName = this.ReferenceTableName;
                    string refColumnName = this.ReferenceColumnName;
                    List<string> parameters = new List<string>();
                    string filterFormat = this.RefFilter.ToParameters(parameters);
                    List<object> parameterValues = new List<object>();
                    foreach (var item in parameters)
                    {
                        parameterValues.Add(fieldValuesOfCurrentRow[item]);
                    }
                    string refFilter = string.Format(filterFormat, parameterValues.ToArray());

                    SourceFieldData tempData = new SourceFieldData()
                        {
                            fieldName = refColumnName,
                            TableName = refTableName,
                            Values = CommonDAL.GetValues(conn, refTableName, refColumnName, refFilter).ToArray()
                        };

                    value = tempData.Values[rd.Next(0, tempData.Values.Count())];
                    break;

                    #endregion

                case JValueCategroy.OtherField:

                    #region OtherField 数据准备

                    if (!fieldValuesOfCurrentRow.ContainsKey(this.OtherFiledName))
                    {
                        throw new FieldValueTypeNotSupportValueCategroyException(this.FieldName, this.ValueType, this.ValueCategroy);
                    }
                    value = fieldValuesOfCurrentRow[this.OtherFiledName];

                    #endregion

                    break;
            }

            return value;
        }

        #endregion
    }


    #region Enum

    public enum JValueCategroy
    {
        List,
        Range,
        Sequence,
        FromTable,
        OtherField
    }

    public enum JValueType
    {
        Numeric,
        String,
        DateTime,
    }

    #endregion

    #region Exception

    public class FieldValueTypeNotSupportValueCategroyException : NotSupportedException
    {
        public FieldValueTypeNotSupportValueCategroyException(string filedName, JValueType valueType, JValueCategroy sourceValueCategroy)
            : base(string.Format("{0}不支持{1},Filed:{2}", valueType.ToString(), sourceValueCategroy.ToString(), filedName))
        {
        }

    }
    public class FieldValueTypeNotSupportOperatorException : NotSupportedException
    {
        public FieldValueTypeNotSupportOperatorException(string filedName, JValueType valueType, string operate)
            : base(string.Format("{0}不支持{1},Filed:{2}", valueType.ToString(), operate, filedName))
        {
        }

    }

    #endregion



}
