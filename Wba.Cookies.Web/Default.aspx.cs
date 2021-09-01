using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wba.Cookies.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                string naam = ReadCookie("persoonlijk", "naam");
                string geboortedatum = ReadCookie("persoonlijk", "geboortedatum");
                string taalcode = ReadCookie("persoonlijk", "taalcode");

                txtNaam.Text = naam;
                txtGeboortedatum.Text = geboortedatum;
                if (taalcode == "")
                    cmbTaal.SelectedIndex = 0;
                else
                    cmbTaal.SelectedValue = taalcode;
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            string naam = txtNaam.Text.Trim();
            string geboortedatum = txtGeboortedatum.Text;
            string taalcode = cmbTaal.SelectedValue;
            SaveCookie("persoonlijk", "naam", naam, 365);
            SaveCookie("persoonlijk", "geboortedatum", geboortedatum, 365);
            SaveCookie("persoonlijk", "taalcode", taalcode, 365);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            DeleteCookie("persoonlijk");
        }

        void SaveCookie(string cookieName, string key, string value, int ttl)
        {
            value = EncryptString(value, "P@$w00rd");
            HttpCookie cookie = Request.Cookies.Get(cookieName);
            if(cookie == null)
                cookie = new HttpCookie(cookieName);
            if (cookie[key] == null)
                cookie.Values.Add(key, value);
            else
                cookie[key] = value;
            cookie.Expires =DateTime.Now.AddDays(ttl);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        string ReadCookie(string cookieName, string key)
        {
            HttpCookie cookie = Request.Cookies.Get(cookieName);
            if(cookie == null)
            {
                return "";
            }
            else
            {
                if (cookie[key] == null)
                {
                    return "";
                }
                else
                {
                    return DecryptString(cookie[key].ToString(),"P@$w00rd");
                }
            }
        }
        void DeleteCookie(string cookieName)
        {
            HttpCookie cookie = Request.Cookies.Get(cookieName);
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        string EncryptString(string InputText, string Password)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
            byte[] Salt = System.Text.Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;
        }
        string DecryptString(string InputText, string Password)
        {
            try
            {
                RijndaelManaged RijndaelCipher = new RijndaelManaged();
                byte[] EncryptedData = Convert.FromBase64String(InputText);
                byte[] Salt = System.Text.Encoding.ASCII.GetBytes(Password.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(EncryptedData);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                string DecryptedData = System.Text.Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
                return DecryptedData;
            }
            catch (Exception fout)
            {
                return InputText;
            }
        }
    }
}