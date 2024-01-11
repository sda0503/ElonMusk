using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class BTestScene: Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("가방");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[장착중인 아이템]");
            Console.WriteLine();
            for (int i = 0; i < Game.game.player.equipedItems.Count; i++)
            {
                Item item = Game.game.player.equipedItems[i];
                Console.WriteLine($"- {i + 1}{item.name} | {item.GetEffectScript()} | {item.desc} | {item.GOLD}");
            }
            Console.WriteLine("[보유한 아이템]");
            {
                for(int i=0; i<Game.game.player.items.Count; i++)
                {
                    (Item,bool) item = Game.game.player.items[i];
                    Console.WriteLine($"- {i + 1}{item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Idle());
                    break;
                case 1:
                    Game.game.ChangeScene(new Equipment());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }
}
