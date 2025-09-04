// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class HollowBullet : Bullet
// {
//     private Dictionary<IDamageable, Coroutine> activeHollows = new Dictionary<IDamageable, Coroutine>();
//
//     protected override void HandleCollision(Collider2D other)
//     {
//         base.HandleCollision(other);
//
//         var iDamageable = other.GetComponent<IDamageable>();
//         if (iDamageable != null && data.BulletType == BulletType.Hollow)
//         {
//             if (activeHollows.ContainsKey(iDamageable))
//             {
//                 StopCoroutine(activeHollows[iDamageable]);
//                 activeHollows.Remove(iDamageable);
//             }
//
//             Coroutine dot = StartCoroutine(HollowDOT(iDamageable, data.Damage / 5, 10f));
//             activeHollows.Add(iDamageable, dot);
//         }
//     }
//
//     private IEnumerator HollowDOT(IDamageable target, int dotDamage, float duration)
//     {
//         float timer = 0f;
//         while (timer < duration)
//         {
//             target.TakeDamage(dotDamage);
//             Debug.Log($"Hollow DOT: {dotDamage} -> Target: {target}");
//             timer += 1f;
//             yield return new WaitForSeconds(1f);
//         }
//
//         if (activeHollows.ContainsKey(target))
//             activeHollows.Remove(target);
//     }
// }