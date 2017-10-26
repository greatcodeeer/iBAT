#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms.Design;

#endregion Using Directives


namespace ScintillaNET.Design
{
    // Provides additional design-time support for the Scintilla control
    internal sealed class ScintillaDesigner : ControlDesigner
    {
        #region Fields

        private Scintilla _scintilla;

        #endregion Fields


        #region Methods

        private Attribute[] GetCustomAttributes(string propertyName)
        {
            List<Attribute> attrs = new List<Attribute>();
            foreach (Attribute a in _scintilla.GetType().GetProperty(propertyName).GetCustomAttributes(false))
                attrs.Add(a);

            return attrs.ToArray();
        }


        public override void Initialize(IComponent component)
        {
            _scintilla = (Scintilla)component;

            base.Initialize(component);
        }


        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            // By default the VS control designer sets the Text property to the control
            // name instead of the default value... which is what we'll do here.
            _scintilla.Text = null;
        }


        protected override void PostFilterProperties(IDictionary properties)
        {
            // To support the Reset option in the property grid a property must
            // have a setter (amongst other things). Most of ours do not because
            // the public API does not require it. To offer a Reset option we
            // instead tell the designer to use the following properties in THIS
            // class as if they were on the control class, thereby allowing us
            // to provide a (hidden) setter and get Reset functionality.

            // All your property are belong to us ;)

            properties["Scrolling"] = TypeDescriptor.CreateProperty(
                GetType(),
                "Scrolling",
                typeof(Scrolling),
                GetCustomAttributes("Scrolling"));

            base.PostFilterProperties(properties);
        }


        private static void Reset(object value)
        {
            // Reset all the design-time object properties

            if (value == null)
                return;

            foreach (PropertyInfo pi in value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                object[] attributes;

                // Look for a Browsable(false) attribute)
                attributes = pi.GetCustomAttributes(typeof(BrowsableAttribute), true);
                if (attributes != null && attributes.Length > 0 && ((BrowsableAttribute)attributes[0]).Browsable == false)
                    continue;

                // Look for a DefaultValue(?) attribute
                attributes = pi.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    pi.SetValue(value, ((DefaultValueAttribute)attributes[0]).Value, null);
                    continue;
                }

                // Look for a Reset* method
                MethodInfo mi = value.GetType().GetMethod("Reset" + pi.Name, new Type[0]);
                if (mi != null)
                    mi.Invoke(value, null);
            }
        }


        private void ResetScrolling()
        {
            Reset(Scrolling);
        }


        private static bool ShouldSerialize(object value)
        {
            // Determine if the object should be design-time serialized

            if (value == null)
                return false;

            foreach (PropertyInfo pi in value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                object[] attributes;

                // Look for a Browsable(false) attribute
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

                return true;
            }

            return false;
        }


        private bool ShouldSerializeScrolling()
        {
            return ShouldSerialize(Scrolling);
        }

        #endregion Methods


        #region Properties

        public Scrolling Scrolling
        {
            get
            {
                return _scintilla.Scrolling;
            }
            set
            {
            }
        }

        #endregion Properties
    }
}
