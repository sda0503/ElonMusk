using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static ElonMusk.BlackJackGame;

namespace ElonMusk
{
    public class BlackJackLobby : GameLobby
    {
        public override void ShowInfo()
        {
            PrintGameTitle();
            Console.WriteLine();
            Console.WriteLine("1. 게임 설명을 듣는다.");
            Console.WriteLine("2. 게임을 플레이 하러 간다.");
            Console.WriteLine("0. 돌아간다");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new Casino());
                    break;
                case 1:
                    Game.game.ChangeScene(new BlackJackInfo());
                    break;
                case 2:
                    Game.game.ChangeScene(new BlackJackGame());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        public static void PrintGameTitle()
        {
            Console.WriteLine(
@"
M#""""""""""""""'M  dP                   dP       oo                   dP       
##  mmmm. `M 88                   88                            88       
#'        .M 88 .d8888b. .d8888b. 88  .dP  dP .d8888b. .d8888b. 88  .dP  
M#  MMMb.'YM 88 88'  `88 88'  `"""" 88888""   88 88'  `88 88'  `"""" 88888""   
M#  MMMM'  M 88 88.  .88 88.  ... 88  `8b. 88 88.  .88 88.  ... 88  `8b. 
M#       .;M dP `88888P8 `88888P' dP   `YP 88 `88888P8 `88888P' dP   `YP 
M#########M                                88                            
                                           dP                            
");
        }
    }

