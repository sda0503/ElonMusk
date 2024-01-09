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

namespace Elonmusk 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    public class Game
    {
        public static Game game;
        Scene curScene;

        // public Player player { get; private set; }
        // public Player player { get; private set; }
        
        public Game()
        {
            game = this;
        }

        public void Start()
        {
            Init();
            Loop();
        }

        void Init()
        {
            // player = new Player();

            curScene = new Idle();
        }

        void Loop()
        {
            while (true)
            {
                curScene.ShowInfo();

                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 선택해주세요.");
                Console.Write(">> ");

                int input = GetPlayerInputInt();
                curScene.GetAction(input);
            }
        }

        public static int GetPlayerInputInt()
        {
            int input = -1;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.Clear();
            }

            Console.Clear();
            return input;
        }

        public void ChangeScene(Scene scene)
        {
            this.curScene = scene;
        }
    }

    public abstract class Scene
    {
        public abstract void ShowInfo();
        public abstract void GetAction(int act);
    }

    public class Idle : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("0. 상태보기");
            Console.WriteLine("1. 인벤토리");
            Console.WriteLine("2. 상점");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                // Scene을 이동할 때에는 Game.game.ChangeScene(new 씬이름()); 을 사용하면 됨
                case 0:
                    Game.game.ChangeScene(new Idle());
                    break;
                case 1:
                    Game.game.ChangeScene(new Idle());
                    break;
                case 2:
                    Game.game.ChangeScene(new Idle());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }
}