using System.Collections.Generic;
using UnityEngine;

public class Card
{
    //之所以要把card 和 cardview分开，是因为card是数据模型，cardview是视图，比如牌堆里的牌，还有carddata，如果更改了carddata，比如降费了，所有carddata都会变
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;
    public Effect ManualTargetEffect => data.ManualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => data.OtherEffects;
    public int Mana { get; private set; }
    private readonly CardData data;
    public Card(CardData cardData)
    {
        this.data = cardData;
        Mana = cardData.Mana;
    }
}
