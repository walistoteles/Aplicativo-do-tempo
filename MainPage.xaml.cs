using Microsoft.VisualBasic;
using Newtonsoft.Json;
using TimeApp.Models;
using TimeApp.Services;
using TimeApp.ViewModel;

namespace TimeApp;

public partial class MainPage : ContentPage
{

    public MainPage()
	{
		InitializeComponent();
        GetWeather("Araguaina");
	}

    public async void GetWeather(string city)
    {
        string location = city;

        string url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid=798939db3697923c9b753b61c21020af";

        var result = await ApiCaller.Get(url);


        if (result.Succesful)
        {
            try
            {
                var weatherInfo = JsonConvert.DeserializeObject<WeatherData>(result.Response);

                BindingContext = weatherInfo;
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Error", result.ErrorMessage, "Ok");

        }
    }

    async void OnGetWeatherButtonClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
        {
            GetWeather(_cityEntry.Text);
        }
    }
}

