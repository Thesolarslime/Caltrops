using UnityEngine;

[CreateAssetMenu(fileName = "LevelBonus", menuName = "Scriptable Objects/LevelBonus")]
public class LevelBonus : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string Description;
}
