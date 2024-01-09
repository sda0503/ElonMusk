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
            Console.WriteLine("몰려오는 적을 막아내지 못한 Player.Name 이 결국 사망하였습니다.");
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
            Console.WriteLine("던전을 모두 클리어해 스파르탄 마을에 평화가 찾아왔습니다!");
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
