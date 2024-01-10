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
}