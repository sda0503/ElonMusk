using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Elonmusk.Player;

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


    // 플레이어 스킬
    #region
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
                if (index <= spawnList.Count && index > 0)
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

            int damage = (int)((Game.game.player.ATK+Game.game.player.EquipmentStat.ATK) * 2);
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
            ConsoleDraw.DrawEnemyChoose(spawnList);
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
                if (index <= spawnList.Count && index > 0)
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
                damage = (int)Math.Ceiling((Game.game.player.ATK+Game.game.player.EquipmentStat.ATK) * 1.5);
            }
            else
            {
                damage = (int)MathF.Ceiling((Game.game.player.ATK + Game.game.player.EquipmentStat.ATK) * 3);
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
            ConsoleDraw.DrawEnemyChoose(spawnList);
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
            int damage = (int)MathF.Ceiling(Game.game.player.ATK+ Game.game.player.EquipmentStat.ATK);
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
            Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.PlayerName.Item1}");
            Console.WriteLine($"Hp {tempPlayerHP} -> {Game.game.player.CurHP}");


            Console.WriteLine();
            Console.ReadLine();
        }
    }
    #endregion

    // 보스 스킬
    #region
    public class Skill_SpawnError : Skill
    {
        public Skill_SpawnError()
        {
            Name = "관련 에러 소환";
            Description1 = "관련 에러 두 개를 발생시킵니다.";
        }
        public override void UseSkill(List<Monster> spawnList)
        {
            Monster boss = null;
            foreach (var monster in spawnList)
            {
                if (monster is UncontrollableBug)
                {
                    boss = (UncontrollableBug)monster;
                }
            }
            Console.WriteLine($"Lv.{boss.Level} {boss.Name}의 {Name}!");
            Console.WriteLine(Description1);
            Console.WriteLine();
            spawnList.Add(new RelatedError());
            spawnList.Add(new RelatedError());
        }
    }

    public class Skill_ErrorIncrease : Skill
    {
        public Skill_ErrorIncrease()
        {
            Name = "에러 증식";
            Description1 = "에러가 걷잡을 수 없이 커집니다. 얼른 해결해야겠네요.";
            Description1 = "자신의 공격력 + 10, 명중률 + 10";
        }
        public override void UseSkill(List<Monster> spawnList)
        {
            Monster boss = null;
            foreach (var monster in spawnList)
            {
                if (monster is UncontrollableBug)
                {
                    boss = (UncontrollableBug)monster;
                }
            }
            Console.WriteLine($"Lv.{boss.Level} {boss.Name}의 {Name}!");
            Console.WriteLine(Description1);
            Console.WriteLine(Description2);
            Console.WriteLine();

            int tempATK = boss.ATK;
            int tempACC = boss.ACC;
            boss.ATK += 10;
            boss.ACC += 10;
            Console.WriteLine($"공격력: {tempATK} -> {boss.ATK}");
            Console.WriteLine($"명중률: {tempACC} -> {boss.ACC}");
            Console.WriteLine();
        }
    }

    public class Skill_Interrogation : Skill
    {
        public Skill_Interrogation()
        {
            Name = "질문 공세";
            Description1 = "엄청난 질문들이 날아온다.";
            Description2 = "압박 질문 두 개를 소환합니다.";
        }
        public override void UseSkill(List<Monster> spawnList)
        {
            Monster boss = null;
            foreach (var monster in spawnList)
            {
                if (monster is Investor)
                {
                    boss = (Investor)monster;
                }
            }
            Console.WriteLine($"Lv.{boss.Level} {boss.Name}의 {Name}!");
            Console.WriteLine(Description1);
            Console.WriteLine(Description2);
            Console.WriteLine();
            spawnList.Add(new Question());
            spawnList.Add(new Question());
        }
    }

    public class Skill_Pressure : Skill
    {
        public Skill_Pressure()
        {
            Name = "압박감 주기";
            Description1 = "굉장한 압박감이 느껴진다. 아무것도 하지 못한 채 턴이 넘어갔다.";
        }
        public override void UseSkill(List<Monster> spawnList)
        {
            Monster boss = null;
            foreach (var monster in spawnList)
            {
                if (monster is Investor)
                {
                    boss = (Investor)monster;
                }
            }
            Console.WriteLine($"Lv.{boss.Level} {boss.Name}의 {Name}!");
            Console.WriteLine(Description1);
            Console.WriteLine();
        }
    }

    #endregion
    public static class ConsoleDraw
    {
        public static void DrawEnemyChoose(List<Monster> spawnList)
        {
            Console.Clear();
            Console.WriteLine("■■■■■■■■■■■■■■");
            Dungeon.Battle.ShowHighlithtesText("오류 수정!! - 플레이어 턴");
            Console.WriteLine("■■■■■■■■■■■■■■");
            Console.WriteLine();
            int j = 1;
            foreach (Monster mob in spawnList)
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
                Console.Write(Dungeon.Battle.PadRightForMixedText(mob.Name, 20));
                Console.WriteLine($"HP {mob.Health}");
                Console.ResetColor();
            }
            j = 1;
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Game.game.player.level} {Game.game.player.PlayerName.Item1}");
            Console.WriteLine($"HP {Game.game.player.MaxHP}/{Game.game.player.CurHP}");
            Console.WriteLine();
            Console.WriteLine("0. 돌아가기");
        }
    }
}
