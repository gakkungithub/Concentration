using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// カードを押した時の役割
/// </summary>
public class CardButtonExtension : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{

    private Image cardImage;
    private Sprite cardSprite;
    private Sprite hideCardSprite;

    public Image GetCardImage
    {
        get { return cardImage; }
    }

    public UnityAction OnClickCallback;
    public UnityAction OnPointerDownCallBack;
    public UnityAction OnPointerUpCallBack;

    public void Initialize(Sprite cardSprite, Sprite hideCardSprite, UnityAction onClickAction)
    {
        this.cardImage = this.GetComponent<Image>();
        this.cardSprite = cardSprite;
        this.hideCardSprite = hideCardSprite;
        // 最初はカードを隠しておく
        this.cardImage.sprite = this.hideCardSprite;

        OnClickCallback += onClickAction;

    }


    // クリック
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickCallback?.Invoke();
        // カードの表示を表にする
        this.cardImage.sprite = this.cardSprite;
    }

    // タップダウン  
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCallBack?.Invoke();
    }
    // タップアップ  
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpCallBack?.Invoke();
    }

    // カーソルがこのボタンに触れたら時
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.cardImage.color = new Color(0.9f, 0.9f, 0.9f);
    }

    // カーソルがこのボタンから離れた時
    public void OnPointerExit(PointerEventData eventData)
    {
        this.cardImage.color = Color.white;
    }
}
