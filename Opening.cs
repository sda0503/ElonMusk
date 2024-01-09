using Elonmusk;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class Opening:Scene
    {
        public override void ShowInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("           /\\                                                 /\\");
            Console.WriteLine(" _         )( ______________________   ______________________ )(         _");
            Console.WriteLine("(_)///////(**)______________________> <______________________(**)\\\\\\\\\\\\\\(_)");
            Console.WriteLine("           )(                                                 )(");
            Console.WriteLine("           \\/                                                 \\/");
            Console.WriteLine();
            Console.WriteLine("__________________________  ______________ ____________________  ________");
            Console.WriteLine("\\__    ___/\\_   _____/\\   \\/  /\\__    ___/ \\______   \\______   \\/  _____/");
            Console.WriteLine("  |    |    |    __)_  \\     /   |    |     |       _/|     ___/   \\  ___ ");
            Console.WriteLine("  |    |    |        \\ /     \\   |    |     |    |   \\|    |   \\    \\_\\  \\");
            Console.WriteLine("  |____|   /_______  //___/\\  \\  |____|     |____|_  /|____|    \\______  /");
            Console.WriteLine("                   \\/       \\_/                    \\/                  \\/ ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=========================================================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 새 게임");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Idle());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }
}
