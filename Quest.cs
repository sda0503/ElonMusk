using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Elonmusk.Item;

namespace Elonmusk 
{
   
    public class Quest : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("퀘스트");
            Console.WriteLine("1.버그를 10개이상 수정하시오");
            Console.WriteLine("2.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0) Game.game.ChangeScene(new Shop());
            else if (act == 1) Game.game.shop.TryBuyItem(act - 1);
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
    /*
   public class Shop : Scene
   {
       string itemType;

       // bool 변수는 팔렸는지의 여부(true면 구매불가)
       public List<(Item, bool)> items { get; protected set; }

       public Shop()
       {
           items = new List<(Item, bool)>();
           items.Add((new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 1000, Item.ItemType.ARMOR, Item.ClassType.ALL), false));
           items.Add((new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 2000, Item.ItemType.ARMOR, Item.ClassType.WARRIOR), false));
           items.Add((new Item("가죽 갑옷", "가죽으로 만들어져 튼튼한 갑옷입니다.", 0, 7, 1500, Item.ItemType.ARMOR, Item.ClassType.ARCHER), false));
           items.Add((new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 3500, Item.ItemType.ARMOR, Item.ClassType.ALL), false));
           items.Add((new Item("낡은 검", "어디에서나 쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 600, Item.ItemType.WEAPON, Item.ClassType.ALL), false));
           items.Add((new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 1500, Item.ItemType.WEAPON, Item.ClassType.WARRIOR), false));
           items.Add((new Item("단궁", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 1500, Item.ItemType.WEAPON, Item.ClassType.WARRIOR), false));
           items.Add((new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 2500, Item.ItemType.WEAPON, Item.ClassType.ALL), false));
       }

       public override void ShowInfo()
       {
           Console.WriteLine("상점");
           Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
           Console.WriteLine();
           Console.WriteLine("[보유 골드]");
           //Console.WriteLine($"{Game.game.player.GOLD}");
           Console.WriteLine();
           Console.WriteLine("[아이템 목록]");
           foreach (var item in items)
           {
               //Console.WriteLine($"- [{changeType(item.Item1.itemType)}]{item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD} G | [{changeType(item.Item1.classType)}]");
               Console.WriteLine(" - " + string.Format("{0,5}{1,-5:NO}{2,-9} | {3,-4} | {4,-4}", "[" + changeType(item.Item1.itemType) + "]", "["+changeType(item.Item1.classType)+"] ", item.Item1.name, item.Item1.GetEffectScript(), item.Item1.GOLD + "G"));
               Console.WriteLine("   "+ item.Item1.desc);
               Console.WriteLine();
               //Console.WriteLine(" - "+string.Format("{0,5}{1,-10:NO}{2,-10}{3,-30}{4,7}{5,14}", "["+changeType(item.Item1.itemType)+"] ", item.Item1.name, item.Item1.GetEffectScript(), item.Item1.desc, item.Item1.GOLD+"G", changeType(item.Item1.classType)));
               // string.Format("{0,10}{0,10}", str, str2);

           }
           Console.WriteLine();
           Console.WriteLine("1.아이템 구매");
           Console.WriteLine("0.나가기");
       }

       string changeType(Item.ItemType item)
       {
           switch (item)
           {
               case Item.ItemType.NONE:
                   itemType = "소모품";
                   break;
               case Item.ItemType.WEAPON:
                   itemType = "무기류";
                   break;
               case Item.ItemType.ARMOR:
                   itemType = "방어구";
                   break;
           }
           return itemType;
       }

       string changeType(Item.ClassType item)
       {
           switch (item)
           {
               case Item.ClassType.ALL:
                   itemType = "제한없음";
                   break;
               case Item.ClassType.WARRIOR:
                   itemType = "전사전용";
                   break;
               case Item.ClassType.ARCHER:
                   itemType = "궁수전용";
                   break;
           }
           return itemType;
       }

       public override void GetAction(int act)
       {
           switch (act)
           {
               case 0:
                   Game.game.ChangeScene(new Idle());
                   break;
               case 1:
                   Game.game.ChangeScene(new Buy());
                   break;
               default:
                   Console.WriteLine("유효한 입력이 아닙니다!");
                   break;
           }
       }

       public void TryBuyItem(int index)
       {
           if (items[index].Item2 == true )
               Console.WriteLine("이미 구매한 아이템입니다.");
           else
           {
               if (Game.game.player.GOLD >= items[index].Item1.GOLD)
               {

                   //Game.game.inventory.AddItem(items[index].Item1);
                   Console.WriteLine("구매를 완료했습니다.");
                   items[index] = (items[index].Item1, true);

               }
               else
                   Console.WriteLine("골드가 부족합니다.");
           }
       }
   }

   public class Buy : Scene
   {
       string itemType;
       public override void ShowInfo()
       {
           Console.WriteLine("상점");
           Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
           Console.WriteLine();
           Console.WriteLine("[보유 골드]");
           //Console.WriteLine($"{Game.game.player.GOLD}");
           Console.WriteLine();
           Console.WriteLine("[아이템 목록]");


           for (int i = 0; i < Game.game.shop.items.Count; i++)
           {
               (Item, bool) item = Game.game.shop.items[i];
               String strSold = (item.Item2) ? "구매완료" : $"{item.Item1.GOLD}G";
               Console.WriteLine(" - " + (i + 1) + string.Format(" {0,5}{1,-5:NO}{2,-12} | {3,-4} | {4,-4}", "[" + changeType(item.Item1.itemType) + "]", "[" + changeType(item.Item1.classType) + "] ", item.Item1.name, item.Item1.GetEffectScript(), strSold));
               //Console.WriteLine($"- {i + 1} {item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {strSold}");
           }


           Console.WriteLine();
           Console.WriteLine("0. 나가기");
       }

       public override void GetAction(int act)
       {
           if (act == 0)
           {
               Game.game.ChangeScene(Game.game.shop);
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

       string changeType(Item.ItemType item)
       {
           switch (item)
           {
               case Item.ItemType.NONE:
                   itemType = "소모품";
                   break;
               case Item.ItemType.WEAPON:
                   itemType = "무기류";
                   break;
               case Item.ItemType.ARMOR:
                   itemType = "방어구";
                   break;
           }
           return itemType;
       }
       string changeType(Item.ClassType item)
       {
           switch (item)
           {
               case Item.ClassType.ALL:
                   itemType = "제한없음";
                   break;
               case Item.ClassType.WARRIOR:
                   itemType = "전사전용";
                   break;
               case Item.ClassType.ARCHER:
                   itemType = "궁수전용";
                   break;
           }
           return itemType;
       }
   }
   */
}