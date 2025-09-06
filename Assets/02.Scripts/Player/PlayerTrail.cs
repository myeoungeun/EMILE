using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    SpriteRenderer sr;
    float goal =0.2f;
    float alphaOper;
    float curr;
    public void Init(Sprite sprite)
    {
        sr = transform.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.color = new Color(1, 1, 0, 0.5f);
        sr.sortingOrder = 10;
        alphaOper = Mathf.Min(goal, sr.color.a) / Mathf.Max(goal, sr.color.a);
        sprite = null;
    }

    public static void CreateTrail(Sprite sprite,Vector3 trailPos,Vector3 scale)
    {
        GameObject temp = new GameObject("playerTrail");
        temp.transform.localScale = scale;
        temp.transform.position = trailPos;
        temp.AddComponent<PlayerTrail>().Init(sprite);
        sprite = null;
    }

    void Update()
    {
        curr += Time.deltaTime;
        sr.color = new Color(1, 1, 0, sr.color.a -(alphaOper*Time.deltaTime));
        if (goal <= curr)
        {
            sr.sprite = null;
            Destroy(gameObject);
        }
    }
}
