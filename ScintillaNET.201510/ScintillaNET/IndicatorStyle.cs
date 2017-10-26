// TODO Eliminate Squiggle and use SquigglePixmap instead?

#region Using Directives

using System;
using ScintillaNET.Internal;

#endregion Using Directives


namespace ScintillaNET
{
    /// <summary>
    ///     Defines the possible display style of an <see cref="Indicator" />.
    /// </summary>
    public enum IndicatorStyle
    {
        /// <summary>
        ///     Underlined with a single, straight line.
        /// </summary>
        Plain = NativeMethods.INDIC_PLAIN,

        /// <summary>
        ///     A squiggly underline.
        /// </summary>
        /// <remarks>Requires 3 pixels of descender space.</remarks>
        Squiggle = NativeMethods.INDIC_SQUIGGLE,

        /// <summary>
        ///     A line of small T shapes.
        /// </summary>
        TT = NativeMethods.INDIC_TT,

        /// <summary>
        ///     Diagonal hatching.
        /// </summary>
        Diagonal = NativeMethods.INDIC_DIAGONAL,

        /// <summary>
        ///     Strike out.
        /// </summary>
        Strike = NativeMethods.INDIC_STRIKE,

        /// <summary>
        ///     An indicator with no visual effect.
        /// </summary>
        /// <remarks>
        ///     This can be valuable when indicators are used to track
        ///     positions rather than change the display.
        /// </remarks>
        Hidden = NativeMethods.INDIC_HIDDEN,

        /// <summary>
        ///     A rectangle around the text.
        /// </summary>
        Box = NativeMethods.INDIC_BOX,

        /// <summary>
        ///     A rectangle with rounded corners around the text using translucent
        ///     drawing with the interior usually more transparent than the border.
        /// </summary>
        RoundBox = NativeMethods.INDIC_ROUNDBOX,

        /// <summary>
        ///     A rectangle around the text using translucent drawing with the
        ///     interior usually more transparent than the border.
        /// </summary>
        StraightBox = NativeMethods.INDIC_STRAIGHTBOX,

        /// <summary>
        ///     A dashed underline.
        /// </summary>
        Dash = NativeMethods.INDIC_DASH,

        /// <summary>
        ///     A dotted underline.
        /// </summary>
        Dots = NativeMethods.INDIC_DOTS,

        /// <summary>
        ///     A squiggly underline.
        /// </summary>
        /// <remarks>Requires 2 pixels of descender space.</remarks>
        SquiggleLow = NativeMethods.INDIC_SQUIGGLELOW,

        /// <summary>
        ///     A dotted rectangle around the text using translucent drawing.
        /// </summary>
        DotBox = NativeMethods.INDIC_DOTBOX,

        /// <summary>
        ///     A squiggly underline.
        /// </summary>
        /// <remarks>Performs better that other squiggle styles.</remarks>
        SquigglePixmap = NativeMethods.INDIC_SQUIGGLEPIXMAP
    }
}
