using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace RSANEW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string veri = textBox1.Text;
            byte[] toEncryptData = Encoding.ASCII.GetBytes(veri);
            //Generate keys
            RSACryptoServiceProvider rsaGenKeys = new RSACryptoServiceProvider();
            string privateXml = rsaGenKeys.ToXmlString(true);
            textBox3.Text = privateXml;
            string publicXml = rsaGenKeys.ToXmlString(false);
            textBox4.Text = publicXml;

            //Encode with public key
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
            rsaPublic.FromXmlString(textBox4.Text);
            byte[] encryptedRSA = rsaPublic.Encrypt(toEncryptData, false);
            string EncryptedResult = Encoding.Default.GetString(encryptedRSA);
            textBox2.Text = EncryptedResult;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Decode with private key
            var rsaPrivate = new RSACryptoServiceProvider();
            rsaPrivate.FromXmlString(textBox3.Text);
            byte[] decryptedRSA = rsaPrivate.Decrypt(Encoding.Default.GetBytes(textBox2.Text), false);
            string originalResult = Encoding.Default.GetString(decryptedRSA);
            textBox5.Text = originalResult;
        }
    }
}
