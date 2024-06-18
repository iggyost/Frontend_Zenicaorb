using Frontend_Zenicaorb.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
	}
    public async Task GetReservationsByUserId()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}reservation/get/{App.enteredUser.UserId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Reservation[]>(content).ToList();
            reservCollectionView.ItemsSource = data;
        }
    }
    private void statusLabel_Loaded(object sender, EventArgs e)
    {
		Label label = sender as Label;
		var statusId = int.Parse(label.AutomationId.ToString());
		switch (statusId)
		{
			case 1:
				label.Text = "Актуально";
				break;
            case 2:
                label.Text = "Размещено";
                break;
            case 3:
                label.Text = "Завершено";
                break;
        }
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await GetReservationsByUserId();
        while (true)
        {
            await Task.Delay(3000);
            if (App.isNeedUpdate == true)
            {
                await GetReservationsByUserId();
            }
            else
            {

            }
        }
    }

    private void flyoutButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
        Shell.Current.CurrentPage.Layout(new Rect(0, 0, Shell.Current.CurrentPage.Width + 1, Shell.Current.CurrentPage.Height + 1));
    }
}