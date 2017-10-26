// TODO Document that draw mode requires the "two phase drawing" (TBD) property to be set.

#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using ScintillaNET.Internal;
using ScintillaNET.Properties;

#endregion Using Directives


namespace ScintillaNET
{
    /// <summary>
    ///     Defines the appearance of an indicator in a <see cref="Scintilla" /> control.
    /// </summary>
    public class Indicator
    {
        #region Fields

        private readonly string _colorBagKey;

        private Scintilla _scintilla;
        private int _index;

        #endregion Fields


        #region Methods

        /// <summary>
        ///     Overridden. See <see cref="Object.Equals" />.
        /// </summary>
        public override bool Equals(object obj)
        {
            Indicator i = obj as Indicator;
            if ((object)i != null)
                return Equals(i);

            return false;
        }


        /// <summary>
        ///     Determines whether the specified indicator is equal to the current indicator.
        /// </summary>
        /// <param name="i">The indicator to compare with the current indicator.</param>
        /// <returns>true if the specified indicator is equal to the current indicator; otherwise, false.</returns>
        public bool Equals(Indicator i)
        {
            if ((object)i != null)
            {
                return _scintilla.Equals(_scintilla)
                    && _index.Equals(i._index);
            }

            return false;
        }


        private Color GetDefaultColor()
        {
            if (_index == 0)
                return Color.FromArgb(0, 127, 0);
            else if (_index == 1)
                return Color.FromArgb(0, 0, 255);
            else if (_index == 2)
                return Color.FromArgb(255, 0, 0);
            else
                return Color.FromArgb(0, 0, 0);
        }


        private IndicatorStyle GetDefaultStyle()
        {
            if (_index == 0)
                return IndicatorStyle.Squiggle;
            else if (_index == 1)
                return IndicatorStyle.TT;
            else
                return IndicatorStyle.Plain;
        }


        /// <summary>
        ///     Overridden. See <see cref="Object.GetHashCode" />.
        /// </summary>
        public override int GetHashCode()
        {
            return _scintilla.GetHashCode() ^ _index;
        }


        /// <summary>
        ///     Resets all indicator display properties to their respective defaults.
        /// </summary>
        public void Reset()
        {
            Alpha = 30;
            Color = GetDefaultColor();
            DrawMode = IndicatorDrawMode.Overlay;
            OutlineAlpha = 50;
            Style = GetDefaultStyle();
        }


        public Range Search()
        {
            return Search(_scintilla.GetRange());
        }


        public Range Search(Range searchRange)
        {
            int foundStart = _scintilla.NativeInterface.IndicatorEnd(_index, searchRange.Start);
            int foundEnd = _scintilla.NativeInterface.IndicatorEnd(_index, foundStart);
            if (foundStart < 0 || foundStart > searchRange.End || foundStart == foundEnd)
                return null;


            return new Range(foundStart, foundEnd, _scintilla);
        }


        public Range Search(Range searchRange, Range startingAfterRange)
        {
            int start = startingAfterRange.End;
            if (start > _scintilla.NativeInterface.GetTextLength())
                return null;

            int foundStart = _scintilla.NativeInterface.IndicatorEnd(_index, start);
            int foundEnd = _scintilla.NativeInterface.IndicatorEnd(_index, foundStart);
            if (foundStart < 0 || foundStart > searchRange.End || foundStart == foundEnd)
                return null;
            
            return new Range(foundStart, foundEnd, _scintilla);
        }


        public List<Range> SearchAll()
        {
            return SearchAll(_scintilla.GetRange());
        }


        public List<Range> SearchAll(Range searchRange)
        {
            Range foundRange = _scintilla.GetRange(-1, -1);

            List<Range> ret = new List<Range>();
            do
            {
                foundRange = Search(searchRange, foundRange);
                if (foundRange != null)
                    ret.Add(foundRange);
            }
            while (foundRange != null);
            return ret;
        }


        /// <summary>
        ///     Overridden. See <see cref="Object.ToString" />.
        /// </summary>
        public override string ToString()
        {
            return GetType().Name
                + " { Index="
                + _index.ToString(CultureInfo.CurrentCulture)
                + " }";
        }

        #endregion Methods


        #region Properties

        /// <summary>
        ///     Gets or sets the alpha transparency value used for drawing the fill color of the
        ///     <see cref="IndicatorStyle.RoundBox" /> and <see cref="IndicatorStyle.StraightBox" /> styles.
        /// </summary>
        /// <returns>
        ///     A <see cref="Byte" /> representing the alpha transparency value.
        ///     The default is 30.
        /// </returns>
        public byte Alpha
        {
            get
            {
                return (byte)_scintilla.DirectMessage(
                    NativeMethods.SCI_INDICGETALPHA,
                    (IntPtr)_index,
                    IntPtr.Zero);
            }
            set
            {
                if (value != Alpha)
                {
                    _scintilla.DirectMessage(
                        NativeMethods.SCI_INDICSETALPHA,
                        (IntPtr)_index,
                        (IntPtr)value);
                }
            }
        }


