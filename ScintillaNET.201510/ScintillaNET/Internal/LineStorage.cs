#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion Using Directives


namespace ScintillaNET.Internal
{
    //
    // Manages per-line data. In here we don't really care what that data is--
    // we just make sure that it is kept in sync with the Scintilla control when
    // lines are added or removed.
    //
    // The current implementation stores data in a generic List<T> which should be
    // pleanty fast to keep up with most documents. If for some crazy reason it is not
    // we can evaluate faster data structures. The beauty of keeping it internal to
    // our class is we can change it at will. :)
    //
    // NOTE: As with most of our other helper libraries, very little error checking
    // is done here. It must be performed higher up.
    //
    internal sealed class LineStorage
    {
        #region Fields

        private Scintilla _scintilla;
        private List<LineStorageData> _lineData;

        #endregion Fields


        #region Methods

        public LineStorageData GetData(int lineIndex)
        {
            Debug.Assert(lineIndex >= 0);
            Debug.Assert(lineIndex < (int)_scintilla.DirectMessage(NativeMethods.SCI_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero));

            return _lineData[lineIndex];
        }


        private void scintilla_SCNotification(object sender, SCNotificationEventArgs e)
        {
            // We only care about modifications that add/delete lines
            NativeMethods.SCNotification scn = e.Notification;
            if (scn.nmhdr.code != NativeMethods.SCN_MODIFIED)
                return;
            else if (scn.linesAdded == 0)
                return;

            // Update the line data list
            int lineIndex = (int)_scintilla.DirectMessage(NativeMethods.SCI_LINEFROMPOSITION, new IntPtr(scn.position), IntPtr.Zero);
            if (scn.linesAdded < 0)
                _lineData.RemoveRange(lineIndex + 1, Math.Abs(scn.linesAdded));
            else
                _lineData.InsertRange(lineIndex + 1, new LineStorageData[scn.linesAdded]);

            Debug.Assert(_lineData.Count == _scintilla.Lines.Count);
        }


        public void SetData(int lineIndex, LineStorageData data)
        {
            Debug.Assert(lineIndex >= 0);
            Debug.Assert(lineIndex < (int)_scintilla.DirectMessage(NativeMethods.SCI_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero));

            _lineData[lineIndex] = data;
        }

        #endregion Methods


        #region Constructors

        public LineStorage(Scintilla scintilla)
        {
            Debug.Assert(scintilla != null);

            _scintilla = scintilla;
            _scintilla.SCNotification += scintilla_SCNotification;

            // Every document has at least one line
            _lineData = new List<LineStorageData>();
            _lineData.Add(new LineStorageData());
        }

        #endregion Constructors
    }
}
