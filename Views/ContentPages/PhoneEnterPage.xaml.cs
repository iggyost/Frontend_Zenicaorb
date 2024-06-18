using System.Text.RegularExpressions;

namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class PhoneEnterPage : ContentPage
{
	public PhoneEnterPage()
	{
		InitializeComponent();
	}

    private async void continueButton_Clicked(object sender, EventArgs e)
    {
        var regex = new Regex("^((\\+7|7|8)+([0-9]){10})$");
        if (regex.IsMatch(phoneEnter.Text))
        {
            loadingIndicator.IsRunning = true;
            phoneBorder.IsEnabled = false;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}users/phone/{phoneEnter.Text}");
            if (response.IsSuccessStatusCode)
            {
                App.enteredPhone = phoneEnter.Text;
                App.isUserFound = true;
                await Navigation.PushAsync(new PasswordEnterPage());
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                App.enteredPhone = phoneEnter.Text;
                App.isUserFound = false;
                await Navigation.PushAsync(new PasswordEnterPage());
            }
        }
        else
        {
            await DisplayAlert("Номер не соответствует формату", "", "OK");
        }
    }
}
