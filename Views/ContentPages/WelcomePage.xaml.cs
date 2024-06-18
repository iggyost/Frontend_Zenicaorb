namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class WelcomePage : ContentPage
{
	public class WelcomeText
	{
		public string Text { get; set; }
	};
	public static List<WelcomeText> list = new List<WelcomeText>();
	public WelcomePage()
	{
		InitializeComponent();
		list = new List<WelcomeText>()
        {
            new WelcomeText { Text = "�������� ������ ������� �� ����� ������� � ���!"},
            new WelcomeText { Text = "��������� ���� �������� � ������� ��������� ��� ���!"},
            new WelcomeText { Text = "�� ���� ��������� ������� ��� ������ �������!"},
        };
		textCarouselView.ItemsSource = list;

	}

    private async void continueButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new PhoneEnterPage());
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		while (true)
		{
			await Task.Delay(7500);
			textCarouselView.CurrentItem = list.ElementAt(0);
            await Task.Delay(7500);
            textCarouselView.CurrentItem = list.ElementAt(1);
            await Task.Delay(7500);
            textCarouselView.CurrentItem = list.ElementAt(2);
        }
    }
}