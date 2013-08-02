using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Justin.BI.OLAP.Entity.Mondrian;

namespace Justin.BI.OLAP
{
    public class MondrianFactory : IOLAPFactory
    {
        private string SchemaPath { get; set; }
        public MondrianFactory(string schemaPath)
        {
            this.SchemaPath = schemaPath;
        }

        public void CreateSolution(Entity.Solution solution)
        {
            this.BackUpSchemaFile();

            Schema schema = new Schema(solution.ID) { Description = solution.Name };


            foreach (var outerCube in solution.Cubes)
            {
                var mCube = new Cube(outerCube.ID);
                mCube.Caption = mCube.Description = outerCube.Name;
                mCube.Table = new Table(outerCube.TableName);
                //维度
                foreach (var outerDimension in outerCube.Dimensions)
                {
                    var mDimension = new Dimension(outerDimension.ID);
                    mDimension.Caption = mDimension.Description = outerDimension.Name;
                    mDimension.ForeignKey = outerDimension.FKColumn;

                    if (outerDimension.Hierarchies != null && outerDimension.Hierarchies.Count > 1)
                    {
                        //层次
                        foreach (var outerHierarchie in outerDimension.Hierarchies)
                        {
                            var mHierarchie = new Hierarchy(outerHierarchie.ID);
                            mHierarchie.Caption = mHierarchie.Description = outerHierarchie.Name;
                            mHierarchie.allLevelName = mHierarchie.allMemberCaption = mHierarchie.AllMemberName = "all";
                            mHierarchie.Table = new Table(outerHierarchie.Levels[0].SourceTable);
                            //粒度
                            foreach (var outerLevel in outerHierarchie.Levels)
                            {
                                var mLevel = new Level(outerLevel.ID);
                                mLevel.Caption = mLevel.Description = outerLevel.Name;
                                mLevel.LevelType = LevelType.Regular;
                                mLevel.NameColumn = outerLevel.KeyColumn;
                                mLevel.Column = outerLevel.KeyColumn;
                                mLevel.CaptionColumn = outerLevel.NameColumn;
                                //TODO:Mondrian父子维处理

                                if (!string.IsNullOrEmpty(outerLevel.ParentColumn))
                                {
                                    #region Closure
                                                                           
                                    mLevel.Closure = new Closure(outerLevel.SourceTable + "_C");
                                    mLevel.Closure.ParentColumn = outerLevel.ParentColumn;
                                    mLevel.Closure.ChildColumn = outerLevel.KeyColumn;

                                    mLevel.Closure.Table = new Table(outerLevel.SourceTable + "_C");

                                    #endregion



                                    mLevel.Table = outerLevel.SourceTable;
                                }
                                mHierarchie.Levels.Add(mLevel);
                            }
                            mDimension.Hierarchies.Add(mHierarchie);
                        }
                    }
                    else
                    {
                        //默认层次
                        var mDefaultHierarchie = new Hierarchy("Default");
                        mDefaultHierarchie.Caption = mDefaultHierarchie.Description = "Default";
                        mDefaultHierarchie.allLevelName = mDefaultHierarchie.allMemberCaption = mDefaultHierarchie.AllMemberName = "all";
                        mDefaultHierarchie.Table = new Table(outerDimension.Levels[0].SourceTable);
                        //默认粒度
                        foreach (var outerLevel in outerDimension.Levels)
                        {
                            var mLevel = new Level(outerLevel.ID);
                            mLevel.Caption = mLevel.Description = outerLevel.Name;
                            mLevel.LevelType = LevelType.Regular;
                            mLevel.NameColumn = outerLevel.KeyColumn;
                            mLevel.Column = outerLevel.KeyColumn;
                            mLevel.CaptionColumn = outerLevel.NameColumn;
                            mLevel.Table = outerLevel.SourceTable;

                            //mLevel.Closure = new Closure("");
                            //mLevel.Closure.
                            mDefaultHierarchie.Levels.Add(mLevel);
                        }
                        mDimension.Hierarchies.Add(mDefaultHierarchie);
                    }
                    mCube.Dimensions.Add(mDimension);
                }

                foreach (var outerMeasure in outerCube.Measures)
                {
                    var mMeasure = new Measure(outerMeasure.ID);
                    mMeasure.Caption = mMeasure.Description = outerMeasure.Name;
                    mMeasure.Aggregator = this.GetAggregator(outerMeasure.Aggregator);
                    mMeasure.Column = outerMeasure.ColumnName;
                    //mMeasure.DataType = ColumnType.Numeric;
                    mMeasure.FormatString = FormatString.Standard;
                    mMeasure.Formatter = "";
                    mMeasure.Visible = outerMeasure.Visable;

                    mCube.Measures.Add(mMeasure);
                }

                schema.Cubes.Add(mCube);
            }


            schema.Serializer(this.SchemaPath);
        }

        public void DeleteSolution(Entity.Solution solution)
        {
            if (File.Exists(SchemaPath))
            {
                File.Delete(SchemaPath);
            }
        }

        private void BackUpSchemaFile()
        {
            if (File.Exists(SchemaPath))
            {
                string bakFileName = SchemaPath + "." + DateTime.Now.ToString("yyyymmdd_hhMMss") + ".bak";
                File.Copy(SchemaPath, bakFileName, true);
                File.Delete(SchemaPath);
            }
        }

        public Justin.BI.OLAP.Entity.Mondrian.Aggregator GetAggregator(Justin.BI.OLAP.Entity.Aggregator aggregator)
        {
            switch (aggregator)
            {
                case Justin.BI.OLAP.Entity.Aggregator.Sum: return Justin.BI.OLAP.Entity.Mondrian.Aggregator.Sum;
                case Justin.BI.OLAP.Entity.Aggregator.Count: return Justin.BI.OLAP.Entity.Mondrian.Aggregator.Count;
                case Justin.BI.OLAP.Entity.Aggregator.Min: return Justin.BI.OLAP.Entity.Mondrian.Aggregator.Min;
                case Justin.BI.OLAP.Entity.Aggregator.Max: return Justin.BI.OLAP.Entity.Mondrian.Aggregator.Max;
                case Justin.BI.OLAP.Entity.Aggregator.DistinctCount: return Justin.BI.OLAP.Entity.Mondrian.Aggregator.DistinctCount;
                case Justin.BI.OLAP.Entity.Aggregator.None: throw new NotSupportedException(string.Format("不支持此枚举类型{0}", aggregator));
                case Justin.BI.OLAP.Entity.Aggregator.ByAccount: throw new NotSupportedException(string.Format("不支持此枚举类型{0}", aggregator));
                case Justin.BI.OLAP.Entity.Aggregator.AverageOfChildren: return Justin.BI.OLAP.Entity.Mondrian.Aggregator.AVG;
                case Justin.BI.OLAP.Entity.Aggregator.FirstChild: throw new NotSupportedException(string.Format("不支持此枚举类型{0}", aggregator));
                case Justin.BI.OLAP.Entity.Aggregator.LastChild: throw new NotSupportedException(string.Format("不支持此枚举类型{0}", aggregator));
                case Justin.BI.OLAP.Entity.Aggregator.FirstNonEmpty: throw new NotSupportedException(string.Format("不支持此枚举类型{0}", aggregator));
                case Justin.BI.OLAP.Entity.Aggregator.LastNonEmpty: throw new NotSupportedException(string.Format("不支持此枚举类型{0}", aggregator));
            }
            throw new NotSupportedException(string.Format("不支持此枚举类型{0}", aggregator));
        }
    }
}
