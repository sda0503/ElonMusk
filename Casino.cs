using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonMusk
{
    public class Casino : Scene
    {

        List<Type> casinoGameList = new List<Type>();

        public override void ShowInfo()
        {
            Console.WriteLine(
@"
  .d88888b   888888ba   .d888888   888888ba  d888888P  .d888888  
  88.    ""'  88    `8b d8'    88   88    `8b    88    d8'    88  
  `Y88888b. a88aaaa8P' 88aaaaa88   88aaaa8P'    88    88aaaaa88 
        `8b  88        88     88   88   `8b.    88    88     88  
  d8'   .8P  88        88     88   88     88    88    88     88  
   Y88888P   dP        88     88   dP     dP    dP    88     88  
ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
MM'""""""""'YMM MMP""""""""""""""MM MP""""""""""""`MM M""""M M""""""""""""""`YM MMP""""""""""YMM 
M' .mmm. `M M' .mmmm  MM M  mmmmm..M M  M M  mmmm.  M M' .mmm. `M 
M  MMMMMooM M         MM M.      `YM M  M M  MMMMM  M M  MMMMM  M 
M  MMMMMMMM M  MMMMM  MM MMMMMMM.  M M  M M  MMMMM  M M  MMMMM  M 
M. `MMM' .M M  MMMMM  MM M. .MMM'  M M  M M  MMMMM  M M. `MMM' .M 
MM.     .dM M  MMMMM  MM Mb.     .dM M  M M  MMMMM  M MMb     dMM 
MMMMMMMMMMM MMMMMMMMMMMM MMMMMMMMMMM MMMM MMMMMMMMMMM MMMMMMMMMMM 
");
            Console.WriteLine("스파르타 카지노에 오신걸 환영합니다.");
            Console.WriteLine("여기서는 보유한 소지금을 걸고 다양한 게임들을 할 수 있습니다.");
            Console.WriteLine();
            // 플레이어 소지금 출력
            // Console.WriteLine($"[소지금 : {Game.game.player.GOLD} G]");
            Console.WriteLine();
            Console.WriteLine("[게임 목록]");
            ShowGameList();
            Console.WriteLine();
            Console.WriteLine("0. 돌아가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0)
            {
                Game.game.ChangeScene(new Idle());
            }
            else if (act > 0 && act < casinoGameList.Count + 1)
            {
                Game.game.ChangeScene((Scene)Activator.CreateInstance(casinoGameList[act - 1]));
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public void ShowGameList()
        {
            // GameLobby를 상속받는 클래스들의 목록을 가져옵니다.
            casinoGameList = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                              from type in domainAssembly.GetTypes()
                              where typeof(GameLobby).IsAssignableFrom(type)
                              select type).ToList();

            // GameLobby 원형을 제외하고 나머지를 "Lobby" 단어를 삭제한 후 출력합니다.
            casinoGameList.Remove(typeof(GameLobby));

            for (int i = 0; i < casinoGameList.Count; i++)
            {
                string gameName = casinoGameList[i].Name;
                Console.WriteLine($"{i + 1}. {gameName.Replace("Lobby", "")}");
            }
        }
    }

    public abstract class GameLobby : Scene
    {
    }
}
