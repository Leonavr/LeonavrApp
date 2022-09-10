using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LeonavrApp
{
    public partial class UpdateForm : Form
    {
        public string login;
        public UpdateForm()
        {
            InitializeComponent();
            UserEmailField.Text = "Введите email";
            UserPasswordField.Text = "Введите пароль";

        }
        public void TextBox_Enter(object sender, EventArgs eventArgs)
        {
            if (((TextBox)sender).Name == "UserEmailField" && UserEmailField.Text.Trim() == "Введите email")
            {
                UserEmailField.Text = "";
                UserEmailField.ForeColor = Color.White;
            }

            if (((TextBox)sender).Name == "UserPasswordField" && UserPasswordField.Text.Trim() == "Введите пароль")
            {
                UserPasswordField.Text = "";
                UserPasswordField.UseSystemPasswordChar = true;
                UserPasswordField.ForeColor = Color.White;
            }
        }
        public void TextBox_Leave(object sender, EventArgs eventArgs)
        {
            if (((TextBox)sender).Name == "UserEmailField" && UserEmailField.Text.Trim() == "")
            {
                UserEmailField.Text = "Введите email";
                UserEmailField.ForeColor = Color.Gray;

            }

            if (((TextBox)sender).Name == "UserPasswordField" && UserPasswordField.Text.Trim() == "")
            {
                UserPasswordField.Text = "Введите пароль";
                UserPasswordField.UseSystemPasswordChar = false;
                UserPasswordField.ForeColor = Color.Gray;


            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (UserEmailField.Text.Trim() == "" || UserEmailField.Text.Trim() == "Введите email")
            {
                MessageBox.Show("Вы не ввели email");
                return;
            }
            if (UserPasswordField.Text.Trim() == "" || UserPasswordField.Text.Trim() == "Введите пароль")
            {
                MessageBox.Show("Вы не ввели пароль");
                return;
            }


            DB db = new DB();
            MySqlCommand command = new MySqlCommand(
                "UPDATE users SET email = @email, password = @password WHERE login = @login",
                db.GetConnection()
                );
            command.Parameters.AddWithValue("login", this.login);
            command.Parameters.AddWithValue("email", UserEmailField.Text);
            command.Parameters.AddWithValue("password", Hash(UserPasswordField.Text));

            db.OpenConnection();
 
            if (command.ExecuteNonQuery() == 1) MessageBox.Show("Готово!");
            else MessageBox.Show("Ошибка");
            
            db.CloseConnection();
        }
        private string Hash(string input)
        {
            byte[] temp = Encoding.UTF8.GetBytes(input);
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(temp);
                return Convert.ToBase64String(hash);
            }
        }
        public string LabelText
        {
            get
            {
                return label2.Text;
            }
            set
            {
                label2.Text = value;
            }
        }
        private void QuitBtn(object sender, EventArgs e)
        {
            this.Close();
        }
        private void customInstaller1_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }

    }
}
