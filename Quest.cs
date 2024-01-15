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
   
    public class Quest : Scene
    {
        public List<(string, string, bool)> questList { get; protected set; }

        public List<(string, string, bool)> menu { get; set; }

        //생성자 이면서 퀘스트 들어오면 체크하기
        public Quest()
        {
            Game.game.player.QusetCnt = 0;
            questList = new List<(string, string, bool)> ();
            questList.Add(new("퍼스트블러드", "버그를 처음 해결하였을 때", false));
            questList.Add(new("팬타킬", "버그를 5마리 이상 해결하였을 때", false));
            questList.Add(new("스파르타", "버그는 잡아도 잡아도 계속 나오네요", false));
            questList.Add(new("승리", "던전을 처음 클리어 하였을 때", false));
            questList.Add(new("부자", "보유골드 1억골드 이상",  false));
            questList.Add(new("존버", "보유골드 1000골드 이하",  false));
            questList.Add(new("타짜", "블랙젝으로 백만골드 이상 획득",  false));
            questList.Add(new("오징어게임", "경마로 456만골드 이상 획득",  false));
            questList.Add(new("인생패망-1", "블랙젝에서 백만골드 탕진", false));
            questList.Add(new("인생패망-2", "경마에서 백만골드 탕진",  false));

            CheckQuest();
         }

        public override void ShowInfo()
        {
            Console.WriteLine("도전과제");
            Console.WriteLine();
            for (int i =0; i< menu.Count; i++)
            {
                if (menu[i].Item3)
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    
                }
                else Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write($"{i + 1} - {menu[i].Item1}");
                for (int n = 0; n < (10 - (int)menu[i].Item1.Length); n++)
                {
                    Console.Write("  ");
                }
                Console.Write($"| {menu[i].Item2}");
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0) Game.game.ChangeScene(Game.game.idle);
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

         public void CheckQuest()
        {
            menu = new List<(string, string, bool)>();
            menu.Clear();
            if (Forsave.KillCnt > 0) { menu.Add(new("퍼스트블러드", "버그를 처음 해결하였을 때", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("퍼스트블러드", "버그를 처음 해결하였을 때", false));
            if (Forsave.KillCnt > 5) { menu.Add(new("팬타킬", "버그를 5마리 이상 해결하였을 때", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("팬타킬", "버그를 5마리 이상 해결하였을 때", false));
            if (Forsave.KillCnt > 300) { menu.Add(new("스파르타", "버그는 잡아도 잡아도 계속 나오네요", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("스파르타", "버그는 잡아도 잡아도 계속 나오네요", false));
            if (Forsave.dungeonClearCnt > 0) { menu.Add(new("승리", "던전을 처음 클리어 하였을 때", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("승리", "던전을 처음 클리어 하였을 때", false));
            if (Game.game.player.GOLD > 100000000) { menu.Add(new("부자", "보유골드 1억골드 이상", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("부자", "보유골드 1억골드 이상", false));
            if (Game.game.player.GOLD < 1000) { menu.Add(new("존버", "보유골드 1000골드 이하", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("존버", "보유골드 1000골드 이하", false));
            if (CasinoData.casinoData.blackJackAchievement._winGold > 1000000) { menu.Add(new("타짜", "블랙젝으로 백만골드 이상 획득", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("타짜", "블랙젝으로 백만골드 이상 획득", false));
            if (CasinoData.casinoData.horseRacingAchivement._winGold > 4560000) { menu.Add(new("오징어게임", "경마로 456만골드 이상 획득", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("오징어게임", "경마로 456만골드 이상 획득", false));
            if (CasinoData.casinoData.blackJackAchievement._loseGold >= 1000000) { menu.Add(new("인생패망-1", "블랙젝에서 백만골드 탕진", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("인생패망-1", "블랙젝에서 백만골드 탕진", false));
            if (CasinoData.casinoData.horseRacingAchivement._loseGold >= 1000000) { menu.Add(new("인생패망-2", "블랙젝에서 백만골드 탕진", true)); Game.game.player.QusetCnt += 1; }
            else menu.Add(new("인생패망-2", "경마에서 백만골드 탕진", false));
        }
    }
}