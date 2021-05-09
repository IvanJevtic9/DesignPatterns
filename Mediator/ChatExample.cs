using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator
{
    public class Generator
    {
        private static int id = 0;
        private static int roomId = 0;

        public static int GeneratePersonID()
        {
            return id++;
        }

        public static int GenerateRoomID()
        {
            return roomId++;
        }
    }

    public class Person
    {
        public int PersonId { get; }
        public string Name { get; set; }

        private List<ChatRoom> chatRooms = new List<ChatRoom>();
        private Dictionary<int, List<string>> receivedMessages = new Dictionary<int, List<string>>(); 

        public Person(string name)
        {
            PersonId = Generator.GeneratePersonID();
            Name = name;
        }

        public void AddRoom(ChatRoom c)
        {
            chatRooms.Add(c);
        }

        public void AddReceivedMessage(int roomId, string message)
        {
            if (receivedMessages.ContainsKey(roomId))
            {
                receivedMessages[roomId].Add(message);
            }
            else
            {
                receivedMessages.Add(roomId, new List<string>() { message });
            }
        }

        public string ReceivedMessagesHistory()
        {
            var sb = new StringBuilder();

            sb.AppendLine("########################################################");
            sb.AppendLine($"Received messages for Person with id: {PersonId}, name: {Name}");
            sb.AppendLine();

            foreach (var roomId in receivedMessages.Keys)
            {
                var room = chatRooms.FirstOrDefault(r => r.RoomId == roomId);
                sb.AppendLine($"Chat room: {room.RoomName}");
                foreach (var message in receivedMessages[roomId])
                {
                    sb.AppendLine(message);
                }
                sb.AppendLine();
            }
            sb.AppendLine("########################################################");

            return sb.ToString();
        }

        public void JoinRoom(ChatRoom room)
        {
            if (room is null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            room.JoinRoom(this);
        }

        public void SentMessage(ChatRoom room, string message)
        {
            if (room != null)
            {
                room.Broadcast(this, message);
            }
        }

        public void SentPrivateMessage(Person targetPerson, string message)
        {
            var room = chatRooms.FirstOrDefault(r => r.isPrivate && r.ContainsPerson(targetPerson));

            if (room == null)
            {
                ChatRoom newRoom = new ChatRoom($"{this.Name}-{targetPerson.Name}.PrivateRoom", true);

                targetPerson.JoinRoom(newRoom);
                this.JoinRoom(newRoom);

                newRoom.Broadcast(this, message);
            }
            else
            {
                room.Broadcast(this, message);
            }
        }
    }

    public class ChatRoom
    {
        public int RoomId { get; }
        public string RoomName { get; set; }

        public readonly bool isPrivate;

        private List<MessageHistory> chatLog = new List<MessageHistory>();
        private List<Person> listOfParticipant = new List<Person>();

        public ChatRoom(string roomName, bool isPrivate = false)
        {
            RoomId = Generator.GenerateRoomID();
            RoomName = roomName;
            this.isPrivate = isPrivate;
        }

        public void Broadcast(Person p, string message)
        {
            chatLog.Add(new MessageHistory(p.PersonId, p.Name, message, SenderType.Person));

            foreach (var part in listOfParticipant)
            {
                if (part.PersonId != p.PersonId)
                {
                    part.AddReceivedMessage(RoomId, $"[{p.PersonId}-{p.Name}]: {message}");
                }
            }
        }

        public void JoinRoom(Person p)
        {
            string joinMessage = $"{p.PersonId}-{p.Name} has joined the room";

            if (listOfParticipant.Contains(p))
            {
                Console.WriteLine($"Person {p.Name} has already joined room");
                return;
            }

            if (isPrivate)
            {
                if (listOfParticipant.Count > 2)
                {
                    Console.WriteLine($"Room is private, person {p.Name} is not able to join");
                    return;
                }

                listOfParticipant.Add(p);
                p.AddRoom(this);
                return;
            }

            foreach (var part in listOfParticipant)
            {
                if (part.PersonId != p.PersonId)
                {
                    part.AddReceivedMessage(RoomId, $"[{p.PersonId}-{p.Name}]: {joinMessage}");
                }
            }

            listOfParticipant.Add(p);
            p.AddRoom(this);
            chatLog.Add(new MessageHistory(RoomId, RoomName, joinMessage, SenderType.Room));
        }

        public bool ContainsPerson(Person person)
        {
            return listOfParticipant.Contains(person);
        }

        public string RoomChatHistory()
        {
            var sb = new StringBuilder();

            sb.AppendLine("########################################################");
            sb.AppendLine($"Room chat history: {RoomId}, name: {RoomName}");

            foreach (var message in chatLog)
            {
                sb.AppendLine(message.ToString());
            }

            sb.AppendLine("########################################################");

            return sb.ToString();
        }
    }

    public enum SenderType
    {
        Person,
        Room
    }

    public class MessageHistory
    {
        public string Message { get; }
        public int Id { get; }
        public string Name { get; }
        public SenderType Sender { get; }

        public MessageHistory(int id, string name, string message, SenderType s)
        {
            Id = id;
            Name = name;
            Message = message;
            Sender = s;
        }

        public override string ToString()
        {
            return $"[{Sender.ToString()}][{Id}-{Name}]: {Message}";
        }
    }

    public class ChatExample
    {
        public static void MainFunc(string[] args)
        {
            var chatRoom = new ChatRoom("Fudbalica");

            var ivanPr = new Person("Ivan Jevtic");
            var milosPr = new Person("Milos Djukic");
            var borkoPr = new Person("Borko Jo");

            chatRoom.JoinRoom(ivanPr);
            chatRoom.JoinRoom(milosPr);
            ivanPr.SentMessage(chatRoom, "Dobrodosli u grupu!!");
            chatRoom.JoinRoom(borkoPr);
            ivanPr.SentMessage(chatRoom, "Gde si Borko sunce m unjegovo");
            milosPr.SentMessage(chatRoom, "Hhahahah");
            borkoPr.SentMessage(chatRoom, "Evo me , sta cu ja u ovoj grupi.");

            borkoPr.SentPrivateMessage(ivanPr, "Sto si me stavio u ovu grupu");
            ivanPr.SentPrivateMessage(borkoPr, "Malo da dobijes kondiciju :D");

            milosPr.SentPrivateMessage(ivanPr, "Kolko si sakupio PIja");

            Console.WriteLine(chatRoom.RoomChatHistory());

            Console.WriteLine(ivanPr.ReceivedMessagesHistory());
            Console.WriteLine(milosPr.ReceivedMessagesHistory());
            Console.WriteLine(borkoPr.ReceivedMessagesHistory());
        }
    }
}
