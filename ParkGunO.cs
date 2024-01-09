using Elonmusk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static int attack(int ATK)
{
    //이래저래 계산하는거
    int i=0;
    return i;
}
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
        bool IsDead { get; set; }
        void TakeDamage(int damage);
    }

    public class Monster : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int ATK { get; set; }
        public bool IsDead { get; set; }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
                IsDead = true;
            }
        }        
    }

    public class Minion : Monster
    {
        public Minion()
        {
            Name = "미니언";
            Level = 2;
            Health = 15;
            ATK = 6;
        }
    }

    public class CanonMinion : Monster
    {
        public CanonMinion()
        {
            Name = "대포미니언";
            Level = 5;
            Health = 25;
            ATK = 10;
        }
    }

    public class VoidInsect : Monster
    {
        public VoidInsect()
        {
            Name = "공허충";
            Level = 3;
            Health = 10;
            ATK = 3;
        }
    }

    public class Battleprepare
    {
        static Random rand = new Random();        
        static List<Monster> list = new List<Monster>() { new VoidInsect(),new Minion(),new CanonMinion()};
        static public List<Monster> spawnlist = new List<Monster>();
        public static List<Monster> SpawnMosnter(int many)
        {            
            for (int i = 0; i < many; i++)
            {
                spawnlist.Add(list[rand.Next(0,list.Count)]);
            }
            return spawnlist;
        }
    }
   
    public class Battle : Scene
    {        
        public override void ShowInfo()
        {
            Console.WriteLine("Battle!!");
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
            switch (check)
            {
                case true:
                switch (act)
                {
                        case 0:
                            Game.game.ChangeScene(new Battle());
                            break;
                        // Scene을 이동할 때에는 Game.game.ChangeScene(new 씬이름()); 을 사용하면 됨                
                        case int:
                            Battleprepare.spawnlist[act - 1].TakeDamage(5);
                            break;
                }
                    break;
                default:
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
}


