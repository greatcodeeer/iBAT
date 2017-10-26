#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ScintillaNET.Internal;
using ScintillaNET.Properties;

#endregion Using Directives


namespace ScintillaNET
{
    /// <summary>
    ///     Manages indicators in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <remarks>
    ///     Indicators are used to display additional information over the top of styling.
    ///     They can be used to show, for example, syntax errors, deprecated names, and bad indentation by drawing underlines under text or boxes around text.
    /// </remarks>
    public class Indicators : IEnumerable<Indicator>
    {
        #region Constants

        /// <summary>
        ///     Represents the largest possible index for an <see cref="Indicator" /> definition.
        ///     This field is constant.
        /// </summary>
        public const int MaxIndex = NativeMethods.INDIC_MAX;

        #endregion Constants


        #region Fields

        private Scintilla _scintilla;

        #endregion Fields


        #region Methods

        /// <summary>
        ///     Creates and returns a new <see cref="Indicator" /> object.
        /// </summary>
        /// <returns>A new <see cref="Indicator" /> object.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual Indicator CreateIndicatorInstance(int index)
        {
            return new Indicator(_scintilla, index);
        }


        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="Indicators" />.
        /// </summary>
        /// <returns>An enumerator for the <see cref="Indicators" />.</returns>
        public IEnumerator<Indicator> GetEnumerator()
        {
            int index = 0;
            while (index <= MaxIndex)
                yield return this[index++];
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void Reset()
        {
            for (int i = 0; i < 32; i++)
                this[i].Reset();
        }

        #endregion Methods


        #region Indexers

        /// <summary>
        ///     Gets the <see cref="Indicator" /> definition at the specified index.
        /// </summary>
        /// <param name="index">The index of the indicator definition to retrieve.</param>
        /// <returns>A <see cref="Indicator" /> representing the indicator defined for the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0 or greater than <see cref="MaxIndex" />.
        /// </exception>
        public virtual Indicator this[int index]
        {
            get
            {
                if (index < 0 || index > MaxIndex)
                    throw new ArgumentOutOfRangeException("index", Resources.Exception_IndexOutOfRangeMaxIndex);

                return CreateIndicatorInstance(index);
            }
        }

        #endregion Indexers


        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Indicators" /> class.
        /// </summary>
        /// <param name="owner">The <see cref="Scintilla" /> control that created this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="owner" /> is null.</exception>
        public Indicators(Scintilla owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            _scintilla = owner;
        }

        #endregion Constructors
    }
}
