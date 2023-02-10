using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
/// <summary>
/// 神経衰弱のプレイヤーの基底クラス
/// </summary>

public class ConcentrationPlayerBase : MonoBehaviour
{
    public int Score;

    //このcurrrentChoiceCardは、すでに裏返しているカードの種類を示す変数である。
    public Card currentChoiceCard;

    public Image currentChoiceCardImage;

    public bool IsMyTurn = false;

    //プレイヤーが裏返すため
    public Sprite hideCardSprite;

    public UnityAction CardChoiceCallback;

    public virtual void PlayerInitialize(Sprite hideCardSprite, UnityAction cardChoiceCallback)
    {
        //プレイヤーの状況を初期化する。
        Score = 0;
        this.hideCardSprite = hideCardSprite;
        this.CardChoiceCallback += cardChoiceCallback;
    }

    //継承先で処理を書く
    public virtual void CardChoice(Card choiceCard, Image choiceCardImage)
    {
        if (currentChoiceCard == null)
        {
            //GameSoundManager.Instance.PlaySE(GameSoundManager.SETypes.CardOpen);
            currentChoiceCard = choiceCard;
            currentChoiceCardImage = choiceCardImage;
            IsMyTurn = true;
            return;
        }

        //同じカードを選んでいる場合は帰る
        if (choiceCard == currentChoiceCard)
        {
            return;
        }

        //GameSoundManager.Instance.PlaySE(GameSoundManager.SETypes.CardOpen)
        if (currentChoiceCard.Number == choiceCard.Number)
        {
            StartCoroutine(PairChoice(choiceCardImage));
        }
        else
        {
            //ミスした時の
            StartCoroutine(MissChoice(choiceCardImage));
        }
    }

    IEnumerator MissChoice(Image choiceCardImage)
    {
        yield return new WaitForSeconds(1f);
        // 自分が選んだカードの裏側に
        choiceCardImage.sprite = hideCardSprite;
        currentChoiceCardImage.sprite = hideCardSprite;
        //自分のターンは終了
        currentChoiceCard = null;
        IsMyTurn = false;

        //GameSoundManager.Instance.PlaySE(GameSoundManager.SETypes.CardClose);
        //カード選択が終わった際のコールバック
        CardChoiceCallback?.Invoke();
    }

    IEnumerator PairChoice(Image choiceCardImage)
    {
        yield return new WaitForSeconds(1f);
        //ペアが揃ったので消す
        currentChoiceCardImage.gameObject.SetActive(false);
        choiceCardImage.gameObject.SetActive(false);
        currentChoiceCard = null;
        //自分のターンを続行
        IsMyTurn = true;
        //スコアを加算
        Score += 2;
    }
}
