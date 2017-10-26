#region Using Directives

using System;

#endregion Using Directives


namespace ScintillaNET.Internal
{
    //
    // The data object that we store for each line of text in a Scintilla control.
    // NOTE:
    //   1). Size matters. Don't let this object grow too big.
    //   2). Don't access properties directly. Use the helper libraries who manage this data.
    //   3). There is no clean-up. You will not be notified when line data is deleted.
    //   4). This is a struct. No reference objects.
    //
    internal struct LineStorageData
    {
        public int CachedLength { get; set; }
    }
}
