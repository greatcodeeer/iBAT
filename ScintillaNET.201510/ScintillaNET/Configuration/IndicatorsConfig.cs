#region Using Directives

using System;
using System.Drawing;
using System.Collections.ObjectModel;

#endregion Using Directives


namespace ScintillaNET.Configuration
{
    public class IndicatorConfig
    {
        #region Fields

        private byte? _alpha;
        private Color _color;
        private bool? _inherit;
        private IndicatorDrawMode? _drawMode;
        private int _index;
        private byte? _outlineAlpha;
        private IndicatorStyle? _style;

        #endregion Fields


        #region Properties

        public byte? Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }


        public bool? Inherit
        {
            get
            {
                return _inherit;
            }
            set
            {
                _inherit = value;
            }
        }


        public IndicatorDrawMode? DrawMode
        {
            get
            {
                return _drawMode;
            }
            set
            {
                _drawMode = value;
            }
        }


        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }


        public byte? OutlineAlpha
        {
            get
            {
                return _outlineAlpha;
            }
            set
            {
                _outlineAlpha = value;
            }
        }


        public IndicatorStyle? Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
            }
        }

        #endregion Properties
    }
}
