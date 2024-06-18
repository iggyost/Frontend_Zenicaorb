using CommunityToolkit.Maui.Views;
using Frontend_Zenicaorb.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class InfoPage : Popup
{
	public InfoPage()
	{
		InitializeComponent();
	}
    public InfoPage(RoomsView room)
    {
        InitializeComponent();
        currentRoom = room;
        GetCurrentRoomImages();
        nameLabel.Text = App.selectedRoom.Name;
        classLabel.Text = App.selectedRoom.Class;
    }
    public static RoomsView currentRoom = new RoomsView();

    public async Task GetCurrentRoomImages()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}roomimagesview/get/{currentRoom.RoomId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            imagesCarouselView.ItemsSource = JsonConvert.DeserializeObject<RoomImagesView[]>(content).ToList();
        }
    }

    private void closeButton_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}