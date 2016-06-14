using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls;

namespace SignalR
{
    public class getIME:TextBox
    {
        [Flags]
        public enum WORDREP_BREAK_TYPE
        {
            WORDREP_BREAK_EOW = 0,
            WORDREP_BREAK_EOS = 1,
            WORDREP_BREAK_EOP = 2,
            WORDREP_BREAK_EOC = 3
        }

        [ComImport]
        [Guid("BE41F4E6-9EAD-498f-A473-F3CA66F9BE8B")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IWordSink
        {
            void PutWord([MarshalAs(UnmanagedType.U4)] int cwc,
            [MarshalAs(UnmanagedType.LPWStr)] string pwcInBuf,
            [MarshalAs(UnmanagedType.U4)] int cwcSrcLen,
            [MarshalAs(UnmanagedType.U4)] int cwcSrcPos);
            void PutAltWord([MarshalAs(UnmanagedType.U4)] int cwc,
            [MarshalAs(UnmanagedType.LPWStr)] string pwcInBuf,
            [MarshalAs(UnmanagedType.U4)] int cwcSrcLen,
            [MarshalAs(UnmanagedType.U4)] int cwcSrcPos);
            void StartAltPhrase();
            void EndAltPhrase();
            void PutBreak(WORDREP_BREAK_TYPE breakType);
        }

        [ComImport]
        [Guid("BE41F4E6-9EAD-498f-A473-F3CA66F9BE8B")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPhraseSink
        {
            void PutSmallPhrase([MarshalAs(UnmanagedType.LPWStr)] string pwcNoun,
            [MarshalAs(UnmanagedType.U4)] int cwcNoun,
            [MarshalAs(UnmanagedType.LPWStr)] string pwcModifier,
            [MarshalAs(UnmanagedType.U4)] int cwcModifier,
            [MarshalAs(UnmanagedType.U4)] int ulAttachmentType);
            void PutPhrase([MarshalAs(UnmanagedType.LPWStr)] string pwcPhrase,
            [MarshalAs(UnmanagedType.U4)] int cwcPhrase);
        }



        public class CWordSink : IWordSink
        {
            public void PutWord(int cwc, string pwcInBuf, int cwcSrcLen, int cwcSrcPos)
            {
                Console.WriteLine("PutWord buffer: " + pwcInBuf.Substring(0, cwc));
            }

            public void PutAltWord(int cwc, string pwcInBuf, int cwcSrcLen, int cwcSrcPos)
            {
                Console.WriteLine("PutAltWord buffer: " + pwcInBuf.Substring(0, cwc));
            }

            public void StartAltPhrase()
            {
                Console.WriteLine("StartAltPhrase");
            }

            public void EndAltPhrase()
            {
                Console.WriteLine("EndAltPhrase");
            }

            public void PutBreak(WORDREP_BREAK_TYPE breakType)
            {
                string strBreak;
                switch (breakType)
                {
                    case WORDREP_BREAK_TYPE.WORDREP_BREAK_EOC:
                        strBreak = "EOC";
                        break;
                    case WORDREP_BREAK_TYPE.WORDREP_BREAK_EOP:
                        strBreak = "EOP";
                        break;
                    case WORDREP_BREAK_TYPE.WORDREP_BREAK_EOS:
                        strBreak = "EOS";
                        break;
                    case WORDREP_BREAK_TYPE.WORDREP_BREAK_EOW:
                        strBreak = "EOW";
                        break;
                    default:
                        strBreak = "ERROR";
                        break;
                }
                Console.WriteLine("PutBreak : " + strBreak);
            }
        }


        public class CPhraseSink : IPhraseSink
        {
            public void PutSmallPhrase(string pwcNoun, int cwcNoun, string pwcModifier, int cwcModifier, int ulAttachmentType)
            {
                Console.WriteLine("PutSmallPhrase: " + pwcNoun.Substring(0, cwcNoun)
                + " , " + pwcModifier.Substring(0, cwcModifier));
            }

            public void PutPhrase(string pwcPhrase, int cwcPhrase)
            {
                Console.WriteLine("PutPhrase: " + pwcPhrase.Substring(0, cwcPhrase));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TEXT_SOURCE
        {
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public delFillTextBuffer pfnFillTextBuffer;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string awcBuffer;
            [MarshalAs(UnmanagedType.U4)]
            public int iEnd;
            [MarshalAs(UnmanagedType.U4)]
            public int iCur;
        }

        // used to fill the buffer for TEXT_SOURCE
        public delegate uint delFillTextBuffer([MarshalAs(UnmanagedType.Struct)] ref TEXT_SOURCE pTextSource);

        [ComImport]
        [Guid("BE41F4E6-9EAD-498f-A473-F3CA66F9BE8B")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IWordBreaker
        {
            void Init([MarshalAs(UnmanagedType.Bool)] bool fQuery,
            [MarshalAs(UnmanagedType.U4)] int maxTokenSize,
            [MarshalAs(UnmanagedType.Bool)] out bool pfLicense);
            void BreakText([MarshalAs(UnmanagedType.Struct)] ref TEXT_SOURCE pTextSource,
            [MarshalAs(UnmanagedType.Interface)] IWordSink pWordSink,
            [MarshalAs(UnmanagedType.Interface)] IPhraseSink pPhraseSink);
            void GetLicenseToUse([MarshalAs(UnmanagedType.LPWStr)] out string ppwcsLicense);
        }

        [ComImport]
        [Guid("BE41F4E6-9EAD-498f-A473-F3CA66F9BE8B")]
        public class CWordBreaker
        {
        
        }

        public class CWB { 
        
        }
    }
}