using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public abstract class Skill
    {
        public string Name { get; set; }

        public int Cost { get; set; }
        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public abstract void UseSkill(List<Monster> spawnList);
    }

    public class Skill_Ask : Skill
    {
        public Skill_Ask()
        {
            Name = "사수에게 물어보기";
            Cost = 10;
            Description1 = "모르는게 있으면 사수에게 끈질기게 물어보자(물리). 사수의 귀가 찢어질 때 까지";
            Description2 = "적 한명을 지정해 공격력 * 2의 데미지를 준다.";
        }

        public override void UseSkill(List<Monster> spawnList)
        {
            
            Draw(spawnList);
            int index;
            while (true)
            {
                index = Game.GetPlayerInputInt();
                if(index == 0)
                {
                    return;
                }
                else if (index <= spawnList.Count && index > 0)
                {
                    if (spawnList[index - 1].IsDead)
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다.");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
            }
            
            Game.game.player.SetPlayerMP(Cost);
            int damage = (int)(Game.game.player.ATK * 2);
            int temp = spawnList[index-1].Health;
            spawnList[index-1].TakeDamage(damage);
            Console.Clear();
            Console.WriteLine($"플레이어의 '{Name}' 스킬 사용");
            Console.WriteLine();
            Console.WriteLine($"Lv.{spawnList[index - 1].Level} {spawnList[index - 1].Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine($"Lv.{spawnList[index - 1].Level} {spawnList[index - 1].Name}");
            Console.WriteLine($"HP {temp} -> {(spawnList[index - 1].IsDead ? "Dead" : spawnList[index - 1].Health)}");
            Console.WriteLine();
            Console.ReadLine();
        }

        public void Draw(List<Monster> spawnList)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            int j = 1;
            foreach (Monster mob in spawnList)
            {
                if (mob.IsDead == true)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{j++} Lv.{mob.Level} {mob.Name}  HP {mob.Health}");
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
    }

    public class Skill_Googling : Skill
    {
        public Skill_Googling()
        {
            Name = "구글 검색";
            Cost = 15;
            Description1 = "이제는 눈치 안보고 구글검색을 마음껏! 걸리면 조금 민망할지도";
            Description2 = "적 한명을 지정해 1/2확률로 공격력 * 3 / 공격력 * 1.5 의 데미지를 준다.";
        }

        public override void UseSkill(List<Monster> spawnList)
        {
            
            Draw(spawnList);
            int index;
            while (true)
            {
                index = Game.GetPlayerInputInt();
                if (index == 0)
                {
                    return;
                }
                else if (index <= spawnList.Count && index > 0)
                {
                    if (spawnList[index - 1].IsDead)
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다.");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
            }
            Game.game.player.SetPlayerMP(Cost);
            int damage;
            Random random = new Random();

            if (random.Next(0, 2) == 0)
            {
                damage = (int)Math.Ceiling(Game.game.player.ATK * 1.5);
            }
            else
            {
                damage = (int)MathF.Ceiling(Game.game.player.ATK * 3);
            }
            
            int temp = spawnList[index - 1].Health;
            spawnList[index - 1].TakeDamage(damage);

            Console.Clear();
            Console.WriteLine($"플레이어의 '{Name}' 스킬 사용");
            Console.WriteLine();
            Console.WriteLine($"Lv.{spawnList[index - 1].Level} {spawnList[index - 1].Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine($"Lv.{spawnList[index - 1].Level} {spawnList[index - 1].Name}");
            Console.WriteLine($"HP {temp} -> {(spawnList[index - 1].IsDead ? "Dead" : spawnList[index - 1].Health)}");
            Console.WriteLine();
            Console.ReadLine();
        }

        public void Draw(List<Monster> spawnList)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            int j = 1;
            foreach (Monster mob in spawnList)
            {
                if (mob.IsDead == true)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{j++} Lv.{mob.Level} {mob.Name}  HP {mob.Health}");
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
    }

    public class Skill_Teach : Skill
    {
        public Skill_Teach()
        {
            Name = "신입 기강잡기";
            Cost = 30;
            Description1 = "신입이 들어왔으니 교육을 시켜주자. 빡세게 교육해주는 만큼 보람을 느낄 수 있다";
            Description2 = "적 전체에게 공격력 * 1.5의 데미지를 준다. 준 데미지만큼 체력을 회복한다.";
        }

        public override void UseSkill(List<Monster> spawnList)
        {
            int damage = (int)MathF.Ceiling(Game.game.player.ATK);
            Game.game.player.SetPlayerMP(Cost);
            Console.Clear();
            Console.WriteLine($"플레이어의 '{Name}' 스킬 사용");
            Console.WriteLine();
            int totalDamage = 0;
            foreach (var monster in spawnList)
            {
                if (!monster.IsDead)
                {
                    int temp = monster.Health;
                    monster.TakeDamage(damage);
                    totalDamage += damage;
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
                    Console.WriteLine($"HP {temp} -> {(monster.IsDead ? "Dead" : monster.Health)}");
                    Console.WriteLine();
                }
            }

            int tempPlayerHP = Game.game.player.CurHP;
            Game.game.player.SetPlayerHP(totalDamage);
            Console.WriteLine($"체력을 {totalDamage}만큼 회복");
            Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.name}");
            Console.WriteLine($"Hp {tempPlayerHP} -> {Game.game.player.CurHP}");


            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
