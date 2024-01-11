using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class Resume :Scene
    {
        public override void ShowInfo()
        {
            string script = Game.game.player.PlayerName.Item2 ? "0. 회사로 가기" : "1. 이름 입력하기 ";
            Console.WriteLine($"{script}");
        }
        
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Idle());
                    break;
                case 1:
                    Game.game.player.SetName(Console.ReadLine());
                    Game.game.ChangeScene(new Idle());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }
}
