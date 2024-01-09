using Elonmusk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void PrintGameTitle()
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

    public class BlackJackGame : Scene
    {
        string[] cardNums = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        List<(Pattern, string)> cards = new List<(Pattern, string)>();
        List<(Pattern, string)> playerHand = new List<(Pattern, string)>();
        List<(Pattern, string)> dealerHand = new List<(Pattern, string)>();

        int betGold = 0;

        public enum Pattern
        {
            SPADES, CLUBS, HEARTS, DIAMONDS
        }


        public override void ShowInfo()
        {
            Console.WriteLine("1. 배팅 금액 입력하기");
            Console.WriteLine("0. 나가기");
        }

        public override void GetAction(int act)
        {
            if (act == 0)
            {
                Game.game.ChangeScene(new BlackJackLobby());
            }
            else
            {
                Bet();
            }
        }

        public void Bet()
        {
            int bet = -1;
            do
            {
                Console.WriteLine("배팅할 금액을 입력해주세요. (0은 나가기)");
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

            if (playerScore > 21 || (dealerScore <= 21 && dealerScore >= playerScore))
            {
                Console.WriteLine("딜러가 승리했습니다! 베팅한 골드를 잃습니다.");
                betGold = 0;
            }
            else if (dealerScore > 21 || playerScore > dealerScore)
            {
                Console.WriteLine($"당신이 승리했습니다! {betGold * 2}만큼 골드를 받습니다.");
                Game.game.player.gainGold(betGold * 2);
            }
            else
            {
                Console.WriteLine("비겼습니다! 베팅한 골드를 돌려받습니다.");
                Game.game.player.gainGold(betGold);
                betGold = 0;
            }
        }

        void PlayerTurn()
        {
            int act = -1;
            do
            {
                ShowCards();
                Console.WriteLine("당신의 턴입니다. 당신의 행동을 선택하세요.");
                Console.WriteLine("1. 카드 뽑기");
                Console.WriteLine("0. 멈추기");
                Console.WriteLine();
                Console.Write(">> ");

                act = Game.GetPlayerInputInt();

                if (act == 1)
                    playerHand.Add(DrawCard());
                if (act == 0)
                    break;
            }
            while (CountScore(playerHand) < 21);
        }

        void DealerTurn()
        {
            Console.WriteLine("딜러의 턴입니다.");
            ShowCards();
            while (CountScore(dealerHand) < 17)
            {
                Console.WriteLine("딜러가 카드를 뽑습니다.");
                dealerHand.Add(DrawCard());
                Thread.Sleep(1000);
                Console.Clear();
                ShowCards();
            }
        }

        void ShowCards()
        {
            Console.WriteLine("딜러의 카드");
            foreach (var card in dealerHand)
            {
                Console.WriteLine($"{card.Item1.ToString()} {card.Item2}");
            }

            Console.WriteLine();
            Console.WriteLine("플레이어의 카드");
            foreach (var card in playerHand)
            {
                Console.WriteLine($"{card.Item1.ToString()} {card.Item2}");
            }
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

    }
}
