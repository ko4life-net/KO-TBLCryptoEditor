using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;

using KO.TBLCryptoEditor.Core;
using KO.TBLCryptoEditor.Utils;

namespace KO.TBLCryptoEditor.Views
{
    public partial class ViewOffsetsWindow : Form
    {
        Dictionary<string, string> basicInfo;
        private TargetPE _pe;

        public ViewOffsetsWindow(TargetPE pe)
        {
            if (pe == null)
                throw new ArgumentNullException();

            _pe = pe;

            InitializeComponent();

            string cryptoType = _pe.CryptoPatches.CryptoType == CryptoType.XOR ? "XOR Cipher" : "DES + XOR Cipher";
            basicInfo = new Dictionary<string, string>
            {
                { "Patches Count", $"{_pe.CryptoPatches.Count}" },
                { "Target Executable", $"{_pe.FilePath}" },
                { "Client Internal Version", $"{_pe.ClientVersion}" },
                { "Linker Version", $"MSVC-{_pe.MajorLinkerVersion}.0 (Microsoft Visual C++)" },
                { "Inlined Functions Count", $"{_pe.CryptoPatches.Count(p => p.Inlined)}" },
                { "Encryption Algorithm", cryptoType }
            };
        }

        private void ViewOffsetsWindow_Load(object sender, EventArgs e)
        {
            SerializePatches();
        }

        private void SerializePatches()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<style>{GetStyleSheet()}\n</style>");

            using (HtmlTable table = new HtmlTable(sb, "Executable Basic Information"))
            {
                foreach (var item in basicInfo)
                {
                    using (HtmlTableRow row = new HtmlTableRow(sb))
                    {
                        row.AddCell($"<strong>{item.Key}</strong>");
                        row.AddCell(item.Value);
                    }
                }
            }

            AddSeparator(sb);

            using (HtmlTable table = new HtmlTable(sb, "Table Encryption Keys"))
            {
                using (HtmlTableRow row = new HtmlTableRow(sb, true))
                {
                    row.AddCell("#");
                    row.AddCell("Key");
                    row.AddCell("File Offset");
                    row.AddCell("Virtual Address");
                    row.AddCell("Inlined Function?");
                }

                var patches = _pe.CryptoPatches;
                int patCount = patches.Count;
                for (int i = 0; i < patCount; i++)
                {
                    CryptoPatch patch = patches[i];
                    CryptoKey fk = patch.Keys.First();

                    string rowId = String.Empty;
                    if (patch.Inlined)
                        rowId = "inlined";
                    else
                        rowId = (i + 1) % 2 != 0 ? "odd" : "even";

                    using (HtmlTableRow row = new HtmlTableRow(sb, false, rowId))
                    {
                        row.AddCell((i+1).ToString());

                        string keys, fileOffsets, VAs;
                        keys = fileOffsets = VAs = String.Empty;
                        foreach (CryptoKey key in patch.Keys)
                        {
                            keys += $"&bull; 0x{key.Key:X4}<br>\n";
                            fileOffsets += $"&bull; 0x{((int)key.Offset):X8}<br>\n";
                            VAs += $"&bull; 0x{((int)key.VirtualAddress):X8}<br>\n";
                        }
                        keys.TrimEnd('\n');
                        fileOffsets.TrimEnd('\n');
                        VAs.TrimEnd('\n');

                        row.AddCell(keys);
                        row.AddCell(fileOffsets);
                        row.AddCell(VAs);
                        row.AddCell(patch.Inlined.ToString());
                    }
                }
            }

            view.DocumentText = sb.ToString();
        }

        private void AddLineWithTag(StringBuilder sb, string tag, int value)
        {
            AddLineWithTag(sb, tag, value.ToString());
        }

        private void AddLineWithTag(StringBuilder sb, string tag, string text)
        {
            AddLine(sb, $"<strong>{tag}:</strong> {text}");
        }

        private void AddLine(StringBuilder sb, string text)
        {
            sb.AppendLine($"{text}<br>");
        }

        private void AddSeparator(StringBuilder sb)
        {
            sb.AppendLine("<br><hr style=\"width:100%;text-align:left;margin-left:0\"><br>");
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            btnCopyClipboard.Text = "Copied!";
            Task.Run(() =>
            {
                Thread.Sleep(700);
                btnCopyClipboard.InvokeSafe(() => btnCopyClipboard.Text = "Copy to Clipboard");
            });

            StringBuilder sb = new StringBuilder();

            foreach (var item in basicInfo)
                sb.AppendLine($"{item.Key}: {item.Value}");

            sb.AppendLine();
            var patches = _pe.CryptoPatches;
            for (int i = 0; i < patches.Count; i++)
            {
                var patch = patches[i];
                sb.AppendLine($"[{(i+1):D2}]: {patch}");
            }

            Clipboard.SetText(sb.ToString());
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "HTML |*.html";
            sfd.Title = "Save as HTML format";
            sfd.InitialDirectory = Path.GetDirectoryName(_pe.FilePath);
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllText(sfd.FileName, view.DocumentText);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string GetStyleSheet()
        {
            return @"
caption {
  font-size: 25px;
  font-weight: bold;
  padding: 5px;
}

table {
  font-family: ""Trebuchet MS"", Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

table td, table th {
  border: 1px solid #ddd;
  padding: 8px;
}

table tbody #even {
	background-color: #f2f2f2;
}

table tbody #inlined {
	background-color: #ff5f5f;
}

table th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #4CAF50;
  color: white;
}
                ";
        }
    }
}
