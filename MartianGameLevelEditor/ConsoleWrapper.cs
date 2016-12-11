using MartianGameLevelEditor.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianGameLevelEditor
{
    public class ConsoleWrapper
    {
        public int Width;
        public int Height;
        public string MapName;

        public ConsoleChar[,] MapLayer;
        public ConsoleChar[,] InterfaceLayer;

        public Tile[,] Tiles;

        static ConsoleWrapper _instance;
        Dictionary<ConsoleKeyInfo, ICommand> _commands;
        Dictionary<TileType, char> _typeChars;

        public string FileName;

        public bool ShowCommands = false;

        private ConsoleWrapper()
        {
            Console.SetBufferSize(32, 16);

            Width = Console.BufferWidth;
            Height = Console.BufferHeight;

            MapLayer = new ConsoleChar[Width, Height];
            InterfaceLayer = new ConsoleChar[Width, Height];

            _commands = new Dictionary<ConsoleKeyInfo, ICommand>();
            //Add commands
            
            _typeChars = new Dictionary<TileType, char>();
            //Add chars for types
            _typeChars.Add(TileType.Land, '.');
            _typeChars.Add(TileType.Rock, 'R');
            _typeChars.Add(TileType.Start, 'S');
            _typeChars.Add(TileType.Finish, 'F');
        }

        public void StartCycle()
        {
            bool loaded = false;
            while (!loaded)
            {
                //Load file
                Console.Clear();
                Console.WriteLine("1. Create new file");
                Console.WriteLine("2. Open file");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    if (choice != 1 && choice != 2)
                    {
                        continue;
                    }

                    if (choice == 1)
                    {
                        Console.WriteLine("Enter file name: ");
                        string fileName = Console.ReadLine();
                        File.CreateText(fileName).Dispose();

                        Console.WriteLine("Enter map Width: ");
                        int width = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter map Height: ");
                        int height = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter map Name: ");
                        string name = Console.ReadLine();

                        FileName = fileName;
                        loaded = true;
                        CreateEmpty(width, height, name);
                    }
                    else
                    {
                        Console.WriteLine("Enter file name: ");
                        string fileName = Console.ReadLine();
                        
                        if (!File.Exists(fileName))
                        {
                            Console.WriteLine("No such file found!");
                            continue;
                        }

                        FileName = fileName;
                        loaded = true;
                        LoadFromFile(FileName);
                    }
                }
                catch
                {
                    continue;
                }
            }

            Console.Clear();


            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }


                ProcessInput(keyInfo);
            }
        }

        public void ProcessInput(ConsoleKeyInfo keyInfo)
        {
            if (_commands.ContainsKey(keyInfo))
            {
                _commands[keyInfo].Call();
            }
        }

        public void CreateEmpty(int width, int height, string mapName)
        {
            Width = width;
            Height = height;
            MapName = mapName;

            Tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y] = new Tile() { X = x, Y = y, Type = TileType.Land };
                }
            }
        }
        public void LoadFromFile(string fileName)
        {
            JObject jObject = JObject.Parse(File.ReadAllText(fileName));

            Width = (int)jObject["Width"];
            Height = (int)jObject["Height"];
            MapName = (string)jObject["MapName"];

            Tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y] = new Tile() { X = x, Y = y, Type = (TileType)Enum.Parse(typeof(TileType), jObject[x.ToString() + "|" + y.ToString()]["Type"].ToString()) };
                }
            }
        }

        public void SaveToFile(string fileName)
        {
            JObject jObject = new JObject();
            jObject.Add("Width", Width);
            jObject.Add("Height", Height);
            jObject.Add("MapName", MapName);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    string pos = x.ToString() + "|" + y.ToString();

                    JObject jTile = new JObject();
                    jTile.Add("Type", Tiles[x, y].Type.ToString());

                    jObject.Add(pos, jTile);
                }
            }

            File.WriteAllText(fileName, jObject.ToString());
        }

        public void DrawTilesToLayer()
        {
            foreach (var tile in Tiles)
            {
                MapLayer[tile.X, tile.Y].Char = _typeChars[tile.Type];
            }
        }
        public void ClearLayer(ConsoleChar[,] layer)
        {
            foreach (var consoleChar in layer)
            {
                consoleChar.Reset();
            }
        }

        public void Draw()
        {
            Console.Clear();

            foreach (var mapChar in MapLayer)
            {
                mapChar.Draw();
            }

            foreach (var interfaceChar in InterfaceLayer)
            {
                interfaceChar.Draw();
            }
        }

        public static ConsoleWrapper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConsoleWrapper();
            }

            return _instance;
        }
    }

    public enum TileType
    {
        Land,
        Rock,
        Start,
        Finish
    }
}
