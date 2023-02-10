using System.Collections;
using System.Collections.Generic;

/// <sumamry>
/// トランプのカードを決める際に動くクラス
/// </summary>

public static class CardHelper
{
    /// <summary>
    /// ジョーカー以外のカードの最大枚数
    /// </summary>
    public const int CardMax = 52;

    /// <summary>
    /// カードの数を決める
    /// </summary>
    /// <returns></returns>
    public static int CardNumJudge(int _num)
    {
        for(int i = 0; i < 13; i++)
        {
            if(_num % 13 == i)
            {
                return i + 1;
            }
        }
        return 0;
    }

    /// <summary>
    /// カードの役を決める
    /// </summary>
    /// <returns></returns>
    public static Card.Suit CardSuitJudge(int _num)
    {
        for(int i = 0; i <(int)Card.Suit.Max; i++)
        {
            if(_num / 13 == i)
            {
                return (Card.Suit)i;
            }
        }
        return Card.Suit.Invalid;
    }
}
