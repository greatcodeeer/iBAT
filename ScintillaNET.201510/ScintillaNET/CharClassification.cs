#region Using Directives

using System;

#endregion Using Directives


namespace ScintillaNET
{
    /// <summary>
    ///     Defines character classifications in a <see cref="Scintilla" /> control.
    /// </summary>
    public enum CharClassification
    {
        /// <summary>
        ///     Characters with this classification are interpreted as whitespace characters.
        /// </summary>
        Whitespace = 0,

        /// <summary>
        ///     Characters with this classification are interpreted as word characters.
        /// </summary>
        Word = 2,

        /// <summary>
        ///     Characters with this classification are interpreted as punctuation characters.
        /// </summary>
        Punctuation = 3
    }
}
