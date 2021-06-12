using System;
using System.Drawing;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace ConsoleApp2
{
    internal class Program
    {
        private static ITelegramBotClient botClient;

        private static void Main()
        {
            botClient = new TelegramBotClient("674900496:AAG-xqjrH3NB9og7l5O5z1EcRz_UgDQQTIw");

            Telegram.Bot.Types.User me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.OnMessageEdited += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                Console.WriteLine(e.Message.Text + " " + e.Message.Chat.Username);
                if (e.Message.Text == "/Hi")
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Hi" + " " + e.Message.Chat.Username);
                }
                else if (e.Message.Text == "/Hello")
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Hello" + " " + e.Message.Chat.Username);
                }
                else
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, @"Usege:
                      /Hi
                      /Hello");
                }
                screenShot("screen1.png");
                {
                    using (System.IO.FileStream stream = System.IO.File.OpenRead("screen1.png"))
                    {
                        // await botClient.SendPhotoAsync(e.Message.Chat.Id, new FileToSend(stream.Name, stream));
                    }
                }
            }
        }

        private static void screenShot(string name)
        {
            Console.WriteLine("Starting the process...");
            Console.WriteLine();
            Bitmap memoryImage;
            memoryImage = new Bitmap(1000, 900);
            Size s = new Size(memoryImage.Width, memoryImage.Height);

            Graphics memoryGraphics = Graphics.FromImage(memoryImage);

            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

            //That's it! Save the image in the directory and this will work like charm.
            string fileName = string.Format(name);

            // save it
            memoryImage.Save(fileName);

        }
    }
}
