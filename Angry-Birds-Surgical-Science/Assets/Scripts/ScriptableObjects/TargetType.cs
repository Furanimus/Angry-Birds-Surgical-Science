using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "target", menuName = "targets/target")]
public class TargetType : ScriptableObject
{
    public float velocity;
    public int score;
}
