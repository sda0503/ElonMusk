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
    public class Job : Scene
    {
        public List<(string, bool)> JobList { get; protected set; }

        public Job()
        {
            JobList = new List<(string, bool)>();
            JobList.Add(("취준생", false));
            JobList.Add(("신입개발자", false));
            JobList.Add(("주니어개발자", false));
            JobList.Add(("시니어개발자", false));
        }

        public override void ShowInfo()
        {
            Console.WriteLine("전직");
            Console.WriteLine("다른 직업으로 바꿀 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[현재 직업]");
            //Console.WriteLine($"{Game.game.player.GOLD}");
            Console.WriteLine();
            Console.WriteLine("전직 가능한 직업");
            Console.WriteLine();
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
    }
}