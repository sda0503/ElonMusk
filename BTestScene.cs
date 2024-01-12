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

    public class SellScene : Scene
    {
        public override void ShowInfo()
        {
            int a = 0;

            Console.WriteLine("상점");
            Console.WriteLine("필요한 없는 아이템을 판매 할 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Game.game.player.GOLD}G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Game.game.player.items.Count; i++)
            {
                (Item, bool) item = Game.game.player.items[i];
                String strEquipped = (item.Item2) ? "[E]" : String.Empty;
                Console.WriteLine($"- {i + 1} {strEquipped}{item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0)
            {
                Game.game.ChangeScene(new Shop());
            }
            else if (act > 0 && act < Game.game.shop.items.Count + 1)
            {
                Game.game.shop.TrySellItem(act - 1);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
