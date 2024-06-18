namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
        locationsMapView.Source = "https://www.zeemaps.com/map/jyvgx?group=5024237&add=1#";

    }

    private void flyoutButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
        Shell.Current.CurrentPage.Layout(new Rect(0, 0, Shell.Current.CurrentPage.Width + 1, Shell.Current.CurrentPage.Height + 1));
    }
}