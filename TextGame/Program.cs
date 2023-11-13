using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TextGame
{
    // Console.BackgroundColor = ConsoleColor.Red; 데미지 입었을 때 화면이 빨간색으로 변하면 재밌겠다
    internal class Program
    {
        private static Character player;
        private static Item item;

        static void Main(string[] args)
        {
            DisplayMain();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            item = new Item("무쇠갑옷", "낡은 검", 5, 2);
        }

        static void DisplayMain()
        {
            Console.Clear();
            // 아스키 코드로 이루어진 타이틀 화면을 위한 인코딩 설정
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "= Sparta Dungeon =";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                                                                               \r\n ####                  #          ######                                       \r\n##  #                 ##           ##  ##                                      \r\n###   ###   ##   ## # ###  ##      ##  ## ## ## # ##    ####  ###  ###  # ##   \r\n ###  ## # # ##  #### ##  # ##     ##  ## ## #  ## ##  ## #  ## # ## ## ## ##  \r\n  ### ## #  ###  ##   ##   ###     ##  ## ## #  ## ##  ## #  #### ## ## ## ##  \r\n#  ## ## # #  #  ##   ##  #  #     ##  ## ## #  ## ##   ###  ##   ## ## ## ##  \r\n####  ###  ##### ##    ## #####   ######   ###  ## ### ##     ###  ###  ## ### \r\n      ##                                               ####                    \r\n      ###                                              #  #                    \r\n                                                       ####                    ");
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine("                         1. 게임시작");
            Console.WriteLine("                         2. 불러오기");
            Console.WriteLine("                         0. 나가기");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                 원하시는 행동을 입력해주세요.");
            Console.Write("                 >> ");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Console.Beep();
                    break;
                case 1:
                    DisplayGameIntro();
                    break;
                case 2:
                    Console.Beep();
                    Console.WriteLine("튜터님을 불러옵니다.\n이거 어케해요?");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }

        static void DisplayGameIntro()
        {
            GameDataSetting();
            Console.Clear();
            Console.Title = "= Sparta Village =";

            Console.ForegroundColor = ConsoleColor.Cyan;
            string txt = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.";
            // 한글자씩 출력
            for (int i = 0; i < txt.Length; i++)
            {
                Console.Write(txt[i]);
                Thread.Sleep(100);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n0. 메인화면");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Console.Beep();
                    DisplayMain();
                    break;
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();
            Console.Title = "= Information =";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();
            Console.Title = "= inventory =";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");
            Console.Write("- " + $"{item.Armor}\t");
            Console.WriteLine("| 방어력 " + $"{item.DefOption}" + " | 무쇠로 만들어져 튼튼한 갑옷입니다.");
            Console.Write("- " + $"{item.Weapon}\t");
            Console.WriteLine("| 공격력 " + $"{item.AtkOption}" + " | 쉽게 볼 수 있는 낡은 검 입니다.");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayerEquip();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }

        static void DisplayerEquip()
        {
            Console.Clear();
            Console.Title = "= Equip =";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");
            Console.Write("- " + $"{item.Armor}\t"); // 배열? 리스트?
            Console.WriteLine("| 방어력 " + $"{item.DefOption}" + " | 무쇠로 만들어져 튼튼한 갑옷입니다.");
            Console.Write("- " + $"{item.Weapon}\t");
            Console.WriteLine("| 공격력 " + $"{item.AtkOption}" + " | 쉽게 볼 수 있는 낡은 검 입니다.");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string Armor { get; }
        public string Weapon { get; }
        public int AtkOption { get; }
        public int DefOption { get; }

        public Item(string armor, string weapon, int atkoption, int defoption)
        {
            Armor = armor;
            Weapon = weapon;
            AtkOption = atkoption;
            DefOption = defoption;
        }
    }
}