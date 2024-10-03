using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace Практика_Зад5
{
    public partial class QrCodeForm : Form
    {
        public QrCodeForm()
        {
            InitializeComponent();
        }

        private void QrCodeForm_Load(object sender, EventArgs e)
        {
            // Получение текста для QR-кода из текстового поля
            string qrCodeText = "https://docs.google.com/forms/d/e/1FAIpQLScL0R9jXoT0bUYbKHMFg8CANcwHf5CbmQbohBJWQmMe9i7MHg/viewform?usp=sf_link";

            // Создание объекта QR-кода
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);

            // Создание объекта для отрисовки QR-кода
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5); // Размер QR-кода (в пикселях)

            // Отображение QR-кода в PictureBox
            pictureBox1.Image = qrCodeImage;

            // Сохранение QR-кода в файл (необязательно)
            // SaveQRCodeToFile(qrCodeImage, "qrcode.png");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        // Метод для сохранения QR-кода в файл
        private void SaveQRCodeToFile(Bitmap qrCodeImage, string fileName)
        {
            qrCodeImage.Save(fileName, ImageFormat.Png);
        }
    }
}
