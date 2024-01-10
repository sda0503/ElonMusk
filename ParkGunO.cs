using Elonmusk;
using ElonMusk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace ElonMusk
{        
    interface ICharacter
    {
        string Name { get; set; }
        int Health { get; set; }
        int ATK { get; set; }
        int ACC { get; set; }
        int Evade { get; set; }
        bool IsDead { get; set; }
        void TakeDamage(int damage);
    }

    //public class Character
    //{
    //    public string Name { get; set; }
    //    public int Level { get; set; }
    //    public int MaxHealth { get; set; }
    //    public int CurHealth { get; set; }
    //    public int ATK { get; set; }
    //    public int DEF { get; set; }
    //    public int ACC { get; set; }
    //    public int Evade { get; set; }
    //    public bool IsDead { get; set; }

    //    public void TakeDamage(int damage)
    //    {
    //        CurHealth -= damage;
    //        if (CurHealth <= 0)
    //        {
    //            CurHealth = 0;
    //            IsDead = true;
    //        }
    //    }

    //    public Character()
    //    {
    //        Name = "Chad";
    //        Level = 1;            
    //        ATK = 10;
    //        DEF = 5;
    //        MaxHealth = 100;
    //        CurHealth = 100;
    //        ACC = 10;
    //        Evade = 10;
    //    }
    //}

    public class Monster : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int ATK { get; set; }
        public int ACC { get; set; }
        public int Evade { get; set; }
        public string Description { get; protected set; }
        public bool IsDead { get; set; }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                IsDead = true;
            }
        }        
    }

    public class Null : Monster
    {
        public Null() //밸런스 잡힌 타입
        {
            Name = "Null";
            Level = 2;
            Health = 15;
            ATK = 6;
            ACC = 5;
            Evade = 5;
            Description = "Null의 값을 가질 수 없는 Object에 Null을 할당했기에 발생하거나, 참조하려는 개체가 Null일 때 발생합니다.";
        }
    }

    public class IndexOutOfRange : Monster
    {
        public IndexOutOfRange() //체력, 공격력 높은 대신 명중,회피가 낮음
        {
            Name = "OutofRange";
            Level = 5;
            Health = 25;
            ATK = 15;
            ACC = 3;
            Evade = 0;
            Description = "배열의 크기를 벗어나 접근할 때 발생합니다.";
        }
    }

    public class OthersCode : Monster
    {
        public OthersCode() //체력, 공격력 낮은 대신 명중,회피가 높음
        {
            Name = "남이 작성한 코드";
            Level = 1;
            Health = 5;
            ATK = 5;
            ACC = 5;
            Evade = 0;
            Description = "남이 작성한 코드입니다. 이해하려면 시간이 걸립니다. 설명이 없다면 더더욱, 주석을 생활화합시다.";
        }
    }

    public class Offline : Monster
    {
        public Offline()
        {
            Name = "연결끊김";
            Level = 7;
            Health = 20;
            ATK = 3;
            ACC = 5;
            Evade = 3;
            Description = "인터넷이 끊겼습니다. 재앙이 아닐 수 없군요.";
        }
    }

    public class TypeError : Monster
    {
        public TypeError()
        {
            Name = "형변환 오류";
            Level = 6;
            Health = 30;
            ATK = 10;
            ACC = 5;
            Evade = 3;
            Description = "암시적 형변환으로 충분치 않을 때 발생합니다.";
        }
    }

    public class UncotrollableBug : Monster //보스
    {
        public UncotrollableBug()
        {
            Name = "관객 공포증 버그";
            Level = 10;
            Health = 70;
            ATK = 15;
            ACC = 10;
            Evade = 5;
            Description = "컨트롤 할 수 없는, 중요한 자리에서 발생한 버그입니다.";
        }
    }

    public enum Doing { beforebattle,battle_ing,beforeDungeon}
    public class Forsave()
    {
        public static bool Dungeonfirst;
        public static int dungeonposx;
        public static int dungeonposy;
        public static Dictionary<string, bool> isclear;
        public static bool[,] dungeon = new bool[3, 3];
        public static void dungeonsetting()
        {
            isclear = new Dictionary<string, bool>(){
            { "isclear1",false },
            { "isclear2",false },
            { "isclear3",false },
            { "isclear4",false },
            { "isclear5",false },
            { "isclear6",false },
            { "isclear7",false },
            { "isclear8",false },
            { "isclear9",false }
            };
            dungeon = new bool[3, 3] {
            { isclear["isclear1"], isclear["isclear2"], isclear["isclear3"] },
            { isclear["isclear4"], isclear["isclear5"], isclear["isclear6"] },
            { isclear["isclear7"], isclear["isclear8"], isclear["isclear9"] } };
        }
    }
    
    public class Dungeon : Scene
    {
        public static Doing doing;
        
        public override void ShowInfo()
        {            
            while(Forsave.Dungeonfirst == false)
            {
                Forsave.dungeonsetting();
                Forsave.Dungeonfirst = true;
            }
            Console.WriteLine("환영합니다.");
            Console.WriteLine("입장하기 전 점검 모시깽");
            Console.WriteLine();
            Console.WriteLine("0. 돌아가기");
            Console.WriteLine("1. 입장하기");
            Console.WriteLine("2. 상태보기");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Idle());
                    break;
                case 1:
                    Forsave.dungeonposx = Forsave.dungeon.GetLength(0)-1;
                    Forsave.dungeonposy = Forsave.dungeon.GetLength(1)-1;
                    Game.game.ChangeScene(new Dungeon_move());
                    break;
                case 2:
                    doing = Doing.beforeDungeon;
                    Game.game.ChangeScene(new PlayerInfo());
                    break;
            }
        }

        public class Dungeon_move : Scene
        {
            public override void ShowInfo()
            {

                Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = true; //처음 입장하는 곳 전투X
                Board();
                Console.WriteLine();
                Console.WriteLine("이동할 방향을 누르세요. ex) 4 : Left, 8 : up, 6 : Right, 2 : Down");
                Console.WriteLine("0. 돌아가기");
                Console.WriteLine();
            }
            public override void GetAction(int act)
            {
                switch (act)
                {
                    case 0:
                        Game.game.ChangeScene(new Dungeon());
                        break;
                    case 4: //왼쪽
                        if (Forsave.dungeonposy - 1 < 0)
                            Console.WriteLine("그곳으로는 갈 수 없습니다.");
                        else
                        {
                            Forsave.dungeonposy--;                                                        
                        }
                        break;

                    case 6: //오른쪽
                        if (Forsave.dungeonposy + 1 >= Forsave.dungeon.GetLength(1))
                            Console.WriteLine("그곳으로는 갈 수 없습니다.");
                        else
                        {
                            Forsave.dungeonposy++;                           
                        }
                        break;

                    case 8: //위
                        if (Forsave.dungeonposx - 1 < 0)
                            Console.WriteLine("그곳으로는 갈 수 없습니다.");
                        else
                        {
                            Forsave.dungeonposx--; 
                        }
                        break;

                    case 2: //아래
                        if (Forsave.dungeonposx + 1 >= Forsave.dungeon.GetLength(1))
                            Console.WriteLine("그곳으로는 갈 수 없습니다.");
                        else
                        {
                            Forsave.dungeonposx++;                          
                        }
                        break;
                        
                    default:
                        Console.WriteLine("그곳으론 갈 수 없습니다..");
                        break;
                }
                if (Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] == false)
                    Game.game.ChangeScene(new Battle()); //돌아가기 했을 때 현재위치가 True로 찍혀있어서 이벤트 발생 안함.
            }
            static void Board()
            {
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", Forsave.dungeon[0, 0] ? "O" : "X", Forsave.dungeon[0, 1] ? "O" : "X", Forsave.dungeon[0, 2] ? "O" : "X");
                Console.WriteLine("_____|_____|_____");
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", Forsave.dungeon[1, 0] ? "O" : "X", Forsave.dungeon[1, 1] ? "O" : "X", Forsave.dungeon[1, 2] ? "O" : "X");
                Console.WriteLine("_____|_____|_____");
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", Forsave.dungeon[2, 0] ? "O" : "X", Forsave.dungeon[2, 1] ? "O" : "X", Forsave.dungeon[2, 2] ? "O" : "X");
                Console.WriteLine("     |     |     ");
            }
        }
    

    public class Battle : Scene
    {
        static Random rand = new Random();        
        static List<Monster> spawnlist = new List<Monster>(4);
        static int BfHp = 0; //전투 시작 전 체력
        static int potion = 3;
        
        //static int turn = 0;

        public override void ShowInfo()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 회복 아이템");
            Console.WriteLine();
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 1:
                    doing = Doing.beforebattle;
                    Game.game.ChangeScene(new PlayerInfo());
                    break;
                case 2:
                    Battleprepare();
                    break;
                case 3:
                    doing = Doing.beforebattle;
                    Game.game.ChangeScene(new UsePotion());
                    break;
            }
        }

        void Battleprepare() //던전에서 이동해서 전투 나왔을 때 안에 함수들 한번만 실행되게 작성
        { 
            //필수구현할 때 전투시작전 체력도 저장해야될듯
            BfHp = Game.game.player.CurHP;
                        
            for (int i = 0; i < rand.Next(1,5); i++)
            {
                switch (rand.Next(5))
                {
                    case 0:
                        spawnlist.Add(new Null());
                        break;
                    case 1:
                        spawnlist.Add(new IndexOutOfRange());
                        break;
                    case 2:
                        spawnlist.Add(new OthersCode());
                        break;
                    case 3:
                        spawnlist.Add(new Offline());
                        break;
                    case 4:
                        spawnlist.Add(new TypeError());
                        break;
                }  
            }
            Game.game.ChangeScene(new Battle_myturn());
        }

        public class Battle_myturn : Scene
        {
            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("■■■■■■■■■■■■■■");
                ShowHighlithtesText("Battle!! - 플레이어 턴");
                Console.WriteLine("■■■■■■■■■■■■■■");
                Console.WriteLine();
                foreach (Monster mob in spawnlist)
                {
                    if (mob.IsDead == true)
                        Console.ForegroundColor = ConsoleColor.DarkGray;                    
                    Console.Write($"Lv.{mob.Level} ");
                    Console.Write(PadRightForMixedText(mob.Name,20));
                    Console.WriteLine($"HP {mob.Health}");
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                Console.WriteLine();
                Console.WriteLine("1. 디버그");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 버그 알아보기") ;
                Console.WriteLine("4. 드링크 사용하기");
            }

            public override void GetAction(int act)
            {
                switch (act)
                {
                    // Scene을 이동할 때에는 Game.game.ChangeScene(new 씬이름()); 을 사용하면 됨                
                    case 1:
                        Game.game.ChangeScene(new BattleAttack());
                        break;
                    case 2:
                        Game.game.ChangeScene(new BattleSkill());
                        break;
                    case 3:
                        Game.game.ChangeScene(new Battle_InfoBug());
                        break;
                    case 4:
                        doing = Doing.battle_ing;
                        Game.game.ChangeScene(new UsePotion());
                        break;
                    default:
                        Console.WriteLine("유효한 입력이 아닙니다!");
                        break;
                }
            }
        }

        public class BattleAttack : Scene
        {
            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("■■■■■■■■■■■■■■");
                ShowHighlithtesText("Battle!!");
                Console.WriteLine("■■■■■■■■■■■■■■");
                Console.WriteLine();
                int j = 1;
                foreach (Monster mob in spawnlist)
                {
                    if (mob.IsDead == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"[{j++}] ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"[{j++}] ");
                        Console.ResetColor();
                    }
                    Console.Write($"Lv.{mob.Level} ");
                    Console.Write(PadRightForMixedText(mob.Name, 20));
                    Console.WriteLine($"HP {mob.Health}");
                    Console.ResetColor();
                }
                j = 1;
                Console.WriteLine();
                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                Console.WriteLine();
                Console.WriteLine("0. 돌아가기");
            }

            public override void GetAction(int act)
            {
                bool check = (act <= spawnlist.Count && act >= 0);
                bool isalive = false;
                switch (check)
                {
                    case true:
                        switch (act)
                        {
                            case 0:
                                Game.game.ChangeScene(new Battle_myturn());
                                break;
                            // Scene을 이동할 때에는 Game.game.ChangeScene(new 씬이름()); 을 사용하면 됨
                            case int:
                                if (spawnlist[act - 1].IsDead)
                                {
                                    Console.WriteLine("이미 죽은 몬스터입니다.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    attack(Game.game.player.ATK, spawnlist[act - 1]);
                                    foreach (Monster mob in spawnlist)
                                    {
                                        if (!mob.IsDead)
                                        {
                                            isalive = true;
                                            break;
                                        }
                                    }
                                    if (isalive == true)
                                        Game.game.ChangeScene(new Battle_enemyturn());
                                    else
                                        Game.game.ChangeScene(new BattleEnd_win());
                                }
                                break;



                        }
                        break;
                    default:
                        Console.WriteLine("다시 선택해주세요.");
                        break;
                }
            }

            public void attack(int PlayerATK, Monster mob)
            {
                Console.Clear();
                Random rand = new Random();
                int error = (int)MathF.Ceiling(PlayerATK / 10f);
                int damage = rand.Next(PlayerATK - error, PlayerATK + error);
                int temp = mob.Health;
                if (Game.game.player.ACC + rand.Next(20) > mob.Evade + rand.Next(20))
                {
                    if (rand.Next(100) > 84)
                    {
                        damage = (int)MathF.Ceiling(damage * 1.6f);
                        mob.TakeDamage(damage);
                        Console.WriteLine("플레이어의 공격!");
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name}");
                        Console.WriteLine($"HP {temp} -> {(mob.IsDead ? "Dead" : mob.Health)}");
                        Console.WriteLine();
                        Console.ReadLine();
                    }
                    else
                    {
                        mob.TakeDamage(damage);
                        Console.WriteLine("플레이어의 공격!");
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name}");
                        Console.WriteLine($"HP {temp} -> {(mob.IsDead ? "Dead" : mob.Health)}");
                        Console.WriteLine();
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("공격이 빗나갔습니다.");
                    Console.ReadLine();
                }
            }
        }

        public class Battle_InfoBug : Scene
        {
            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("■■■■■■■■■■■■■■");
                ShowHighlithtesText("Battle!! - 버그 알아보기");
                Console.WriteLine("■■■■■■■■■■■■■■");
                Console.WriteLine();
                int j = 1;
                foreach (Monster mob in spawnlist)
                {
                    if (mob.IsDead == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"[{j++}] ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"[{j++}] ");
                        Console.ResetColor();
                    }
                    Console.Write($"Lv.{mob.Level} ");
                    Console.Write(PadRightForMixedText(mob.Name, 12));
                    Console.WriteLine($"HP {mob.Health}");
                    Console.ResetColor();
                }
                j = 1;
                Console.WriteLine();
                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                Console.WriteLine();
                Console.WriteLine("0. 돌아가기");
            }

            public override void GetAction(int act)
            {
                bool check = (act <= spawnlist.Count && act >= 0);                    
                switch (check)
                {
                    case true:
                        switch (act)
                        {
                            case 0:
                                Game.game.ChangeScene(new Battle_myturn());
                                
                                break;
                            // Scene을 이동할 때에는 Game.game.ChangeScene(new 씬이름()); 을 사용하면 됨
                            case int:                                
                                Console.WriteLine(spawnlist[act - 1].Description);
                                Console.ReadLine();
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("다시 선택해주세요.");
                        break;
                }
            }
        }

        public class BattleSkill : Scene
        {
            public override void ShowInfo()
            {

            }

            public override void GetAction(int act)
            {

            }
        }

        public class Battle_enemyturn : Scene
        {            
            public override void ShowInfo()
            {
                int temp = Game.game.player.CurHP;
                Console.Clear();
                Console.WriteLine("■■■■■■■■■■■■■■");
                ShowHighlithtesText("Battle!! - 적 턴");
                Console.WriteLine("■■■■■■■■■■■■■■");
                Console.WriteLine();
                enemyattack();

                Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                Console.WriteLine($"HP {temp} -> {Game.game.player.CurHP}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");

            }

            public override void GetAction(int act)
            {
                switch (act)
                {
                    default:
                        if (Game.game.player.IsDead != true)
                        Game.game.ChangeScene(new Battle_myturn());
                        
                        else
                        Game.game.ChangeScene(new BattleEnd_Lose());
                        break;
                }
            }

            public void enemyattack()
            {
                Console.Clear();
                foreach (Monster mob in spawnlist)
                {
                    if (mob.IsDead != true)
                    {

                        if (Game.game.player.Evade + rand.Next(20) < mob.ACC + rand.Next(20))
                        {
                            Game.game.player.TakeDamage(mob.ATK);
                            Console.WriteLine($"Lv.{mob.Level} {mob.Name}의 공격!");
                            Console.WriteLine($"{Game.game.player.name} 을(를) 맞췄습니다. [데미지 : {mob.ATK}]");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"Lv.{mob.Level} {mob.Name}의 공격이 빗나갔습니다!");
                            Console.WriteLine();
                        }
                    }
                }
                Console.ReadLine();
            }
        }

        public class BattleEnd_win : Scene
        {
            int sumexp;
            public override void ShowInfo()
            {
                Console.Clear();
                Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = true;
                Console.WriteLine("■■■■■■■■■■■■■■");
                ShowHighlithtesText("Battle!! - Result");
                Console.WriteLine("■■■■■■■■■■■■■■");
                Console.WriteLine();
                PrintTextWithHighlighst(ConsoleColor.Green,"","Victory","");                
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터 {spawnlist.Count}마리를 잡았습니다.");
                foreach(Monster mob in spawnlist)
                {
                    sumexp += mob.Level;
                }
                spawnlist.Clear();
                Console.Write($"Lv. ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{Game.game.player.level} ");
                Console.ResetColor ();
                Console.WriteLine($" {Game.game.player.name}");                
                //Console.WriteLine($"exp {Game.game.player.exp} -> {Game.game.player.exp+sumexp}");
                //Game.game.player.Addexp(sumexp);
                //Console.WriteLine();
                //Console.WriteLine("[획득 아이템]");
                sumexp = 0;
                Console.WriteLine("0. 돌아가기");                 
                }
            public override void GetAction(int act)
            {
                switch (act)
                {
                    case 0:                           
                        Game.game.ChangeScene(new Dungeon_move());
                        break;                    
                }
            }
        }

        public class BattleEnd_Lose : Scene
        {
            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("■■■■■■■■■■■■■■");
                ShowHighlithtesText("Battle!! - Result");
                Console.WriteLine("■■■■■■■■■■■■■■");
                Console.WriteLine();
                PrintTextWithHighlighst(ConsoleColor.Red, "", "You Lose", "");                            
                Console.WriteLine();
                spawnlist.Clear();
                Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                Console.WriteLine($"Hp {BfHp} -> {Game.game.player.CurHP}");                                   
                Console.WriteLine("0. 돌아가기");
            }
            public override void GetAction(int act)
            {
                switch (act)
                {
                    default:
                        //마을로 돌아가게 구현
                        break;
                }
            }
        }
               

        public class UsePotion : Scene
        {
            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("■■■■■■■■■■■■■■");
                ShowHighlithtesText("원기 회복");                
                Console.WriteLine("■■■■■■■■■■■■■■");
                Console.WriteLine("비밀의 드링크를 사용하면 체력을 30 회복 할 수 있습니다.  (남은 드링크 : {0})", potion); 
                Console.WriteLine();
                Console.WriteLine("1. 사용하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
            }

            public override void GetAction(int act)
            {
                switch (act)
                {
                    case 0:
                        if (doing == Doing.beforebattle)
                            Game.game.ChangeScene(new Battle());
                        else if (doing == Doing.battle_ing)
                            Game.game.ChangeScene(new Battle_myturn());
                        break;                        
                    case 1:
                        if (potion > 0 && Game.game.player.CurHP != 100)
                        {
                            Console.Clear();
                            potion--;
                            int temp = Game.game.player.CurHP;
                            Game.game.player.UsePotion();
                            Console.WriteLine($"체력을 회복하였습니다. (남은 드링크 : {potion})");
                            Console.WriteLine($"HP : {temp} -> {Game.game.player.CurHP}");
                            Console.ReadLine();
                            if (doing == Doing.beforebattle)
                                Game.game.ChangeScene(new Battle());
                            else if (doing == Doing.battle_ing)
                                Game.game.ChangeScene(new Battle_myturn());
                        }
                        else if (potion < 1)
                        {
                            Console.Clear();
                            Console.WriteLine($"드링크가 부족합니다.");
                            Console.ReadLine();
                        }
                        else if (potion > 0 && Game.game.player.CurHP == 100)
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체력이 가득 차 있습니다.");
                            Console.ReadLine();
                        }
                        break;
                    default:
                        Console.WriteLine("유효한 입력이 아닙니다!");
                        break;
                }
            }

           
    }
        private static void ShowHighlithtesText(string Text)//글자색깔바꾸기
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Text);
            Console.ResetColor();
        }

        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; //한글과 같은 넓은 문제에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; //나머지 문자에 대해 길이를 1로 취급
                }
            }
            return length;
        }

        private static void PrintTextWithHighlighst(ConsoleColor color,string s1, string s2, string s3 = "") //사이에 있는 텍스트 색깔 바꾸기
        {
            Console.Write(s1);
            Console.ForegroundColor = color;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }
        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
}
}


