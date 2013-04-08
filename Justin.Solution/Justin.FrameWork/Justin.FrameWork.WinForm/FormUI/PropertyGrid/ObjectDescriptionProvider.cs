using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.WinForm.FormUI.PropertyGrid
{
    #region PropertyGrid

    public class ObjectDescriptionProvider : TypeDescriptionProvider
    {
        private static TypeDescriptionProvider defaultTypeProvider =
                       TypeDescriptor.GetProvider(typeof(Object));

        public ObjectDescriptionProvider() : base(defaultTypeProvider) { }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            ICustomTypeDescriptor defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            return (objectType.IsPrimitive || objectType == typeof(string)) ? defaultDescriptor : new ObjectCustomTypeDescriptor(defaultDescriptor);
        }
    }
    public class ObjectCustomTypeDescriptor : CustomTypeDescriptor
    {
        public ObjectCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent) { }

        public override AttributeCollection GetAttributes()
        {
            var attributes = base.GetAttributes().Cast<Attribute>().ToList();
            if (!attributes.Exists(t => t is TypeConverterAttribute))
            {
                attributes.Add(new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
            }
            return new AttributeCollection(attributes.ToArray());
        }
    }
    #endregion
}
