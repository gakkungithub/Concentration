/*using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Dealer : MonoBehaviour
{
    private Deck Deck = new Deck();

    [SerializeField]
    private SpriteAtlas CardAtlas;

    //トランプを表示するImage
    public Image CardImage;

    //トランプを産むルート
    [SerializeField]
    private RectTransform cardBG;

    private void Start()
    {
        Deck.GetDeck();

        //Linq式における例: where
        //ラムダ式でboolを判定し、List内に判定条件に合致する要素を返す
        var clubCards = Deck.CardDeck.Where(card => card.CardSuit == Card.Suit.Club).ToList();
        var clubOne = clubCards.FirstOrDefault();
        // clubの1
        // Linqにおける例: any
        //ラムダ式でboolを判定し、List内に判定条件に合致するかtrueかfalseで返す
        var clubCardsInHeartCard = Deck.CardDeck.Any(card => card.CardSuit == Card.Suit.Heart);
        //false

        //カードの文字列をフックに表示する
        //card.sprite = CardAtlas.GetSprite($"Card_{((int)clubOne.CardSuit * 13) + clubOne.Number - 1}");

        foreach(var card in Deck.CardDeck)
        {
            var cardImage = Instantiate(CardImage, cardBG);
            //カードの文字列をフックに表示する
            //cardImage.sprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
            cardImage.sprite = CardAtlas.GetSprite($"Card_54");

            var button = cardImage.gameObject.AddComponent<Button>();

            button.onClick.AddListener(() =>
            {
                cardImage.sprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
            });
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

using TMPro;
using static ConcentrationGameProgressionManager;

public class Dealer : MonoBehaviour
{
    private Deck Deck = new Deck();


    [SerializeField]
    private SpriteAtlas CardAtlas;

    // トランプを表示するImage
    public Image CardImage;

    // ディーラーにターンを判定してもらう
    public enum Turn
    {
        Player,
        CPU
    }
    public Turn ActorTurn = Turn.Player;


    // 1つ前に選択したカード
    private Card currentCard;
    // 1つ前に選択したカード
    private Image currentCardImage;
    // トランプを産むルート
    [SerializeField]
    private RectTransform cardBG;

    public RectTransform GetCardBGRoot
    {
        get { return cardBG; }
    }

    [SerializeField]
    private ConcentrationGameProgressionManager concentrationGameProgressionManager;

    [SerializeField]
    private ConcentrationPlayer Player;

    [SerializeField]
    private ConcentrationCPU CPU;

    [SerializeField]
    private TextMeshProUGUI turnInformationText;


    public ConcentrationPlayerBase GetCPUConcentrationPlayer
    {
        get { return CPU; }
    }

    public int GetPlayerCardCount
    {
        get { return Player.Score; }
    }

    public int GetCPUCardCount
    {
        get { return CPU.Score; }
    }

    [SerializeField]
    private TextMeshProUGUI resultInformationText;

    public ConcentrationGameProgressionManager.GameModes GameModes;

    // 結果の表示を行う
    public void ResultInformation(bool isPlayerWin)
    {
        resultInformationText.gameObject.SetActive(true);
        switch (GameModes)
        {
            case GameModes.CPUCardIsComputersChoice:
                if (isPlayerWin)
                {
                    GameVisualEffectManager.Instance.GameResultParticlePlay(GameVisualEffectManager.GameMainResult.GameWin);
                    resultInformationText.text = $"Player Win";
                }
                else
                {
                    resultInformationText.text = $"Player Lose";
                }
                break;

            case GameModes.CPUCardIsPlayerChoice:

                if (isPlayerWin)
                {
                    GameVisualEffectManager.Instance.GameResultParticlePlay(GameVisualEffectManager.GameMainResult.GameWin);
                    resultInformationText.text = $"Player 1 Win";
                }
                else
                {
                    GameVisualEffectManager.Instance.GameResultParticlePlay(GameVisualEffectManager.GameMainResult.GameWin);
                    resultInformationText.text = $"Player 2 Lose";
                }
                break;
        }
    }

    public void Deal()
    {
        // 一時的に昇順に戻す
        Deck.GetDeck();

        Player.PlayerInitialize(CardAtlas.GetSprite($"Card_54"), TurnChange);
        CPU.PlayerInitialize(CardAtlas.GetSprite($"Card_54"), TurnChange);

        // CPUに今回のゲームのモードを伝える
        CPU.GameModes = this.GameModes;

        // Linqにおける例：where
        // ラムダ式でboolを判定し、List内に判定条件に合致する要素を返す
        var clubCards = Deck.CardDeck.Where(card => card.CardSuit == Card.Suit.Club).ToList();

        var clubOne = clubCards.FirstOrDefault();
        // clubの1

        // Linqにおける例：any
        // ラムダ式でboolを判定し、List内に判定条件に合致するかtrueかflaseで返す
        var clubCardsInHeartCard = clubCards.Any(card => card.CardSuit == Card.Suit.Heart);
        // false

        StartCoroutine(FinishDealingCards());
    }

    private IEnumerator FinishDealingCards()
    {
        // カードを産む
        foreach (var card in Deck.CardDeck)
        {
            var cardImage = Instantiate(CardImage, cardBG);
            var cardButton = cardImage.gameObject.GetComponent<CardButtonExtension>();
            var cardSprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
            var hideCardSprite = CardAtlas.GetSprite($"Card_54");
            cardButton.Initialize(cardSprite, hideCardSprite, () => {
                switch (ActorTurn)
                {
                    case Turn.Player:
                        Player.CardChoice(card, cardImage);
                        break;
                    case Turn.CPU:

                        CPU.CardChoice(card, cardImage);
                        break;
                }
            });

        }
        //産み終わってから1フレーム待つ
        yield return new WaitForEndOfFrame();
        // GridLayoutGroupを外す
        GetCardBGRoot.GetComponent<GridLayoutGroup>().enabled = false;
    }

    /// <summary>
    /// ターン変更のメソッド
    /// </summary>
    private void TurnChange()
    {
        switch (ActorTurn)
        {
            case Turn.Player:

                if (!Player.IsMyTurn)
                {
                    ActorTurn = Turn.CPU;
                    CPU.IsMyTurn = true;
                }
                break;
            case Turn.CPU:

                if (!CPU.IsMyTurn)
                {
                    ActorTurn = Turn.Player;
                    Player.IsMyTurn = true;
                }
                break;
        }
        StartCoroutine(TurnInformaiton(ActorTurn));
    }

    private IEnumerator TurnInformaiton(Turn turn)
    {
        turnInformationText.gameObject.SetActive(true);
        switch (turn)
        {
            case Turn.Player:
                turnInformationText.text = $"Next turn is Player";
                break;

            case Turn.CPU:
                if (GameModes == GameModes.CPUCardIsPlayerChoice)
                {
                    turnInformationText.text = $"Next turn is Player2";
                }
                else
                {
                    turnInformationText.text = $"Next turn is CPU";
                }
                break;
        }
        yield return new WaitForSeconds(0.9f);
        turnInformationText.gameObject.SetActive(false);

    }

}
