using IMAPI2.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using SparkBurnApplication.Common;
using System.Drawing;

namespace SparkBurnApplication
{
    public partial class FlashBurnForm : Form
    {
        private Image loadingGif;
        private string gifPath;

        public FlashBurnForm()
        {
            InitializeComponent();
            GetRemovableDisks();
            GetSupporters();
            progressBarBurn.Visible = false;
            labelProgress.Text = ConstVariable.BURN_IDLE;
            gifPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "SparkBurnApplication", "Assets", ConstVariable.APP_LOADING_GIF);

            if (File.Exists(gifPath))
            {
                loadingGif = Image.FromFile(gifPath);
                pictureBoxLoading.Image = loadingGif;
                pictureBoxLoading.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxLoading.Visible = false;
            }

        }


        #region Click Event



        /// <summary>
        /// Event click de tai lai danh sach o dia cho dropbox "buttonResetPartitions"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonResetPartitions_Click(object sender, EventArgs e)
        {
            GetRemovableDisks();
        }


        /// <summary>
        /// Event click In dia dua tren danh sach file duoc khai bao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBurn_Click(object sender, EventArgs e)
        {
            List<string> missingFiles = new List<string>();

            // Hien gif loading
            pictureBoxLoading.Visible = false;

            if (listBoxFiles.Items.Count == 0)
            {
                MessageBox.Show("Không có file được chọn để in.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxDrives.SelectedItem == null || string.IsNullOrWhiteSpace(comboBoxDrives.SelectedItem.ToString()))
            {
                MessageBox.Show("Vui lòng chọn ổ đĩa chứa đĩa để in.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> filePaths = new List<string>();
            foreach (var item in listBoxFiles.Items)
            {
                filePaths.Add(item.ToString());
            }

            string selectedDrive = comboBoxDrives.SelectedItem.ToString();
            string removableDisc = Path.GetPathRoot(selectedDrive).TrimEnd('\\').Replace(":", "");

            try
            {
                labelProgress.Text = ConstVariable.BURN_CHECKING;

                // Dat lai gia tri thanh tien trinh ve 0
                progressBarBurn.Value = 0;
                progressBarBurn.Visible = true;

                // Check xem nhung file duoc input co ton tai hay khong
                foreach (var filePath in filePaths)
                { 
                    try
                    {
                        if (!File.Exists(filePath))
                        {
                            missingFiles.Add(filePath);
                        }
                    }
                    catch
                    {
                        missingFiles.Add(filePath);
                    }
                    
                }
                if (missingFiles.Count > 0)
                {
                    string missingFilesMessage = string.Join(Environment.NewLine, missingFiles);
                    MessageBox.Show($"Anh/chị vui lòng thử lại, phần mềm không tìm thấy (những) file sau:\n{missingFilesMessage}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tim DeviceID
                MsftDiscMaster2 discMaster = new MsftDiscMaster2();
                if (!discMaster.IsSupportedEnvironment)
                {
                    MessageBox.Show("Lỗi: IMAPI2 không tương thích với hệ thống.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check co o dia duoc chon khong
                if (discMaster.Count == 0)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy ổ đĩa.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tim DeviceID cho o dia duoc chon
                string deviceId = string.Empty;
                foreach (string recorderId in discMaster)
                {
                    MsftDiscRecorder2 discRecorder = new MsftDiscRecorder2();
                    discRecorder.InitializeDiscRecorder(recorderId);

                    string driveLetter = MediaItem.MediaHelper.GetDriveLetterForRecorder(discRecorder);
                    if (driveLetter.Equals(removableDisc, StringComparison.OrdinalIgnoreCase))
                    {
                        deviceId = recorderId;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(deviceId))
                {
                    MessageBox.Show($"Không tìm thấy ổ đĩa '{removableDisc}'.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Khoi tao disc recorder cho DeviceID tuong ung
                MsftDiscRecorder2 selectedDiscRecorder = new MsftDiscRecorder2();
                selectedDiscRecorder.InitializeDiscRecorder(deviceId);

                // Khoi tao burner in dia
                MsftDiscFormat2Data discFormatter = new MsftDiscFormat2Data
                {
                    Recorder = selectedDiscRecorder,
                    ClientName = "Disc Burner Application"
                };

                // Set thanh tien trinh "progressBarBurn"
                discFormatter.Update += DiscFormatter_Update;
                labelProgress.Text = ConstVariable.BURN_START;

                // Check dia blank
                if (!discFormatter.MediaHeuristicallyBlank)
                {
                    MessageBox.Show("Chỉ có thể ghi dữ liệu vào đĩa chưa qua sử dụng. Đã hủy tiến trình.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                    //// Cho phep User format dia neu dia khong blank, tuy nhien CD-ROM chi cho phep doc (write only) nen bo qua
                    //bool isRemovableDisc = IsRemovableDisc(deviceId);
                    //DialogResult result = MessageBox.Show(
                    //       "Đĩa chứa dữ liệu, anh/chị có muốn Format lại?",
                    //       "Đồng ý Format",
                    //       MessageBoxButtons.YesNo,
                    //       MessageBoxIcon.Question
                    //   );

                    //if (result == DialogResult.Yes)
                    //{
                    //    // Xoa dia
                    //    EraseDisc(deviceId);
                    //    MessageBox.Show("Đĩa được Format thành công, quá trình in đã được tiếp tục!");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Đã hủy tiến trình.");
                    //    return;
                    //}
                }

                MsftFileSystemImage fileSystemImage = new MsftFileSystemImage();
                fileSystemImage.ChooseImageDefaults(selectedDiscRecorder);

                // Add file vao list in
                IFsiDirectoryItem rootItem = fileSystemImage.Root;
                foreach (var filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        IStream fileStream = FileToIStream(filePath);
                        rootItem.AddFile(Path.GetFileName(filePath), fileStream);
                    }
                }

                // Tao result Stream
                IStream resultImage = fileSystemImage.CreateResultImage().ImageStream;

                // In dia
                discFormatter.Write(resultImage);
                MessageBox.Show("In đĩa thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelProgress.Text = ConstVariable.BURN_SUCCESS;
            }
            catch (COMException comEx)
            {
                labelProgress.Text = ConstVariable.BURN_FAILED;
                MessageBox.Show($"Vui lòng liên lạc người hỗ trợ. Đã xảy ra lỗi sau:\nCOMException{comEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                labelProgress.Text = ConstVariable.BURN_FAILED;
                MessageBox.Show($"Vui lòng liên lạc người hỗ trợ. Đã xảy ra lỗi sau:\nException{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBarBurn.Visible = false;
                pictureBoxLoading.Visible = false;
            }

            return;
        }


        /// <summary>
        /// Event click Nhap serial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInputSerial_Click(object sender, EventArgs e)
        {
            pictureBoxLoading.Visible = !pictureBoxLoading.Visible;
        }



        #endregion



        #region Ham xu ly du lieu chinh



        //



        #endregion



        #region Ham phu tro



        /// <summary>
        /// Tao ra IStream tu path duoc khai bao
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>IStream cho file thuoc path duoc khai bao</returns>
        private IStream FileToIStream(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new Interop.HelperInterop.ManagedIStream(fs);
        }


        /// <summary>
        /// Task update thanh tien trinh "progressBarBurn" va label "labelProgress"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="progress"></param>
        private void DiscFormatter_Update(object sender, object progress)
        {
            if (progress is IDiscFormat2DataEventArgs progressData)
            {
                int percentComplete = (int)((progressData.ElapsedTime * 100) / progressData.TotalTime);

                Invoke(new Action(() =>
                {
                    progressBarBurn.Value = Math.Min(percentComplete, 100);

                    switch (percentComplete)
                    {
                        case 95:
                            labelProgress.Text = ConstVariable.BURN_ENDING;
                            break;
                        case 1:
                            labelProgress.Text = ConstVariable.BURN_PROGRESSING;
                            break;
                    }

                }));
            }
        }



        #endregion



        #region Khoi tao



        /// <summary>
        /// Lay danh sach o dia hop le va gan vao DataSource cua "comboBoxDrives"
        /// </summary>
        private void GetRemovableDisks()
        {
            try
            {
                // Lay ra list o dia tren thiet bi
                var drives = DriveInfo.GetDrives();

                // Lay ra o dia bat dau bang "CD" hoac duoc danh gia la Removable
                var removableDrives = drives
                    .Where(d => d.DriveType == DriveType.Removable || d.DriveType.ToString().StartsWith("CD"))
                    .Select(d => d.Name)
                    .ToList();

                comboBoxDrives.DataSource = removableDrives;

                if (removableDrives.Count > 0)
                {
                    comboBoxDrives.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy ổ đĩa phù hợp trên máy, vui lòng thử lại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vui lòng liên lạc người hỗ trợ. Đã xảy ra lỗi sau:\nException{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// Lay thong tin nguoi ho tro
        /// </summary>
        private void GetSupporters()
        {
            string supporter = ConstVariable.APP_SUPPORTER;
            try
            {
                string sqlQuery = $@"";


            }
            catch (Exception)
            {

            }

            labelSupporter.Text = supporter;
        }



        #endregion





        #region Backlog (No le cua thoi gian)



        /// <summary>
        /// Format lai dia trong phan vung duoc chon
        /// </summary>
        /// <param name="deviceId">Id thiet bi (O dia)</param>
        private void EraseDisc(string deviceId)
        {
            try
            {
                MsftDiscRecorder2 discRecorder = new MsftDiscRecorder2();
                discRecorder.InitializeDiscRecorder(deviceId);

                MsftDiscFormat2Erase discEraser = new MsftDiscFormat2Erase
                {
                    Recorder = discRecorder,
                    ClientName = "Disc Burner Application",
                    FullErase = true
                };

                discEraser.EraseMedia();
                MessageBox.Show("Disc erased successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while erasing the disc: {ex.Message}");
            }
        }


        /// <summary>
        /// Check xem o dia co phai RemovableDisc hay khong
        /// </summary>
        /// <param name="deviceId">Id thiet bi (O dia)</param>
        /// <returns>Bool check True (La o dia roi) hoac False (Khong phai o dia roi)</returns>
        private bool IsRemovableDisc(string deviceId)
        {
            // You can use the Windows Management Instrumentation (WMI) to check if the device is removable
            var driveInfo = new DriveInfo(deviceId);
            return driveInfo.DriveType == DriveType.Removable;
        }



        #endregion





    }
}
