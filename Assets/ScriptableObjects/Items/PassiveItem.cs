using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/PassiveItem")]
public class PassiveItem : ScriptableObject
{
    public string Name;
    public Sprite Image;

    public int MaxHealthMod;
    public int MaxManaMod;
    public int ArmourMod;
    public int SpeedMod;
    public int CastTimeMod;
    public int RegenMod;
}
