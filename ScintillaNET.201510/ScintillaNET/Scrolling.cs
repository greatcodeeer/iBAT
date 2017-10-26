#region Using Directives

using System;
using System.ComponentModel;
using System.Windows.Forms;
using ScintillaNET.Design;
using ScintillaNET.Internal;

#endregion Using Directives


namespace ScintillaNET
{
    /// <summary>
    ///     Represents the scrolling options of a <see cref="Scintilla"/> control. 
    /// </summary>
    [TypeConverterAttribute(typeof(ScintillaExpandableObjectConverter))]
    public class Scrolling
    {
        #region Fields

        private Scintilla _scintilla;

        #endregion Fields


        #region Methods

        /// <summary>
        ///     Scrolls the display by the specified number of lines and columns.
        /// </summary>
        /// <param name="lines">The number of display lines to scroll. Positive numbers scroll down, negative numbers scroll up.</param>
        /// <param name="columns">The number of columns to scroll. Positive numbers scroll right, negative numbers scroll left.</param>
        public virtual void ScrollBy(int lines, int columns)
        {
            // NOTE: We reverse the order of the params
            _scintilla.DirectMessage(NativeMethods.SCI_LINESCROLL, new IntPtr(columns), new IntPtr(lines));
        }


        /// <summary>
        ///     Scrolls the contents of the control to the current caret position.
        /// </summary>
        public virtual void ScrollToCaret()
        {
            _scintilla.DirectMessage(NativeMethods.SCI_SCROLLCARET, IntPtr.Zero, IntPtr.Zero);
        }


        /// <summary>
        ///     Scrolls the contents of the control to the line index specified.
        /// </summary>
        /// <param name="lineIndex">The zero-based index of the line to bring into view.</param>
        /// <remarks>
        ///     The current selection wil also be reset and the caret placed at the beginning
        ///     of the line specified. A value outside of the range of possible line indexes
        ///     will automatically be clamped to a valid index.
        /// </remarks>
        public virtual void ScrollToLine(int lineIndex)
        {
            _scintilla.DirectMessage(NativeMethods.SCI_GOTOLINE, new IntPtr(lineIndex), IntPtr.Zero);
        }

        #endregion Methods


        #region Properties

        /// <summary>
        ///     Gets or sets the number of pixels by which the <see cref="Scintilla" /> control is scrolled horizontally.
        /// </summary>
        /// <returns>
        ///     The number of pixels by which the <see cref="Scintilla" /> control is scrolled horizontally.
        ///     The default is 0.
        /// </returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int HorizontalScrollOffset
        {
            get
            {
                return (int)_scintilla.DirectMessage(NativeMethods.SCI_GETXOFFSET, IntPtr.Zero, IntPtr.Zero);
            }
            set
            {
                if (value != HorizontalScrollOffset)
                    _scintilla.DirectMessage(NativeMethods.SCI_SETXOFFSET, new IntPtr(value), IntPtr.Zero);
            }
        }


