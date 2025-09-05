using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMoveHandler
{
    Rigidbody2D rb { get; set; }
    void OnMove(Vector2 dir);
}
