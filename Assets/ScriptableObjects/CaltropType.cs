using UnityEngine;

[CreateAssetMenu(fileName = "CaltropType", menuName = "Scriptable Objects/CaltropType")]
public class CaltropType : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public string Description;
    public Sprite Icon; // what it look like in ui
    public Sprite Tile; // what it look like on the ground
    public string DirectionPlaced; // what direction "Forward, Leftside, Rightside, Back, Up, Down, Left, Right" (Leftside and Rightside are based on faced direction)
    public int DistancePlaced; // how far
    public float CastTime; // how long in seconds it takes to summon this caltrop
    public int Damage; // how much damage a caltrop does
    public int Durability; // how many times a caltrop can be stepped on before it dies
    public string Infliction; // "None" for no effect, Stun, Disorient, 
    public float InflictionDuration; // duration in seconds of the infliction effect if there is one
    public bool PlayerImmune; // if true, the player won't trigger the caltrop when stepping on it
}
