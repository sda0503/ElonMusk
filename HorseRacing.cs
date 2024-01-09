using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class HorseRacingLobby : GameLobby
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
                    Game.game.ChangeScene(new HorseRacingInfo());
                    break;
                case 2:
                    Game.game.ChangeScene(new HorseRacingGame());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        static void PrintGameTitle()
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
     d888ba.88ba  dP     dP .d88888b   88888888b 8888ba.88ba   88888888b 
     88  `8b  `8b 88     88 88.    ""'  88        88  `8b  `8b  88        
     88   88   88 88     88 `Y88888b.  888888    88   88   88  888888  
     88   88   88 88     88       `8b  88        88   88   88  88        
     88   88   88 Y8.   .8P d8'   .8P  88        88   88   88  88        
d88888P   dP   dP `Y88888P'  Y88888P   88888888P dP   dP   dP  8888888888b
");
        }
    }

    public class HorseRacingInfo : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("스파르타 무스메는 5마리의 열정 넘치는 무스메들 중에서 승리하는 말을 맞추는 게임입니다.");
            Console.WriteLine("승리하는 말을 맞추면 베팅한 금액의 세 배를 얻습니다.");
            Console.WriteLine("반대로 맞추지 못했을 경우에는 베팅한 금액을 모두 잃습니다.");
            Console.WriteLine("각 말들의 특징은 다음과 같습니다.");
            Console.WriteLine();
            Console.WriteLine("첫째 말은 성격이 급해서 처음부터 빠르게 달리고 지친 후에 천천히 달립니다.");
            Console.WriteLine("둘째 말은 겁이 많아 뒤쳐지면 빨라지고, 앞장서면 느려집니다.");
            Console.WriteLine("셋째 말은 차분하게 처음부터 끝까지 꾸준한 속도로 달립니다.");
            Console.WriteLine("넷째 말은 제멋대로라 기분에 따라 달립니다. 빠르게 달리지만 역주행을 할 수도 있습니다.");
            Console.WriteLine("다섯째 말은 노력가여서 처음에 천천히 달리다가 점점 더 빠르게 달립니다.");
            Console.WriteLine();
            Console.WriteLine("0. 돌아간다");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new BlackJackLobby());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }

    public class HorseRacingGame : Scene
    {
        bool isRunning = false;
        int laneLength = 1000;

        public override void ShowInfo()
        {
            // print
            printHorseLane();

            if (isRunning)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WindowWidth = laneLength;
                Console.WriteLine("");
                Console.WriteLine("LANE 1 :");
                Console.WriteLine("");
                Console.WriteLine("LANE 2 :");
                Console.WriteLine("");
                Console.WriteLine("LANE 3 :");
                Console.WriteLine("");
                Console.WriteLine("LANE 4 :");
                Console.WriteLine("");
                Console.WriteLine("LANE 5 :");
                Console.WriteLine("");
            }

            if (!isRunning)
            {
                Console.WriteLine("1. 배팅 말 설정");
                Console.WriteLine("2. 배팅 금액 설정");
                Console.WriteLine("3. 게임 시작");
                Console.WriteLine("0. 돌아가기");
            }
        }

        public override void GetAction(int act)
        {
            if (!isRunning)
            {
                switch (act)
                {
                    case 0:
                        Game.game.ChangeScene(new HorseRacingLobby());
                        break;
                    default:
                        Console.WriteLine("유효한 입력이 아닙니다!");
                        break;
                }
            }
        }

        void printHorseLane()
        { }
    }
}
