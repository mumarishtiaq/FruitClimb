using UnityEngine;

[CreateAssetMenu(fileName ="New Fruit", menuName ="Fruits")]
public class FruitTemplate : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int points;
    public Color color;
}
