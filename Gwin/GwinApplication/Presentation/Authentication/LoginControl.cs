using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.GwinApplication.BLL.Authentication;

namespace App.Gwin.Components.Authentication
{
    public partial class LoginControl : UserControl
    {
        /// <summary>
        /// Indicate if authentication is valide
        /// </summary>
        public bool LoginValide { get;  set; }

        public LoginControl()
        {
            InitializeComponent();
            RememberMeCheckBox.Enabled = false;
            




        }

        public IButtonControl GetConnexionButton()
        {
            return bt_Login;
        }

        private void bt_Login_Click(object sender, EventArgs e)
        {

            if (new AuthenticationBLO().Authentication(LoginTextBox.Text, PasswordTextBox.Text))
            {

                this.LoginValide = true;
                this.Parent.Dispose();
               
            }
               
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Login or Password is incorrect");
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            GwinApp.CloseApplication();
            return;
            
        }
    }
}
