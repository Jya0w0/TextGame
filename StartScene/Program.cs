namespace StartScene
{
    class Player
    {
        public void Start()
        {
            string startTxt = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.";
            string startChoose = "1. 상태 보기\n2. 인벤토리";
            string startQuestion = "원하시는 행동을 입력해주세요.";
            // 한글자씩 출력
            //float outputTime = 0.2f;
            //for (int i = 0; i < startTxt.Length; i++)
            //{
            //    Console.Write(startTxt[i]);
            //}
            Console.WriteLine(startTxt);
            Console.WriteLine();
            Console.WriteLine(startChoose);
            Console.WriteLine();
            Console.WriteLine(startQuestion);
            Console.Write(">> ");
        }

        public void Stat(Player player)
        {
            int lv = 01;
            string job = "Chad ( 전사 )";
            int att = 10;
            int def = 5;
            int hp = 100;
            int gold = 1500;

            Console.WriteLine("Lv. " + lv);
            Console.WriteLine(job);
            Console.WriteLine("공격력 : " + att);
            Console.WriteLine("방어력 : " + def);
            Console.WriteLine("체 력 : " + hp);
            Console.WriteLine("Gold : " + gold + " G");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            // Start
            player.Start();

            // 스타트 선택지
            string choose = Console.ReadLine();
            int chooseNum = int.Parse(choose);

            Console.Clear();

            switch (chooseNum)
            {
                case 1 : Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.");
                    Console.WriteLine();
                    player.Stat(player);
                    if (chooseNum == 0)
                    {

                    }
                    break;
                case 2: Console.WriteLine();
                    break;
                default: Console.WriteLine();
                    break;
            }
            
        }
    }
}