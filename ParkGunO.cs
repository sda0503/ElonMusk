using Elonmusk;
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

        public virtual void TakeDamage(int damage)
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
            Name = "Null Reference Exception";
            Level = 2;
            Health = 10;
            ATK = 6;
            ACC = 16;
            Evade = 5;
            Description = "Null의 값을 가질 수 없는 Object에 Null을 할당했기에 발생하거나, 참조하려는 개체가 Null일 때 발생합니다.";
        }
    }

    public class IndexOutOfRange : Monster
    {
        public IndexOutOfRange() //체력, 공격력 높은 대신 명중,회피가 낮음
        {
            Name = "OutofRange";
            Level = 4;
            Health = 20;
            ATK = 10;
            ACC = 10;
            Evade = 0;
            Description = "배열의 크기를 벗어나 접근할 때 발생합니다. 혹시 Array[Length]가 되어있진 않나요?";
        }
    }

    public class OthersCode : Monster
    {
        public OthersCode() //별로 위협적이진 않지만 크게 데일수 있다는 느낌으로 명중0회피0에 데미지만 쎄게
        {
            Name = "남이 작성한 코드";
            Level = 1;
            Health = 5;
            ATK = 20;
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
            ATK = 8;
            ACC = 14;
            Evade = 12;
            Description = "인터넷이 끊겼습니다. 재앙이 아닐 수 없군요. 당신은 가진 지식으로 문제를 헤쳐나가야 합니다.";
        }
    }

    public class TypeError : Monster
    {
        public TypeError()
        {
            Name = "TypeError";
            Level = 5;
            Health = 25;
            ATK = 7;
            ACC = 15;
            Evade = 8;
            Description = "암시적 형변환으로 충분치 않을 때 발생합니다. int num = (int)10.5f 처럼 명시적으로 작성해보세요.";
        }
    }

    public class runtimeerror : Monster
    {
        public runtimeerror()
        {
            Name = "runtime error";
            Level = 9;
            Health = 30;
            ATK = 10;
            ACC = 16;
            Evade = 12;
            Description = "갖은 이유로 실행 중 발생한 오류입니다. 오류 코드를 잘 읽고 디버깅 해보도록 합시다.";
        }
    }

    public class UncontrollableBug : Monster //보스
    {
        public UncontrollableBug()
        {
            Name = "\"관객 공포증\" 버그";
            Level = 10;
            Health = 70;
            ATK = 15;
            ACC = 16;
            Evade = 9;
            Description = "컨트롤 할 수 없는, 중요한 자리에서 발생한 버그입니다.";
        }

        public override void TakeDamage(int damage)
        {            
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                IsDead = true;
                Forsave.isdungeonclear = true; // 보스 잡을 때 엔딩분기 가르기용으로 작성됨.
            }
        }
    }

    public class Investor : Monster //보스
    {
        public Investor()
        {
            Name = "\"THE\" 투자자";
            Level = 15;
            Health = 150;
            ATK = 20;
            ACC = 16;
            Evade = 13;
            Description = "여러분에게 관심이 있는 투자자입니다. 투자자를 설득하고 더 큰 바다로 나아가세요.";
        }

        public override void TakeDamage(int damage)
        {            
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                IsDead = true;
                Forsave.isdungeonclear = true;
                Forsave.isGameclear = true; // 보스 잡을 때 엔딩분기 가르기용으로 작성됨.
            }
        }
    }

    public enum Doing { Idle, beforebattle, battle_ing, beforeDungeon } //상태보기 선택지가 많아서 내가 어디서 왔는 지 구분하기 위해 작성됨

    public class Forsave()
    {
        public static bool Dungeonfirst; //던전 첫 입장인지 확인
        public static bool isdungeonclear;
        public static bool isGameclear;
        public static int dungeonposx;
        public static int dungeonposy;
        public static int dungeonClearCnt; //던전 클리어 횟수 - 둘다 퀘스트나 전직에 사용
        public static int KillCnt; //몹 잡은 횟수
        public static int potion = 3; //포션 개수
        public static Dictionary<int, string> isclear;
        public static int[,] dungeon = new int[3, 3]; // 0 : 미클리어 - 전투, 1 : 미클리어 - 함정, 2 : 미클리어 - 보상, 3 : 클리어, 4 : 현재위치, 5 : 보스방
        public static void dungeonsetting()
        {
            isdungeonclear = false;
            int[] EventList = new int[7] {0,0,0,0,1,1,2}; //이벤트 갯수 정하기
            EventList = EventList.OrderBy(item => new Random().Next()).ToArray() ;
            isclear = new Dictionary<int, string>(){ //방 표시용
            { 0,"X"},
            { 1,"X"},
            { 2,"X"},
            { 3,"O"},
            { 4,"H"},
            { 5,"B"}
            };
            dungeon = new int[3, 3] { //보스방이랑 시작위치만 고정하고 나머지 방은 Random으로 섞어버리자. || 전투 4개, 함정 2개, 보상 1개 해서 배열 만든다음에 오더바이해서 섞어버리고 포문으로 넣어버리는 게 더 깔끔할 듯.
            { 5, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 3 } };
            int k = 0;
            for (int i= 0; i< dungeon.GetLength(0); i++) //고정위치 빼고 남은 방에 이벤트 분배
            {
                for (int j =0; j<dungeon.GetLength(1); j++)
                {
                    if (dungeon[i, j] == 0)
                    {
                        dungeon[i, j] = EventList[k];
                        k++;
                    }
                }
            }
            k = 0;

            Forsave.dungeonposx = Forsave.dungeon.GetLength(0) - 1;
            Forsave.dungeonposy = Forsave.dungeon.GetLength(1) - 1;
        }
    }

    public class Dungeon : Scene
    {
        public static Doing doing;
        public override void ShowInfo()
        {
            if (Forsave.Dungeonfirst == false) //최초 입장 시에만 던전 초기화
            {
                Forsave.dungeonsetting();
                Forsave.Dungeonfirst = true;
            }
            Console.WriteLine("■■■■■■■■■■■");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("발표 준비하기");
            Console.ResetColor();
            Console.WriteLine("■■■■■■■■■■■");
            Console.WriteLine();
            Console.WriteLine("여기는 발표회장에 들어가기 전 마지막으로 디버깅 해보는 장소입니다.");
            Console.WriteLine("입장하기 발생하는 오류들을 수정하고, 성공적으로 발표를 마쳐보세요!");
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
                    Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 4; //처음 입장하는 곳 전투X
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
                Board();
                Console.WriteLine();
                Console.WriteLine("현재위치 : H || 발표회장 : B");
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
                            Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 3; //그 위치에서 이동한다는건 클리어 했기에 이동하는거
                            Forsave.dungeonposy--;
                        }
                        break;

                    case 6: //오른쪽
                        if (Forsave.dungeonposy + 1 >= Forsave.dungeon.GetLength(1))
                            Console.WriteLine("그곳으로는 갈 수 없습니다.");
                        else
                        {
                            Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 3;
                            Forsave.dungeonposy++;
                        }
                        break;

                    case 8: //위
                        if (Forsave.dungeonposx - 1 < 0)
                            Console.WriteLine("그곳으로는 갈 수 없습니다.");
                        else
                        {
                            Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 3;
                            Forsave.dungeonposx--;
                        }
                        break;

                    case 2: //아래
                        if (Forsave.dungeonposx + 1 >= Forsave.dungeon.GetLength(1))
                            Console.WriteLine("그곳으로는 갈 수 없습니다.");
                        else
                        {
                            Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 3;
                            Forsave.dungeonposx++;
                        }
                        break;

                    default:
                        Console.WriteLine("그곳으로는 갈 수 없습니다..");
                        break;
                }
                switch (Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy])
                {
                    case 0:
                        Game.game.ChangeScene(new Battle()); 
                        break;
                    case 1:
                        Game.game.ChangeScene(new Battle.Battle_Trap());
                        break;
                    case 2:
                        Game.game.ChangeScene(new Battle.Battle_Price());
                        break;
                    case 3:
                        Console.Clear();
                        Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 4; //클리어한 방끼리 이동할 떄를 위해 작성
                        break;
                    case 5:
                        Game.game.ChangeScene(new Battle());
                        break;
                    default:
                        break;
                }
            }
            static void Board()
            {
                string[] roomname = new string[9]; //던전 크기에 따라 바꿔야됨.
                int j = 0;
                foreach (int i in Forsave.dungeon)
                {
                    switch (i)
                    {
                        case 0:
                            roomname[j] = Forsave.isclear[0];
                            break;
                        case 1:
                            roomname[j] = Forsave.isclear[1];
                            break;
                        case 2:
                            roomname[j] = Forsave.isclear[2];
                            break;
                        case 3:
                            roomname[j] = Forsave.isclear[3];
                            break;
                        case 4:
                            roomname[j] = Forsave.isclear[4];
                            break;
                        case 5:
                            roomname[j] = Forsave.isclear[5];
                            break;
                    }
                    j++;
                }
                j = 0;
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", roomname[0], roomname[1], roomname[2]);
                Console.WriteLine("_____|_____|_____");
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", roomname[3], roomname[4], roomname[5]);
                Console.WriteLine("_____|_____|_____");
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", roomname[6], roomname[7], roomname[8]);
                Console.WriteLine("     |     |     ");
            }
        }


        public class Battle : Scene
        {
            static Random rand = new Random();
            static List<Monster> spawnlist = new List<Monster>(4); //몬스터 최대 4마리까지
            static int BfHp = 0; //전투 시작 전 체력
            

            //static int turn = 0;

            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("버그를 발견했습니다.");
                Console.WriteLine("이제 디버깅을 시작할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 디버깅 시작");
                Console.WriteLine("3. 회복 아이템");
                Console.WriteLine();
            }

            public override void GetAction(int act)
            {
                switch (act)
                {
                    case 1:
                        doing = Doing.beforebattle; //상태보기로 이동 시, enum을 변경해서 다시 이 Scene으로 돌아올 수 있도록 작성
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
                BfHp = Game.game.player.CurHP;
                int spawnCut = (Game.game.player.level < 3 ? 3 : 5);
                int LevelCut = (Game.game.player.level < 3 ? 3 : 6);
                if (Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] != 5)
                {
                    for (int i = 0; i < rand.Next(1, spawnCut); i++)
                    {
                        switch (rand.Next(LevelCut))
                        {
                            case 0:
                                spawnlist.Add(new OthersCode()); //레벨1                                
                                break;
                            case 1:
                                spawnlist.Add(new Null()); //레벨2                                
                                break;
                            case 2:
                                spawnlist.Add(new IndexOutOfRange()); //레벨4
                                break;
                            case 3:
                                spawnlist.Add(new TypeError()); //레벨5
                                break;
                            case 4:
                                spawnlist.Add(new Offline()); //레벨7                               
                                break;
                            case 5:
                                spawnlist.Add(new runtimeerror()); //레벨9
                                break;
                        }
                    }
                }
                else if (Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] == 5 && Game.game.player.JobToInt(Game.game.player.job) < 2) //일반보스 조건 더걸기
                    spawnlist.Add(new UncontrollableBug());
                else if (Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] == 5 && Game.game.player.JobToInt(Game.game.player.job) >= 2) //찐보스 조건 더걸기
                    spawnlist.Add(new Investor());

                Game.game.ChangeScene(new Battle_myturn());
            }

            public class Battle_myturn : Scene
            {
                public override void ShowInfo()
                {
                    Console.Clear();
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    ShowHighlithtesText("오류 수정!! - 플레이어 턴");
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    Console.WriteLine();
                    foreach (Monster mob in spawnlist)
                    {
                        if (mob.IsDead == true)
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"Lv.{mob.Level} ");
                        Console.Write(PadRightForMixedText(mob.Name, 25));
                        Console.WriteLine($"HP {mob.Health}");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                    Console.WriteLine("[내 정보]");
                    Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.PlayerName.Item1}");
                    Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                    Console.WriteLine($"MP {Game.game.player.MaxMP}/{Game.game.player.CurMP}");
                    Console.WriteLine();
                    Console.WriteLine("1. 디버그");
                    Console.WriteLine("2. 스킬");
                    Console.WriteLine("3. 버그 알아보기");
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
                            Game.game.ChangeScene(new BattleSkillChoose());
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
                    ShowHighlithtesText("오류 수정!! - 플레이어 턴");
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
                        Console.Write(PadRightForMixedText(mob.Name, 25));
                        Console.WriteLine($"HP {mob.Health}");
                        Console.ResetColor();
                    }
                    j = 1;
                    Console.WriteLine();
                    Console.WriteLine("[내 정보]");
                    Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                    Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                    Console.WriteLine($"MP {Game.game.player.MaxMP}/{Game.game.player.CurMP}");
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
                                        Console.WriteLine("이미 해결된 버그입니다.");
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

                public void attack(float ATK, Monster mob)
                {
                    Console.Clear();
                    Random rand = new Random();
                    float PlayerATK = Game.game.player.ATK + Game.game.player.EquipmentStat.ATK; //기본 공격력 + 장비 공격력
                    int error = (int)MathF.Ceiling(PlayerATK / 10f);
                    int damage = rand.Next((int)MathF.Ceiling(PlayerATK) - error, (int)MathF.Ceiling(PlayerATK) + error);
                    int temp = mob.Health;
                    int ACC= Game.game.player.ACC + rand.Next(20);
                    int Evade = mob.Evade + rand.Next(20);
                    if (ACC >=Evade)
                    {
                        if (rand.Next(100) > 84)
                        {
                            damage = (int)MathF.Ceiling(damage * 1.6f);
                            mob.TakeDamage(damage);
                            Console.WriteLine($"{Game.game.player.name}의 오류 수정!");
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
                            Console.WriteLine($"{Game.game.player.name}의 오류 수정!");
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
                        Console.WriteLine("수정이 실패했습니다.");
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
                    ShowHighlithtesText("오류 수정!! - 버그 알아보기");
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
                        Console.Write(PadRightForMixedText(mob.Name, 25));
                        Console.WriteLine($"HP {mob.Health}");
                        Console.ResetColor();
                    }
                    j = 1;
                    Console.WriteLine();
                    Console.WriteLine("[내 정보]");
                    Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                    Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                    Console.WriteLine($"MP {Game.game.player.MaxMP}/{Game.game.player.CurMP}");
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

            public class BattleSkillChoose : Scene
            {
                public override void ShowInfo()
                {
                    Console.WriteLine("Battle!! - 플레이어 턴");
                    Console.WriteLine();
                    foreach (Monster mob in spawnlist)
                    {
                        if (mob.IsDead == true)
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name}  HP {mob.Health}");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                    Console.WriteLine("[내 정보]");
                    Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                    Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                    Console.WriteLine($"MP {Game.game.player.MaxMP}/{Game.game.player.CurMP}");
                    Console.WriteLine();
                    int i = 1;
                    foreach (var skill in Game.game.player.skills)
                    {
                        Console.WriteLine($"{i++}. {skill.Name} - MP {skill.Cost}");
                        Console.WriteLine($"- {skill.Description1}");
                        Console.WriteLine($"{skill.Description2}");
                        Console.WriteLine();
                    }
                    Console.WriteLine("0. 취소");
                }

                public override void GetAction(int act)
                {
                    
                    if (act <= Game.game.player.skills.Count && act >= 1)
                    {
                        if (Game.game.player.skills[act - 1].Cost <= Game.game.player.CurMP)
                        {
                            Game.game.player.skills[act - 1].UseSkill(spawnlist);
                            bool isAlive = false;
                            foreach (Monster mob in spawnlist)
                            {
                                if (!mob.IsDead)
                                {
                                    isAlive = true;
                                    break;
                                }
                            }
                            if (isAlive == true)
                                Game.game.ChangeScene(new Battle_enemyturn());
                            else
                            {
                                //if (stage == bossStage)
                                //Game.game.ChangeScene(new HappyEndding());
                                //else
                                Game.game.ChangeScene(new BattleEnd_win());
                            }
                        }
                        else
                        {
                            Console.WriteLine("MP가 부족합니다.");
                        }
                    }
                    else if (act == 0)
                    {
                        Game.game.ChangeScene(new Battle_myturn());
                    }
                    else
                        Console.WriteLine("유효한 입력이 아닙니다!");
                }

            }

            public class Battle_enemyturn : Scene
            {
                public override void ShowInfo()
                {
                    int temp = Game.game.player.CurHP;
                    Console.Clear();
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    ShowHighlithtesText("오류 수정!! - 적 턴");
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
                            int Evade = Game.game.player.Evade + rand.Next(20);
                            int ACC = mob.ACC + rand.Next(20);
                            int damage = Game.game.player.DEF - mob.ATK;
                            if (damage < 0)
                                damage = 0;
                            
                            if ( Evade < ACC )
                            {
                                Game.game.player.SetPlayerHP(-damage);
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
                    string togo = "";
                    Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 4; //전투에서 승리하고 나면 그 자리에 있으니까 현재위치로 표시
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    ShowHighlithtesText("오류 수정!! - Result");
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    Console.WriteLine();
                    PrintTextWithHighlighst(ConsoleColor.Green, "", "디버깅 완료!", "");
                    Forsave.KillCnt += spawnlist.Count;
                    if (!(spawnlist[0] is UncontrollableBug) && !(spawnlist[0] is Investor)) //보스 판별
                    {
                        Console.WriteLine();
                        Console.WriteLine($"디버깅에서 오류 {spawnlist.Count}개를 수정했습니다.");
                        Console.WriteLine();
                        Console.Write("현재까지 잡은 버그 갯수 : ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("{0}", Forsave.KillCnt);
                        Console.ResetColor();
                        togo = "준비과정으로 돌아가기";
                    }
                    else if (spawnlist[0] is UncontrollableBug)
                    {                      
                        Console.WriteLine("축하합니다! 발표를 성공적으로 마쳤습니다!");
                        Forsave.dungeonClearCnt++;
                        Console.WriteLine();
                        Console.Write("진행한 발표 횟수 : ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("{0}", Forsave.dungeonClearCnt);
                        Console.ResetColor();

                        Console.Write("현재까지 잡은 버그 갯수 : ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("{0}", Forsave.KillCnt);
                        Console.ResetColor();
                        togo = "회사로 돌아가기";
                    }
                    else if (spawnlist[0] is Investor)
                    {
                        Console.WriteLine("축하합니다! 투자 유치를 성공적으로 마쳤습니다!");
                        Console.WriteLine("당신의 발표에 감동한 투자자가 엄청난 투자를 약속했습니다!");
                        Console.WriteLine("수많은 버그를 뚫고 이뤄낸 성과!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("당신은 영웅입니다!");
                        Console.ResetColor();                        
                        //여기서 엔딩으로 넘어가면 될 듯
                    }

                    foreach (Monster mob in spawnlist)
                    {
                        sumexp += mob.Level;
                    }
                    
                    Console.Write($"Lv. ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"{Game.game.player.level} ");
                    Console.ResetColor();
                    Console.WriteLine($" {Game.game.player.name}");
                    Console.WriteLine($"exp {Game.game.player.EXP} -> {Game.game.player.EXP + sumexp}");
                    Console.WriteLine();
                    Console.WriteLine("[획득 아이템]");
                    if (!(spawnlist[0] is UncontrollableBug) && !(spawnlist[0] is Investor))
                    {
                        PrintTextWithHighlighst(ConsoleColor.Magenta, "", $"{spawnlist.Count * 150} G");
                        Game.game.player.gainGold(spawnlist.Count * 150);
                    }
                    else if (spawnlist[0] is UncontrollableBug)
                    {
                        PrintTextWithHighlighst(ConsoleColor.Magenta, "", $"{spawnlist.Count * 5000} G");
                        Game.game.player.gainGold(spawnlist.Count * 5000);
                    }
                    else if (spawnlist[0] is Investor)
                    {
                        PrintTextWithHighlighst(ConsoleColor.Magenta, "", $"{spawnlist.Count * 300000} G");
                        Game.game.player.gainGold(spawnlist.Count * 300000);
                    }
                    int getpotion = rand.Next(100);
                    if (getpotion >= 80)
                    {
                        Console.Write("포션 ");
                        Console.ForegroundColor= ConsoleColor.Magenta;
                        Console.Write($"{Forsave.potion} -> {Forsave.potion+1}");
                        Console.ResetColor();
                        Forsave.potion++;
                    }
                    
                    Game.game.player.Addexp(sumexp);
                    Game.game.player.LevelCal();
                    spawnlist.Clear();
                    Console.WriteLine();
                    //Console.WriteLine("[획득 아이템]"); //확률에 따라서 그냥 랜덤 아이템 레어도 가격 낮은 걸로 드랍.
                    sumexp = 0;
                    Console.WriteLine($"0. {togo}");
                }
                public override void GetAction(int act)
                {
                    switch (act)
                    {
                        case 0:
                            if (!Forsave.isdungeonclear)
                                Game.game.ChangeScene(new Dungeon_move());
                            else if (Forsave.isdungeonclear && Forsave.isGameclear)
                            {
                                {
                                    //Game.game.ChangeScene(new Dungeon()); //엔딩으로 넘어가기
                                    Forsave.isdungeonclear = false;
                                    Forsave.Dungeonfirst = false;
                                }
                            }
                            else if (Forsave.isdungeonclear && !Forsave.isGameclear)
                            {
                                Game.game.ChangeScene(new Dungeon());
                                Forsave.isdungeonclear = false;
                                Forsave.Dungeonfirst = false;
                            }
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
                    ShowHighlithtesText("오류 수정!! - Result");
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    Console.WriteLine();
                    PrintTextWithHighlighst(ConsoleColor.Red, "", "디버깅에 실패했습니다.", "");
                    Console.WriteLine();
                    spawnlist.Clear();
                    Console.WriteLine("준비해서 다시 도전합시다.");
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
                    Console.WriteLine($"Hp {BfHp} -> {Game.game.player.CurHP}");
                    Console.WriteLine("0. 회사로 돌아가기");
                }
                public override void GetAction(int act)
                {
                    switch (act)
                    {
                        default:
                            Game.game.ChangeScene(Game.game.idle);
                            break;
                    }
                }
            }


            public class UsePotion : Scene //기본으로 3개 주어지는 포션
            {
                public override void ShowInfo()
                {
                    Console.Clear();
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    ShowHighlithtesText("원기 회복");
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    Console.WriteLine("비밀의 드링크를 사용하면 체력을 30 회복 할 수 있습니다.  (남은 드링크 : {0})", Forsave.potion);
                    Console.WriteLine();
                    Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
                    Console.WriteLine($"MP {Game.game .player.MaxMP}/{Game.game.player.CurMP}");
                    Console.WriteLine();
                    Console.WriteLine("1. 사용하기");
                    Console.WriteLine("2. 이건 싫다. 다른 건 없나?");
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
                            if (Forsave.potion > 0 && Game.game.player.CurHP != 100)
                            {
                                Console.Clear();
                                Forsave.potion--;
                                int temp = Game.game.player.CurHP;
                                Game.game.player.UsePotion();
                                Console.WriteLine($"체력을 회복하였습니다. (남은 드링크 : {Forsave.potion})");
                                Console.WriteLine($"HP : {temp} -> {Game.game.player.CurHP}");
                                Console.ReadLine();
                                if (doing == Doing.beforebattle)
                                    Game.game.ChangeScene(new Battle());
                                else if (doing == Doing.battle_ing)
                                    Game.game.ChangeScene(new Battle_myturn());
                            }
                            else if (Forsave.potion < 1)
                            {
                                Console.Clear();
                                Console.WriteLine($"드링크가 부족합니다.");
                                Console.ReadLine();
                            }
                            else if (Forsave.potion > 0 && Game.game.player.CurHP == 100)
                            {
                                Console.Clear();
                                Console.WriteLine("이미 체력이 가득 차 있습니다.");
                                Console.ReadLine();
                            }
                            break;
                        case 2:
                            Game.game.ChangeScene(new UsePotion_Find());
                            break;
                        default:
                            Console.WriteLine("유효한 입력이 아닙니다!");
                            break;
                    }
                }
            }

            public class UsePotion_Find : Scene //구매한 아이템 사용하기
            {
                List<int> itemindex = new List<int>();
                public override void ShowInfo()
                {
                    Console.Clear();
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    ShowHighlithtesText("다른 건 뭐가 있을까");
                    Console.WriteLine("■■■■■■■■■■■■■■");
                    Console.WriteLine();
                    
                    int j = 1;
                    for (int i = 0; i < Game.game.player.items.Count; i++)
                    {
                        (Item, bool) item = Game.game.player.items[i];
                        if (item.Item1.itemType == Item.ItemType.USE)
                        {
                            itemindex.Add(i);
                            string name = item.Item1.name;
                            string effect = "";
                                switch (name)
                                {
                                    //체력
                                    case "아이스아메리카노":
                                    effect = "HP : 10";
                                        break;
                                    case "코카콜라(빨간포션)":
                                    effect = "HP : 30";
                                    break;
                                    case "샌드위치":
                                    effect = "HP : 50";
                                    break;
                                    //MP
                                    case "팹시(파란포션)":
                                    effect = "MP : 30";
                                    break;
                                }
                            Console.WriteLine($" - {j++} {Game.game.player.items[i].Item1.name}  {Game.game.player.items[i].Item1.desc} | {effect}");
                        }
                    }
                    j = 1;                    
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                }

                public override void GetAction(int act)
                {
                    if (act >= 0 && act <= itemindex.Count)
                    {
                        switch (act)
                        {
                            case 0:
                                Game.game.ChangeScene(new UsePotion());
                                break;
                            case int:
                                string name = Game.game.player.items[itemindex[act-1]].Item1.name;
                                switch (name)
                                {
                                    //체력
                                    case "아이스아메리카노":
                                        Game.game.player.SetPlayerHP(10);
                                        break;
                                    case "코카콜라(빨간포션)":
                                        Game.game.player.SetPlayerHP(30);
                                        break;
                                    case "샌드위치":
                                        Game.game.player.SetPlayerHP(50);
                                        break;
                                    //MP
                                    case "팹시(파란포션)":
                                        Game.game.player.SetPlayerMP(-30);
                                        break;
                                }
                                Game.game.player.items.RemoveAt(itemindex[act-1]);
                                break;                           
                        }
                    }

                }
            }


            public class Battle_Trap : Scene
            {
                public override void ShowInfo()
                {
                    Console.Clear();
                    Console.WriteLine("너무 열심히 일한 나머지 지쳤습니다.");
                    Console.WriteLine();
                    PrintTextWithHighlighst(ConsoleColor.Magenta, "Lv. ", $"{Game.game.player.level} ", $" {Game.game.player.PlayerName.Item1}");
                    Console.WriteLine();
                    Console.WriteLine($"HP {Game.game.player.CurHP} -> {Game.game.player.CurHP-10}");
                    Game.game.player.SetPlayerHP(-10);
                    Console.WriteLine(); 
                    Console.WriteLine("0. 돌아가기");
                }

                public override void GetAction(int act)
                {
                    switch (act)
                    {
                        case 0:
                            Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 4;
                            Game.game.ChangeScene(new Dungeon_move());
                            break;
                        default:
                            break;
                    }
                }
            }

            public class Battle_Price : Scene
            {
                public override void ShowInfo()
                {
                    Console.Clear();
                    Console.WriteLine("지나가다 돈을 주웠습니다.");
                    Console.WriteLine();
                    PrintTextWithHighlighst(ConsoleColor.Magenta, "", $"{50000} G");
                    Game.game.player.gainGold(50000);
                    Console.WriteLine();
                    //나중에 히든 아이템 추가하기
                    Console.WriteLine();
                    Console.WriteLine("0. 돌아가기");
                }

                public override void GetAction(int act)
                {
                    switch (act)
                    {
                        case 0:
                            Forsave.dungeon[Forsave.dungeonposx, Forsave.dungeonposy] = 4;
                            Game.game.ChangeScene(new Dungeon_move());
                            break;
                        default:
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

            private static void PrintTextWithHighlighst(ConsoleColor color, string s1, string s2, string s3 = "") //사이에 있는 텍스트 색깔 바꾸기
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


