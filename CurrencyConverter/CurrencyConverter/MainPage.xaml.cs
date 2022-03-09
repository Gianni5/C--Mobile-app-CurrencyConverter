using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;


namespace CurrencyConverter
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            pickerCurrency.Items.Add("USD");  // adding to the picker

            pickerCurrency.Items.Add("GBP");
            pickerCurrency.Items.Add("EUR");
            pickerCurrency.Items.Add("FKP");
        }



        public async void GetAPI(string Currency)
        {
            //Fetch
            var client = new HttpClient();
            var response = await client.GetAsync("https://api.exchangeratesapi.io/latest?base=AUD");
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            responseLabel.Text = responseString;


            //parse response into JSON
            //GET Whole Object 
            var obj = JsonConvert.DeserializeObject<JObject>(responseString);
            //Get Rates Object
            var rates = obj.Value<JObject>("rates");
            //Get AUD Value
            float value = rates.Value<float>(Currency);
            //Update UI
           
            //converting text to float
            float amount = float.Parse(entryAmount.Text);
            amount = value * amount;
            responseLabel.Text = $"{amount}";
        }

        private void ConvertRates_Clicked(object sender, EventArgs e)
        {

            string convertedSelectedItem = (string)pickerCurrency.SelectedItem; // creating a variable that convert to string the selected items 

            GetAPI(convertedSelectedItem);// call the method 
        }

        

       

       



        


    }
}