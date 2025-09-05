using UnityEngine;
using UnityEngine.UI;

public class BossDamageButton : MonoBehaviour
{
    [SerializeField] private BossEnemy boss; // Scene에서 배치한 BossEnemy
    [SerializeField] private Button damageButton;

    void Start()
    {
        damageButton.onClick.AddListener(() => boss.TakeDamage(10));
    }

}

