

using Elonmusk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{

    public class SpartaMusumeLobby : GameLobby
    {
        public override void ShowInfo()
        {
            PrintGameTitle();
            Console.WriteLine();
            Console.WriteLine("1. 게임 설명을 듣는다.");
            Console.WriteLine("2. 게임을 플레이 하러 간다.");
            Console.WriteLine("0. 돌아간다");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Casino());
                    break;
                case 1:
                    Game.game.ChangeScene(new SpartaMusumeInfo());
                    break;
                case 2:
                    Game.game.ChangeScene(new SpartaMusumeGame());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        public static void PrintGameTitle()
        {
            Console.WriteLine(
@"
MP""""""""""""`MM MM""""""""""""""`YM MMP""""""""""""""MM MM""""""""""""""`MM M""""""""""""""""M MMP""""""""""""""MM 
M  mmmmm..M MM  mmmmm  M M' .mmmm  MM MM  mmmm,  M Mmmm  mmmM M' .mmmm  MM 
M.      `YM M'        .M M         MM M'        .M MMMM  MMMM M         MM 
MMMMMMM.  M MM  MMMMMMMM M  MMMMM  MM MM  MMMb. ""M MMMM  MMMM M  MMMMM  MM 
M. .MMM'  M MM  MMMMMMMM M  MMMMM  MM MM  MMMMM  M MMMM  MMMM M  MMMMM  MM 
Mb.     .dM MM  MMMMMMMM M  MMMMM  MM MM  MMMMM  M MMMM  MMMM M  MMMMM  MM 
MMMMMMMMMMM MMMMMMMMMMMM MMMMMMMMMMMM MMMMMMMMMMMM MMMMMMMMMM MMMMMMMMMMMM 
     d888ba.88ba  dP     dP .d88888b   dP     dP  8888ba.88ba   88888888b 
     88  `8b  `8b 88     88 88.     '  88     88  88  `8b  `8b  88        
     88   88   88 88     88 `Y88888b.  88     88  88   88   88  888888  
     88   88   88 88     88       `8b  88     88  88   88   88  88        
     88   88   88 Y8.   .8P d8'   .8P  Y8.   .8P  88   88   88  88        
d88888P   dP   dP `Y88888P'  Y88888P   `Y88888P'  dP   dP   dP  8888888888b
");
        }
    }

    public class SpartaMusumeInfo : Scene
    {
        public override void ShowInfo()
        {
            SpartaMusumeLobby.PrintGameTitle();
            Console.WriteLine();
            Console.WriteLine("스파르타 무스메는 5마리의 열정 넘치는 경기닭들 중에서 승리하는 닭을 맞추는 게임입니다.");
            Console.WriteLine("승리하는 닭을 맞추면 베팅한 금액의 다섯 배를 얻습니다.");
            Console.WriteLine("반대로 맞추지 못했을 경우에는 베팅한 금액을 모두 잃습니다.");
            Console.WriteLine("각 닭들의 특징은 다음과 같습니다.");
            Console.WriteLine();
            Console.WriteLine("첫째 닭은 성격이 급해서 처음부터 빠르게 달리고, 이후 점점 지쳐 천천히 달립니다.");
            Console.WriteLine("둘째 닭은 눈치를 봐서 다른 닭들의 평균 속도보다 살짝 빠르게 달립니다.");
            Console.WriteLine("셋째 닭은 차분하게 처음부터 끝까지 꾸준한 속도로 달립니다.");
            Console.WriteLine("넷째 닭은 제멋대로라 기분에 따라 달립니다. 정말 빠르게 달릴 수도, 정말 천천히 달릴 수도 있습니다.");
            Console.WriteLine("다섯째 닭은 노력가여서 처음에 천천히 달리다가 점점 더 빠르게 달립니다.");
            Console.WriteLine();
            Console.WriteLine("0. 돌아간다");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new SpartaMusumeLobby());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }

    public class SpartaMusumeGame : Scene
    {

        bool isRunning = false;
        public const int laneLength = 200000;

        // Horse는 1-5까지 사용
        List<Horse> horses = new List<Horse>();

        int betHorseNum = 0;
        int betGold = 0;
        int winHorse = 0;

        static int WinStreak = 0;

        public override void ShowInfo()
        {
            Console.Clear();
            printHorseLane();
            Console.WriteLine();
            Console.WriteLine("먼저 배팅할 닭을 지정해주세요 (1-5번, 0을 입력하면 돌아갑니다)");
            Console.WriteLine();

            // 배팅할 말 설정
#if TEST
            Test(100000);
#endif
        }

        public override void GetAction(int act)
        {
            if (!isRunning)
            {
                switch (act)
                {
                    case 0:
                        Game.game.ChangeScene(new SpartaMusumeLobby());
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        this.betHorseNum = act;
                        Bet();
                        break;
                    default:
                        Console.WriteLine("유효한 입력이 아닙니다!");
                        break;
                }
            }
        }


        void Bet() 
        {
            // 배팅할 금액 설정
            Console.Clear();
            printHorseLane();
            Console.WriteLine();
            Console.WriteLine($"현재 소지 금액 {Game.game.player.GOLD}");
            Console.WriteLine("배팅할 금액을 입력해주세요. (0을 입력하면 나갑니다)");
            Console.WriteLine();
            Console.WriteLine("원하시는 금액을 선택해주세요.");
            Console.Write(">> ");

            int bet = -1;
            do
            {
                bet = Game.GetPlayerInputInt();
                if (bet > Game.game.player.GOLD)
                {
                    Console.WriteLine("입력한 금액이 보유하고 있는 금액보다 많습니다!");
                    bet = -1;
                }
            }
            while (bet < 0);

            if (bet == 0) 
            {
                betGold = 0;
                betHorseNum = 0;
            }
            else
            {
                Game.game.player.ConsumeGold(bet);
                betGold = bet;
                PlayHorseRacing();
            }
        }

#if TEST
        void Test(int test) 
        {
            int[] winHorese = new int[5];

            for(int i=0; i< test; i++) 
            {
                HorseRacing();
                winHorese[winHorse-1]++;
            }

            Console.WriteLine($"{winHorese[0]},{winHorese[1]},{winHorese[2]},{winHorese[3]},{winHorese[4]}");

            int act = Game.GetPlayerInputInt();
        }
#endif
        void PlayHorseRacing() 
        {
            Console.WriteLine("경닭 시작!");

            Thread thread = new Thread(new ThreadStart(HorseRacing));
            thread.Start();
            thread.Join();

            Console.WriteLine();
            Console.WriteLine($"경기 종료!! 승리한 닭은 {winHorse}번닭입니다.");

            if(betHorseNum == winHorse) 
            {
                Console.WriteLine($"맞추셨습니다! {betGold * 5}만큼 골드를 받습니다.");
                Game.game.player.gainGold(betGold * 5);
                CasinoData.casinoData.horseRacingAchivement._winGold += betGold * 5;
                betGold = 0;
            }
            else 
            {
                Console.WriteLine($"승리한 닭을 맞추지 못했습니다. 배팅한 금액을 잃습니다.");
                CasinoData.casinoData.horseRacingAchivement._loseGold += betGold;
                betGold = 0;
            }

            RegameOrExit();
        }

        void RegameOrExit() 
        {
            Console.WriteLine();
            Console.WriteLine("1. 다시하기");
            Console.WriteLine("0. 로비로 돌아간다");

            int act = -1;
            do
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 선택해주세요.");
                Console.Write(">> ");
                act = Game.GetPlayerInputInt();
            }
            while (act != 1 && act != 0);

            if (act == 1)
                Bet();
            else if (act == 0)
                Game.game.ChangeScene(new SpartaMusumeLobby());
        }

        void printHorseLane()
        {
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("wvwwwvwvwvwvwvwvwvwvwwvwvwvwwwvwvwvwvwvwvwwvvwwvvwwvvwwvwwvwvwvwvwvwvwvwvwwvwvwv");
            Console.WriteLine("wwwwvwwvwvwvwvwvwvwvwwvwvwvwvwvwvwvwvwvwvwvvvvwwwwvwwvwwwvwwvwwwvwvwwvwvwwwwwwwv");
            Console.WriteLine("wvvwvwvwvvvwvwwwvwwvwvwwwvwvwwwvwvwvwvwvwwwwvvvwvwwwvwvwwwwvvwwwvwvwwwvwwvwvwwvw");
            Console.WriteLine("wwwwvwwvwvwvwvwvwvwvwwvwvvwvwvwvwwvwvwvwvwvwvwvwvvvvwwwwvwwvwwwvwwvwwwvwvwwwwwwv");

            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("================================================================================");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("wvwwwvwvwvvwvwwwvwvwvwwwwvvwvwwwvwwwvwvwvwvwvwwwvwvwvwwwvwvwwvwvwwvwvwvwwvwwvvww");
            Console.WriteLine("wvvwvwvwvvvwvvwwwvwvwvwwwvwvwwwvwvwwwvwvwvwvwvwwwwvwvwvwwwwvvwwwvwvwwwvwwvwvwwvw");
            Console.WriteLine("wwwwvwwvwvwvwvwvwvwvvwvwwwwvwvwvvwvwvwvwvwvwvwvwvvvvwwwwvwwvwwwvwwvwwwvwvwwwwwwv");
            Console.WriteLine("wvvwvwvwvvvwvwwwvwwvwvwwvwvwwwvwvwvwwvwvwvwvwvwwwwvwvwvwwwwvvwwwvwvwwwvwwvwvwwvw");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================================================================");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("vwvwwwvwwwvwvwvwvwwwvwvwwwvwvwvwwwvwwvwvwvwwvwwvwvwwvwwwvwwvwvwvwwwvwwwvvwvwwvwv");
            Console.WriteLine("wvwwwvwvwvwvwwwvwvwvwwwwvwvwwwvwvwvwvwvwwvwwvwwwvwvwvwwwvwvwwvwvwwvwvwvwwvwwvvww");
            Console.WriteLine("wwwwvwwvwvwvwvwvwvwvwwvwvwvwvwvwvwwvwwvwvwvwvwvwvvvvwwwwvwwvwwwvwwvwwwvwvwwwwwwv");
            Console.WriteLine("wvwwwvwvwvwvwwwvwwvwwvwvwvwwwwvwvwwwvwvwvwvwvwwwvwvwvwwwvwvwwvwvwwvwvwvwwvwwvvww");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================================================================");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("wwwwvwwvwvwvwvwvwvwvwwvwvwvwvwwvwwvwvwvwvwvwvwvwvvvvwwwwvwwvwwwvwwvwwwvwvwwwwwwv");
            Console.WriteLine("wvvwvwvwvvvwvwwwvwwvwwvwwvwvwwwvwvwwwvwvwvwvwvwwwwvwvwvwwwwvvwwwvwvwwwvwwvwvwwvw");
            Console.WriteLine("wvwwwvwvwvwvwvwwvwwvwvwvwvwwvwvwvwwwvwvwvwvwvwvwwvvwwvwvwvwvwvwvwvwvwvwwvwvwvwvw");
            Console.WriteLine("wvvwvwvwvvvwvwwwvwwvwvwwwwvwwvwvwvwwwvwvwvwvwvwwwwvwvwvwwwwvvwwwvwvwwwvwwvwvwwvw");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================================================================");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("wvvwvwvwvvvwvwwwvwwvwvwwwvwvwwwvwvwvwwvwwvwvwvwwwwvwvwvwwwwvvwwwvwvwwwvwwvwvwwvw");
            Console.WriteLine("wvwwwvwvwvwvwwwvwvwvwwwwvwvwwwvwvwwvwwvwvwvwvwwwvwvwvwwwvwvwwvwvwwvwvwvwwvwwvvww");
            Console.WriteLine("wvwwwvwvwvwvwvwvwwvwwvwvwvwwvwvwvwwwvwvwvwvwvwvwwvvwwvwvwvwvwvwvwvwvwvwwvwvwvwvw");
            Console.WriteLine("wvwwwvwvwvwvwwwvwvwvwwwwvwvwwwvwvwvwwvwwvwvwvwwwvwvwvwwwvwvwwvwvwwvwvwvwwvwwvvww");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.ResetColor();

            printHorses();
        }

        void printHorses() 
        {
            int origin_x = Console.CursorLeft;
            int origin_y = Console.CursorTop;
            if(horses.Count == 0) 
            {
                for (int i = 0; i < 5; i++)
                {
                    // x 위치는 length 비례 : 총 74칸
                    // length / laneLength * 74;
                    double position = 0;
                    Console.SetCursorPosition((int)position, i * 5 + 1);
                    Horse dummyHorse = new Horse(0);
                    dummyHorse.PrintChicken(-1);
                }
            }
            else 
            {
                for(int i=0; i<horses.Count; i++) 
                {
                    // x 위치는 length 비례 : 총 67칸
                    // length / laneLength * 67;
                    double position = ((double)horses[i].length / laneLength) * 68;
                    Console.SetCursorPosition((int)position, i * 5 + 1);
                    horses[i].PrintChicken((int)position % 4);
                }
            }
            Console.SetCursorPosition(origin_x, origin_y);
        }

        void HorseRacing() 
        {
            InitializeHorses();
            bool win = false;
            int renderCnt = 0;
            int timeCnt = 0;
            List<int> winHorsesIndexes = new List<int>();

            while(!win) 
            {
#if !TEST
                renderCnt++;
                if(renderCnt > 10) 
                { 
                    Console.Clear();
                    printHorseLane();
                    renderCnt = 0;
                }
#endif
                foreach (var horse in horses) 
                {
                    horse.Run();
#if TEST
                    // Console.WriteLine($"Horse : Speed = {horse.speed, -5}, Length = {horse.length, -20}");
#endif
                    if (horse.length > laneLength) win = true;
                }

                timeCnt++;
                // 이건 10번 호출됨
                if(timeCnt > 100) 
                {
                    CalculateHorseSpeed();
                    timeCnt = 0;
                }

                // 이게 1000번 호출됨
#if !TEST
                Thread.Sleep(10);
#endif
            }

            for(int i=0; i<horses.Count; i++) 
            {
                if (horses[i].length > laneLength)
                    winHorsesIndexes.Add(i);
            }

            if(winHorsesIndexes.Count == 1) 
            {
                winHorse = winHorsesIndexes[0] + 1;
            }
            else 
            {
                int maxIndex = 0;
                double maxRate = 0;
                for(int i=0; i<winHorsesIndexes.Count; i++) 
                {
                    Horse winHorse = horses[winHorsesIndexes[i]];
                    double overRate = ((double)(winHorse.length - laneLength)) / winHorse.speed;

                    if(maxRate < overRate)
                    { 
                        maxRate = overRate;
                        maxIndex = i;
                    }
                }
                winHorse = maxIndex + 1;
            }
        }

        void InitializeHorses() 
        {
            horses.Clear();
            Random random = new Random();

            horses.Add(new Horse(random.Next(250, 349)));
            horses.Add(new Horse(random.Next(150, 252)));
            horses.Add(new Horse(random.Next(175, 225)));
            horses.Add(new Horse(random.Next(50, 360)));
            horses.Add(new Horse(random.Next(75, 124)));
        }

        // 게임 내에서 10번정도 호출됨
        void CalculateHorseSpeed() 
        {
            Random random = new Random();
            

            // 1번마
            // 1번마의 속도는 확률 평균 300부터 시작해서 최소 100 이하로 감소
            // 랜덤한 값을 감소시켜 평균을 100으로 만듬
            // 매번 최소 20의 값을 감소시키면 됨 - 단 0이나 음수가 되지 않도록
            horses[0].speed -= random.Next(20, 35);

            // 2번마
            // 2번마의 속도는 확률 평균 200부터 시작하지만 3번마보다는 편차가 큼
            // 2번마는 본인의 length가 가장 작으면 확률평균 300으로 달리고
            // 본인의 length가 가장 크면 확률평균 100으로 달림
            int avg = 0;
            foreach(var horse in horses) 
            {
                avg += horse.speed;
            }

            avg = (avg - horses[1].speed) / 4;
            horses[1].speed = random.Next(avg, avg + 46);


            // 3번마
            // 3번마는 확률 평균 200
            horses[2].speed = random.Next(190, 225);

            // 4번마
            // 4번마는 확률 평균 200이지만 편차가 매우큼
            horses[3].speed = random.Next(0, 375);

            // 5번마
            // 5번마는 확률 평균 100에서 시작해서 300 + a까지 올라감
            // 매번 최소 20의 값을 증가
            horses[4].speed += random.Next(20, 30);
        }

        class Horse 
        {
            // 최대 길이는 200000
            public int length { get; private set; }

            // 기본 속도는 100
            public int speed = 100;

            public Horse(int speed) 
            {
                this.speed = speed;
            }

            public void Run() 
            {
                length += speed;
            }

            static string[] chickens = { chicken0, chicken1, chicken2, chicken3 };

            const string chickenIdle = @"
          ,mm
         / ' >
((___.-''   |
 \__       /
   \___(__/
     |_|_
";
            const string chicken0 = @"
           ,mm
          / ' >
((___.-'''  /
 \__       /
  \__(____/
     /\_
";
            const string chicken1 = @"
          ,mm
         / ' >
((___.-''   /
 \__       /
  \_____(_/
    /_   \_
";

            const string chicken2 = @"
         ,mm
        | ' >
((___.-''   |
 \__       /
  \___(___/
     /  \_
";
            const string chicken3 = @"
         ,mm
        | ' >
((___.-''  ,|
 \__       /
  \_(_____/
    /_    \_
";
            public void PrintChicken(int num) 
            {
                if(num == -1) 
                {
                    DrawChicken(chickenIdle);
                }
                else 
                {
                    switch(num) 
                    {
                        case 0:
                            DrawChicken(chicken0);
                            break;
                        case 1:
                            DrawChicken(chicken1);
                            break;
                        case 2:
                            DrawChicken(chicken2);
                            break;
                        case 3: 
                            DrawChicken(chicken3);
                            break;
                    }
                }
            }

            void DrawChicken(string chicken) 
            {
                int start_x = Console.GetCursorPosition().Left;

                bool isEmpty = true;
                foreach(char ch in chicken) 
                {
                    if(ch == '\n') 
                    {
                        Console.SetCursorPosition(start_x, Console.GetCursorPosition().Top + 1);
                        isEmpty = true;
                    }
                    else if(ch != ' ') 
                    {
                        isEmpty = false;
                        Console.Write(ch);
                    }
                    else 
                    {
                        if (isEmpty)
                            Console.SetCursorPosition(Console.GetCursorPosition().Left + 1, Console.GetCursorPosition().Top);
                        else
                            Console.Write(ch);
                    }
                }
            }
        }

        public void CountWinStreak(WINFLAG flag)
        {
            if (WinStreak == 0)
                WinStreak += (int)flag;
            else if (WinStreak > 0)
            {
                if (flag != WINFLAG.WIN)
                    WinStreak = (int)flag;
                else
                {
                    WinStreak++;
                    if (WinStreak > CasinoData.casinoData.horseRacingAchivement._maxWinStreak)
                    {
                        CasinoData.casinoData.horseRacingAchivement._maxWinStreak = WinStreak;
                    }
                }
            }
            else
            {
                if (flag != WINFLAG.LOSE)
                    WinStreak = (int)flag;
                else
                {
                    WinStreak--;
                    if (WinStreak < CasinoData.casinoData.horseRacingAchivement._maxLoseStreak)
                    {
                        CasinoData.casinoData.horseRacingAchivement._maxLoseStreak = WinStreak;
                    }
                }
            }
        }
    }
}
