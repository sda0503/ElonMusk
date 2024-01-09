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

    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public int CurHealth { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int ACC { get; set; }
        public int Evade { get; set; }
        public bool IsDead { get; set; }

        public void TakeDamage(int damage)
        {
            CurHealth -= damage;
            if (CurHealth <= 0)
            {
                CurHealth = 0;
                IsDead = true; //마을에서 치료하면 살아나는걸로?
            }
        }

        public Character()
        {
            Name = "Chad";
            Level = 1;            
            ATK = 10;
            DEF = 5;
            MaxHealth = 100;
            CurHealth = 100;
            ACC = 10;
            Evade = 10;
        }
    }

    public class Monster : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int ATK { get; set; }
        public int ACC { get; set; }
        public int Evade { get; set; }
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

    public class Minion : Monster
    {
        public Minion() //밸런스 잡힌 타입
        {
            Name = "미니언";
            Level = 2;
            Health = 15;
            ATK = 6;
            ACC = 5;
            Evade = 5;            
        }
    }

    public class CanonMinion : Monster
    {
        public CanonMinion() //체력, 공격력 높은 대신 명중,회피가 낮음
        {
            Name = "대포미니언";
            Level = 5;
            Health = 25;
            ATK = 15;
            ACC = 3;
            Evade = 0;
        }
    }

    public class VoidInsect : Monster
    {
        public VoidInsect() //체력, 공격력 낮은 대신 명중,회피가 높음
        {
            Name = "공허충";
            Level = 3;
            Health = 10;
            ATK = 3;
            ACC = 7;
            Evade = 7;
        }
    }

    public class Battle : Scene
    {
        static Random rand = new Random();
        static Character player = new Character();
        static List<Monster> spawnlist = new List<Monster>(4);
        static int BfHp = 0; //전투 시작 전 체력
        //static int turn = 0;

        public override void ShowInfo()
        {
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
                    //상태보기
                    break;
                case 2:
                    Battleprepare();
                    break;
                case 3:
                    break;
            }
        }

        void Battleprepare() //던전에서 이동해서 전투 나왔을 때 안에 함수들 한번만 실행되게 작성
        { 
            //필수구현할 때 전투시작전 체력도 저장해야될듯
            BfHp = player.CurHealth;
                        
            for (int i = 0; i < rand.Next(1,5); i++)
            {
                switch (rand.Next(0, 3))
                {
                    case 0:
                        spawnlist.Add(new Minion());
                        break;
                    case 1:
                        spawnlist.Add(new VoidInsect());
                        break;
                    case 2:
                        spawnlist.Add(new CanonMinion());
                        break;
                }  
            }
            Game.game.ChangeScene(new Battle_myturn());
        }

        public class Battle_myturn : Scene
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
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player.MaxHealth}/{player.CurHealth}");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
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
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                int j = 1;
                foreach (Monster mob in spawnlist)
                {
                    if (mob.IsDead == true)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{j++} Lv.{mob.Level} {mob.Name}  HP {mob.Health}");
                    Console.ResetColor();
                }
                j = 1;
                Console.WriteLine();
                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player.MaxHealth}/{player.CurHealth}");
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
                                    Game.game.ChangeScene(new BattleAttack());
                                }
                                else
                                {
                                    attack(player.ATK, spawnlist[act - 1]);
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
                        Game.game.ChangeScene(new BattleAttack());
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
                if (player.ACC + rand.Next(20) > mob.Evade + rand.Next(20))
                {
                    if (rand.Next(100) > 84)
                    {
                        damage = (int)MathF.Ceiling(damage * 1.6f);
                        mob.TakeDamage(damage);
                        Console.WriteLine("플레이어의 공격!");
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name}");
                        Console.WriteLine($"HP {temp} {(mob.IsDead ? "Dead" : mob.Health - damage)}");
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
                int temp = player.CurHealth;
                Console.Clear();
                Console.WriteLine("Battle!! - 적 턴");
                Console.WriteLine();
                enemyattack();

                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {temp} -> {player.CurHealth}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");

            }

            public override void GetAction(int act)
            {
                switch (act)
                {
                    default:
                        if (player.IsDead != true)
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

                        if (player.Evade + rand.Next(20) < mob.ACC + rand.Next(20))
                        {
                            player.TakeDamage(mob.ATK);
                            Console.WriteLine($"Lv.{mob.Level} {mob.Name}의 공격!");
                            Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {mob.ATK}]");
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
            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("Victory");
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터 {spawnlist.Count}마리를 잡았습니다.");
                spawnlist.Clear();
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"Hp {BfHp} -> {player.CurHealth}");
                //플레이어 캐릭터 정보
                //                            
                Console.WriteLine("0. 돌아가기");
            }
            public override void GetAction(int act)
            {
                switch (act)
                {
                    default:
                        //던전으로 돌아가게 구현
                        break;
                }
            }
        }

        public class BattleEnd_Lose : Scene
        {
            public override void ShowInfo()
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("You Lose");
                Console.WriteLine();
                spawnlist.Clear();
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"Hp {BfHp} -> {player.CurHealth}");                                   
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
    }
}


