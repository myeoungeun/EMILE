using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Data/Bullet Data")]
public class BulletData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string bulletName;
    [SerializeField] private int imageId;
    [SerializeField] private AttackType attackType;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private int pierceNum;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int explosionRange;
    [SerializeField] private int explosionEffectId;

    public int Id { get { return id; } }
    public string Name { get { return bulletName; } }
    public int ImageId { get { return imageId; } }
    public AttackType AttackType { get { return attackType; } }
    public BulletType BulletType { get { return bulletType; } }
    public int Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public int PierceNum { get { return pierceNum; } }
    public float RotationSpeed { get { return rotateSpeed; } }
    public int ExplosionRange { get { return explosionRange; } }
    public int ExplosionEffectId { get {return explosionEffectId; } }
}
