using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    // 플레이어 체력이 0이 됐을 때 해당 씬으로 넘어감
    public class BadEndding : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("몰려오는 버그에 결국 (주)스파르타는 파산했습니다.");
            Console.WriteLine("---Bad Endding---");
            Console.WriteLine();
            Console.WriteLine("새 게임 시작?");
            Console.WriteLine();
            Console.WriteLine("0. 새 게임");
            Console.WriteLine("1. 종료하기");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.Start();
                    break;
                case 1:
                    return;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }

    public class HappyEndding : Scene
    {
        public override void ShowInfo()
        {
            Console.Clear();
            Console.WriteLine("성공적으로 투자를 유치한 (주)스파르타는 게임을 출시하게 되고");
            Console.ReadLine();
            Console.WriteLine("출시한 게임이 엄청난 성공을 거두며 대기업으로 성장하게 된다.");
            Console.ReadLine();
            Console.WriteLine($"{Game.game.player.PlayerName.Item1} 또한 그 공로를 인정받아 임원 자리를 얻게 되지만,");
            Console.ReadLine();
            Console.WriteLine($"새로운 도전을 좋아하는 {Game.game.player.PlayerName.Item1}은");
            Console.ReadLine();
            Console.WriteLine("자리를 마다하고 새로운 게임 개발 여정을 시작하게 되는데...");
            Console.WriteLine("---Happy Endding---");
            Console.WriteLine();
            Console.WriteLine("새 게임 시작?");
            Console.WriteLine();
            Console.WriteLine("0. 새 게임");
            Console.WriteLine("1. 종료하기");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.Start();
                    break;
                case 1:
                    return;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }
}
