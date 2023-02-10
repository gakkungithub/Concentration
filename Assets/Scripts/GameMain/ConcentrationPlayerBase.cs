using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
/// <summary>
/// �_�o����̃v���C���[�̊��N���X
/// </summary>

public class ConcentrationPlayerBase : MonoBehaviour
{
    public int Score;

    //����currrentChoiceCard�́A���łɗ��Ԃ��Ă���J�[�h�̎�ނ������ϐ��ł���B
    public Card currentChoiceCard;

    public Image currentChoiceCardImage;

    public bool IsMyTurn = false;

    //�v���C���[�����Ԃ�����
    public Sprite hideCardSprite;

    public UnityAction CardChoiceCallback;

    public virtual void PlayerInitialize(Sprite hideCardSprite, UnityAction cardChoiceCallback)
    {
        //�v���C���[�̏󋵂�����������B
        Score = 0;
        this.hideCardSprite = hideCardSprite;
        this.CardChoiceCallback += cardChoiceCallback;
    }

    //�p����ŏ���������
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

        //�����J�[�h��I��ł���ꍇ�͋A��
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
            //�~�X��������
            StartCoroutine(MissChoice(choiceCardImage));
        }
    }

    IEnumerator MissChoice(Image choiceCardImage)
    {
        yield return new WaitForSeconds(1f);
        // �������I�񂾃J�[�h�̗�����
        choiceCardImage.sprite = hideCardSprite;
        currentChoiceCardImage.sprite = hideCardSprite;
        //�����̃^�[���͏I��
        currentChoiceCard = null;
        IsMyTurn = false;

        //GameSoundManager.Instance.PlaySE(GameSoundManager.SETypes.CardClose);
        //�J�[�h�I�����I������ۂ̃R�[���o�b�N
        CardChoiceCallback?.Invoke();
    }

    IEnumerator PairChoice(Image choiceCardImage)
    {
        yield return new WaitForSeconds(1f);
        //�y�A���������̂ŏ���
        currentChoiceCardImage.gameObject.SetActive(false);
        choiceCardImage.gameObject.SetActive(false);
        currentChoiceCard = null;
        //�����̃^�[���𑱍s
        IsMyTurn = true;
        //�X�R�A�����Z
        Score += 2;
    }
}
