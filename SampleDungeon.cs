using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class SampleDungeon : Scene
    {
        public int Level { get; set; }
        public int maxLevel = 5;
        public SampleDungeon()
        {
            Level = 1;
        }
        
        public override void ShowInfo()
        {
            Console.WriteLine($"{Level++}층 클리어");
            if (IsAllClear())
            {
                Game.game.ChangeScene(Game.game.happyEndding);
                //Game.game.GameOver = true;
            }
            Console.WriteLine();
            if(!IsAllClear())
            {
                Console.WriteLine("0. 다음 층 올라가기");
            }
            Console.WriteLine("1. 나가기");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:

                    break;
                case 1:
                    if (IsAllClear()) Game.game.ChangeScene(Game.game.happyEndding);
                    else Game.game.ChangeScene(Game.game.idle);
                    return;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        public bool IsAllClear()
        {
            if (Level > maxLevel) return true;
            else return false;
        }
    }
}