    public class BlackJackInfo : Scene
    {
        public override void ShowInfo()
        {
            BlackJackLobby.PrintGameTitle();
            Console.WriteLine();
            Console.WriteLine("블랙잭 게임에 대한 설명입니다.");
            Console.WriteLine("카드는 심볼(♠,♥,♦,♣ 중 1개)과 숫자(2~9, J, Q, K, A 중 하나)로 이루어진 총 52장의 카드로 진행합니다.");
            Console.WriteLine("J, Q, K는 10으로 간주하며, A는 1 또는 11로 간주합니다.");
            Console.WriteLine();
            Console.WriteLine("먼저, 게임 시작 시에 딜러와 플레이어는 카드를 두 장을 받습니다.");
            Console.WriteLine("이후 플레이어는 카드를 더 뽑을 지, 이대로 멈출 것인지 결정합니다.");
            Console.WriteLine("딜러는 플레이어의 결정에 상관없이, 합이 17보다 작으면 뽑습니다.");
            Console.WriteLine("이후 두 사람의 카드를 비교해 합이 21에 가까운 사람이 승리합니다.");
            Console.WriteLine("단, 21을 넘어갈 시에 반드시 패배합니다.");
            Console.WriteLine("두 사람의 합이 같은 경우 합이 21인 경우를 제외하고 딜러가 승리합니다.");
            Console.WriteLine("승리하면 배팅한 금액의 2배를 얻고, 패배하면 배팅한 금액을 모두 잃습니다.");
            Console.WriteLine();
            Console.WriteLine("0. 돌아간다");
        }
        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new BlackJackLobby());
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }
    }

    public class BlackJackGame : Scene
    {
        string[] cardNums = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        List<(Pattern, string)> cards = new List<(Pattern, string)>();
        List<(Pattern, string)> playerHand = new List<(Pattern, string)>();
        List<(Pattern, string)> dealerHand = new List<(Pattern, string)>();

        int betGold = 0;
        static int WinStreak = 0;

        public enum Pattern
        {
            SPADES, CLUBS, HEARTS, DIAMONDS
        }

        public override void ShowInfo()
        {
            BlackJackLobby.PrintGameTitle();
            Console.WriteLine();
            Console.WriteLine("1. 배팅 금액 입력하기");
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            switch (act)
            {
                case 0:
                    Game.game.ChangeScene(new BlackJackLobby());
                    break;
                case 1:
                    Bet();
                    break;
                default:
                    Console.WriteLine("유효한 입력이 아닙니다!");
                    break;
            }
        }

        public void Bet()
        {
            Console.Clear();
            BlackJackLobby.PrintGameTitle();
            Console.WriteLine();
            Console.WriteLine($"현재 소지 금액 {Game.game.player.GOLD}");
            Console.WriteLine("배팅할 금액을 입력해주세요. (0은 나가기)");
            Console.WriteLine();
            int bet = -1;
            do
            {
                bet = Game.GetPlayerInputInt();
                if (bet > Game.game.player.GOLD)
                {
                    Console.WriteLine("입력한 금액이 보유하고 있는 금액보다 많습니다!");
                    bet = -1;
                }
            }
            while (bet < 0);

            if (bet == 0)
                betGold = 0;
            else
            {
                Game.game.player.ConsumeGold(bet);
                betGold = bet;
                PlayBlackJack();
            }
        }

        void PlayBlackJack()
        {
            InitCards();
            // 카드 분배
            playerHand.Add(DrawCard());
            dealerHand.Add(DrawCard());
            playerHand.Add(DrawCard());
            dealerHand.Add(DrawCard());

            // 플레이어 턴
            PlayerTurn();

            // 딜러의 턴
            DealerTurn();

            int playerScore = CountScore(playerHand);
            int dealerScore = CountScore(dealerHand);

            Console.WriteLine();
            Console.WriteLine("승패를 가립니다");
            Console.WriteLine($"플레이어의 스코어는 {playerScore}, 딜러의 스코어는 {dealerScore}");
            Console.WriteLine();

            if (playerScore > 21 || (dealerScore <= 21 && dealerScore >= playerScore))
            {
                Console.WriteLine("딜러가 승리했습니다! 베팅한 골드를 잃습니다.");
                CasinoData.casinoData.blackJackAchievement._loseGold += betGold;
                CountWinStreak(WINFLAG.LOSE);
                betGold = 0;
            }
            else if (dealerScore > 21 || playerScore > dealerScore)
            {
                Console.WriteLine($"당신이 승리했습니다! {betGold * 2}만큼 골드를 받습니다.");
                CasinoData.casinoData.blackJackAchievement._winGold += betGold;
                Game.game.player.gainGold(betGold * 2);
                CountWinStreak(WINFLAG.WIN);
            }
            else
            {
                Console.WriteLine("비겼습니다! 베팅한 골드를 돌려받습니다.");
                Game.game.player.gainGold(betGold);
                CountWinStreak(WINFLAG.DRAW);
                betGold = 0;
            }

            RegameOrExit();
        }

        void PlayerTurn()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"현재 배팅한 금액 [{betGold}]");
            Console.WriteLine();
            ShowCards();
            int act = -1;
            while(CountScore(playerHand) < 21) 
            {
                Console.WriteLine("당신의 턴입니다. 당신의 행동을 선택하세요.");
                Console.WriteLine("1. 카드 뽑기");
                Console.WriteLine("0. 멈추기");

                act = Game.GetPlayerInputInt();

                if (act == 1)
                {
                    playerHand.Add(DrawCard());
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine($"현재 배팅한 금액 [{betGold}]");
                    Console.WriteLine();
                    ShowCards();
                }
                if (act == 0)
                    break;
            }
            Console.WriteLine("당신의 턴이 끝났습니다.");
            Console.WriteLine();
        }

        void DealerTurn()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"현재 배팅한 금액 [{betGold}]");
            Console.WriteLine();
            ShowCards();

            Console.WriteLine("딜러의 턴입니다.");
            while (CountScore(dealerHand) < 17)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"현재 배팅한 금액 [{betGold}]");
                Console.WriteLine();
                ShowCards();
                Console.WriteLine("딜러가 카드를 뽑습니다.");
                dealerHand.Add(DrawCard());
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"현재 배팅한 금액 [{betGold}]");
                Console.WriteLine();
                ShowCards();
            }
            Console.WriteLine("딜러가 턴을 종료합니다.");
        }

        void RegameOrExit() 
        {
            Console.WriteLine();
            Console.WriteLine("1. 다시하기");
            Console.WriteLine("0. 로비로 돌아간다");

            int act = -1;
            do
            {
                act = Game.GetPlayerInputInt();
            }
            while (act != 1 && act != 0);

            if (act == 1)
                Bet();
            else if (act == 0)
                Game.game.ChangeScene(new BlackJackLobby());
        }

        void ShowCards()
        {
            Console.WriteLine("딜러의 카드");
            List<string> dealerCardString = new List<string>();
            
            foreach (var card in dealerHand)
            {
                dealerCardString.Add(CardVisualizer.ToCardVisual(card.Item1, card.Item2));
            }
            Console.WriteLine(CardVisualizer.CombineCardString(dealerCardString));
            Console.WriteLine();

            Console.WriteLine("플레이어의 카드");
            List<string> playerCardString = new List<string>();

            foreach (var card in playerHand)
            {
                playerCardString.Add(CardVisualizer.ToCardVisual(card.Item1, card.Item2));
            }
            Console.WriteLine(CardVisualizer.CombineCardString(playerCardString));
            Console.WriteLine();
        }

        void InitCards()
        {
            cards.Clear();
            playerHand.Clear();
            dealerHand.Clear();

            // 카드 생성
            foreach (var cardNum in cardNums)
            {
                cards.Add((Pattern.SPADES, cardNum));
                cards.Add((Pattern.HEARTS, cardNum));
                cards.Add((Pattern.CLUBS, cardNum));
                cards.Add((Pattern.DIAMONDS, cardNum));
            }

            // 카드 섞기
            Random random = new Random();
            cards = cards.OrderBy(item => random.Next()).ToList();
        }

        (Pattern, string) DrawCard()
        {
            (Pattern, string) card = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return card;
        }

        int CountScore(List<(Pattern, string)> hand)
        {
            int score = 0;
            int numberOfAces = 0;

            foreach (var card in hand)
            {
                if (card.Item2 == "A")
                {
                    score += 11;
                    numberOfAces++;
                }
                else if ("JQK".Contains(card.Item2))
                {
                    score += 10;
                }
                else
                {
                    score += Convert.ToInt32(card.Item2);
                }
            }

            while (score > 21 && numberOfAces > 0)
            {
                score -= 10;
                numberOfAces--;
            }

            return score;
        }

        public void CountWinStreak(WINFLAG flag) 
        {
            if (WinStreak == 0)
                WinStreak += (int)flag;
            else if(WinStreak > 0) 
            {
                if(flag != WINFLAG.WIN)
                    WinStreak = (int)flag;
                else 
                {
                    WinStreak++;
                    if(WinStreak > CasinoData.casinoData.blackJackAchievement._maxWinStreak) 
                    {
                        CasinoData.casinoData.blackJackAchievement._maxWinStreak = WinStreak;
                    }
                }
            }
            else 
            {
                if (flag != WINFLAG.LOSE)
                    WinStreak = (int)flag;
                else
                {
                    WinStreak--;
                    if (WinStreak < CasinoData.casinoData.blackJackAchievement._maxLoseStreak)
                    {
                        CasinoData.casinoData.blackJackAchievement._maxLoseStreak = WinStreak;
                    }
                }
            }
        }
    }

    public static class CardVisualizer 
    {
        public static string ToCardVisual(BlackJackGame.Pattern pattern, string numString) 
        {
            int num = 0;
            if (Int32.TryParse(numString, out num))
            {
                return String.Format(cardVisualFormat[num-1], numString, PatternToString(pattern));
            }
            else 
            {
                return String.Format(cardVisualFormat[0], numString, PatternToString(pattern));
            }
        }

        static string PatternToString(BlackJackGame.Pattern pattern)
        {
            switch (pattern)
            {
                case Pattern.SPADES:
                    return "♠";
                case Pattern.HEARTS:
                    return "♥";
                case Pattern.DIAMONDS:
                    return "◆";
                default:
                    return "♣";
            }
        }

        public static string CombineCardString(List<string> cards) 
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Clear();

            List<string[]> splitCardsString = new List<string[]>();
            splitCardsString.Clear();

            foreach (string card in cards) 
            {
                splitCardsString.Add(card.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
            }

            for (int j = 0; j < splitCardsString[0].Length; j++)
            {
                for (int i = 0; i < splitCardsString.Count; i++) 
                {
                    stringBuilder.Append(splitCardsString[i][j]);
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        static List<string> cardVisualFormat = new List<string>(){ @"
._________.
|{0}        |
|{1}        |
|         |
|         |
|    {1}    |
|         |
|         |
|        {1}|
|        {0}|
'---------'
"
,
@"
._________.
|{0}        |
|{1}        |
|    {1}    |
|         |
|         |
|         |
|    {1}    |
|        {1}|
|        {0}|
'---------'
"
,
@"
._________.
|{0}        |
|{1}        |
|    {1}    |
|         |
|    {1}    |
|         |
|    {1}    |
|        {1}|
|        {0}|
'---------'
"
,
@"
._________.
|{0}        |
|{1}        |
|   {1} {1}   |
|         |
|         |
|         |
|   {1} {1}   |
|        {1}|
|        {0}|
'---------'
"
,
@"
.________.
|{0}        |
|{1}        |
|   {1} {1}   |
|         |
|    {1}    |
|         |
|   {1} {1}   |
|        {1}|
|        {0}|
'---------'
"
,
@"
._________.
|{0}        |
|{1}        |
|   {1} {1}   |
|         |
|   {1} {1}   |
|         |
|   {1} {1}   |
|        {1}|
|        {0}|
'---------'
",
@"
._________.
|{0}        |
|{1}        |
|   {1} {1}   |
|    {1}    |
|   {1} {1}   |
|         |
|   {1} {1}   |
|        {1}|
|        {0}|
'---------'
"
,
@"
._________.
|{0}        |
|{1}        |
|   {1} {1}   |
|    {1}    |
|   {1} {1}   |
|    {1}    |
|   {1} {1}   |
|        {1}|
|        {0}|
'---------'
"
,
@"
._________.
|{0}        |
|{1}  {1} {1}   |
|         |
|   {1} {1}   |
|    {1}    |
|   {1} {1}   |
|         |
|   {1} {1}  {1}|
|        {0}|
'---------'
"
,
@"
._________.
|{0}       |
|{1}  {1} {1}   |
|    {1}    |
|   {1} {1}   |
|         |
|   {1} {1}   |
|    {1}    |
|   {1} {1}  {1}|
|       {0}|
'---------'
"};
    }
}
