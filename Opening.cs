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
            Console.WriteLine(String.Format("{0,100}", " #######  ######      #     ######    ######     #                               "));
            Console.WriteLine(String.Format("{0,100}", " ###  ##  ### ###    ###    ## ####   ######    ###              ####      ###   "));
            Console.WriteLine(String.Format("{0,100}", " ###      ##   ##    ###    #######     ##      ###             #####    ### ##  "));
            Console.WriteLine(String.Format("{0,100}", " #######  ### ###   #####   #####       ##     #####            ##       ### ##  "));
            Console.WriteLine(String.Format("{0,100}", "     ###  ######    ## ##   ## ###      ##     ## ##            ##       ### ##  "));
            Console.WriteLine(String.Format("{0,100}", " ##  ###  ##       ### ###  ##  ###     ##    ### ###    ##     #####    ######  "));
            Console.WriteLine(String.Format("{0,100}", "  #######  ##       ### ###  ##   ##     ##    ### ###    ##      ####      ###   "));
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(String.Format("{0,60}", "0. 이력서 작성하로가기"));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
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
