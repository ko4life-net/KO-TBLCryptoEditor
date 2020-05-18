using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace KO.TBLCryptoEditor.Controls
{
    public class CryptoKeyInput : TextBox
    {
        public short Key => Convert.ToInt16(Text, 16);

        public CryptoKeyInput()
        {
            TextChanged += delegate { Validate(); };
        }

        private void Validate()
        {
            Action cartToEnd = () =>
            {
                SelectionStart = Text.Length;
                SelectionLength = 0;
            };

            if (!Text.StartsWith("0x"))
            {
                Text = "0x";
                cartToEnd();
                return;
            }

            if (Text.Length > 6)
            {
                Text = Text.Substring(0, 6);
                cartToEnd();
                return;
            }

            if (Text.Length > 2)
            {
                if (!Int32.TryParse(Text.Substring(2, Text.Length - 2), NumberStyles.HexNumber,
                                    CultureInfo.CurrentCulture, out int _))
                {
                    Text = "0x";
                    BackColor = Color.Crimson;
                    cartToEnd();
                    return;
                }
            }


            Text = "0x" + Text.Substring(2, Text.Length - 2).ToUpper();
            BackColor = Text.Length != 6 ? Color.Crimson : SystemColors.Control;
            cartToEnd();
        }
    }
}
