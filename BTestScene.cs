using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class BTestScene:Scene
    {
        public override void ShowInfo()
        {
            int a = 0;
            
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Game.game.player.GOLD}G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Game.game.shop.items.Count; i++)
            {
                if (i == 0) Console.WriteLine("[방어류]");
                if (i == 3) Console.WriteLine("\n[무기류]");
                if (i == 5) Console.WriteLine("\n[장신구]");
                if (i == 8) Console.WriteLine("\n[소모품]");
                (Item, bool) item = Game.game.shop.items[i];
                char space = ' ';
                int cntSpace = item.Item1.desc.Count(cnt => (cnt == space));
                String strSold = (item.Item2) ? "구매완료" : $"{item.Item1.GOLD}G";
                Console.Write($"{i + 1} {item.Item1.name}");
                for(int n=0; n< (10 - (int)item.Item1.name.Length); n++)
                {
                    Console.Write("  ");
                }
                Console.Write($"| {item.Item1.GetEffectScript()} | {item.Item1.desc}");
                for(int k=0; k< (28 -(int)item.Item1.desc.Length); k++)
                {
                    Console.Write("  ");
                }
                for(int m = 0; m < cntSpace; m++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine($"| { strSold}");
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
                Game.game.shop.TryBuyItem(act - 1);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
