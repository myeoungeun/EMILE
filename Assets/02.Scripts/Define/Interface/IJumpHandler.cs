using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Move와 분리를 위해 별도선언
/// </summary>
interface IJumpHandler
{
    Rigidbody2D rb { get; set; }
    void OnJump(float jumpForce);
}
