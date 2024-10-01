using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Gan")]
public class GansData : ScriptableObject
{
    public float Range;
    public float Damage;
    public GameObject HitEffect;
    public GameObject FireEffect;
    public GameObject Bullet;
    public string FireAnimation;
    public float ShootDelay;
}


/*
 
 using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Building System/Build Data")]
public class BuildingData : ScriptableObject
{
    public string DisplayName;
    public Sprite Icon;
    public float GridSnapSize;
    public GameObject Prefab;
    public Vector3 BuildingSize;
    public PartType PartType;
}

public enum PartType 
{
    Room = 0,
    Corridor = 1,
    Decoration = 2
}
 
 
 */