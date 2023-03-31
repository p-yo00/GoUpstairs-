using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCard : MonoBehaviour
{
    public SkillData skillData;
    public UseSkill useSkill;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("attackRange"))
        {
            useSkill.use(skillData.id);
            GameObject.Find("skill").SetActive(false);
        }
    }
}
