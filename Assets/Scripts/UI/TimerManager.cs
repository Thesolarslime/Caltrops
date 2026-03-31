using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    public int Time;
    public bool TimeUp = false;

    public PlayerManager Player;

    private bool TickTock;

    private TextMeshProUGUI Text;
    private AudioPlayer Sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sound = GetComponent<AudioPlayer>();
        Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time < 1 && !TimeUp)
        {
            Text.text = "MOVES UNTIL DEATH\n- ----------------------- -\n0";
            Player.Stats.StartCoroutine(Player.Stats.Die());
            Sound.PlaySound(0, false, 1f);
            TimeUp = true;

        }
        else
        {
            Text.text = "MOVES UNTIL DEATH\n- ----------------------- -\n" + Time;
        }
    }

    public void Tick()
    {
        Time--;
        if (Time < 51)
        {
            if (TickTock)
            {
                Sound.PlaySound(1, false, 0.4f);
                TickTock = false;
            }
            else
            {
                Sound.PlaySound(2, false, 0.4f);
                TickTock = true;
            }
        }
        
    }
}
