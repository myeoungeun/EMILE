using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Move�� �и��� ���� ��������
/// </summary>
interface IJumpHandler
{
    Rigidbody2D rb { get; set; }
    void OnJump(float jumpForce);
}
