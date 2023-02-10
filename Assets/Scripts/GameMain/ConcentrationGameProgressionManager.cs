using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConcentrationGameProgressionManager : MonoBehaviour
{
    [SerializeField]
    private Dealer Dealer;

    public enum GameStates
    {
        Invalide = -1,
        Start,
        Deal,
        Choice,
        GameResult,
        GameEnd
    }

    private GameStates gameState = GameStates.Invalide;

    public GameStates GetGameStates
    {
        get { return gameState; }
    }

    public enum GameModes
    {
        CPUCardIsPlayerChoice,
        CPUCardIsComputersChoice
    }

    public GameModes GameMode = GameModes.CPUCardIsPlayerChoice;

    private float choiceTime = 1.5f;

    private bool isCPUChoice = false;

    [SerializeField]
    private TextMeshProUGUI resultText;

    public void Result()
    {

    }

    private void Update()
    {
        switch (gameState)
        {
            case GameStates.Invalide:
                gameState = GameStates.Start;
                break;
            case GameStates.Start:
                Dealer.GameModes = this.GameMode;
                //GameSoundManager.Instance.PlayBGM(GameSoundManager.BGMTypes.GameMain);
                //�X�^�[�g�̉��K�Ȃǂ������ꍇ�͂����ɏ���
                gameState = GameStates.Deal;
                break;
            case GameStates.Deal:
                Dealer.Deal();
                gameState = GameStates.Choice;
                break;

            case GameStates.Choice:
                //�Q�[�����[�h��CPU���R���s���[�^���I�ԏꍇ��
                if(GameMode == GameModes.CPUCardIsComputersChoice)
                {
                    if(Dealer.GetCPUConcentrationPlayer.IsMyTurn)
                    {
                        choiceTime -= Time.deltaTime;
                        if(choiceTime < 0)
                        {
                            var randChoice = Random.Range(0, Dealer.GetCardBGRoot.GetComponentsInChildren<CardButtonExtension>().Length + 1);
                            var randCount = 0;
                            isCPUChoice = false;
                            foreach(var card in Dealer.GetCardBGRoot.GetComponentsInChildren<CardButtonExtension>())
                            {
                                randCount++;
                                if(card.GetCardImage != Dealer.GetCPUConcentrationPlayer.currentChoiceCardImage)
                                {
                                    if(card.gameObject.activeSelf && !isCPUChoice && randCount == randChoice)
                                    {
                                        card.OnPointerClick(null);
                                        isCPUChoice = true;
                                    }
                                }
                            }
                            choiceTime = 1.5f;
                        }
                    }
                }
            �@�@//���́��𗝉����悤�B

                //�J�[�h��؂���
                if(Dealer.GetPlayerCardCount + Dealer.GetCPUCardCount == 52)
                {
                    gameState = GameStates.GameResult;
                }
                break;

            case GameStates.GameResult:
                //�Q�[�����U���g�p�̉��Z���s��
                Dealer.ResultInformation(Dealer.GetPlayerCardCount > Dealer.GetCPUCardCount);
                gameState = GameStates.GameEnd;
                break;

            case GameStates.GameEnd:
                if(Dealer.GetPlayerCardCount > Dealer.GetCPUCardCount)
                {
                    Debug.Log("Player�̏���");
                }
                else
                {
                    Debug.Log("CPU�̏���");
                }
                break;
                
        }
    }
}
