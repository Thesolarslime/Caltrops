using UnityEngine;
using UnityEngine.EventSystems;

public class WallTiling : MonoBehaviour
{
    public Sprite WallSprite;
    public Sprite WallAboveWallSprite;

    public ObjectStats Stats;
    private SpriteRenderer Sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position + new Vector3(0, -1), new Vector2(0, -1), Color.blue, 1);
        RaycastHit2D Hit = Physics2D.Raycast(transform.position + new Vector3(0, -1), new Vector2(0, -1), 0.45f);
        if (Hit.collider != null)
        {
            if (Hit.collider.GetComponent<ObjectStats>().Type == "Wall")
            {
                Sprite.sprite = WallAboveWallSprite;
            }
            else
            {
                Sprite.sprite = WallSprite;
            }
        }
        else
        {
            Sprite.sprite = WallSprite;
        }
    }
}
