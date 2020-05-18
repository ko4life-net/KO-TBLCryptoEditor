using System;
using System.Runtime.InteropServices;

namespace KO.TBLCryptoEditor.Utils
{
    [StructLayout(LayoutKind.Explicit)]
    public struct DWORD
    {
        [FieldOffset(0)] public int iValue;
        [FieldOffset(0)] public float fValue;
        [FieldOffset(0)] public short sValue1;
        [FieldOffset(2)] public short sValue2;
        [FieldOffset(0)] public byte byValue1;
        [FieldOffset(1)] public byte byValue2;
        [FieldOffset(2)] public byte byValue3;
        [FieldOffset(3)] public byte byValue4;

        public static explicit operator DWORD(int value)
        {
            var dword = new DWORD();
            dword.iValue = value;
            return dword;
        }

        public static explicit operator DWORD(short value)
        {
            var dword = new DWORD();
            dword.sValue1 = value;
            return dword;
        }

        public static explicit operator DWORD(ushort value)
        {
            var dword = new DWORD();
            dword.sValue1 = (short)value;
            return dword;
        }
    }
}
