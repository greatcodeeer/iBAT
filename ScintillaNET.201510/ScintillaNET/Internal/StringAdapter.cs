#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion Using Directives


namespace ScintillaNET.Internal
{
    //
    // Provides an access layer for getting text in and out of Scintilla. Since Scintilla may
    // internally store text as a single byte (ASCII) or multiple bytes, (Codepage, UTF-8) a
    // byte index may not represent a character index. This causes confusion for users.
    //
    // To stop the users from hanging themselves we have several routines that convert text
    // from the .NET string format (UTF-16) to Scintilla's internal format and vice-versa.
    // To boost performance of the translation of byte index to char index (and vice-versa) we
    // maintain a cache of each line's character length.
    //
    // NOTE: Very little error checking is performed here so it must be done higher up.
    //
    internal sealed class StringAdapter
    {
        #region Constants

        private const int UNCACHED = -1;

        #endregion Constants


        #region Fields

        private Scintilla _scintilla;
        private LineStorage _lineStorage;

        #endregion Fields


        #region Methods

        private void InvalidateCache(int lineIndex, int linesAdded)
        {
            Debug.Assert(lineIndex >= 0);
            Debug.Assert(lineIndex < (int)_scintilla.DirectMessage(NativeMethods.SCI_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero));

            // Invalidate the cached length for the modified line and any lines added
            int endLineIndex = lineIndex + linesAdded + 1;
            while (lineIndex < endLineIndex)
            {
                SetCachedLength(lineIndex, UNCACHED);
                lineIndex++;
            }
        }


        private void scintilla_SCNotification(object sender, SCNotificationEventArgs e)
        {
            // We only care about text modifications
            NativeMethods.SCNotification scn = e.Notification;
            if (scn.nmhdr.code != NativeMethods.SCN_MODIFIED)
                return;

            bool invalidateCache = false;
            invalidateCache |= ((scn.modificationType & NativeMethods.SC_MOD_INSERTTEXT) == NativeMethods.SC_MOD_INSERTTEXT);
            invalidateCache |= ((scn.modificationType & NativeMethods.SC_MOD_DELETETEXT) == NativeMethods.SC_MOD_DELETETEXT);

            if (invalidateCache)
            {
                int lineIndexModified = (int)_scintilla.DirectMessage(NativeMethods.SCI_LINEFROMPOSITION, new IntPtr(scn.position), IntPtr.Zero);
                InvalidateCache(lineIndexModified, (scn.linesAdded > 0 ? scn.linesAdded : 0));
            }
        }


        private void SetCachedLength(int lineIndex, int cachedLength)
        {
            LineStorageData lsd = _lineStorage.GetData(lineIndex);
            lsd.CachedLength = cachedLength;

            _lineStorage.SetData(lineIndex, lsd);
        }

        #endregion Methods


        #region Constructors

        public StringAdapter(Scintilla scintilla, LineStorage lineStorage)
        {
            Debug.Assert(scintilla != null);
            Debug.Assert(lineStorage != null);

            _scintilla = scintilla;
            _scintilla.SCNotification += scintilla_SCNotification;

            _lineStorage = lineStorage;
        }

        #endregion Constructors
    }
}
