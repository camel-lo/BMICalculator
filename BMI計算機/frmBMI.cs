using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMI計算機
{
    public partial class frmBMI : Form
    {
        public frmBMI()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // 顯示結果

            bool isHeightValid = double.TryParse(txtHeight.Text, out double height);
            bool isWeightValid = double.TryParse(txtWeight.Text, out double weight);
            // 驗證身高輸入
            if (isHeightValid)
            {
                if (height <= 0)
                {
                    MessageBox.Show("身高必須大於零。", "身高值錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的身高數值。", "身高值錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 驗證體重輸入
            if (isWeightValid)
            {
                if (weight <= 0)
                {
                    MessageBox.Show("體重必須大於零。", "體重值錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的體重數值。", "體重值錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 將身高從公分轉換為公尺
            height = height / 100;
            // 計算BMI
            double bmi = weight / (height * height);

            string[] strResultList = { "體重過輕", "健康體位", "體位過重", "輕度肥胖","中度肥胖", "重度肥胖" };
            Color[] colorList = { Color.FromArgb(100, 160, 220),  // 體重過輕：柔和的寧靜藍
                                  Color.FromArgb(85, 190, 140),   // 健康體位：清新的薄荷綠
                                  Color.FromArgb(240, 170, 80),   // 體位過重：溫暖的奶油橘
                                  Color.FromArgb(240, 130, 100),  // 輕度肥胖：柔和的珊瑚粉
                                  Color.FromArgb(220, 90, 90),    // 中度肥胖：不刺眼的磚紅色
                                  Color.FromArgb(160, 110, 190) };

            string strResult = "";
            Color colorResult = Color.Black;
            int resultIndex = 0;
            if (bmi < 18.5)
            {
                resultIndex = 0;
            }
            else if (bmi < 25)
            {
                resultIndex = 1;
            }
            else if (bmi < 30)
            {
                resultIndex = 2;
            }
            else if (bmi < 35)
            {
                resultIndex = 3;
            }
            else if (bmi < 40)
            {
                resultIndex = 4;
            }
            else
            {
                resultIndex = 5;
            }
            strResult = strResultList[resultIndex];
            colorResult = colorList[resultIndex];
            lblResult.Text = $"{bmi:F2} ({strResult})";
            lblResult.BackColor = colorResult;

            switch (resultIndex)
            {
                case 0:
                    picResult.Image = Properties.Resources.img_level0;
                    break;
                case 1:
                    picResult.Image = Properties.Resources.img_level1;
                    break;
                case 2:
                    picResult.Image = Properties.Resources.img_level2;
                    break;
                case 3:
                    picResult.Image = Properties.Resources.img_level3;
                    break;
                case 4:
                    picResult.Image = Properties.Resources.img_level4;
                    break;
                case 5:
                    picResult.Image = Properties.Resources.img_level5;
                    break;
            }
        }

        private void txtHeight_Enter(object sender, EventArgs e)
        {
            if (txtHeight.Text == "請輸入身高 Ex:180")
            {
                txtHeight.Text = "";
                txtHeight.ForeColor = Color.Black;
            }

            else
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    txtHeight.SelectAll();
                });
            }
        }

        private void txtHeight_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHeight.Text))
            {
                txtHeight.Text = "請輸入身高 Ex:180";
                txtHeight.ForeColor = Color.Gray;
            }
        }

        private void txtWeight_Enter(object sender, EventArgs e)
        {
            if (txtWeight.Text == "請輸入體重 Ex:60")
            {
                txtWeight.Text = "";
                txtWeight.ForeColor = Color.Black;
            }
            else
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    txtWeight.SelectAll();
                });
            }
        }

        private void txtWeight_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                txtWeight.Text = "請輸入體重 Ex:60";
                txtWeight.ForeColor = Color.Gray;
            }
        }

        private void grpInput_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = (GroupBox)sender;
            Graphics g = e.Graphics;

            Color borderColor = Color.Gray;
            int borderThickness = 2;

            using (Pen pen = new Pen(borderColor, borderThickness))
            {
                int textHeight = box.Font.Height;
                Rectangle rect = new Rectangle(1, textHeight / 2, box.Width - 2, box.Height - (textHeight / 2) - 2);
                g.DrawRectangle(pen, rect);
            }

            SizeF stringSize = g.MeasureString(box.Text, box.Font);
            Rectangle textRect = new Rectangle(8, 0, (int)stringSize.Width + 4, (int)stringSize.Height);

            g.FillRectangle(new SolidBrush(box.BackColor), textRect);
            g.DrawString(box.Text, box.Font, new SolidBrush(box.ForeColor), textRect.Location);
        }
    }
}
