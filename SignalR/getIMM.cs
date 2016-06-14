using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SignalR
{
    public class getIMM : System.Windows.Forms.TextBox
    {
            const int GCL_REVERSECONVERSION = 0x0002;

            [DllImport("Imm32.dll")]
            public static extern IntPtr ImmGetContext(IntPtr hWnd);

            [DllImport("User32.dll")]
            public static extern IntPtr GetKeyboardLayout(int idThread);

            [DllImport("Imm32.dll")]
            public static extern int ImmGetConversionList(
                IntPtr hKL,
                IntPtr hIMC,
                string lpSrc,
                IntPtr lpDst,
                int dwBufLen,
                int uFlag
            );

            [DllImport("Imm32.dll")]
            public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

            [StructLayout(LayoutKind.Sequential)]
            public class CANDIDATELIST
            {
                public int dwSize;
                public int dwStyle;
                public int dwCount;
                public int dwSelection;
                public int dwPageStart;
                public int dwPageSize;
                public int dwOffset;
            }

            public string GetReverseConversion(string AText)
            {
                IntPtr hIMC = ImmGetContext(this.Handle);
                IntPtr hKL = GetKeyboardLayout(0);
                CANDIDATELIST list = new CANDIDATELIST();
                int dwSize = ImmGetConversionList(hKL, hIMC, AText, IntPtr.Zero, 0, GCL_REVERSECONVERSION);
                IntPtr BufList = Marshal.AllocHGlobal(dwSize);
                ImmGetConversionList(hKL, hIMC, AText, BufList, dwSize, GCL_REVERSECONVERSION);
                Marshal.PtrToStructure(BufList, list);
                byte[] buf = new byte[dwSize];
                Marshal.Copy(BufList, buf, 0, dwSize);
                Marshal.FreeHGlobal(BufList);
                int os = list.dwOffset;
                string str = System.Text.Encoding.Default.GetString(buf, os, buf.Length - os - 3);
                ImmReleaseContext(this.Handle, hIMC);

                return str;
            }
    }
}