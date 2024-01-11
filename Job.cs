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

        private int currentGold;
        private int killCount;
        private int cleraDungoen;

        public Job()
        {
            JobList = new List<(string, bool)>();
            JobList.Add(("인턴", false));
            JobList.Add(("신입개발자", false));
            JobList.Add(("주니어개발자", false));
            JobList.Add(("시니어개발자", false));

            currentGold = 100000 * Game.game.player.level;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("인사평가");
            Console.WriteLine("조건을 채우면 진급이 가능합니다.");
            Console.WriteLine();
            Console.WriteLine("[현재 직책]");
            Console.WriteLine($"{Game.game.player.JobToString(Game.game.player.job)}");
            Console.WriteLine();
            Console.WriteLine("[다음 직책]");
            Console.WriteLine($"{Game.game.player.JobToString(Game.game.player.job + 1)}");
            Console.WriteLine(); 
            Console.WriteLine("[진급조건]");
            Console.WriteLine("보유골드 : "+ Game.game.player.GOLD + "/"+ currentGold);
            Console.WriteLine("버그소탕 : "+ Game.game.player.GOLD + "/"+ killCount);
            Console.WriteLine("완료파일 : "+ Game.game.player.GOLD + "/"+ cleraDungoen);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 진급");
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0)
            {
                Game.game.ChangeScene(Game.game.shop);
            }

            else if (act == 1)
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