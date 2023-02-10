using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// �J�[�h�����������̖���
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
        // �ŏ��̓J�[�h���B���Ă���
        this.cardImage.sprite = this.hideCardSprite;

        OnClickCallback += onClickAction;

    }


    // �N���b�N
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickCallback?.Invoke();
        // �J�[�h�̕\����\�ɂ���
        this.cardImage.sprite = this.cardSprite;
    }

    // �^�b�v�_�E��  
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCallBack?.Invoke();
    }
    // �^�b�v�A�b�v  
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpCallBack?.Invoke();
    }

    // �J�[�\�������̃{�^���ɐG�ꂽ�玞
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.cardImage.color = new Color(0.9f, 0.9f, 0.9f);
    }

    // �J�[�\�������̃{�^�����痣�ꂽ��
    public void OnPointerExit(PointerEventData eventData)
    {
        this.cardImage.color = Color.white;
    }
}
