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
    
    internal class ParkGunO
    {
        
    }

    interface ICharacter
    {
        string Name { get; set; }
        int Health { get; set; }
        int ATK { get; set; }
        int ACC { get; set; }
        int Evaed { get; set; }
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
        public int Evaed { get; set; }
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
            Evaed = 5;
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
            Evaed = 0;
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
            Evaed = 7;
        }
    }

    public class Battleprepare //던전에서 이동해서 전투 나왔을 때 안에 함수들 한번만 실행되게 작성
    {
        static Random rand = new Random();
        static public List<Monster> spawnlist = new List<Monster>(4);
        //필수구현할 때 전투시작전 체력도 저장해야될듯

        //int temp = player.health;


        //static List<Type> MonsterList = new List<Type>();
        //public void showMonsterlist()
        //{            
        //    MonsterList = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
        //                   from type in domainAssembly.GetTypes()
        //                   where typeof(Monster).IsAssignableFrom(type)
        //                   select type).ToList();

        //    MonsterList.Remove(typeof(Monster));            
        //}     

        public void SpawnMosnter(int many)
        {            
            for (int i = 0; i < many; i++)
            {
                switch (rand.Next(0,3)) 
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
               //Monster newmob = Activator.CreateInstance(MonsterList[rand.Next(1,MonsterList.Count)]);
            }            
        }
        public void Battlestart()
        {
            SpawnMosnter(rand.Next(1,5));
            Game.game.ChangeScene(new Battle_myturn());
        }
    }
   
    public class Battle_myturn : Scene
    {        
        public override void ShowInfo()
        {            
            Console.WriteLine("Battle!! - 플레이어 턴");
            Console.WriteLine();
            foreach (Monster mob in Battleprepare.spawnlist)
            {
                Console.WriteLine($"Lv.{mob.Level} {mob.Name}  HP {mob.Health}");
            }

            Console.WriteLine("[내 정보]");
            //플레이어 캐릭터 정보
            //
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
            foreach (Monster mob in Battleprepare.spawnlist)
            {
                Console.WriteLine($"{j++} Lv.{mob.Level} {mob.Name}  HP {mob.Health}");
            }
            j = 1;
            Console.WriteLine("[내 정보]");
            //플레이어 캐릭터 정보
            //                            
            Console.WriteLine("0. 돌아가기");
        }

        public override void GetAction(int act)
        {
            bool check = (act <= Battleprepare.spawnlist.Count && act >= 0);
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
                            //Battleprepare.spawnlist[act - 1].TakeDamage(attack(/*플레이어 공격력*/), Battleprepare.spawnlist[act-1]);                            
                            foreach (Monster mob in Battleprepare.spawnlist)
                            {
                                isalive = mob.IsDead;
                            }
                            if (isalive == false)
                                Game.game.ChangeScene(new Battle_enemyturn());
                            else
                                Game.game.ChangeScene(new BattleEnd_win());
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("다시 선택해주세요.");
                    Game.game.ChangeScene(new Battle_myturn());
                    break;
            }
        }

        public int attack(int PlayerATK,Monster mob)
        {
            Random rand = new Random();
            int error = (int)MathF.Ceiling(PlayerATK / 10f);
            int damage = rand.Next(PlayerATK - error, PlayerATK + error);
            /*
            if (Player.ACC+rand.Next(20) > mob.Evade+rand.Next(20)){
                if (rand.Next(100) > 84)
                {
                    damage = (int)MathF.Ceiling(damage * 1.6f);
                    Console.WriteLine("플레이어의 공격!");
                    Console.WriteLine($"Lv.{mob.Level} {mob.Name} 을(를) 맞췄습니다. [데미지 : {damage} - 치명타 공격!!");
                    Console.WriteLine($"Lv.{mob.Level} {mob.Name}");
                    Console.WriteLine($"HP {mob.Health} {(mob.IsDead ? "Dead" : mob.Health-damage)}");
                }
                else
                {
                    Console.WriteLine("플레이어의 공격!");
                    Console.WriteLine($"Lv.{mob.Level} {mob.Name} 을(를) 맞췄습니다. [데미지 : {damage}");
                    Console.WriteLine($"Lv.{mob.Level} {mob.Name}");
                    Console.WriteLine($"HP {mob.Health} {(mob.IsDead ? "Dead" : mob.Health-damage)}");
                }
                }
            else
                Console.WriteLine("공격이 빗나갔습니다.");
             */

            return damage;
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
        static Random rand = new Random();
        public override void ShowInfo()
        {
            int temp; //enemyattack 들어가기 전 플레이어 체력
            Console.WriteLine("Battle!! - 적 턴");
            Console.WriteLine();
            enemyattack();

            Console.WriteLine($"Lv.playerlevel playername");
            Console.WriteLine($"HP temp -> playerhealth");
            Console.WriteLine();
            Console.WriteLine("0. 다음");

        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                default:
                    //if (player.isdead != true)
                    Game.game.ChangeScene(new Battle_myturn());
                    //else
                    //Game.game.ChangeScene(new Battleend_Lose());
                    break;
            }
        }

        public void enemyattack()
        {
            foreach (Monster mob in Battleprepare.spawnlist)
            {
                if (mob.IsDead != true)
                {
                    /*
                    if (Player.Evade + rand.Next(20) < mob.ACC + rand.Next(20))
                    {
                        player.Takedamage(mob.ATK);
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name}의 공격!");
                        Console.WriteLine($"Player 을(를) 맞췄습니다. [데미지 : {mob.ATK}]");
                        Console.WriteLine();
                    }
                    else
                        Console.WriteLine($"Lv.{mob.Level} {mob.Name}의 공격이 빗나갔습니다!");
                    */

                }
            }
        }
    }

    public class BattleEnd_win : Scene
    {
        public override void ShowInfo()
        {
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {Battleprepare.spawnlist.Count}마리를 잡았습니다.");
            Battleprepare.spawnlist.Clear();           
            Console.WriteLine($"Lv.player.Level Player.Name");
            Console.WriteLine($"Hp battleprepare.temp -> player.health");
            //플레이어 캐릭터 정보
            //                            
            Console.WriteLine("0. 돌아가기");
        }
        public override void GetAction(int act)
        {
            switch(act)
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
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Battleprepare.spawnlist.Clear();
            Console.WriteLine($"Lv.player.Level Player.Name");
            Console.WriteLine($"Hp battleprepare.temp -> player.health");
            //플레이어 캐릭터 정보
            //                            
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


