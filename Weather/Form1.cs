using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Weather
{
    public partial class Form1 : Form
    {
        private const string ApiKey = "8c2c60f96e13ec78fed673d62d77eb02"; 
        private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            city.Multiline = true;
            city.TextAlign = HorizontalAlignment.Center;
            city.Font = new Font("Arial", 14, FontStyle.Bold);
            city.Size = new Size(250, 40);
            city.Location = new Point((ClientSize.Width - city.Width) / 2, 30);

            Label[] labels = { min, max, осади };
            int offset = city.Bottom + 10;

            foreach (var label in labels)
            {
                label.AutoSize = true;
                label.Font = new Font("Arial", 12, FontStyle.Bold);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = new Point((ClientSize.Width - label.Width) / 2, offset);
                offset = label.Bottom + 10;
            }

            prognoz.Size = new Size(200, 50);
            prognoz.Font = new Font("Arial", 12, FontStyle.Bold);
            prognoz.BackColor = Color.LightBlue;
            prognoz.Location = new Point((ClientSize.Width - prognoz.Width) / 2, offset + 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Загрузити останнє місто
            string lastCity = Weather.Properties.Settings.Default.LastCity; 
            if (!string.IsNullOrEmpty(lastCity))
            {
                city.Text = lastCity; 
            }
        }

        private async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format(ApiUrl, city, ApiKey);
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Проверяем статус ответа
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Помилка при отриманні даних: {response.StatusCode.ToString()}");
                        return null;
                    }

                    var responseString = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseString);

                    if (data?.main == null)
                    {
                        MessageBox.Show("Не вдалося отримати дані.");
                        return null;
                    }

                    double tempMin = data.main.temp_min;
                    double tempMax = data.main.temp_max;
                    bool isRaining = data.weather[0].main.ToString().Contains("Rain");

                    return new WeatherInfo
                    {
                        TempMin = tempMin,
                        TempMax = tempMax,
                        IsRaining = isRaining
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}");
                    return null;
                }
            }
        }

        private class WeatherInfo
        {
            public double TempMin { get; set; }
            public double TempMax { get; set; }
            public bool IsRaining { get; set; }
        }

        private async void prognoz_Click_1(object sender, EventArgs e)
        {
            string cityName = city.Text;
            if (string.IsNullOrWhiteSpace(cityName))
            {
                MessageBox.Show("Введіть назву міста");
                return;
            }

            WeatherInfo weather = await GetWeatherAsync(cityName);
            if (weather != null)
            {
                min.Text = $"Мін. темп.: {weather.TempMin}°C";
                max.Text = $"Макс. темп.: {weather.TempMax}°C";
                осади.Text = $"Осади: {(weather.IsRaining ? "Так" : "Ні")}";

                // Сохранение последнего города
                Weather.Properties.Settings.Default.LastCity = cityName;
                Properties.Settings.Default.Save();
            }
        }
    }
}
