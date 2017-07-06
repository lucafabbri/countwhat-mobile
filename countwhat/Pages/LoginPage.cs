using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;


namespace countwhat.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected async System.Threading.Tasks.Task Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            var accountService = new Services.AccountService();

            if ((await accountService.Login(email.Text, password.Text)))
            {
                Debug.WriteLine("Yeah");
            }
            else
            {
                Debug.WriteLine("Shit!!!");
            }
        }
    }
}
