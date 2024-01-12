using ElonMusk;
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
        private int cleraQuest;

        public Job()
        {
            JobList = new List<(string, bool)>();
            JobList.Add(("인턴", false));
            JobList.Add(("신입개발자", false));
            JobList.Add(("주니어개발자", false));
            JobList.Add(("시니어개발자", false));

            currentGold = 100000  +( 100000 * Game.game.player.JobToInt(Game.game.player.job));
            killCount = 5 + (5 * Game.game.player.JobToInt(Game.game.player.job));
            cleraDungoen =  1+ (1 * Game.game.player.JobToInt(Game.game.player.job));
            cleraQuest = 2 + (2 * Game.game.player.JobToInt(Game.game.player.job));
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
            Console.WriteLine("버그소탕 : "+ Forsave.KillCnt + "/"+ killCount);
            Console.WriteLine("완료파일 : "+ Forsave.dungeonClearCnt + "/"+ cleraDungoen);
            Console.WriteLine("도전과제 : "+ Game.game.player.QusetCnt + "/" + cleraQuest);
            Console.WriteLine();
            Console.WriteLine("1. 진급");
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0)
            {
                Game.game.ChangeScene(Game.game.idle);
            }

            else if (act == 1)
            {
                if (checkPlayerInfo())
                {
                    Game.game.player.job = Game.game.player.job + 1;
                    Console.WriteLine("진급에 성공하였습니다.");
                    Console.WriteLine("당신의 직급은 " + Game.game.player.JobToString(Game.game.player.job) + "입니다.");
                }
                else
                {
                    Console.WriteLine("진급에 필요한 조건이 부족합니다");
                }
            }

            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public bool checkPlayerInfo()
        {
            if(Game.game.player.GOLD > currentGold && Forsave.KillCnt > killCount && Forsave.dungeonClearCnt> cleraDungoen && Game.game.player.QusetCnt> cleraQuest)
                return true;
            else
                return false;
        }
    }
}