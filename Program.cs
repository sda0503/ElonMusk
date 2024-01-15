using ElonMusk;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        public Player player { get; private set; }

        public PlayerInfo playerInfo { get; private set; }
        public Shop shop { get; private set; }
        public Buy buy { get; private set; }
        public Inventory inventory { get; private set; }
        public Opening opening { get; private set; }

        public BTestScene btestScene { get; private set; }
        public SellScene sellScene { get; private set; }

        public Resume resume { get; private set; }
        public BTestScene bTestScene { get; private set; }
        public SpartaMusumeGame sparta {get; private set; }

        public Idle idle { get; private set; }
        public Equipment equipment { get; private set; }

        public Quest quset { get; private set; }

        public Job job { get; private set; }

        public Casino casino { get; private set; }


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
            player = new Player();
            playerInfo = new PlayerInfo();
            shop = new Shop();
            inventory = new Inventory();
            idle = new Idle();
            equipment = new Equipment();
            buy = new Buy();
            opening = new Opening();

            btestScene = new BTestScene();
            sellScene  = new SellScene();
            resume = new Resume();
            quset = new Quest();
            job = new Job();
            casino = new Casino();

            curScene = new Dungeon();
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
            Console.WriteLine();
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ");
            Console.WriteLine("(주)스파르타에 오신 여러분 환영합니다.");
            Console.WriteLine("");
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ");
            Console.WriteLine("           __________                                 ");
            Console.WriteLine("         .'----------`.                              ");
            Console.WriteLine("         | .--------. |                             ");
            Console.WriteLine("         | |########| |       __________              ");
            Console.WriteLine("         | |########| |      /__________\\            ");
            Console.WriteLine(".--------| `--------' |------|    --=-- |-------------.");
            Console.WriteLine("|        `----,-.-----'      |o ======  |             | ");
            Console.WriteLine("|       ______|_|_______     |__________|             | ");
            Console.WriteLine("|      /  %%%%%%%%%%%%  \\                             | ");
            Console.WriteLine("|     /  %%%%%%%%%%%%%%  \\                            | ");
            Console.WriteLine("|     ^^^^^^^^^^^^^^^^^^^^                            | ");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ");

            
            Console.WriteLine();
            Console.WriteLine("0. 상태창");
            Console.WriteLine("1. 가방");
            Console.WriteLine("2. 상점");
            Console.WriteLine("3. 일하기");
            Console.WriteLine("4. 인사평가");
            Console.WriteLine("5. 발표하기");
            Console.WriteLine("6. 강원랜드");
            Console.WriteLine("7. 도전과제");
            Console.WriteLine("8. 저장");

        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                // Scene을 이동할 때에는 Game.game.ChangeScene(new 씬이름()); 을 사용하면 됨
                case 0: //상태창
                    Dungeon.doing = Doing.Idle;
                    Game.game.ChangeScene(new PlayerInfo());
                    break;
                case 1: //가방
                    Game.game.ChangeScene(new Inventory());
                    break;
                case 2: //상점
                    Game.game.ChangeScene(new Shop());
                    break;
                case 3: //일하기
                    Game.game.ChangeScene(new Dungeon());
                    break;
                case 4: //인사평가
                    Game.game.ChangeScene(new Job());
                    break;

                case 5: //던전입장
                    Game.game.ChangeScene(new Dungeon());
                    break;
                case 6: //강원래드
                    Game.game.ChangeScene(new Casino());
                    break;
                case 7: //퀘스트
                    Game.game.ChangeScene(new Quest());

                    break;
                case 8: //저장
                    Game.game.ChangeScene(new Idle());
                    break;
                case 99:
                    Game.game.ChangeScene(new Resume());
                    break;
                case 34:
                    Game.game.ChangeScene(new BTestScene());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }

    public class PlayerInfo : Scene
    {       
        public override void ShowInfo()
        {
            Game.game.player.ShowPlayerProfile();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    if (Dungeon.doing == Doing.beforebattle)
                        Game.game.ChangeScene(new Dungeon.Battle());
                    else if (Dungeon.doing == Doing.beforeDungeon)
                        Game.game.ChangeScene(new Dungeon());
                    else if (Dungeon.doing == Doing.Idle)
                        Game.game.ChangeScene(Game.game.idle);
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }

    public class Inventory : Scene
    {
        
        public override void ShowInfo()
        {
            Console.WriteLine("가방");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Game.game.player.items.Count; i++)
            {
                (Item, bool) item = Game.game.player.items[i];
                String strEquipped = (item.Item2) ? "[E]" : String.Empty;
                Console.WriteLine($"- {i + 1} {strEquipped}{item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}");
                Console.WriteLine(item.Item1.itemType);
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Idle());
                    break;
                case 1:
                    Game.game.ChangeScene(new Equipment());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }

    public class Equipment : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("가방 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Game.game.player.items.Count; i++)
            {
                (Item, bool) item = Game.game.player.items[i];
                if(item.Item1.itemType.ToString() == "NONE" || item.Item1.itemType.ToString()=="USE")
                {
                    continue;
                }
                String strEquipped = (item.Item2) ? "[E]" : String.Empty;
                Console.WriteLine($"- {i + 1} {strEquipped}{item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }
        public override void GetAction(int act)
        {
            if (act == 0)
            {
                Game.game.ChangeScene(new Inventory());
            }
            else if (act > 0 && act < Game.game.player.items.Count + 1)
            {
                Game.game.player.EquipOrDequip(act - 1);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }

    public class Shop : Scene
    {
        // bool 변수는 팔렸는지의 여부(true면 구매불가)
        public List<(Item, bool)> items { get; protected set; }

        public Shop()
        {
            items = new List<(Item, bool)>();
            items.Add((new Item("오래된 검정 후드티", "편하게 입기 좋은 후드티", 0, 5, 10000, Item.ItemType.ARMOR), false));
            items.Add((new Item("깔끔해 보이는 정장", "멀끔해 보이나 코딩력은 낮아보인다.", 0, 15, 80000, Item.ItemType.ARMOR), false));
            items.Add((new Item("개발자의 체크 난방", "디자인은 별로이지만 코딩력이 상당히 높아보이는 옷", 0, 30, 100000, Item.ItemType.ARMOR), false));
            items.Add((new Item("오래된 키보드&마우스세트", "오래되어 작동이 잘안되는 키보드와 마우스 세트", 15, 0, 10000, Item.ItemType.WEAPON), false));
            items.Add((new Item("인체공학 키보드&마우스 세트", "인체에 최적화 된 쓰기 편한 키보드와 마우스 세트입니다.", 30, 0, 200000, Item.ItemType.WEAPON), false));
            items.Add((new Item("C# 전공책", "냄비 받침으로 쓰기 좋은 두꺼운 전공책", 5, 0, 30000, Item.ItemType.ACCESSORY), false));
            items.Add((new Item("오래된 노트북", "메모장이 겨우 돌아가는 노트북!.", 12, 0, 300000, Item.ItemType.ACCESSORY), false));
            items.Add((new Item("최신형 맥북", "최신형 맥북 개발자라면 맥 정도는 써야지요", 25, 0, 1700000, Item.ItemType.ACCESSORY), false));
            items.Add((new Item("Chat GPT 코칭권", "무엇이든지 답해주는 있는 만능 아이템", 0, 0, 50000, Item.ItemType.USE), false));
            items.Add((new Item("아이스아메리카노", "추운날에도 나를 깨워주는 각성제", 0, 0, 1000, Item.ItemType.USE), false));
            items.Add((new Item("코카콜라(빨간포션)", "먹으면 체력을 회복해주는 아이템", 0, 0, 1500, Item.ItemType.USE), false));
            items.Add((new Item("팹시(파란포션)", "먹으면 기력을 회복해주는 아이템", 0, 0, 1500, Item.ItemType.USE), false));
            items.Add((new Item("샌드위치", "배고플때 허기 채우기 좋은 아이템", 0, 0, 2500, Item.ItemType.USE), false));
        }

        public override void ShowInfo()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Game.game.player.GOLD}G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            foreach (var item in items)
            {
                if (item.Item1.itemType.ToString() == "ARMOR")
                {
                    Console.WriteLine($"- {item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}G");
                }
            }
            Console.WriteLine();
            Console.WriteLine($" [무기류]");
            foreach (var item in items)
            {
                if (item.Item1.itemType.ToString() == "WEAPON")
                {
                    Console.WriteLine($"- {item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}G");
                }
            }
            Console.WriteLine();
            Console.WriteLine($" [장신구]");
            foreach (var item in items)
            {
                if (item.Item1.itemType.ToString() == "ACCESSORY")
                {
                    Console.WriteLine($"- {item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}G");
                }
            }
            Console.WriteLine();
            Console.WriteLine($" [소모품]");
            foreach (var item in items)
            {
                if (item.Item1.itemType.ToString() == "USE")
                {
                    Console.WriteLine($"- {item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}G");
                }
            }
            Console.WriteLine();
            Console.WriteLine("1.아이템 구매");
            Console.WriteLine("2.아이템 판매");
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Idle());
                    break;
                case 1:
                    Game.game.ChangeScene(new Buy());
                    break;
                case 2:
                    Game.game.ChangeScene(new SellScene());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        public void TryBuyItem(int index)
        {
            if (items[index].Item2 == true)
                Console.WriteLine("이미 구매한 아이템입니다.");
            else
            {
                if (Game.game.player.GOLD >= items[index].Item1.GOLD)
                {
                    Game.game.player.AddItem(items[index].Item1);
                    Console.WriteLine("구매를 완료했습니다.");
                    items[index] = (items[index].Item1, true);
                    Game.game.player.ConsumeGold(items[index].Item1.GOLD);
                }
                else Console.WriteLine("골드가 부족합니다.");
                if (items[index].Item1.itemType.ToString() == "USE")
                {
                    items[index] = (items[index].Item1, false);
                }
            }
        }
        public void TrySellItem(int index)
        {
            Game.game.player.RemoveItem(index);
        }
    }

    public class Buy : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Game.game.player.GOLD}");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Game.game.shop.items.Count; i++)
            {
                (Item, bool) item = Game.game.shop.items[i];
                String strSold = (item.Item2) ? "구매완료" : $"{item.Item1.GOLD}";
                Console.WriteLine($"- {i + 1} {item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {strSold}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0)
            {
                Game.game.ChangeScene(new Shop());
            }
            else if (act > 0 && act < Game.game.shop.items.Count + 1)
            {
                Game.game.shop.TryBuyItem(act - 1);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

    }

    public class Item : Stat
    {
        public string desc { get; private set; }

        public enum ItemType { NONE, WEAPON, ARMOR, ACCESSORY, USE };

        public ItemType itemType = ItemType.NONE;
        public Item()
        {
            name = "Item";
            desc = "ItemDescription";
        }


        public Item(string name, string desc, int ATK, int DEF, int GOLD, ItemType thisType)
        {
            this.name = name;
            this.desc = desc;
            this.ATK = ATK;
            this.DEF = DEF;
            this.GOLD = GOLD;
            this.itemType = thisType;
        }

        public Item(Item item)
        {
            this.name = item.name;
            this.desc = item.desc;
            this.ATK = item.ATK;
            this.DEF = item.DEF;
            this.GOLD = item.GOLD;
            this.itemType = item.itemType;
        }

        public string GetEffectScript()
        {
            if (ATK > 0)
                return $"공격력 +{ATK}";
            if (DEF > 0)
                return $"방어력 +{DEF}";
            return "효과 없음";
        }
    }

    public class Stat
    {
        public string name { get; protected set; }
        public int level { get; protected set; }

        public float ATK { get; protected set; }
        public int DEF { get; protected set; }
        public int ACC { get; protected set; }
        public int Evade { get; protected set; }

        public int GOLD { get; protected set; }
        public int EXP { get; protected set; }

        public bool IsDead { get; protected set; }

        public Stat(string name = "", int level = 0, int ATK = 0, int DEF = 0, int GOLD = 0)
        {
            this.name = name;
            this.level = level;
            this.ATK = ATK;
            this.DEF = DEF;            
            this.GOLD = GOLD;
        }

        

        public static Stat operator +(Stat s1, Stat s2)
        {
            s1.level += s2.level;
            s1.ATK += s2.ATK;
            s1.DEF += s2.DEF;
            s1.GOLD += s2.GOLD;
            return s1;
        }
    }

    public class Unit : Stat
    {
        public int MaxHP { get; protected set; }
        public int CurHP { get; protected set; }

        public int MaxMP { get; protected set; }
        public int CurMP { get; protected set; }
    }

    public class Player : Unit
    {
        public List<(Item, bool)> items { get; private set; }
        public List<Item> equipedItems {  get; private set; }
        private(string, bool) playerName;
        public string jobName;
        public int QusetCnt =0;

        public (string, bool) PlayerName{get{ return playerName;}set { playerName = value; } }
        public enum JOB
        {
            Intern,                 //인턴
            Assistant,              //사원
            JuniorProgrammer,       //주니어
            SeniorProgrammer,       //시니어 (대리,주임)
            Manager                 //과장
        }

        public JOB job;        

        public string JobToString(JOB j)
        {
            switch (j)
            {
                case JOB.Intern: jobName = "인턴"; break;
                case JOB.Assistant: jobName = "사원"; break;
                case JOB.JuniorProgrammer: jobName = "주니어개발자"; break;
                case JOB.SeniorProgrammer: jobName = "시니어개발자"; break;
            }
            return jobName;
        }

        public int jobNum;
        public int JobToInt(JOB j)
        {
            switch (j)
            {
                case JOB.Intern: jobNum = 0; break;
                case JOB.Assistant: jobNum = 1; break;
                case JOB.JuniorProgrammer: jobNum = 2; break;
                case JOB.SeniorProgrammer: jobNum = 3; break;
            }
            return jobNum;
        }

        public Stat EquipmentStat
        {
            get
            {
                Stat stat = new Stat();
                foreach (var i in Game.game.player.items)
                {
                    if (i.Item2)
                    {
                        stat = stat + i.Item1;
                    }
                }
                return stat;
            }
        }

        public Stat RealtimeStat
        {
            get { return EquipmentStat + this; }
        }

        public List<Skill> skills;

        public Player()
        {
            items = new List<(Item, bool)>();
            equipedItems = new List<Item> ();
            playerName.Item2 = false;
            name = "Victor";
            level = 5;
            job = JOB.SeniorProgrammer;
            ATK = 10;
            DEF = 5;
            MaxHP = 100;
            CurHP = 100; 
            ACC = 100;
            Evade = 10;
            GOLD = 1500000;
            MaxMP = 100;
            CurMP = 100;
            skills = [new Skill_Ask(), new Skill_Googling(), new Skill_Teach()];
        }

        public void ShowPlayerProfile()
        {                            
            Console.WriteLine("이력서");
            Console.WriteLine($"Lv.{level}");
            Console.WriteLine($"이름 : {playerName.Item1}");
            Console.WriteLine($"직급 : {JobToString(job)}");
            if (EquipmentStat.ATK == 0)
                Console.WriteLine($"코딩력(물리) : {ATK}");
            else
                Console.WriteLine($"코딩력(물리) : {ATK} (+{EquipmentStat.ATK})");
            if (EquipmentStat.DEF == 0)
                Console.WriteLine($"코딩력(논리) : {DEF}");
            else
                Console.WriteLine($"코딩력(논리) : {DEF} (+{EquipmentStat.DEF})");
            Console.WriteLine($"체력 : {CurHP} / {MaxHP}");
            Console.WriteLine($"명중률 : {ACC}");
            Console.WriteLine($"회피율 : {Evade}");
            Console.WriteLine($"소지금 : {GOLD}G");
        }

        public void SetName(string name)
        {
            this.playerName.Item1 = name;
            this.playerName.Item2 = true;
        }

        public void gainGold(int gold)
        {
            if (gold > 0)
                this.GOLD += gold;
        }

        public void ConsumeGold(int gold)
        {
            if (gold > this.GOLD)
            {
                throw new Exception("골드를 가진 이상 소모하려고 합니다!");
            }
            else
            {
                this.GOLD -= gold;
            }
        }        

        public void SetPlayerHP(int value)
        {
            CurHP += value;
            if (CurHP >= MaxHP)
                CurHP = MaxHP;
            else if(CurHP <= 0)
            {
                CurHP = 0;
                IsDead = true;
            }
        }
        public void Revive()
        {
            if (IsDead == true)
            {
                IsDead = false;
                SetPlayerHP(1);
            }
        }

        public void SetPlayerMP(int value)
        {
            CurMP -= value;
            if (CurMP >= MaxMP)
                CurMP = MaxMP;
            else if (CurMP <= 0)
            {
                CurMP = 0;                
            }
        }

        public void Addexp(int value)
        {
            if (EXP <= 100)
                EXP += value;
            else if (EXP > 100)
                EXP = 100;
        }

        public void Levelup(int value)
        {
            
            Console.WriteLine("레벨 업!");
            Console.WriteLine($"Lv. {level} -> {level + 1} {name}");
            level++;
            ATK += 0.5f;
            DEF += 1;
        }

        public void LevelCal()
        {
            switch (level)
            {
                case 1:
                    if (EXP >= 10)
                        Levelup(level);
                    break;
                case 2:
                    if (EXP >= 35)
                        Levelup(level);
                    break;
                case 3:
                    if (EXP >= 65)
                        Levelup(level);
                    break;
                case 4:
                    if (EXP >= 100)
                        Levelup(level);
                    break;
            }
        }

        public void UsePotion()
        {
            CurHP += 30;
            if (CurHP >= 100)
            {
                CurHP = 100;
            }
        }     
        public void EquipOrDequip(int index)
        {
            var selectedItem = items[index];

            if (selectedItem.Item2)
            {
                items[index] = (selectedItem.Item1, false);
                Console.WriteLine($"{selectedItem.Item1.name}이(가) 해제되었습니다.");
            }
            else
            {
                var itemType = selectedItem.Item1.itemType;
                foreach (var i in items.Where(i => i.Item2 && i.Item1.itemType == itemType).ToList())
                {
                    items[items.IndexOf(i)] = (i.Item1, false);
                    Console.WriteLine($"{i.Item1.name}이(가) 해제되었습니다.");
                }
                items[index] = (selectedItem.Item1, true);
                Console.WriteLine($"{selectedItem.Item1.name}이(가) 장착되었습니다.");
            }
        }
        public void AddItem(Item item)
        {
            items.Add((new Item(item), false));
        }

        public void RemoveItem(int index)
        {
            items.RemoveAt(index);
        }
    }
}