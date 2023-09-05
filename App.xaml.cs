using Telegram.Bot.Types;
using Telegram.Bot;

namespace MauiApp1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new LogInPage());
    }
}
