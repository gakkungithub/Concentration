using System.Collections;
using System.Collections.Generic;

public class Card
{
    /// <summary>
    /// トランプの柄
    /// </summary>
    public enum Suit
    {
        Invalid = -1,
        Club,
        Dia,
        Heart,
        Spade, 
        Max
    }
    /// <summary>
    /// トランプの柄
    /// </summary>
    public Suit CardSuit = Suit.Invalid;


    /// <summary>
    ///トランプの数字
    /// </summary>
    public int Number = 0;

    /// <summary>
    ///カードの初期化
    /// <summary>
    /// <param name = "suit">柄</param>
    /// <param name = "number">トランプの数字</param>
    public Card(Suit suit, int number)
    {
        this.CardSuit = suit;
        this.Number = number;
    }
}
