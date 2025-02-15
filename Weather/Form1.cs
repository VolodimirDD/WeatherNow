using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Configuration;

namespace Weather
{
    public partial class Form1 : Form
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["ApiKey"];
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

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Помилка отримання даних: {response.ReasonPhrase}");
                        return null;
                    }

                    string responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<WeatherApiResponse>(responseString);

                    if (data?.Main == null || data.Weather == null || data.Weather.Count == 0)
                    {
                        MessageBox.Show("Не вдалося отримати дані про погоду.");
                        return null;
                    }

                    return new WeatherInfo
                    {
                        TempMin = data.Main.TempMin,
                        TempMax = data.Main.TempMax,
                        IsRaining = data.Weather[0].Main.Contains("Rain")
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}");
                    return null;
                }
            }
        }

        public class WeatherInfo
        {
            public double TempMin { get; set; }
            public double TempMax { get; set; }
            public bool IsRaining { get; set; }
        }

        public class WeatherApiResponse
        {
            [JsonProperty("main")]
            public MainInfo Main { get; set; }

            [JsonProperty("weather")]
            public List<WeatherDescription> Weather { get; set; }
        }

        public class MainInfo
        {
            [JsonProperty("temp_min")]
            public double TempMin { get; set; }

            [JsonProperty("temp_max")]
            public double TempMax { get; set; }
        }

        public class WeatherDescription
        {
            [JsonProperty("main")]
            public string Main { get; set; }
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
