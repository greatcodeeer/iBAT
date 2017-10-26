#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

#endregion Using Directives


namespace ScintillaNET.Internal
{
    partial class NativeMethods
    {
        #region Constants

        public const int
            ANNOTATION_HIDDEN = 0,
            ANNOTATION_STANDARD = 1,
            ANNOTATION_BOXED = 2;

        public const int
            INDIC_PLAIN = 0,
            INDIC_SQUIGGLE = 1,
            INDIC_TT = 2,
            INDIC_DIAGONAL = 3,
            INDIC_STRIKE = 4,
            INDIC_HIDDEN = 5,
            INDIC_BOX = 6,
            INDIC_ROUNDBOX = 7,
            INDIC_STRAIGHTBOX = 8,
            INDIC_DASH = 9,
            INDIC_DOTS = 10,
            INDIC_SQUIGGLELOW = 11,
            INDIC_DOTBOX = 12,
            INDIC_SQUIGGLEPIXMAP = 13,
            INDIC_MAX = 31;

        public const int
            SC_CACHE_NONE = 0,
            SC_CACHE_CARET = 1,
            SC_CACHE_PAGE = 2,
            SC_CACHE_DOCUMENT = 3;

        public const int
            SC_CP_UTF8 = 65001;

        // These values come from the Scintilla documentation,
        // not the Scintilla.h header file.
        public const int
            SC_CP_JAPANESE = 932,
            SC_CP_CHINESE_SIMPLIFIED = 936,
            SC_CP_KOREAN_UNIFIED = 949,
            SC_CP_CHINESE_TRADITIONAL = 950,
            SC_CP_KOREAN_JOHAB = 1361,
            SC_CP_ASCII = 20127;

        public const int
            SC_MARK_CIRCLE = 0,
            SC_MARK_ROUNDRECT = 1,
            SC_MARK_ARROW = 2,
            SC_MARK_SMALLRECT = 3,
            SC_MARK_SHORTARROW = 4,
            SC_MARK_EMPTY = 5,
            SC_MARK_ARROWDOWN = 6,
            SC_MARK_MINUS = 7,
            SC_MARK_PLUS = 8,
            SC_MARK_VLINE = 9,
            SC_MARK_LCORNER = 10,
            SC_MARK_TCORNER = 11,
            SC_MARK_BOXPLUS = 12,
            SC_MARK_BOXPLUSCONNECTED = 13,
            SC_MARK_BOXMINUS = 14,
            SC_MARK_BOXMINUSCONNECTED = 15,
            SC_MARK_LCORNERCURVE = 16,
            SC_MARK_TCORNERCURVE = 17,
            SC_MARK_CIRCLEPLUS = 18,
            SC_MARK_CIRCLEPLUSCONNECTED = 19,
            SC_MARK_CIRCLEMINUS = 20,
            SC_MARK_CIRCLEMINUSCONNECTED = 21,
            SC_MARK_BACKGROUND = 22,
            SC_MARK_DOTDOTDOT = 23,
            SC_MARK_ARROWS = 24,
            SC_MARK_PIXMAP = 25,
            SC_MARK_FULLRECT = 26,
            SC_MARK_LEFTRECT = 27,
            SC_MARK_AVAILABLE = 28,
            SC_MARK_UNDERLINE = 29,
            SC_MARK_RGBAIMAGE = 30,
            SC_MARK_CHARACTER = 10000;

        public const int
            SC_WRAP_NONE = 0,
            SC_WRAP_WORD = 1,
            SC_WRAP_CHAR = 2;

        public const int
            SC_WRAPINDENT_FIXED = 0,
            SC_WRAPINDENT_SAME = 1,
            SC_WRAPINDENT_INDENT = 2;

        public const int
            SC_MOD_INSERTTEXT = 0x1,
            SC_MOD_DELETETEXT = 0x2,
            SC_MOD_CHANGEANNOTATION = 0x20000;

        public const int
            SC_WRAPVISUALFLAG_NONE = 0x0000,
            SC_WRAPVISUALFLAG_END = 0x0001,
            SC_WRAPVISUALFLAG_START=  0x0002;

        public const int
            SC_WRAPVISUALFLAGLOC_DEFAULT = 0x0000,
            SC_WRAPVISUALFLAGLOC_END_BY_TEXT = 0x0001,
            SC_WRAPVISUALFLAGLOC_START_BY_TEXT = 0x0002;

        public const int
            SCEN_CHANGE = 768;

