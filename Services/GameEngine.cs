using Microsoft.EntityFrameworkCore;
using W9_assignment_template.Data;
using W9_assignment_template.Models;

namespace W9_assignment_template.Services;

public class GameEngine
{
    private readonly GameContext _context;

    public GameEngine(GameContext context)
    {
        _context = context;
    }

    public void DisplayRooms()
    {
        var rooms = _context.Rooms.Include(r => r.Characters).ToList();

        foreach (var room in rooms)
        {
            Console.WriteLine($"Room: {room.Name} - {room.Description}");
            foreach (var character in room.Characters)
            {
                Console.WriteLine($"    Character: {character.Name}, Level: {character.Level}");
            }
        }
    }

    public void DisplayCharacters()
    {
        var characters = _context.Characters.ToList();
        if (characters.Any())
        {
            Console.WriteLine("\nCharacters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}");
            }
        }
        else
        {
            Console.WriteLine("No characters available.");
        }
    }

    public void AddRoom()
    {
        Console.Write("Give the room a name: ");
        var roomName = Console.ReadLine();
        Console.Write("Give the room a description: ");
        var roomDesc = Console.ReadLine();

        var newRoom = new Room
        {
            Name = roomName,
            Description = roomDesc
        };

        _context.Rooms.Add(newRoom);
        _context.SaveChanges();

        Console.WriteLine($"The room \"{roomName}\" has been added.");
    }

    public void AddCharacter()
    {
        Console.Write("Give the character a name: ");
        var charName = Console.ReadLine();
        Console.Write("Give the character a level: ");
        var charLevel = int.Parse(Console.ReadLine());

        Console.WriteLine($"------------------------------------------------");
        foreach (var room in _context.Rooms)
        {
            Console.WriteLine($"{room.Id}. {room.Name}");
        }
        Console.WriteLine($"------------------------------------------------");
        Console.Write("What is the id of the room that the character is in?: ");
        var charsRoom = int.Parse(Console.ReadLine());

        var foundRoom = _context.Rooms.FirstOrDefault(r => r.Id == charsRoom);
        if (foundRoom != null)
        {
            var newChar = new Character
            {
                Name = charName,
                Level = charLevel,
                RoomId = charsRoom
            };

            _context.Characters.Add(newChar);
            _context.SaveChanges();

            Console.WriteLine($"The character \"{charName}\" has been added.");
        }else
        {
            Console.WriteLine($"The character \"{charName}\" had failed to been added.");
            return;
        }
    }

    public void FindCharacter()
    {
        Console.Write("Enter character name to search: ");
        var name = Console.ReadLine();

        var foundChar = _context.Characters.FirstOrDefault(c => c.Name == name);
        if (foundChar != null)
        {
            Console.WriteLine($"Character \"{foundChar.Name}\" is:\nLevel {foundChar.Level}\nRoom {_context.Rooms.FirstOrDefault(r => r.Id == foundChar.RoomId).Name}");
        }
        else
        {
            Console.WriteLine($"Character \"{name}\" was not found.");
        }
    }

    public void LevelUpCharacter()
    {
        Console.Write("Enter character name to search: ");
        var name = Console.ReadLine();

        var foundChar = _context.Characters.FirstOrDefault(c => c.Name == name);
        if (foundChar != null)
        {
            foundChar.Level++;
            _context.SaveChanges();
            Console.WriteLine($"Character \"{foundChar.Name}\" has leveld up to: {foundChar.Level}");
        }
        else
        {
            Console.WriteLine($"Character \"{name}\" was not found.");
        }
    }
}