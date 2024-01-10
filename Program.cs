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

        public Idle idle { get; private set; }
        public Inventory inventory { get; private set; }
        public Equipment equipment { get; private set; }
        public Buy buy { get; private set; }
        public Shop shop { get; private set; }

        public HappyEndding happyEndding { get; private set; }
        public BadEndding badEndding { get; private set; }

        public Battle battle { get; private set; }


        
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
            battle = new Battle();
            curScene = battle;
        }

        void Loop()
        {
            while (true)
            {
                curScene.ShowInfo();
                int input = GetPlayerInputInt();
                curScene.GetAction(input);
            }
        }

        public static int GetPlayerInputInt()
        {
            int input = -1;
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해주세요.");
            Console.Write(">> ");
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 선택해주세요.");
                Console.Write(">> ");
            }
            return input;
        }

        public void ChangeScene(Scene scene)
        {
            Console.Clear();
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
        public List<(Item, bool)> items { get; private set; }

        public Inventory()
        {
            items = new List<(Item, bool)>();
        }


        public override void ShowInfo()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            foreach (var item in items)
            {
                String strEquipped = (item.Item2) ? "[E]" : String.Empty;
                Console.WriteLine($"- {strEquipped}{item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}");
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
                    Game.game.ChangeScene(Game.game.idle);
                    break;
                case 1:
                    Game.game.ChangeScene(Game.game.equipment);
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        public void EquipOrDequip(int index)
        {
            items[index] = (items[index].Item1, !(items[index].Item2));
        }

        public void AddItem(Item item)
        {
            items.Add((new Item(item), false));
        }
    }

    public class Equipment : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Game.game.inventory.items.Count; i++)
            {
                (Item, bool) item = Game.game.inventory.items[i];
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
                Game.game.ChangeScene(Game.game.idle);
            }
            else if (act > 0 && act < Game.game.inventory.items.Count + 1)
            {
                Game.game.inventory.EquipOrDequip(act - 1);
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
            items.Add((new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 1000), false));
            items.Add((new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 2000), false));
            items.Add((new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 3500), false));
            items.Add((new Item("낡은 검", "어디에서나 쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 600), false));
            items.Add((new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 1500), false));
            items.Add((new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 2500), false));
        }

        public override void ShowInfo()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Game.game.player.GOLD}");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            foreach (var item in items)
            {
                Console.WriteLine($"- {item.Item1.name} | {item.Item1.GetEffectScript()} | {item.Item1.desc} | {item.Item1.GOLD}");
            }
            Console.WriteLine();
            Console.WriteLine("1.아이템 구매");
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(Game.game.idle);
                    break;
                case 1:
                    Game.game.ChangeScene(Game.game.buy);
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
                    Game.game.inventory.AddItem(items[index].Item1);
                    Console.WriteLine("구매를 완료했습니다.");
                    items[index] = (items[index].Item1, true);
                }
                else
                    Console.WriteLine("골드가 부족합니다.");
            }
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
                Game.game.ChangeScene(Game.game.shop);
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

        public enum ItemType { NONE, WEAPON, ARMOR };

        ItemType itemType = ItemType.NONE;
        public Item()
        {
            name = "Item";
            desc = "ItemDescription";
        }

        public Item(string name, string desc, int ATK, int DEF, int GOLD)
        {
            this.name = name;
            this.desc = desc;
            this.ATK = ATK;
            this.DEF = DEF;
            this.GOLD = GOLD;
        }

        public Item(Item item)
        {
            this.name = item.name;
            this.desc = item.desc;
            this.ATK = item.ATK;
            this.DEF = item.DEF;
            this.GOLD = item.GOLD;
        }

        public string GetEffectScript()
        {
            if (ATK > 0)
                return $"공격력 +{ATK}";
            if (DEF > 0)
                return $"방어력 +{ATK}";
            return "효과 없음";
        }
    }

    public class Stat
    {
        public string name { get; protected set; }
        public int level { get; protected set; }

        public int ATK { get; protected set; }
        public int DEF { get; protected set; }
        public int ACC { get; protected set; }
        public int Evade { get; protected set; }

        public int GOLD { get; protected set; }

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
        public enum JOB
        {
            Intern,
            NewComer,
            Jonior,
            senior
        }

        public JOB job;

        public Stat EquipmentStat
        {
            get
            {
                Stat stat = new Stat();
                foreach (var i in Game.game.inventory.items)
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
            name = "Victor";
            level = 1;
            job = JOB.senior;
            ATK = 10;
            DEF = 5;
            MaxHP = 100;
            CurHP = 100;
            ACC = 100;
            Evade = 10;
            GOLD = 1500;
            MaxMP = 100;
            CurMP = 100;
            skills = [new Skill_Teach(), new Skill_Ask(), new Skill_Googling()];
        }

        public void ShowPlayerProfile()
        {
            Console.WriteLine("플레이어의 현재 정보입니다.");
            Console.WriteLine($"Lv.{level}");
            Console.WriteLine($"이름 : {name}");
            Console.WriteLine($"직업 : {job.ToString()}");
            if (EquipmentStat.ATK == 0)
                Console.WriteLine($"공격력 : {ATK}");
            else
                Console.WriteLine($"공격력 : {ATK} (+{EquipmentStat.ATK})");
            if (EquipmentStat.DEF == 0)
                Console.WriteLine($"공격력 : {DEF}");
            else
                Console.WriteLine($"공격력 : {DEF} (+{EquipmentStat.DEF})");
            Console.WriteLine($"명중률 : {ACC}");
            Console.WriteLine($"회피율 : {Evade}");
            Console.WriteLine($"최대 체력 : {MaxHP}");
            Console.WriteLine($"현재 체력 : {CurHP}");
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

        public void TakeDamage(int damage)
        {
            CurHP -= damage;
            if (CurHP <= 0)
            {
                CurHP = 0;
                IsDead = true;
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

        public void SetPlayerMP(int value)
        {
            CurMP -= value;
        }

        public void UsePotion()
        {
            CurHP += 30;
            if (CurHP >= 100)
            {
                CurHP = 100;                
            }            
        }
    }
}