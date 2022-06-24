using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace Weather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cityBox.Select();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string city = cityBox.Text;
            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&APPID=b580cdcadf5006e077af6fc4a0b46af9&lang=ru";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = await request.GetResponseAsync();
            string answer = string.Empty;


            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();

            richTextBox1.Text = answer;


            DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }

            OpenWeather.OpenWeather oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(answer);

            panel1.BackgroundImage = oW.weather[0].Icon;

            cityBox.Text = city + ", " + UnixTimeStampToDateTime(oW.dt).ToLocalTime();
            label1.Text = "Облачность (баллов): " + ((int)oW.clouds.all).ToString();
            label2.Text = oW.weather[0].description;
            label3.Text = "Видимость (м):" + oW.visibility.ToString();
            label4.Text = "Температура (°C): " + oW.main.temp.ToString("0.#");
            label5.Text = "Влажность (%): " + oW.main.humidity.ToString();
            label6.Text = "Давление (гПа): " + oW.main.pressure.ToString();
            label7.Text = "Направление (°): " + oW.wind.deg.ToString();
            label8.Text = "Скорость (м/с): " + ((int)oW.wind.speed).ToString();
            label9.Text = "Порыв (м/с): " + ((int)oW.wind.gust).ToString();
            label10.Text = UnixTimeStampToDateTime(oW.sys.sunrise).ToShortTimeString();
            label11.Text = UnixTimeStampToDateTime(oW.sys.sunset).ToShortTimeString();

            






        }

             
    }
}
