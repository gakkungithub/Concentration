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
                //スタートの演習などしたい場合はここに書く
                gameState = GameStates.Deal;
                break;
            case GameStates.Deal:
                Dealer.Deal();
                gameState = GameStates.Choice;
                break;

            case GameStates.Choice:
                //ゲームモードでCPUがコンピュータが選ぶ場合は
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
            　　//この↑を理解しよう。

                //カードを切り取る
                if(Dealer.GetPlayerCardCount + Dealer.GetCPUCardCount == 52)
                {
                    gameState = GameStates.GameResult;
                }
                break;

            case GameStates.GameResult:
                //ゲームリザルト用の演算を行う
                Dealer.ResultInformation(Dealer.GetPlayerCardCount > Dealer.GetCPUCardCount);
                gameState = GameStates.GameEnd;
                break;

            case GameStates.GameEnd:
                if(Dealer.GetPlayerCardCount > Dealer.GetCPUCardCount)
                {
                    Debug.Log("Playerの勝ち");
                }
                else
                {
                    Debug.Log("CPUの勝ち");
                }
                break;
                
        }
    }
}
