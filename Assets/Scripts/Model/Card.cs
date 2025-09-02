using System.Collections.Generic;
using UnityEngine;

public class Card
{
    //֮����Ҫ��card �� cardview�ֿ�������Ϊcard������ģ�ͣ�cardview����ͼ�������ƶ�����ƣ�����carddata�����������carddata�����罵���ˣ�����carddata�����
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
