using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillData", menuName ="Scriptable/SkillData", order =int.MaxValue)]
public class SkillData : ScriptableObject
{
    public Material material;
    public string name;
    public int id;
}
