using System;
using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TextGame
{
    // Console.BackgroundColor = ConsoleColor.Red; 데미지 입었을 때 화면이 빨간색으로 변하면 재밌겠다
    internal class Program
    {
        private static Character player;
        private static Item[] items;

        // 메인 실행
        static void Main()
        {
            GameDataSetting();
            GameLogo();
            DisplayGameIntro();
        }

        // 각종 데이터
        static void GameDataSetting()
        {
            // 다시 메인화면으로 돌아왔을 때를 위한 초기화
            Item.ItemCount = 0;
            player = null;

            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 0 = 무기 1 = 대가리 2 = 갑옷 3 = 신발
            // 아이템 정보 세팅
            items = new Item[10];
            AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", Item.ItemType.Body, 0, 5, 0));
            AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", Item.ItemType.Weapon, 2, 0, 0));
            AddItem(new Item("고양이 수염", "고양이 수염은 행운을 가져다 줍니다. 야옹!", Item.ItemType.Acc, 7, 7, 7));
        }

        // 아이템 추가
        static void AddItem(Item item)
        {
            if (Item.ItemCount == 10) return; // 아이템이 꽉차면 아무일도 일어나지 않는다
            items[Item.ItemCount] = item; // 0개 -> items[0], 1개 -> items[1] ...
            Item.ItemCount++;
        }

        // 게임로고
        static void GameLogo()
        {
            Console.Clear();
            // 아스키 코드로 이루어진 타이틀 화면을 위한 인코딩 설정
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "= Sparta Dungeon =";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                                                                               \r\n ####                  #          ######                                       \r\n##  #                 ##           ##  ##                                      \r\n###   ###   ##   ## # ###  ##      ##  ## ## ## # ##    ####  ###  ###  # ##   \r\n ###  ## # # ##  #### ##  # ##     ##  ## ## #  ## ##  ## #  ## # ## ## ## ##  \r\n  ### ## #  ###  ##   ##   ###     ##  ## ## #  ## ##  ## #  #### ## ## ## ##  \r\n#  ## ## # #  #  ##   ##  #  #     ##  ## ## #  ## ##   ###  ##   ## ## ## ##  \r\n####  ###  ##### ##    ## #####   ######   ###  ## ### ##     ###  ###  ## ### \r\n      ##                                               ####                    \r\n      ###                                              #  #                    \r\n                                                       ####                    ");
            Console.WriteLine();
            Console.WriteLine("          ===== 계속하려면 아무 키나 입력하십시오. =====          ");
            Console.ResetColor();
            Console.ReadKey();
            Console.Beep();
        }

        // 스타트
        static void DisplayGameIntro()
        {
            Console.Clear();
            Console.Title = "= Sparta Village =";

            LineTextColor("==================================================");
            string startTxt = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n";
            OutputTxt(startTxt);
            LineTextColor("==================================================");
            Console.WriteLine();
            ChooseTextColor("1. 상태보기\n2. 인벤토리\n3. 상    점\n0. 메인화면");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    Console.Beep();
                    Main();
                    break;
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
            }
        }

        // 상태창
        static void DisplayMyInfo()
        {
            Console.Clear();
            Console.Title = "= Information =";

            ChooseTextColor("= 상태보기 =");
            LineTextColor("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            StatTextColor("Lv. ", player.Level.ToString("00")); // 00, 07 등 한자릿수도 두자릿수로 표현하기 위해 string 타입으로 변환
            Console.WriteLine();
            Console.WriteLine("{0} ( {1} )", player.Name, player.Job);

            int itemStatAtk = itemStatSumAtk();
            int itemStatDef = itemStatSumDef();
            int itemStatHp = itemStatSumHp();
            StatTextColor("공격력 : ", (player.Atk + itemStatAtk).ToString(), itemStatAtk > 0 ? string.Format(" (+{0})", itemStatAtk) : "");
            StatTextColor("방어력 : ", (player.Def + itemStatDef).ToString(), itemStatDef > 0 ? string.Format(" (+{0})", itemStatDef) : "");
            StatTextColor("체 력 : ", player.Hp.ToString(), itemStatHp > 0 ? string.Format(" (+{0})", itemStatHp) : "");
            StatTextColor("Gold : ", player.Gold.ToString());
            Console.WriteLine();
            ChooseTextColor("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        // 장비 능력치 더하기
        private static int itemStatSumAtk()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCount; i++)
            {
                if (items[i].IsEquipped) sum += items[i].AtkOption;
            }
            return sum;
        }
        private static int itemStatSumDef()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCount; i++)
            {
                if (items[i].IsEquipped) sum += items[i].DefOption;
            }
            return sum;
        }
        private static int itemStatSumHp()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCount; i++)
            {
                if (items[i].IsEquipped) sum += items[i].HpOption;
            }
            return sum;
        }

        // 인벤토리
        static void DisplayInventory()
        {
            Console.Clear();
            Console.Title = "= inventory =";

            ChooseTextColor("= 인벤토리 =");
            LineTextColor("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            ChooseTextColor("[아이템 목록]");

            for (int i = 0; i < Item.ItemCount; i++)
            {
                items[i].ItemStat();
            }
            Console.WriteLine("");
            ChooseTextColor("1. 장착관리\n0. 나가기");
            Console.WriteLine();
            Console.ResetColor();
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
            }
        }

        // 장착 관리
        static void DisplayerEquip()
        {
            Console.Clear();
            Console.Title = "= Item Equip =";

            ChooseTextColor("= 장착관리 =");
            LineTextColor("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            ChooseTextColor("[아이템 목록]");
            for (int i = 0; i < Item.ItemCount; i++)
            {
                items[i].ItemStat(true, i + 1);
            }
            Console.WriteLine();
            ChooseTextColor("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, Item.ItemCount);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                default:
                    ToggleEquipStatus(input - 1); // 유저가 입력하는건 1, 2, 3...  실제 배열에는 0, 1, 2...
                    DisplayerEquip();
                    break;
            }
        }

        // 장비 장착 여부
        private static void ToggleEquipStatus(int index)
        {
            items[index].IsEquipped = !items[index].IsEquipped; // ! : 불형의 변수를 반대로 만들어주는 것
        }

        // 상점
        static void DisplayShop()
        {
            Console.Clear();
            Console.Title = "= Item Shop =";

            ChooseTextColor(" = 상   점 = ");
            Console.WriteLine();
            // 한글자씩 출력
            Console.ForegroundColor = ConsoleColor.Cyan;
            string shopTxt = "상점이 있던 곳이다.\n그러나 상점은 온데간데 없고 부서진 건물의 흔적만 있을 뿐이다...\n나중에 다시 오자.\n";
            OutputTxt(shopTxt);
            Console.WriteLine();
            ChooseTextColor("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        // 선택지 메서드
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
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
            }
        }

        // 한글자씩 출력
        static void OutputTxt(string txt) // string = Char(문자) 배열
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            // 아무 키나 눌렀을 때 스킵
            int speed = 50;
            int txtCount = 0; // 글자수
            while (txtCount != txt.Length) // txt 글자수가 끝까지 나올 때까지
            {
                if (Console.KeyAvailable) // 아무 키나 눌렸을 때 true
                {
                    speed = 0;
                    Console.ReadKey(true);
                }
                Console.Write(txt[txtCount]); // 글자를 하나하나 가져올 인덱스
                Thread.Sleep(speed);
                txtCount++;
            }
        }

        // 설명글씨색
        private static void LineTextColor(string line)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        // 선택지글씨색
        private static void ChooseTextColor(string line)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        // 스텟글씨색
        private static void StatTextColor(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
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
        // 장비 타입
        public enum ItemType
        {
            Weapon,
            Head,
            Body,
            Shoes,
            Acc
        }

        public string Name { get; }
        public string Description { get; }
        public ItemType Type { get; }
        public int AtkOption { get; }
        public int DefOption { get; }
        public int HpOption { get; }
        public bool IsEquipped { get; set; }

        public static int ItemCount = 0; // static을 붙임으로 Item이라는 클래스에 귀속된다

        public Item(string name, string description, ItemType type, int atkOption, int defOption, int hpOption, bool isEquipped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            AtkOption = atkOption;
            DefOption = defOption;
            HpOption = hpOption;
            IsEquipped = isEquipped;
        }

        // 장비 착용 표시
        public void ItemStat(bool equippedItem = false, int index = 0) // 기본값이 false
        {
            Console.Write("- ");
            if (equippedItem)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("{0} ", index); // 몇 번째 아이템인가
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.WriteLine(PadRightForMixedText(Name, 9)); // 방어력, 공격력 옵션들 간격맞춤
            }
            else
            Console.WriteLine(PadRightForMixedText(Name, 12));
            Console.Write(" | ");

            if (AtkOption != 0) Console.Write($"공격력 {(AtkOption >= 0 ? "+" : "")}{AtkOption} "); // 삼항연산자
            if (DefOption != 0) Console.Write($"방어력 {(DefOption >= 0 ? "+" : "")}{DefOption} "); // 옵션이 0이 아니라면 옵션수치를 내보내라
            if (HpOption != 0) Console.Write($"체 력 {(HpOption >= 0 ? "+" : "")}{HpOption} "); // [조건 ? 참 : 거짓]

            Console.Write(" | ");

            Console.WriteLine(Description);
        }

        // 정렬
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }
        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str); // 텍스트의 길이를 구한다
            int padding = totalLength - currentLength; // 원하는 크기과 실제 텍스트의 길이를 계산한다
            return str.PadRight(str.Length + padding); // 필요한 길이만큼의 공간을 더해준다
        }
    }
}