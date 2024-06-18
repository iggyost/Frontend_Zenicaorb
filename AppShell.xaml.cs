using Frontend_Zenicaorb.Views.ContentPages;

namespace Frontend_Zenicaorb;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    private void closeFlyoutButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = false;
    }

    private void userPhone_Loaded(object sender, EventArgs e)
    {
        userPhone.Text = "+" + App.enteredUser.Phone;
    }

    private void exitButton_Clicked(object sender, EventArgs e)
    {
        App.enteredPhone = null;
        App.enteredUser = null;
        Application.Current.MainPage = new NavigationPage(new PhoneEnterPage());
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new ProfilePage());
    }
}
