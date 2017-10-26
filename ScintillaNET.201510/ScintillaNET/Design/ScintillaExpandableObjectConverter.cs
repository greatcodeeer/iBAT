#region Using Directives

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;

#endregion Using Directives


namespace ScintillaNET.Design
{
    // A custom ExpandableObjectConverter that provides a nicer string representation
    internal class ScintillaExpandableObjectConverter : ExpandableObjectConverter
    {
        #region Methods

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // We only care about string conversions
            if (destinationType != typeof(String))
                return base.ConvertTo(context, culture, value, destinationType);

            try
            {
                StringBuilder sb = new StringBuilder();

                // Enumerate all the public properties and build a string representation
                foreach (PropertyInfo pi in context.PropertyDescriptor.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    // Skip null values
                    if (pi.GetValue(value, null) == null)
                        continue;

                    object[] attributes;

                    // Look for a Browsable(false) attribute)
                    attributes = pi.GetCustomAttributes(typeof(BrowsableAttribute), true);
                    if (attributes != null && attributes.Length > 0 && ((BrowsableAttribute)attributes[0]).Browsable == false)
                        continue;

                    // Look for a DefaultValue(?) attribute
                    attributes = pi.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                    if (attributes != null && attributes.Length > 0)
                    {
                        object v1 = pi.GetValue(value, null);
                        object v2 = ((DefaultValueAttribute)attributes[0]).Value;
                        if (v1 == v2 || v1.Equals(v2))
                            continue;
                    }

                    // Look for a ShouldSerialize* method
                    MethodInfo mi = value.GetType().GetMethod("ShouldSerialize" + pi.Name, new Type[0]);
                    if (mi != null && (bool)mi.Invoke(value, null) == false)
                        continue;

                    // Print separator
                    if (sb.Length > 0)
                        sb.Append("; ");

                    // Print name
                    sb.Append(pi.Name);
                    sb.Append("=");

                    // Convert to string
                    string str = null;
                    TypeConverter tc = TypeDescriptor.GetConverter(pi.PropertyType);
                    if (tc != null)
                        str = tc.ConvertToString(pi.GetValue(value, null));
                    else
                        str = pi.GetValue(value, null).ToString();

                    // Print value
                    sb.Append("\"");
                    if (str != null)
                    {
                        // Escape some non-printable chars
                        str = str.Replace("\r", "\\r");
                        str = str.Replace("\n", "\\n");
                        str = str.Replace("\t", "\\t");
                        str = str.Replace("\"", "\\\"");
                        sb.Append(str);
                    }
                    sb.Append("\"");
                }

                return context.PropertyDescriptor.PropertyType.Name + " { " + sb + " } ";
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex);
                throw;
            }
        }

        #endregion Methods
    }
}
