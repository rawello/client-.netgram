using System.Security.AccessControl;
using TL;
using WTelegram;
using static System.Net.Mime.MediaTypeNames;


namespace MauiApp1;

public class LogInPage : ContentPage
{
    public static Client client = new Client(00000000 /*your app api_id at int*/, "your app api_hash");

    public LogInPage()
	{
		Label lbl = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Wlcm to .netgram" };
        Button btnbtn = new Button { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "btn for verification" };

        Entry phone = new Entry { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Placeholder = "phone number"};
        Entry password = new Entry { IsPassword = true,HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Placeholder = "password (skip if havent 2FA)" };
        Button exit = new Button { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.End, Text = "log out" };


        btnbtn.Clicked += async (sender, e) => {
            if(phone.Text != null)
            {
                await DoLogin(phone.Text, password.Text);
            }
        };
        exit.Clicked += async(sender, e) => await client.Auth_LogOut();

        Content = new StackLayout
        {
            Children =
                {   lbl,
                    phone,
                    password,
                    btnbtn,
                    exit
                }
        };
    }

    public async Task DoLogin(string tempStr, string psword)
    {
        while (client.User == null)
        {
            switch (await client.Login(tempStr))
            {
                case "verification_code": tempStr = await DisplayPromptAsync("Input code", "verification code", keyboard: Keyboard.Numeric, maxLength: 5); continue;
                case "password": tempStr = psword; break;
                default: tempStr = null; break;
            }
        }
        await Navigation.PushAsync(new Chat());
    }
}