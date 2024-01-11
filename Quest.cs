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
        public List<(string, string, int, bool)> questList { get; protected set; }

        //생성자 이면서 퀘스트 들어오면 체크하기
        public Quest()
        {
            questList = new List<(string, string, int, bool)> ();
            questList.Add(new("퍼스트블러드", "버그를 처음 해결하였을 때", 1, false));
            questList.Add(new("팬타킬", "버그를 5마리 이상 해결하였을 때", 5, false));
            questList.Add(new("스파르타", "버그는 잡아도 잡아도 계속 나오네요", 300, false));
            questList.Add(new("승리", "던전을 처음 클리어 하였을 때", 1, true));
            questList.Add(new("부자", "보유골드 1억골드 이상", 100000000, false));
            questList.Add(new("존버", "보유골드 1000골드 이하", 1000, false));
            questList.Add(new("타짜", "블랙젝으로 백만골드 이상 획득", 1000000, false));
            questList.Add(new("오징어게임", "경마로 456만골드 이상 획득", 4560000, false));
            questList.Add(new("엔딩", "경마로 456만골드 이상 획득", 1, false));
        }

        public override void ShowInfo()
        {
            Console.WriteLine("도전과제");
            Console.WriteLine();
            for (int i =0; i<questList.Count; i++)
            {
                if (questList[i].Item4) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"{i + 1} - {questList[i].Item1}");
                for (int n = 0; n < (8 - (int)questList[i].Item1.Length); n++)
                {
                    Console.Write("  ");
                }
                Console.Write($"| {questList[i].Item2}");
                Console.WriteLine();
                Console.ResetColor();
            }
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
}