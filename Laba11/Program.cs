using System.Collections;
using l10;
using Laba10;

namespace Laba11
{
    class Program
    {
        private const int STANDARD_OIL = 10;
        
        public static void Main()
        {
            Task1();
            Task2();
            Task3();
        }


        public static void Task1()
        {
            Queue ochered = new Queue();
            for (int i = 0; i < STANDARD_OIL; i++)
            {
                ochered.Enqueue(new VRGame("Игра" + (i + 1), 1, 15, new Game.IdNumber(i + 1), Device.Mobile, 15, true, true));
            }
            
            Request1Wrapper(ochered);
            Request2Wrapper(ochered);
            Request3Wrapper(ochered);

            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();
            
            Console.WriteLine("Вывод элементов");

            foreach (var item in ochered)
            {
                Game game = (Game) item;
                game.ShowVirtual();
                Console.WriteLine("------------------");
            }
            
            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();

            Console.WriteLine("Произошло клонирование....");
            Queue newOchered = (Queue)ochered.Clone();
            
            //ХЗ КАК СОРТИРОВАТЬ ОЧЕРЕДЬ

            VideoGame searchElement = new VRGame("Игра1", 1, 15, new Game.IdNumber(1), Device.Mobile, 15, true, true);

            foreach (var item in ochered)
            {
                if (item.Equals(searchElement) && item is Game)
                {
                    Game game = (Game) item;
                    game.ShowVirtual();
                    break;
                }
            }
        }
        
        static IEnumerable<Game> Request1(IEnumerable collection, uint minGreaterOrEqual)
        {
            foreach (var item in collection)
            {
                Game game = (Game) item;
                if (game.MinimumPlayers >= minGreaterOrEqual && minGreaterOrEqual <= game.MaximumPlayers)
                {
                    yield return game;
                }
            }
        }

        static void Request1Wrapper(IEnumerable collection)
        {
            uint minGreaterOrEqual = Helpers.Helpers.EnterUInt("Минимальное количество игроков", 1, uint.MaxValue);

            Console.WriteLine($"Игры, минмальное количество игроков >= {minGreaterOrEqual.ToString()}");
            foreach (Game game in Request1(collection, minGreaterOrEqual))
            {
                game.ShowVirtual();
                Console.WriteLine("-----------------------");
            }
        }

        static IEnumerable<Game> Request2(IEnumerable collection, Device device)
        {
            foreach (var item in collection)
            {
                Game game = (Game) item;
                if (game is VideoGame videoGame && videoGame.Device == device)
                {
                    yield return game;
                }
            }
        }

        static void Request2Wrapper(IEnumerable collection)
        {
            Console.WriteLine();
            uint deviceType = Helpers.Helpers.EnterUInt("тип устройства: 1 - телефон, 2 - пк, 3 - игровая консоль", 1, 3);
            Device device = Device.Mobile;

            if (deviceType == 1)
            {
                device = Device.Mobile;
            }
            else if (deviceType == 2)
            {
                device = Device.Pc;
            }
            else if (deviceType == 3)
            {
                device = Device.Console;
            }

            Console.WriteLine($"\nИгры, в которые можно играть на {device}");
            foreach (Game game in Request2(collection, device))
            {
                Console.WriteLine(game.Name);
            }
        }

        static IEnumerable<Game> Request3(IEnumerable collection)
        {
            foreach (var item in collection)
            {
                Game game = (Game) item;
                if (game is VRGame vr && vr.AreVRGlassesRequired)
                {
                    yield return game;
                }
            }
        }

        static void Request3Wrapper(IEnumerable collection)
        {
            Console.WriteLine($"\nИгры, в которые можно играть в vr очках");
            foreach (Game game in Request3(collection))
            {
                Console.WriteLine(game.Name);
            }
        }

        public static void Task2()
        {
            List<VideoGame> list = new List<VideoGame>();
            for (int i = 0; i < STANDARD_OIL; i++)
            {
                list.Add(new VRGame("Игра" + (i + 1), 1, 15, new Game.IdNumber(i + 1), Device.Mobile, 15, true, true));
            }
            
            Request1Wrapper(list);
            Request2Wrapper(list);
            Request3Wrapper(list);

            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();
            
            Console.WriteLine("Вывод элементов");

            foreach (var item in list)
            {
                Game game = (Game) item;
                game.ShowVirtual();
                Console.WriteLine("------------------");
            }
            
            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();

            Console.WriteLine("Произошло клонирование....");
            //клонирование через цикл foreach, так как метод Clone для List не определен
            List<VideoGame> newList = new List<VideoGame>();
            foreach (var videoGame in list)
            {
                newList.Add(videoGame);
            }
            
            list.Sort();

            VideoGame searchElement = new VRGame("Игра1", 1, 15, new Game.IdNumber(1), Device.Mobile, 15, true, true);
 
            foreach (var item in list)
            {
                if (item.Equals(searchElement) && item is Game)
                {
                    Game game = (Game) item;
                    game.ShowVirtual();
                    break;
                }
            }
        }

        public static void Task3()
        {
            var testColletions = new TestCollections();
        }
        
        
    }
}
