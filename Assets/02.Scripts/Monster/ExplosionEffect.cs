using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public void AnimationFinished()
    {
        Destroy(transform.root.gameObject);
    }
}
