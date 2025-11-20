using UnityEngine;

[CreateAssetMenu(fileName = "fish", menuName = "Scriptable Objects/fish")]
public class fish : ScriptableObject
{
    public string name;
    public Sprite fishPic;

    public float ChoasProb;
    public float ShortestShuffle;
    public float LongestShuffle;
    public int AttemptBeforeCD;
    public float CooldowbDuration;
}
