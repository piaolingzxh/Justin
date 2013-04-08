using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin.BI.DBLibrary.TestDataGenerate
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class FileInfoAttribute : Attribute
    {
        public string DefaultFileExtension { get; private set; }
        public string DefaultDisplayName { get; private set; }
        private List<string> AllowFileExtension { get; set; }


        public FileInfoAttribute(string defaultFileExtension, string displayName)
        {
            this.DefaultFileExtension = defaultFileExtension;
            this.DefaultDisplayName = displayName;

            this.AllowFileExtension = new List<string>();
            this.AllowFileExtension.Add(defaultFileExtension);
        }
        public FileInfoAttribute(string defaultFileExtension, string displayName, string[] allowFileExtension)
            : this(defaultFileExtension, displayName)
        {
            if (allowFileExtension != null)
            {
                foreach (var item in allowFileExtension)
                {
                    if (!this.AllowFileExtension.Contains(item))
                    {
                        this.AllowFileExtension.Add(item);
                    }
                }
            }
        }

        public string[] GetAllowFileExtensions(bool ignoreDefault = false)
        {
            return ignoreDefault ? this.AllowFileExtension.Where(row => row != this.DefaultFileExtension).ToArray() : this.AllowFileExtension.ToArray();
        
        }
    }
}
