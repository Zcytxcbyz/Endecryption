using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;
using AESalgorithm;
using Base64code;
using DESalgorithm;
using MD5algorithm;
using RSAalgorithm;

namespace endecryption
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string cfgfilename = "Cofig.cfg";
        public bool isAESexists = File.Exists("AESalgorithm.dll");
        public bool isDESexists = File.Exists("DESalgorithm.dll");
        public bool isRSAexists = File.Exists("RSAalgorithm.dll");
        public bool isMD5exists = File.Exists("MD5algorithm.dll");
        public bool isBASEexists = File.Exists("Base64code.dll");
        public double NormalHeigth, NormalWidth;
        public bool Maximized;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AESencrypt_Click(object sender, RoutedEventArgs e)
        {
            setrichtextboxtext(AES.AESEncrypt(getrichtextboxtext(AESText1), AESkey.Text), AESText2);
        }

        private void AESdecrypt_Click(object sender, RoutedEventArgs e)
        {
            setrichtextboxtext(AES.AESDecrypt(getrichtextboxtext(AESText2), AESkey.Text), AESText1);
        }

        private void MainWindows_Loaded(object sender, RoutedEventArgs e)
        {
            MenuAES.IsChecked = true;           
            if (isAESexists){
                AESalg.Visibility = Visibility.Visible;
                MenuAES.Visibility = Visibility.Visible;
            }else{
                AESalg.Visibility = Visibility.Collapsed;
                MenuAES.Visibility = Visibility.Collapsed;
                algfocus();
            }
            if (isDESexists) { 
                DESalg.Visibility = Visibility.Visible;
                MenuDES.Visibility = Visibility.Visible;
            }else { 
                DESalg.Visibility = Visibility.Collapsed;
                MenuDES.Visibility = Visibility.Collapsed;
                algfocus();
            }
            if (isRSAexists) { 
                RSAalg.Visibility = Visibility.Visible;
                MenuRSA.Visibility = Visibility.Visible;
            }else { 
                RSAalg.Visibility = Visibility.Collapsed;
                MenuRSA.Visibility = Visibility.Collapsed;
                algfocus();
            }
            if (isMD5exists) { 
                MD5alg.Visibility = Visibility.Visible;
                MenuMD5.Visibility = Visibility.Visible;
            }else { 
                MD5alg.Visibility = Visibility.Collapsed;
                MenuMD5.Visibility = Visibility.Collapsed;
                algfocus();
            }
            if (isBASEexists)
            {
                BASEalg.Visibility = Visibility.Visible;
                MenuBASE.Visibility = Visibility.Visible;
            }
            else
            {
                BASEalg.Visibility = Visibility.Collapsed;
                MenuBASE.Visibility = Visibility.Collapsed;
                algfocus();
                
            }
            if (!isAESexists && !isDESexists && !isRSAexists & !isMD5exists && !isBASEexists)
            {
                MainTabControl.Visibility = Visibility.Collapsed;
                MenuAlg.Visibility = Visibility.Collapsed;
            }
            if (File.Exists(cfgfilename))
            {
                string isMaximized = cfgRead("Maximized");
                if (isMaximized == "True" || isMaximized == "true")
                {
                    this.WindowState = WindowState.Maximized;
                    Maximized = true;
                }
                else
                {
                    Maximized = false;
                }
                NormalHeigth = Convert.ToDouble(cfgRead("Height"));
                NormalWidth = Convert.ToDouble(cfgRead("Width"));
                this.Height = Convert.ToDouble(cfgRead("Height"));
                this.Width = Convert.ToDouble(cfgRead("Width"));
                this.Title = cfgRead("Title");
            }
            else
            {
                cfgCreate();
            }        
        }
        private void algfocus()
        { 
            if (isAESexists)
            {
                AESalg.Focus();
            }
            else if (isDESexists)
            {
                DESalg.Focus();
            }
            else if (isRSAexists)
            {
                RSAalg.Focus();
            }
            else if (isMD5exists)
            {
                MD5alg.Focus();
            }
            else if (isBASEexists)
            {
                BASEalg.Focus();
            }
        }
        private static string getrichtextboxtext(RichTextBox richtextbox)
        {
            return new TextRange(richtextbox.Document.ContentStart, richtextbox.Document.ContentEnd).Text;
        }
        private static void setrichtextboxtext(string text, RichTextBox richtextbox)
        {
            richtextbox.Document.Blocks.Clear();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Run(text));
            richtextbox.Document.Blocks.Add(p);
        }

        private void DESencrypt_Click(object sender, RoutedEventArgs e)
        {
            setrichtextboxtext(DES.Encrypt(getrichtextboxtext(DESText1), DESkey.Text), DESText2);
        }

        private void DESdecrypt_Click(object sender, RoutedEventArgs e)
        {
            setrichtextboxtext(DES.Decrypt(getrichtextboxtext(DESText2), DESkey.Text), DESText1);
        }

        private void Generatekey_Click(object sender, RoutedEventArgs e)
        {
            RSA rsa = new RSA();
            string publickey, privatekey;
            rsa.RSAKey(out privatekey, out publickey);
            setrichtextboxtext(privatekey, RSAprivatekey);
            setrichtextboxtext(publickey, RSApublickey);
        }

        private void RSAencrypt_Click(object sender, RoutedEventArgs e)
        {
            RSA rsa = new RSA();
            string publickey, privatekey;
            rsa.RSAKey(out privatekey, out publickey);
            setrichtextboxtext(privatekey, RSAprivatekey);
            setrichtextboxtext(publickey, RSApublickey);
            setrichtextboxtext(rsa.RSAEncrypt(getrichtextboxtext(RSApublickey), getrichtextboxtext(RSAText1)), RSAText2);
        }

        private void RSAdecrypt_Click(object sender, RoutedEventArgs e)
        {
            RSA rsa = new RSA();
            setrichtextboxtext(rsa.RSADecrypt(getrichtextboxtext(RSAprivatekey), getrichtextboxtext(RSAText2)), RSAText1);
        }

        private void MD5encrypt_Click(object sender, RoutedEventArgs e)
        {
            setrichtextboxtext(MD5Crypto.MD5Encrypt(getrichtextboxtext(MD5Text1)), MD5Text2);
        }

        private void BASEencrypt_Click(object sender, RoutedEventArgs e)
        {
            setrichtextboxtext((Base64.ToBase64String(getrichtextboxtext(BASEText1))),BASEText2);
        }

        private void BASEdecrypt_Click(object sender, RoutedEventArgs e)
        {
            setrichtextboxtext((Base64.UnBase64String(getrichtextboxtext(BASEText2))), BASEText1);
        }

        private void AESalg_Click(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuAES.IsChecked = true;
            AESalg.Focus();
        }

        private void DESalg_Click(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuDES.IsChecked = true;
            DESalg.Focus();
        }

        private void RSAalg_Click(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuRSA.IsChecked = true;
            RSAalg.Focus();
        }

        private void MD5alg_Click(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuMD5.IsChecked = true;
            MD5alg.Focus();
        }

        private void BASEalg_Click(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuBASE.IsChecked = true;
            BASEalg.Focus();
        }

        private void MenuCut_Click(object sender, RoutedEventArgs e)
        {
            MenuCut.Command = System.Windows.Input.ApplicationCommands.Cut;
        }

        private void MenuCopy_Click(object sender, RoutedEventArgs e)
        {
            MenuCopy.Command = System.Windows.Input.ApplicationCommands.Copy;
        }

        private void MenuPaste_Click(object sender, RoutedEventArgs e)
        {
            MenuPaste.Command = System.Windows.Input.ApplicationCommands.Paste;
        }

        private void MenuSelectall_Click(object sender, RoutedEventArgs e)
        {
            MenuSelectall.Command = System.Windows.Input.ApplicationCommands.SelectAll;
        }

        private void MenuHelp_Click(object sender, RoutedEventArgs e)
        {
            MenuHelp.Command = System.Windows.Input.ApplicationCommands.Help;
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutBox().Show();
        }
        private void UnCheckall()
        {
            MenuAES.IsChecked = false;
            MenuDES.IsChecked = false;
            MenuRSA.IsChecked = false;
            MenuMD5.IsChecked = false;
            MenuBASE.IsChecked = false;
        }

        private void AESalg_GotFocus(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuAES.IsChecked = true;
        }

        private void DESalg_GotFocus(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuDES.IsChecked = true;
        }

        private void RSAalg_GotFocus(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuRSA.IsChecked = true;
        }

        private void MD5alg_GotFocus(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuMD5.IsChecked = true;
        }

        private void BASEalg_GotFocus(object sender, RoutedEventArgs e)
        {
            UnCheckall();
            MenuBASE.IsChecked = true;
        }


        private void MenuSaveall_Click(object sender, RoutedEventArgs e)
        {
            SaveCipher(sender, e);
            SaveClear(sender, e);
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void OpenCipher(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "打开密文";
            ofd.DefaultExt = ".txt";
            ofd.Filter = "文本文件|*.txt";
            if (ofd.ShowDialog() == true)
            {
                setrichtextboxtext(ReadFile(ofd.FileName), AESText2);
                setrichtextboxtext(ReadFile(ofd.FileName), DESText2);
                setrichtextboxtext(ReadFile(ofd.FileName), RSAText2);
                setrichtextboxtext(ReadFile(ofd.FileName), MD5Text2);
                setrichtextboxtext(ReadFile(ofd.FileName), BASEText2); 
            }
        }

        private void SaveCipher(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Title = "保存密文";
            sfd.DefaultExt = ".txt";
            sfd.Filter = "文本文件|*.txt";
            if (sfd.ShowDialog() == true)
            {
                CreateFile(sfd.FileName.Replace(".txt", "") + "_AES.txt", getrichtextboxtext(AESText2));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_DES.txt", getrichtextboxtext(DESText2));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_RSA.txt", getrichtextboxtext(RSAText2));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_MD5.txt", getrichtextboxtext(MD5Text2));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_Base64.txt", getrichtextboxtext(BASEText2));
            }
        }

        private void OpenClear(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "打开明文";
            ofd.DefaultExt = ".txt";
            ofd.Filter = "文本文件|*.txt";
            if (ofd.ShowDialog() == true)
            {
                setrichtextboxtext(ReadFile(ofd.FileName), AESText1);
                setrichtextboxtext(ReadFile(ofd.FileName), DESText1);
                setrichtextboxtext(ReadFile(ofd.FileName), RSAText1);
                setrichtextboxtext(ReadFile(ofd.FileName), MD5Text1);
                setrichtextboxtext(ReadFile(ofd.FileName), BASEText1); 
            }
        }

        private void SaveClear(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Title = "保存明文";
            sfd.DefaultExt = ".txt";
            sfd.Filter = "文本文件|*.txt";
            if (sfd.ShowDialog() == true)
            {
                CreateFile(sfd.FileName.Replace(".txt", "") + "_AES.txt", getrichtextboxtext(AESText1));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_DES.txt", getrichtextboxtext(DESText1));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_RSA.txt", getrichtextboxtext(RSAText1));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_MD5.txt", getrichtextboxtext(MD5Text1));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_Base64.txt", getrichtextboxtext(BASEText1));
            }
        }
        public void CreateFile(string path,string str)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(str);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public void WriteFile(string path, string str)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public string ReadFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return str;
        }

        private void MenuSaveKey_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Title = "保存密钥";
            sfd.DefaultExt = ".txt";
            sfd.Filter = "文本文件|*.txt";
            if (sfd.ShowDialog() == true)
            {
                CreateFile(sfd.FileName.Replace(".txt", "") + "_AES.txt", AESkey.Text);
                CreateFile(sfd.FileName.Replace(".txt", "") + "_DES.txt", DESkey.Text);
                CreateFile(sfd.FileName.Replace(".txt", "") + "_RSA_publickey.txt", getrichtextboxtext(RSApublickey));
                CreateFile(sfd.FileName.Replace(".txt", "") + "_RSA_privatekey.txt", getrichtextboxtext(RSAprivatekey));
            }
        }
        public void cfgCreate()
        {
            WriteFile(cfgfilename, "Title=" + this.Title);
            WriteFile(cfgfilename, "Height=" + this.Height);
            WriteFile(cfgfilename, "Width=" + this.Width);
            WriteFile(cfgfilename, "Maximized=" + "False");
        }
        public void cfgWrite(string name, string value)
        {
            Regex rgx = new Regex("(?<=" + name + "=).*");
            string str = rgx.Replace(ReadFile(cfgfilename), value);
            CreateFile(cfgfilename, str.Replace("\n","\r\n"));
        }
        public string cfgRead(string name)
        {
            Match mc = Regex.Match(ReadFile(cfgfilename), "(?<=" + name + "=).*");
            mc.ToString();
            return mc.ToString().Replace("\r","");
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                cfgWrite("Height", this.Height.ToString());
                cfgWrite("Width", this.Width.ToString());
                cfgWrite("Maximized", "False");
            }
            else
            {
                cfgWrite("Height", NormalHeigth.ToString());
                cfgWrite("Width", NormalWidth.ToString());
                cfgWrite("Maximized", "True");
            }
            cfgWrite("Title", this.Title);
            Environment.Exit(0);
        }

        private void MainForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                NormalHeigth = this.Height;
                NormalWidth = this.Width;
            }
            else if(this.WindowState==WindowState.Maximized)
            {
                Maximized = true;
            }
        }
    }   
}
