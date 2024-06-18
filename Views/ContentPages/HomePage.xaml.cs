using CommunityToolkit.Maui.Views;
using Frontend_Zenicaorb.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}
	public static List<RoomsView> roomsView = new List<RoomsView>();
	public async Task GetAllRooms()
	{
		HttpClient client = new HttpClient();
		HttpResponseMessage response = await client.GetAsync($"{App.conString}roomsview/get");
		if (response.IsSuccessStatusCode)
		{
			string content = await response.Content.ReadAsStringAsync();
			roomsView = JsonConvert.DeserializeObject<RoomsView[]>(content).ToList();
			roomsCollectionView.ItemsSource = roomsView;
			App.allRooms = roomsView;
		}
	}
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		await GetAllRooms();
    }

    private async void additionInfoButton_Clicked(object sender, EventArgs e)
    {
		Button button = sender as Button;
		var roomId = int.Parse(button.AutomationId.ToString());
		var currentRoom = roomsView.Where(x => x.RoomId == roomId).FirstOrDefault();
		App.selectedRoom = currentRoom;
		await this.ShowPopupAsync(new InfoPage(currentRoom));
    }

    private async void reserveButton_Clicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        var roomId = int.Parse(button.AutomationId.ToString());
        var currentRoom = roomsView.Where(x => x.RoomId == roomId).FirstOrDefault();
        App.selectedRoom = currentRoom;
		await Navigation.PushModalAsync(new ReserveRoomPage(currentRoom));
    }

    private void flyoutButton_Clicked(object sender, EventArgs e)
    {
		Shell.Current.FlyoutIsPresented = true;
        Shell.Current.CurrentPage.Layout(new Rect(0, 0, Shell.Current.CurrentPage.Width + 1, Shell.Current.CurrentPage.Height + 1));
    }
}