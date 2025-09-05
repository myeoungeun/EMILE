using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Data/Bullet Data")]
public class BulletData : ScriptableObject
{
    [Header("기본 정보")]
    [SerializeField] private int id;
    [SerializeField] private string bulletName;
    [SerializeField] private int imageId;
    [SerializeField] private AttackType attackType;
    [SerializeField] private BulletType bulletType;
    
    [Header("전투 관련")]
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private int pierceNum; //실제로 사용안함
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int explosionRange;
    [SerializeField] private int explosionEffectId;
    
    [Header("발사 관련")]
    [SerializeField] private int shotInterval; // 발사 간격
    [SerializeField] private int shotMaxCount; // 최대 총알 수 (default : -1, 무한)
    [SerializeField] private GameObject bulletPrefab;
    
    public GameObject BulletPrefab => bulletPrefab;
    public int ShotMaxCount { get => shotMaxCount; set => shotMaxCount = value; }
    public int ShotInterval => shotInterval;
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
