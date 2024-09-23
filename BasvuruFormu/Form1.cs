using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasvuruFormu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void HataEkle(Control control, string hataMesaji)
        {
            try
            {
                errorProvider1.SetError(control, hataMesaji);
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                timer1.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdSoyad.Text.Length == 0)
                {
                    HataEkle(txtAdSoyad, "lutfen ad soyad giriniz");
                }
                else if (cmbSehir.SelectedIndex < 0)
                {
                    HataEkle(cmbSehir, "sehir seçiniz");
                }
                else if (txtTcNo.Text.StartsWith("0"))
                {
                    errorProvider1.SetError(txtTcNo, "tc no sıfır ile başlamaz");
                    timer1.Enabled = true;
                }
                else if (Convert.ToInt32(txtTcNo.Text.Substring(txtTcNo.Text.Length - 1)) % 2 != 0)
                {
                    HataEkle(txtTcNo, "son hane çift olmalı");
                }
                else if (!rdbBay.Checked && !rdbBayan.Checked)
                {
                    HataEkle(rdbBayan, "cinsiyet seçiniz");
                }
                else if (lblYas.Text.Length == 0)
                {
                    HataEkle(dateTimePicker1, "dogum tarihi giriniz");
                }
                else
                {
                    string ehliyetler = "";
                    foreach (Control item in groupBox1.Controls)
                    {
                        if (item is CheckBox && ((CheckBox)item).Checked)
                        {
                            ehliyetler += item.Text;
                        }
                    }

                    if (ehliyetler.Length > 2)
                    {
                        ehliyetler = ehliyetler.Substring(0, ehliyetler.Length - 2);
                    }

                    MessageBox.Show(String.Format("ad Soyad:{0}"+Environment.NewLine+"Sehir{1}"+Environment.NewLine+"TC KimlikNo:{2}"+Environment.NewLine+"cinsiyet:{3}"+Environment.NewLine+"Ehliyet:{4}"+Environment.NewLine+"Yas:{5}"+Environment.NewLine+"Cocuk SAyisi{6}"+Environment.NewLine+"Web Adresi{7}"+Environment.NewLine,txtAdSoyad.Text,cmbSehir.SelectedItem.ToString(),txtTcNo.Text,rdbBay.Checked?"Bay":"Bayan",ehliyetler,lblYas.Text,numericUpDown1.Value,txtWebAdres.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblYas.Text = Convert.ToInt32(Math.Floor(DateTime.Now.Subtract(dateTimePicker1.Value).TotalDays / 365.25)).ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public bool UrlIsValid(string url)
        {
            bool br = false;
            try
            {
                IPHostEntry ipHost = Dns.Resolve(url);
                br = true;
            }
            catch (Exception)
            {

                br = false;
            }
            return br;
        }

        private void txtWebAdres_Leave(object sender, EventArgs e)
        {
            try
            {
                if (UrlIsValid(txtWebAdres.Text))
                {
                    webBrowser1.Navigate(txtWebAdres.Text);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
