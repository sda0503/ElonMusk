using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class Take100Damage : Scene
    {   
        public override void ShowInfo()
        {
            Console.WriteLine($"현재 플레이어의 체력: {Game.game.player.CurHP}");
            Console.WriteLine();
            Console.WriteLine("0. 데미지 받기");
            Console.WriteLine("1. 나가기");
        }

        public void TakeDamage()
        {
            Random random = new Random();
            int damage = random.Next(20, 50);
            Console.WriteLine($"플레이어가 {damage}만큼의 데미지를 입음");

            //Game.game.player.SetCurHP(damage* -1);
            if (Game.game.player.CurHP <= 0)
            {
                Game.game.ChangeScene(Game.game.badEndding);
            }
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    TakeDamage();
                    break;
                case 1:
                    Game.game.ChangeScene(Game.game.idle);
                    return;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        
    }
}
