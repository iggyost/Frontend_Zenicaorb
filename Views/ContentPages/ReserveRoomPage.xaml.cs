using Frontend_Zenicaorb.ApplicationData;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Frontend_Zenicaorb.Views.ContentPages;

public partial class ReserveRoomPage : ContentPage
{
    public ReserveRoomPage()
    {
        InitializeComponent();
    }
    public ReserveRoomPage(RoomsView room)
    {
        InitializeComponent();
        nameLabel.Text = App.selectedRoom.Name;
        classLabel.Text = App.selectedRoom.Class;
        totalCostLabel.Text = App.selectedRoom.Cost.ToString();
        startDatePicker.PickerDisplayDate = DateTime.Today;
        endDatePicker.PickerDisplayDate = DateTime.Today;
    }
    public class Reserv()
    {
        public string Name { get; set;}
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set;}
        public string Wishes { get; set; }
        public int StatusId { get; set;}
        public int RoomId { get; set;}
        public int UserId { get; set; }
        public int PetWeight { get; set;}
        public int PetHeight { get; set;}
        public bool IsVideoSurveillance { get; set;}
        public bool IsPhotoReports { get; set;}
        public decimal TotalCost { get; set;}
    };
    public async Task CreateReservation(string name, DateTime startDate, DateTime endDate, string wishes,
                                        int statusId, int roomId, int userId, int petWeight, int petHeight,
                                        bool isVideoSurv, bool isPhotoRep, decimal totalCost)
    {
        HttpClient client = new HttpClient();
        Reserv reservation = new Reserv()
        {
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            Wishes = wishes,
            StatusId = statusId,
            RoomId = roomId,
            UserId = userId,
            PetWeight = petWeight,
            PetHeight = petHeight,
            IsVideoSurveillance = isVideoSurv,
            IsPhotoReports = isPhotoRep,
            TotalCost = totalCost
        };
        var response = await client.PostAsJsonAsync($"{App.conString}reservation/reserve", reservation);
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            App.userReservations = JsonConvert.DeserializeObject<Reservation[]>(content).ToList();
            await DisplayAlert("Заявка успешно создана!", "", "OK");
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Ошибка при создании заявки!", "", "OK");
        }
    }
    private async void reserveButton_Clicked(object sender, EventArgs e)
    {
        if (startDatePicker.Date >= DateTime.Now)
        {
            if (endDatePicker.Date > startDatePicker.Date)
            {
                int currentWeight = 0;
                int currentHeight = 0;
                string currentWishes = null;
                if (weightLabel.Text != null)
                {
                    currentWeight = int.Parse(weightLabel.Text);
                }
                if (heightLabel.Text != null)
                {
                    currentHeight = int.Parse(heightLabel.Text);
                }
                if (wishesEditor.Text != null)
                {
                    currentWishes = wishesEditor.Text;
                }
                await CreateReservation(nameLabel.Text,
                                        (DateTime)startDatePicker.Date, (DateTime)endDatePicker.Date, 
                                        currentWishes,
                                        1, App.selectedRoom.RoomId, App.enteredUser.UserId, 
                                        currentWeight,
                                        currentHeight, 
                                        surveillanceRadiobutton.IsChecked, photoReportsRadiobutton.IsChecked,
                                        int.Parse(totalCostLabel.Text));
            }
            else
            {
                await DisplayAlert("Дата заселения не может быть позже даты отъезда", "", "ОК"); ;
            }
        }
        else
        {
            await DisplayAlert("Дата заселения не может быть раньше чем сегодня", "", "ОК");
        }
    }

    private void photoReportsRadiobutton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (photoReportsRadiobutton.IsChecked == true)
        {
            var currentCost = int.Parse(totalCostLabel.Text);
            currentCost = currentCost + 100;
            totalCostLabel.Text = currentCost.ToString();
        }
        else
        {
            var currentCost = int.Parse(totalCostLabel.Text);
            currentCost = currentCost - 100;
            totalCostLabel.Text = currentCost.ToString();
        }
    }

    private void surveillanceRadiobutton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (surveillanceRadiobutton.IsChecked == true)
        {
            var currentCost = int.Parse(totalCostLabel.Text);
            currentCost = currentCost + 250;
            totalCostLabel.Text = currentCost.ToString();
        }
        else
        {
            var currentCost = int.Parse(totalCostLabel.Text);
            currentCost = currentCost - 250;
            totalCostLabel.Text = currentCost.ToString();
        }
    }

    private void startDatePicker_DateChanged(object sender, EventArgs e)
    {
        if (startDatePicker.Date < endDatePicker.Date)
        {
            var currentCost = Convert.ToInt32(App.selectedRoom.Cost);
            if (photoReportsRadiobutton.IsChecked == true)
            {
                currentCost += 100;
            }
            if (surveillanceRadiobutton.IsChecked == true)
            {
                currentCost += 250;
            }
            try
            {
                var difference = endDatePicker.Date.Value.Subtract(startDatePicker.Date.Value);
                currentCost = currentCost + (difference.Days * Convert.ToInt32(App.selectedRoom.Cost));
                totalCostLabel.Text = currentCost.ToString();
            }
            catch (Exception)
            {

            }
        }
    }

    private void endDatePicker_DateChanged(object sender, EventArgs e)
    {
        if (startDatePicker.Date < endDatePicker.Date)
        {
            var currentCost = Convert.ToInt32(App.selectedRoom.Cost);
            if (photoReportsRadiobutton.IsChecked == true)
            {
                currentCost += 100;
            }
            if (surveillanceRadiobutton.IsChecked == true)
            {
                currentCost += 250;
            }
            try
            {
                var difference = endDatePicker.Date.Value.Subtract(startDatePicker.Date.Value);
                currentCost = currentCost + (difference.Days * Convert.ToInt32(App.selectedRoom.Cost));
                totalCostLabel.Text = currentCost.ToString();
            }
            catch (Exception)
            {

            }
        }
    }
}