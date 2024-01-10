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
            Console.WriteLine("몰려오는 버그들을 모두 해결해 (주)스파르타에 평호가 찾아왔습니다!!");
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
