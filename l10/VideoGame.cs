using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using l10;

namespace Laba10
{
    public enum Device
    {
        Mobile,
        Pc,
        Console
    }
    public class VideoGame : Game
    {
        public Device Device { get; private set; }

        public string DeviceName
        {
            get
            {
                if (Device == Device.Mobile) return "Телефон";
                else if (Device == Device.Pc) return "ПК";
                else if (Device == Device.Console) return "Игровая консоль";
                return "Неизвестный";
            }
        }

        //добавляем создание базового класса
        public Game Base => new Game(Name, MinimumPlayers, MaximumPlayers, new IdNumber(Id.Number));

        private uint _levelCount { get; set; }
        public uint LevelCount
        {
            get => _levelCount;
            private set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Игра совсем без уровней бессмыслена");
                }
                else
                {
                    _levelCount = value;
                }
            }
        }

        public VideoGame() : base()
        {
            LevelCount = 1;
            Device = Device.Mobile;
        }

        public VideoGame(string name, uint minimumPlayers, uint maximumPlayers, IdNumber id, Device device, uint levelsCount) : base(name, minimumPlayers, maximumPlayers, id)
        {
            LevelCount = levelsCount;
            Device = device;
        }

        public VideoGame(VideoGame game) : base(game)
        {
            LevelCount = game.LevelCount;
            Device = game.Device;
        }

        public new void Show()
        {
            Console.WriteLine($"" +
                $"Тип устройства = {DeviceName}; " +
                $"Количество уровней {LevelCount}; "
                );
        }

        public override void ShowVirtual()
        {
            base.ShowVirtual();
            Show();
        }

        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            base.Init();

            uint deviceType = Helpers.Helpers.EnterUInt("Выберите тип устройства: 1 - телефон, 2 - пк, 3 - игровая консоль", 1, 3);

            if (deviceType == 1)
            {
                Device = Device.Mobile;
            }
            else if (deviceType == 2)
            {
                Device = Device.Pc;
            }
            else if (deviceType == 3)
            {
                Device = Device.Console;
            }

            LevelCount = Helpers.Helpers.EnterUInt("Количество уровней", 1);
        }

        public override void RandomInit()
        {
            base.RandomInit();

            Random rnd = Helpers.Helpers.GetOrCreateRandom();

            int deviceType = rnd.Next(1, 4);
            if (deviceType == 1)
            {
                Device = Device.Mobile;
            }
            else if (deviceType == 2)
            {
                Device = Device.Pc;
            }
            else if (deviceType == 3)
            {
                Device = Device.Console;
            }
            LevelCount = (uint)rnd.Next(1, int.MaxValue);
        }

        public override bool Equals(object? obj)
        {
            bool parentEqual = base.Equals(obj);
            return parentEqual
                && (obj is VideoGame videoGame)
                && LevelCount == videoGame.LevelCount
                && Device == videoGame.Device;

        }

        public new object Clone() // метод клонирования объектов (ICloneable)
        {
            return new VideoGame(Name, MinimumPlayers, MaximumPlayers, new IdNumber(Id.Number), Device, LevelCount);
        }


    }

}
