using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class Program
{
    private static TelegramBotClient telegramBot;
    private const string startCommand = "/start";
    private const string aboutMeCommand = "About me";
    private const string skillsCommand = "Skills";
    private const string languagesCommand = "Languages";
    private const string contactMeCommand = "Contact me";
    private static void Main(string[] args)
    {
        string token = @"Token";
        telegramBot = new TelegramBotClient(token);

        telegramBot.StartReceiving(HandleUpdate, HandleError);

        Console.ReadLine();
    }

    private static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message?.Type is MessageType.Text)
        {
            if (update.Message.Text is startCommand)
            {
                var markup = MenuMarkup();

                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Welcome to Zafar's resume.",
                    replyMarkup: markup);
            }
            if (update.Message.Text is aboutMeCommand)
            {
                var photoPath = @"PhotoPath";

                using (var photoStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read))
                {
                    await client.SendPhotoAsync(
                    chatId: update.Message.Chat.Id,
                    photo: new InputFileStream(photoStream),
                    caption: "As a .NET Developer at tarteeb.uz, I apply my SWOT analysis and problem-solving skills " +
                    "to create and maintain web applications using EF Core, MVC, and C#. I enjoy working with a dynamic" +
                    " and collaborative team, and I am always eager to learn new technologies and best practices.\r\n\r\n" +
                    "In addition, I am a .NET Mentor at MohirDev, where I oraanize programming meetups to help students " +
                    "become more proficient in .NET development. I am passionate about sharing my knowledge and experience " +
                    "with others, and I find mentoring rewarding and fulfilling. My goal is to contribute to " +
                    "impactful and meaninaful projects that make a difference in the world.");
                }
            }
            if(update.Message.Text is skillsCommand)
            {
                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "C#\nJavaScript\nHtml\nCSS");
            }
            if(update.Message.Text is languagesCommand)
            {
                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Russian 🇷🇺\nEnglish 🇺🇸\nUzbek🇺🇿");
            }
            if(update.Message.Text is contactMeCommand)
            {
                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "@zafar_urakov\n+9989904903894\nToshkent");
            }
        }
        else
        {
            await client.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                $"Please send only text message.");
        }
    }
    private static ReplyKeyboardMarkup MenuMarkup()
    {
        return new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
            new KeyboardButton[]{new KeyboardButton("About me"), new KeyboardButton("Skills") },
            new KeyboardButton[]{new KeyboardButton("Languages"), new KeyboardButton("Contact me")
            } })
        {
            ResizeKeyboard = true
        };
    }

    private static async Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        await client.SendTextMessageAsync(
            chatId: 1924521160,
            $"Error: {exception.Message}");
    }
}