        public const int
            SCI_GETLENGTH = 2006,
            SCI_GOTOLINE = 2024,
            SCI_GETCURLINE = 2027,
            SCI_SETCODEPAGE = 2037,
            SCI_STYLESETFONT = 2056,
            SCI_SETWORDCHARS = 2077,
            SCI_INDICSETSTYLE = 2080,
            SCI_INDICGETSTYLE = 2081,
            SCI_INDICSETFORE = 2082,
            SCI_INDICGETFORE = 2083,
            SCI_SETHSCROLLBAR = 2130,
            SCI_GETHSCROLLBAR = 2131,
            SCI_GETCODEPAGE = 2137,
            SCI_GETSELECTIONSTART = 2143,
            SCI_GETSELECTIONEND = 2145,
            SCI_GETFIRSTVISIBLELINE = 2152,
            SCI_GETLINE = 2153,
            SCI_GETLINECOUNT = 2154,
            SCI_LINEFROMPOSITION = 2166,
            SCI_POSITIONFROMLINE = 2167,
            SCI_LINESCROLL = 2168,
            SCI_SCROLLCARET = 2169,
            SCI_CANPASTE = 2173,
            SCI_CUT = 2177,
            SCI_COPY = 2178,
            SCI_PASTE = 2179,
            SCI_GETDIRECTPOINTER = 2185,
            SCI_SETTARGETSTART = 2190,
            SCI_SETTARGETEND = 2192,
            SCI_WRAPCOUNT = 2235,
            SCI_SETWRAPMODE = 2268,
            SCI_GETWRAPMODE = 2269,
            SCI_SETLAYOUTCACHE = 2272,
            SCI_GETLAYOUTCACHE = 2273,
            SCI_SETSCROLLWIDTH = 2274,
            SCI_GETSCROLLWIDTH = 2275,
            SCI_SETENDATLASTLINE = 2277,
            SCI_GETENDATLASTLINE = 2278,
            SCI_SETVSCROLLBAR = 2280,
            SCI_GETVSCROLLBAR = 2281,
            SCI_LINESJOIN = 2288,
            SCI_LINESSPLIT = 2289,
            SCI_ZOOMIN = 2333,
            SCI_ZOOMOUT = 2334,
            SCI_LINELENGTH = 2350,
            SCI_SETZOOM = 2373,
            SCI_GETZOOM = 2374,
            SCI_SETXOFFSET = 2397,
            SCI_GETXOFFSET = 2398,
            SCI_COPYRANGE = 2419,
            SCI_SETWHITESPACECHARS = 2443,
            SCI_SETWRAPVISUALFLAGS = 2460,
            SCI_GETWRAPVISUALFLAGS = 2461,
            SCI_SETWRAPVISUALFLAGSLOCATION = 2462,
            SCI_GETWRAPVISUALFLAGSLOCATION = 2463,
            SCI_SETWRAPSTARTINDENT = 2464,
            SCI_GETWRAPSTARTINDENT = 2465,
            SCI_SETPASTECONVERTENDINGS = 2467,
            SCI_GETPASTECONVERTENDINGS = 2468,
            SCI_SETWRAPINDENTMODE = 2472,
            SCI_GETWRAPINDENTMODE = 2473,
            SCI_STYLEGETFONT = 2486,
            SCI_INDICSETUNDER = 2510,
            SCI_INDICGETUNDER = 2511,
            SCI_SETPOSITIONCACHE = 2514,
            SCI_GETPOSITIONCACHE = 2515,
            SCI_SETSCROLLWIDTHTRACKING = 2516,
            SCI_GETSCROLLWIDTHTRACKING = 2517,
            SCI_COPYALLOWLINE = 2519,
            SCI_INDICSETALPHA = 2523,
            SCI_INDICGETALPHA = 2524,
            SCI_ANNOTATIONSETTEXT = 2540,
            SCI_ANNOTATIONGETTEXT = 2541,
            SCI_ANNOTATIONSETSTYLE = 2542,
            SCI_ANNOTATIONGETSTYLE = 2543,
            SCI_ANNOTATIONSETSTYLES = 2544,
            SCI_ANNOTATIONGETSTYLES = 2545,
            SCI_ANNOTATIONGETLINES = 2546,
            SCI_ANNOTATIONCLEARALL = 2547,
            SCI_ANNOTATIONSETVISIBLE = 2548,
            SCI_ANNOTATIONGETVISIBLE = 2549,
            SCI_ANNOTATIONSETSTYLEOFFSET = 2550,
            SCI_ANNOTATIONGETSTYLEOFFSET = 2551,
            SCI_INDICSETOUTLINEALPHA = 2558,
            SCI_INDICGETOUTLINEALPHA = 2559,
            SCI_SETFIRSTVISIBLELINE = 2613,
            SCI_GETRANGEPOINTER = 2643,
            SCI_GETGAPPOSITION = 2644,
            SCI_GETWORDCHARS = 2646,
            SCI_GETWHITESPACECHARS = 2647,
            SCI_SETPUNCTUATIONCHARS = 2648,
            SCI_GETPUNCTUATIONCHARS = 2649;

        public const int
            SCN_MODIFIED = 2008,
            SCN_ZOOM = 2018,
            SCN_HOTSPOTCLICK = 2019,
            SCN_HOTSPOTDOUBLECLICK = 2020,
            SCN_HOTSPOTRELEASECLICK = 2027;

        #endregion Constants


        #region Callbacks

        public delegate IntPtr Scintilla_DirectFunction(
            IntPtr sci,
            int iMessage,
            IntPtr wParam,
            IntPtr lParam);

        #endregion Callbacks


        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct Sci_NotifyHeader
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SCNotification
        {
            public Sci_NotifyHeader nmhdr;
            public int position;
            public int ch;
            public int modifiers;
            public int modificationType;
            public IntPtr text;
            public int length;
            public int linesAdded;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int line;
            public int foldLevelNow;
            public int foldLevelPrev;
            public int margin;
            public int listType;
            public int x;
            public int y;
            public int token;
            public int annotationLinesAdded;
            public int updated;
        }

        #endregion Structures
    }
}
