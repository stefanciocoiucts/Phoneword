using System;
using Xamarin.Forms;

namespace Phoneword
{
    public partial class MainPage : ContentPage //Main page is linked to ContentPage
    {
        string translatedNumber; //Declares a string 

        public MainPage()
        {
            InitializeComponent(); //Initialises what is previous to it (Mainpage) 
        }

        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
            //Grabs the translated number from another page and allows the user to call it
            if (!string.IsNullOrWhiteSpace(translatedNumber)) //If it is NOT null
            {
                callButton.IsEnabled = true; //Enables the call button
                callButton.Text = "Call " + translatedNumber;
                //The number is called
            }
            else
            {
                callButton.IsEnabled = false; //Else, it doesn't allow you to call
                callButton.Text = "Call"; //There's no translated number, so no display
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + translatedNumber + "?",
                    "Yes",
                    "No")) //This pops up an alert with the text above
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    App.PhoneNumbers.Add(translatedNumber);
                    callHistoryButton.IsEnabled = true;
                    dialer.Dial(translatedNumber); 
                   //If a number's been called, it adds it to Call History and
                    // also allows us to access the Call History page
                }
            }
        }
        async void OnCallHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CallHistoryPage());
        }
    }
}