using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Keepi.Shared;
using System.IO;
using System.Text.Json;

namespace Keepi.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly Db_Context _context;
        private readonly string chatFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Chat Files");

        public ChatController(Db_Context context)
        {
            _context = context;
        }

        [HttpGet("createNewChat/{user1}/{user2}")]
        public async Task<List<Chat>> CreateNewChatFile(string user1, string user2)
        {
            try
            {
                if (!Directory.Exists(chatFilesDirectory))
                {
                    Directory.CreateDirectory(chatFilesDirectory);
                }

                User u = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == user2);

                string fileName_option1 = user1 + ";" + user2;
                string fileName_option2 = user2 + ";" + user1;

                var chatFilePath_option1 = Path.Combine(chatFilesDirectory, $"{fileName_option1}.json");
                var chatFilePath_option2 = Path.Combine(chatFilesDirectory, $"{fileName_option2}.json");

                if (System.IO.File.Exists(chatFilePath_option1))
                {
                    return new List<Chat> { new Chat() { FileName = fileName_option1, User = u } };
                }
                else if (System.IO.File.Exists(chatFilePath_option2))
                {
                    return new List<Chat> { new Chat() { FileName = fileName_option2, User = u } };
                }
                else
                {
                    var initialChatData = new
                    {
                        chatId = fileName_option1,
                        messages = new List<object>()
                    };

                    var jsonContent = JsonSerializer.Serialize(initialChatData, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(chatFilePath_option1, jsonContent);

                    return new List<Chat> { new Chat() { FileName = fileName_option1, User = u } };
                }

                //if (!System.IO.File.Exists(chatFilePath))
                //{
                //    var initialChatData = new
                //    {
                //        chatId = fileName,
                //        messages = new List<object>()
                //    };

                //    var jsonContent = JsonSerializer.Serialize(initialChatData, new JsonSerializerOptions { WriteIndented = true });
                //    System.IO.File.WriteAllText(chatFilePath, jsonContent);
                //}
            }
            catch (Exception)
            {
                return null;
            }


            return new List<Chat> { };
        }

        [HttpGet("addMessageToChat/{fileName}/{userId}/{message}")]
        public async Task<List<bool>> AddMessageToChat(string fileName, string userId, string message)
        {
            try
            {
                var chatFilePath = Path.Combine(chatFilesDirectory, $"{fileName}.json");

                if (System.IO.File.Exists(chatFilePath))
                {
                    var chatData = JsonSerializer.Deserialize<ChatData>(System.IO.File.ReadAllText(chatFilePath));

                    if (chatData != null)
                    {
                        chatData.Messages.Add(new ChatMessage
                        {
                            UserId = userId,
                            Content = message,
                            Timestamp = DateTime.UtcNow
                        });

                        var updatedJsonContent = JsonSerializer.Serialize(chatData, new JsonSerializerOptions { WriteIndented = true });
                        System.IO.File.WriteAllText(chatFilePath, updatedJsonContent);

                        string[] users = fileName.Split(';');
                        User sender = null;
                        User receiver = null;
                        if (users[0] == userId)
                        {
                            sender = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == users[0]);
                            receiver = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == users[1]);
                        }
                        else if (users[1] == userId)
                        {
                            sender = await _context.Users.FirstOrDefaultAsync(u=> u.Id.ToString() == users[1]);
                            receiver = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == users[0]);
                        }

                        if (sender != null && receiver != null)
                        {
                            var a = NotificationsHelper.AddNewNotification(receiver.Id.ToString(), NotificationType.Message, $"{sender.Username} sent you a message");
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }


            return new List<bool> { true };
        }

        [HttpGet("readChatFile/{fileName}")]
        public async Task<List<ChatData>> ReadChatFile(string fileName)
        {
            var chatFilePath = Path.Combine(chatFilesDirectory, $"{fileName}.json");

            if (System.IO.File.Exists(chatFilePath))
            {
                var chatData = JsonSerializer.Deserialize<ChatData>(System.IO.File.ReadAllText(chatFilePath));
                if (chatData != null)
                    return new List<ChatData> { chatData };
                else
                {
                    return null;

                }
            }

            return null;
        }

        [HttpGet("getUserChats/{user}")]
        public async Task<List<Chat>> GetUserChats(string user)
        {
            List<Chat> chats = new List<Chat>();

            try
            {
                if (Directory.Exists(chatFilesDirectory))
                {
                    string[] files = Directory.GetFiles(chatFilesDirectory);

                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string[] tmp = fileName.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmp.Length > 0)
                        {
                            if (tmp[0] == user)
                            {
                                User u = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == tmp[1]);
                                if (u != null)
                                {
                                    chats.Add(new Chat() { User = u, FileName = fileName });
                                }
                            }
                            else if (tmp[1] == user)
                            {
                                User u = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == tmp[0]);
                                if (u != null)
                                {
                                    chats.Add(new Chat() { User = u, FileName = fileName });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return chats;
        }

        [HttpGet("deleteChat/{fileName}")]
        public async Task<List<bool>> DeleteChat(string fileName)
        {
            try
            {
                string filePath = Path.Combine(chatFilesDirectory, $"{fileName}.json");
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return new List<bool> { true };
                }
            }
            catch (Exception)
            {
                return new List<bool> { false };
            }

            return new List<bool> { false };
        }
    }
}
