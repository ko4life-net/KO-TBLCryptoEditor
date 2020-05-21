using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using log4net;
using Microsoft.WindowsAPICodePack.Dialogs;

using KO.TBLCryptoEditor.Controls;
using KO.TBLCryptoEditor.Core;
using KO.TBLCryptoEditor.Utils;
using System.Runtime.CompilerServices;

namespace KO.TBLCryptoEditor.Views
{
    public partial class MainWindow : Form
    {
        private static readonly ILog Log = Logger.GetLogger(nameof(MainWindow));

        private TargetPE _targetFile;
        private short[] _previousKeys;

        public MainWindow()
        {
            InitializeComponent();
            _targetFile = null;
            _previousKeys = null;

            Text += $" (ko4life.net - v{Application.ProductVersion})";
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            panelDragArea.DragLeave += (ss, ee) => panelDragArea.BackColor = SystemColors.Control;
            panelDragArea.DragEnter += (ss, ee) => panelDragArea.BackColor = SystemColors.ActiveCaption;
            panelDragArea.DragDrop += (ss, ee) => LoadPE(((string[])ee.Data.GetData(DataFormats.FileDrop))[0]);
            panelDragArea.DragEnter += (ss, ee) => {
                if (ee.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    panelDragArea.BackColor = SystemColors.ActiveCaption;
                    ee.Effect = DragDropEffects.Copy;
                }
            };
        }

        private void EnableControls()
        {
            btnRandomKeys.Enabled = true;
            btnUpdateClient.Enabled = true;
            cbxManualUpdate.Enabled = true;
            btnGeneralReport.Enabled = true;
            btnUpdateData.Enabled = true;
            //gbxOptions.Controls.Cast<Control>().ForEach(c => c.Enabled = true);
        }

        private void DisableControls()
        {
            btnRandomKeys.Enabled = false;
            btnUpdateClient.Enabled = false;
            btnGeneralReport.Enabled = false;
            cbxManualUpdate.Enabled = false;
            btnGeneralReport.Enabled = false;
            btnUpdateData.Enabled = false;
            //gbxOptions.Controls.Cast<Control>().ForEach(c => c.Enabled = false);
        }

        private void LoadPE(string filePath)
        {
            Log.Info($"Loading PE file {filePath}");
            panelDragArea.BackColor = SystemColors.Control;
            void failed(string message, string title)
            {
                Log.Error(message);
                DisableControls();
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Waiting for user action.";
                tbxKey1.Text = tbxKey2.Text = tbxKey3.Text = "0xFFFF";
            }

            _targetFile = new TargetPE(filePath);
            if (!_targetFile.IsValid)
            {
                failed("Invalid File Path or Extension.", "Invalid File");
                return;
            }

            var result = _targetFile.Initialize(cbxSkipKOValidation.Checked, cbxSkipClientVersion.Checked);
            if (result != null)
            {
                failed("Failed to initialize target executable. Reason:\n" + result.Reason, "Invalid File");
                return;
            }

            Log.Info("File successfully loaded and initialized keys.");
            tbxKey1.Text = tbxKey2.Text = tbxKey3.Text = "0x";
            var samplePatch = _targetFile.CryptoPatches.Find(p => !p.Inlined);
            tbxKey1.Text += samplePatch[0].Key.ToString("X4");
            tbxKey2.Text += samplePatch[1].Key.ToString("X4");
            tbxKey3.Text += samplePatch[2].Key.ToString("X4");

            Log.Info("Caching loaded crypto keys: " + samplePatch.GetKeysFormatted());
            _previousKeys = new short[CryptoPatch.KEYS_COUNT];
            for (int i = 0; i < CryptoPatch.KEYS_COUNT; i++)
                _previousKeys[i] = (short)samplePatch.Keys[i].Key;

            tbxKey2.Enabled = _targetFile.CanUpdateKey2;

            lblStatus.Text = $"[v{_targetFile.ClientVersion}]: Loaded: {filePath}";
            EnableControls();

            // DES encryption won't be supported for batch updating client tbls.
            btnUpdateData.Enabled = _targetFile.CryptoPatches.CryptoType == CryptoType.XOR;
        }

        private void btnGenerateKeys_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(Seed: (int)DateTime.Now.Ticks);
            tbxKey1.Text = $"0x{rnd.Next(200, 35000):X4}";
            if (tbxKey2.Enabled)
                tbxKey2.Text = $"0x{rnd.Next(1000, 31050):X4}";
            tbxKey3.Text = $"0x{rnd.Next(3500, 45000):X4}";
        }

