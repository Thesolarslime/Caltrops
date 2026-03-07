using UnityEngine;

public class HealthAndManaSlots : MonoBehaviour
{
    public string Type; // manaslot, manabar, hpslot, hpbar
    public int ID;

    private bool ActiveSlot;

    public ObjectStats Player;
    private SpriteRenderer Sprite;
    private ParticleSystem Particles;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Type)
        {
            case "manaslot":
                if (ID > Player.MaxMana)
                {
                    Sprite.enabled = false;
                }
                else
                {
                    if (ID > Player.Mana)
                    {
                        if (ActiveSlot)
                        {
                            Particles.Stop();
                            Sprite.enabled = false;
                            ActiveSlot = false;
                        }
                    }
                    else
                    {
                        if (!ActiveSlot)
                        {
                            Particles.Play();
                            Sprite.enabled = true;
                            ActiveSlot = true;
                        }
                        
                    }
                }
                    break;
            case "manabar":
                Sprite.size = new Vector2(0.16f + (Player.MaxMana *  0.28f), 1);
                break;
            case "hpslot":
                if (ID > Player.MaxHealth)
                {
                    Sprite.enabled = false;
                }
                else
                {
                    if (ID > Player.Health)
                    {
                        if (ActiveSlot)
                        {
                            Particles.Stop();
                            Sprite.enabled = false;
                            ActiveSlot = false;
                        }
                    }
                    else
                    {
                        if (!ActiveSlot)
                        {
                            Particles.Play();
                            Sprite.enabled = true;
                            ActiveSlot = true;
                        }

                    }
                }
                break;
            case "hpbar":
                Sprite.size = new Vector2(0.16f + (Player.MaxHealth * 0.281f), 1);
                break;
        }
    }
}
