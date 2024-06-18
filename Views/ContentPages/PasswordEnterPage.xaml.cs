using Frontend_Zenicaorb.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class PasswordEnterPage : ContentPage
{
    public PasswordEnterPage()
    {
        InitializeComponent();
    }
    public async Task LoginUser()
    {
        passwordBorder.IsEnabled = false;
        loadingIndicator.IsRunning = true;

        if (passwordEnter.Text.Length > 3)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}users/enter/{App.enteredPhone}/{passwordEnter.Text}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                App.enteredUser = JsonConvert.DeserializeObject<User>(content);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Ошибка ввода пароля", "", "ОК");
            }
        }
        else
        {
            await DisplayAlert("Не менее 3-х символов в пароле!", "", "ОК");
        }
        passwordBorder.IsEnabled = true;
        loadingIndicator.IsRunning = false;
    }
    public async Task RegUser()
    {
        passwordBorder.IsEnabled = false;
        loadingIndicator.IsRunning = true;

        if (passwordEnter.Text.Length > 3)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}users/reg/{App.enteredPhone}/{passwordEnter.Text}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                App.enteredUser = JsonConvert.DeserializeObject<User>(content);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Ошибка ввода пароля", "", "ОК");
            }
        }
        else
        {
            await DisplayAlert("Не менее 3-х символов в пароле!", "", "ОК");
        }
        passwordBorder.IsEnabled = true;
        loadingIndicator.IsRunning = false;
    }

    private async void continueButton_Clicked(object sender, EventArgs e)
    {
        if (App.isUserFound == true)
        {
            await LoginUser();
        }
        else
        {
            await RegUser();
        }
    }
}