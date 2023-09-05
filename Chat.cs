using System;
using System.Linq;
using TL;

namespace MauiApp1;

public class Chat : ContentPage
{
    public Chat()
    {
        List<string> chatsDialogs = new List<string>();

        Picker slct = new Picker { Title = "select chat ('no' to handwriting tag)" };

        Task.Run(async delegate
        {
            var dialogs = await LogInPage.client.Messages_GetAllDialogs();
            foreach (Dialog dialog in dialogs.dialogs)
            {
                switch (dialogs.UserOrChat(dialog))
                {
                    case User user when user.IsActive:
                        if (user.username != null)
                        {
                            chatsDialogs.Add(user.username);
                        }
                        break;
                    case ChatBase chat when chat.IsActive: await Console.Out.WriteLineAsync(); break;
                }
            }
        });   

        slct.ItemsSource = chatsDialogs;
        Entry tag = new Entry { Placeholder = "write tag-name if havent in list" };
        Entry msg = new Entry { Placeholder = "write message" };
        Button send = new Button { Text = "send msg" };

        Content = new StackLayout { Children = { tag, slct, msg, send }, Padding = 7 };

        send.Clicked += async (sender, e) =>
        {
            if(slct.SelectedItem == null)
            {
                
            }else if(slct.SelectedItem.ToString() == "no")
            {
                var resolved = await LogInPage.client.Contacts_ResolveUsername(tag.Text);
                await LogInPage.client.SendMessageAsync(resolved, msg.Text);
            }
            else
            {
                var resolved = await LogInPage.client.Contacts_ResolveUsername(slct.SelectedItem.ToString());
                await LogInPage.client.SendMessageAsync(resolved, msg.Text);
            }
            msg.Text = "";
        };
    }
}

