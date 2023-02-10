using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ConcentrationCPU : ConcentrationPlayerBase
{
    public ConcentrationGameProgressionManager.GameModes GameModes;

    public TextMeshProUGUI ScoreText;
    /// <summary>
    /// CPU‚Ì‘I‘ð
    /// </summary>
    public override void CardChoice(Card choiceCard, Image choiceCardImage)
    {
        base.CardChoice(choiceCard, choiceCardImage);
        if (GameModes == ConcentrationGameProgressionManager.GameModes.CPUCardIsComputersChoice)
        {

            ScoreText.text = $"CPU Score:{Score}";
            return;
        }
        ScoreText.text = $"Player2 Score:{Score}";

    }
}