        private void btnPatchClient_Click(object sender, EventArgs e)
        {
            if (cbxCreateBackup.Checked && !_targetFile.CreateBackup())
            {
                var answer = MessageBox.Show("Failed to create backup.\n" +
                                             "Would you like to continue anyway?", "Warning",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.No)
                    return;
            }

            if (Utility.IsFileLocked(_targetFile.FilePath))
            {
                MessageBox.Show("Target file seem to be locked by other processes.\n" +
                                "Make sure to close the following apps and try again:\n" + 
                                Utility.WhoIsFileLocking(_targetFile.FilePath),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_targetFile.Patch(tbxKey1.Key, tbxKey2.Key, tbxKey3.Key))
            {
                MessageBox.Show("Successfully patched new encryption keys.\n" +
                                "Make sure to save the log file generated in the same directory as a reference.\n" +
                                $"Next, click the '{btnUpdateData.Text}' button, so your client can load the tbls correctly.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to patch encryption.\n" +
                                "Please view log file in the same directory and report if necessary.",
                                "Failed to Update Encryption", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxManualUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (!_targetFile.IsValid)
                return;

            tbxKey1.ReadOnly = !cbxManualUpdate.Checked;
            tbxKey2.ReadOnly = !cbxManualUpdate.Checked && _targetFile.CanUpdateKey2;
            tbxKey3.ReadOnly = !cbxManualUpdate.Checked;
        }

        private void btnViewOffsets_Click(object sender, EventArgs e)
        {
            new GeneralReportWindow(_targetFile).ShowDialog(this);
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            string targetRootDir = _targetFile.FileInfo.DirectoryName;
            string targetDataDir = Path.Combine(targetRootDir, "Data");
            string dirPath = String.Empty;
            if (Directory.Exists(targetDataDir))
            {
                var answer = MessageBox.Show($"Found Data directory in:\n{targetDataDir}\n" +
                                             $"Would you like to use it to update your current tables encryption? If not, select different directory.",
                                             "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (answer == DialogResult.Cancel)
                    return;

                if (answer == DialogResult.No)
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                    dialog.InitialDirectory = targetRootDir;
                    dialog.IsFolderPicker = true;
                    if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
                        return;

                    dirPath = dialog.FileName;
                }
                else if (answer == DialogResult.Yes)
                    dirPath = targetDataDir;
            }
            else
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = targetRootDir;
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
                    return;

                dirPath = dialog.FileName;
            }
 
            if (UpdateTablesEncryption(dirPath))
                MessageBox.Show("Successfully update tables to new encryption.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to update tables to the new encryption.\n" +
                                "Your current TBLs encryption mismatched with the firstly loaded executable.\n" +
                                "Please review the log file for more details.",
                                "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private bool UpdateTablesEncryption(string dirPath)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);
            if (cbxCreateBackup.Checked)
            {
                int iBackupCount = 1;
                string destDirTmp;
                do
                {
                    destDirTmp = $"{di.FullName}_bak{iBackupCount++}";
                    if (!Directory.Exists(destDirTmp))
                    {
                        Directory.CreateDirectory(destDirTmp);
                        di.CopyFilesRecursively(new DirectoryInfo(destDirTmp));
                        break;
                    }
                } while (Directory.Exists(destDirTmp));
            }

            // Slander.tbl gets encrypted separately from the rest of the tbls, so let's keep it as is, as we don't need to update it.
            var files = di.GetFiles("*.tbl", SearchOption.TopDirectoryOnly)
                          .Where(f => !f.Name.StartsWith("Slander", StringComparison.OrdinalIgnoreCase));

            foreach (var fi in files)
            {
                byte[] data = File.ReadAllBytes(fi.FullName);
                if (IsTableDecrypted(data))
                    Log.Info($"Table {fi.Name} is not encrypted. Updating to new encryption.");
                else
                {
                    FileSecurity.DecryptXOR(data, _previousKeys[0], _previousKeys[1], _previousKeys[2]);
                    if (!IsTableDecrypted(data)) // verify that we managed to decrypt it.
                    {
                        Log.Error($"Failed to decrypt {fi.Name}.");
                        return false;
                    }
                }

                FileSecurity.EncryptXOR(data, tbxKey1.Key, tbxKey2.Key, tbxKey3.Key);
                File.WriteAllBytes(fi.FullName, data);
            }

            return true;
        }

        private bool IsTableDecrypted(byte[] data)
        {
            int readIndex = 0;
            const int sizeOfDataType = 4;
            int dataTypeCount = BitConverter.ToInt32(data, readIndex);
            if (dataTypeCount < 0 || dataTypeCount * sizeOfDataType > data.Length)
                return false;

            readIndex += 4;
            const int testCount = 5;
            for (int i = 0; i < dataTypeCount; i++, readIndex += 4)
            {
                if (i >= testCount)
                    break;

                int iDataType = BitConverter.ToInt32(data, readIndex);
                // enum DATA_TYPE {DT_NONE, DT_CHAR, DT_BYTE, DT_SHORT, DT_WORD, DT_INT, DT_DWORD, DT_STRING, DT_FLOAT, DT_DOUBLE};
                bool IfWeGetSomeNoneSenseValueReadMustBeEncrypted = iDataType > 50 || iDataType < 0;
                if (IfWeGetSomeNoneSenseValueReadMustBeEncrypted)
                    return false;
            }

            return true;
        }
    }
}
