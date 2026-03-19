using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/PassiveItem")]
public class PassiveItem : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Image;

    public int MaxHealthMod;
    public int MaxManaMod;
    public int ArmourMod;
    public int SpeedMod;
    public float CastTimeMod;
    public int RegenMod;

    public int OnPickupXP;
    public bool OnPickupUselessMarble;
}
