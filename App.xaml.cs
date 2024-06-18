using Frontend_Zenicaorb.ApplicationData;
using Frontend_Zenicaorb.Views.ContentPages;

namespace Frontend_Zenicaorb;

public partial class App : Application
{
	public static string enteredPhone;
    public static string conString = "http://192.168.0.10:45455/api/";
	public static RoomsView selectedRoom;
	public static User enteredUser;
	public static bool isUserFound = false;
	public static List<RoomsView> allRooms = new List<RoomsView>();
	public static List<Reservation> userReservations = new List<Reservation>();
	public static bool isNeedUpdate = false;
    public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new WelcomePage());
	}
}