        /// <summary>
        ///     Gets or sets the color of the indicator.
        /// </summary>
        /// <returns>A <see cref="Color"/> that represents the color of the indicator.</returns>
        /// <remarks>
        ///     For indicator index 0 the default is #007F00 (dark green).
        ///     For indicator index 1 the default is #0000FF (blue).
        ///     For indicator index 2 the default is #FF0000 (red).
        ///     For indicator index 3 and above the default is #000000 (black).
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///     The <see cref="Color"/> specified has an alpha value that is less that <see cref="Byte.MaxValue"/>.
        /// </exception>
        public Color Color
        {
            get
            {
                // Look in the color bag first and fallback if not found
                if (_scintilla.ColorBag.ContainsKey(_colorBagKey))
                    return _scintilla.ColorBag[_colorBagKey];

                return Utilities.RgbToColor((int)_scintilla.DirectMessage(
                    NativeMethods.SCI_INDICGETFORE,
                    (IntPtr)_index,
                    IntPtr.Zero));
            }
            set
            {
                // Transparent colors are not allowed
                if (value != Color.Empty && value.A < Byte.MaxValue)
                    throw new ArgumentException(Resources.Exception_TransparentColor, "value");

                if (value != Color)
                {
                    if (value.IsEmpty)
                        value = GetDefaultColor();

                    // Scintilla can't keep track of named colors round-trip.
                    // If a color is known, we keep a local copy of it.
                    if (value.IsKnownColor)
                        _scintilla.ColorBag[_colorBagKey] = value;
                    else
                        _scintilla.ColorBag.Remove(_colorBagKey);

                    _scintilla.DirectMessage(
                        NativeMethods.SCI_INDICSETFORE,
                        (IntPtr)_index,
                        (IntPtr)Utilities.ColorToRgb(value));
                }
            }
        }


        /// <summary>
        ///     Gets or sets the draw mode for applying indicators to text.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="IndicatorDrawMode" /> values.
        ///     The default is <see cref="IndicatorDrawMode.Overlay" />.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     The value assigned is not one of the <see cref="IndicatorDrawMode" /> values.
        /// </exception>
        public IndicatorDrawMode DrawMode
        {
            get
            {
                return (IndicatorDrawMode)_scintilla.DirectMessage(
                    NativeMethods.SCI_INDICGETUNDER,
                    (IntPtr)_index,
                    IntPtr.Zero);
            }
            set
            {
                if (!Enum.IsDefined(typeof(IndicatorDrawMode), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(IndicatorDrawMode));

                if (value != DrawMode)
                {
                    _scintilla.DirectMessage(
                        NativeMethods.SCI_INDICSETUNDER,
                        (IntPtr)_index,
                        (IntPtr)value);
                }
            }
        }


        /// <summary>
        ///     Gets the index this indicator represents in a <see cref="Scintlla" /> control.
        /// </summary>
        /// <returns>
        ///     An <see cref="Int32" /> representing the zero-based index this
        ///     indicator represents in a <see cref="Scintlla" /> control.
        /// </returns>
        public int Index
        {
            get
            {
                return _index;
            }
        }


        /// <summary>
        ///     Gets or sets the alpha transparency value used for drawing the outline color of the
        ///     <see cref="IndicatorStyle.RoundBox" /> and <see cref="IndicatorStyle.StraightBox" /> styles.
        /// </summary>
        /// <returns>
        ///     A <see cref="Byte" /> representing the alpha transparency value.
        ///     The default is 50.
        /// </returns>
        public byte OutlineAlpha
        {
            get
            {
                return (byte)_scintilla.DirectMessage(
                    NativeMethods.SCI_INDICGETOUTLINEALPHA,
                    (IntPtr)_index,
                    IntPtr.Zero);
            }
            set
            {
                if (value != Alpha)
                {
                    _scintilla.DirectMessage(
                        NativeMethods.SCI_INDICSETOUTLINEALPHA,
                        (IntPtr)_index,
                        (IntPtr)value);
                }
            }
        }


        /// <summary>
        ///     Gets or sets the display style of the indicator.
        /// </summary>
        /// <returns>One of the <see cref="IndicatorStyle" /> values.</returns>
        /// <remarks>
        ///     For indicator index 0 the default is <see cref="IndicatorStyle.Squiggle" />.
        ///     For indicator index 1 the default is <see cref="IndicatorStyle.TT" />.
        ///     For indicator index 2 and above the default is <see cref="IndicatorStyle.Plain" />.
        /// </remarks>
        /// <exception cref="InvalidEnumArgumentException">
        ///     The value assigned is not one of the <see cref="IndicatorStyle" /> values.
        /// </exception>
        public virtual IndicatorStyle Style
        {
            get
            {
                return (IndicatorStyle)_scintilla.DirectMessage(
                    NativeMethods.SCI_INDICGETSTYLE,
                    (IntPtr)_index,
                    IntPtr.Zero);
            }
            set
            {
                if (!Enum.IsDefined(typeof(IndicatorStyle), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(IndicatorStyle));

                if (value != Style)
                {
                    _scintilla.DirectMessage(
                        NativeMethods.SCI_INDICSETSTYLE,
                        (IntPtr)_index,
                        (IntPtr)value);
                }
            }
        }

        #endregion Properties


        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Indicator" /> class.
        /// </summary>
        /// <param name="owner">The <see cref="Scintilla" /> control that created this object.</param>
        /// <param name="index">The zero-based index of the document line containing the annotation.</param>
        protected internal Indicator(Scintilla owner, int index)
        {
            _scintilla = owner;
            _index = index;

            _colorBagKey = "Indicator." + index + ".Color";
        }

        #endregion Constructors
    }
}
