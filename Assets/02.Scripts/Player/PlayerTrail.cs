using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    SpriteRenderer sr;
    float goal =0.2f;
    float curr;
    public void Init(Sprite sprite)
    {
        sr = transform.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.color = new Color(1, 1, 0, 0.5f);
        sprite = null;
    }
    void Update()
    {
        curr += Time.deltaTime;
        if (goal <= curr)
        {
            sr.sprite = null;
            Destroy(gameObject);
        }
    }
}