        /// <summary>
        ///     Gets or sets whether the <see cref="Scintilla" /> control automatically increases the horizontal
        ///     scroll width based on the text width.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="Scintilla" /> control automatically increases the horizontal scroll
        ///     width based on the text width; otherwise, false. The default is true.
        /// </returns>
        [DefaultValue(true)]
        [Description("Enables automatic scaling of the horizontal scroll width.")]
        [NotifyParentProperty(true)]
        public virtual bool HorizontalScrollTracking
        {
            get
            {
                return (_scintilla.DirectMessage(NativeMethods.SCI_GETSCROLLWIDTHTRACKING, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero);
            }
            set
            {
                if (value != HorizontalScrollTracking)
                    _scintilla.DirectMessage(NativeMethods.SCI_SETSCROLLWIDTHTRACKING, (value ? new IntPtr(1) : IntPtr.Zero), IntPtr.Zero);
            }
        }


        /// <summary>
        ///     Gets or sets the number of pixels by which the <see cref="Scintilla" /> control can scroll horizontally.
        /// </summary>
        /// <returns>
        ///     The number of pixels by which the <see cref="Scintilla" /> control can scroll horizontally.
        ///     The default is 1.
        /// </returns>
        [DefaultValue(1)]
        [Description("Sets the rage this control can be scrolled horizontally.")]
        [NotifyParentProperty(true)]
        public virtual int HorizontalScrollWidth
        {
            get
            {
                return (int)_scintilla.DirectMessage(NativeMethods.SCI_GETSCROLLWIDTH, IntPtr.Zero, IntPtr.Zero);
            }
            set
            {
                if (value != HorizontalScrollWidth)
                    _scintilla.DirectMessage(NativeMethods.SCI_SETSCROLLWIDTH, (IntPtr)value, IntPtr.Zero);
            }
        }


        /// <summary>
        ///     Gets or sets which scroll bars should appear in a <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="ScrollBars" /> enumeration values that indicates whether
        ///     a <see cref="Scintilla" /> control appears with no scroll bars, a horizontal scroll bar,
        ///     a vertical scroll bar, or both. The default is <see cref="ScrollBars.Both" />.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     The value assigned is not one of the <see cref="ScrollBars" /> values.
        /// </exception>
        [DefaultValue(ScrollBars.Both)]
        [Description("Indicates which scroll bars will be shown for this control.")]
        [NotifyParentProperty(true)]
        public virtual ScrollBars ScrollBars
        {
            get
            {
                bool h = (_scintilla.DirectMessage(NativeMethods.SCI_GETHSCROLLBAR, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero);
                bool v = (_scintilla.DirectMessage(NativeMethods.SCI_GETVSCROLLBAR, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero);

                if (h && v)
                    return ScrollBars.Both;
                else if (h)
                    return ScrollBars.Horizontal;
                else if (v)
                    return ScrollBars.Vertical;
                else
                    return ScrollBars.None;
            }
            set
            {
                if (!Enum.IsDefined(typeof(ScrollBars), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ScrollBars));

                if (value != ScrollBars)
                {
                    bool h = (value & ScrollBars.Horizontal) == ScrollBars.Horizontal;
                    bool v = (value & ScrollBars.Vertical) == ScrollBars.Vertical;

                    _scintilla.DirectMessage(NativeMethods.SCI_SETHSCROLLBAR, (h ? new IntPtr(1) : IntPtr.Zero), IntPtr.Zero);
                    _scintilla.DirectMessage(NativeMethods.SCI_SETVSCROLLBAR, (v ? new IntPtr(1) : IntPtr.Zero), IntPtr.Zero);
                }
            }
        }


        /// <summary>
        ///     Gets or sets whether vertical scrolling is allowed past the last line of text
        ///     in a <see cref="Scintilla" /> control
        /// </summary>
        /// <returns>
        ///     true to allow vertical scrolling past the last line of text in a <see cref="Scintilla" /> control;
        ///     otherwise, false. The default is false.
        /// </returns>
        [DefaultValue(false)]
        [Description("Allows scrolling past the last line of text.")]
        [NotifyParentProperty(true)]
        public virtual bool ScrollPastEnd
        {
            get
            {
                return (_scintilla.DirectMessage(NativeMethods.SCI_GETENDATLASTLINE, IntPtr.Zero, IntPtr.Zero) == IntPtr.Zero);
            }
            set
            {
                if (value != ScrollPastEnd)
                    _scintilla.DirectMessage(NativeMethods.SCI_SETENDATLASTLINE, (value ? IntPtr.Zero : new IntPtr(1)), IntPtr.Zero);
            }
        }

        #endregion Properties


        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Scrolling" /> class
        ///     for the given <see cref="Scintilla" /> control.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="scintilla" /> is null.</exception>
        public Scrolling(Scintilla scintilla)
        {
            if (scintilla == null)
                throw new ArgumentNullException("scintilla");

            _scintilla = scintilla;
        }

        #endregion Constructors
    }
}
