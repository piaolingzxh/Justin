using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Justin.FrameWork.Utility;

namespace Justin.Controls.Mondrian
{
    public class MondrianCompiler
    {

        public static string format = @"
using System.Collections.Generic;
using System.Linq;
using Justin.Controls.Mondrian;
namespace Justin.BI.DBLibrary.Compiler
{{
    public class MondrianCompiler
    {{
        public static void InitChildren(object obj)
        {{
            {0} instance = ({0})obj;
            List<Element> elements = new List<Element>();
            {1}
            instance.Elements = elements.ToArray();
            instance.ElementTypes = elements.Select(row => row.ElementType).ToArray();
        }}
    }}
}}
";

        public static string itemFormat = @"
            if(instance.{0}!=null &&instance.{0}.Count!=0)
            {{
                elements.AddRange(instance.{0});
                foreach (var item in instance.{0})
                {{
                    item.PrepareChildrenElements();
                }}
            }}";
        public static void InitElementChildren(Type elementType, object element, List<Tuple<ChildElementAttribute, PropertyInfo>> childElementProperties)
        {

            StringBuilder sb = new StringBuilder();
            foreach (var item in childElementProperties.Where(row => row.Item1.ChildCategory == ChildCategory.ChildrenColection).OrderBy(row => row.Item1.Order))
            {
                sb.AppendFormat(itemFormat, item.Item2.Name).AppendLine();
            }
            Element elementInstance = element as Element;
            elementInstance.Items = null;
            elementInstance.ItemTypes = null;

            List<Element> elementList = new List<Element>();
            foreach (var item in childElementProperties.Where(row => row.Item1.ChildCategory == ChildCategory.ChildrenList || row.Item1.ChildCategory == ChildCategory.ChildrenColection))
            {

                object o = item.Item2.GetValue(element, null);
                if (o != null)
                {
                    IEnumerable<Element> elements = o as IEnumerable<Element>;

                    if (elements != null && elements.Count() > 0)
                    {
                        foreach (Element tempelement in elements)
                        {
                            //++???????????
                            // tempelement.PrepareChildrenElements();
                        }
                        if (item.Item1.ChildCategory == ChildCategory.ChildrenColection)
                        {
                            elementList.AddRange(elements);
                        }
                    }
                }
            }
            if (elementList.Count > 0)
            {
                elementInstance.Items = elementList.ToArray();
                elementInstance.ItemTypes = elementList.Select(row => row.ElementType).ToArray();
            }

            string code = string.Format(format, elementType.Name, sb.ToString());

            CSharpCompilerWraper codeWrapper = new CSharpCompilerWraper();
            codeWrapper.FrameworkVersion = FrameworkVersion.Version20;
            codeWrapper.CustomeAssemblies = null;

            ScriptCode scriptCode = new ScriptCode();
            scriptCode.SourceCode = code;
            scriptCode.StartUpList.Add(new StartUpInfo() { ClassName = "Justin.BI.DBLibrary.Compiler.MondrianCompiler", Instance = element, MethordName = "InitChildren", order = 0, MethordParameters = new object[] { element } });
            // codeWrapper.Run(scriptCode, null);

        }

    }
}
