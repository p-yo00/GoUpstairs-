using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillcardCreator : MonoBehaviour
{
    public List<SkillData> skillLists;
    
    public void createCard()
    {
        SkillData[] selected = new SkillData[2];
        for(int i=0; i<2; i++)
        {
            int rand = Random.Range(0, skillLists.Count);
            selected[i] = skillLists[rand];
            skillLists.RemoveAt(rand);
        }
        skillLists.Add(selected[0]);
        skillLists.Add(selected[1]);
        print("1"+selected[0].id); print("2"+selected[1].id);
        transform.Find("skill1").gameObject.GetComponent<SkillCard>().skillData = selected[0];
        transform.Find("skill1").gameObject.GetComponent<MeshRenderer>().material = selected[0].material;
        transform.Find("skill2").gameObject.GetComponent<SkillCard>().skillData = selected[1];
        transform.Find("skill2").gameObject.GetComponent<MeshRenderer>().material = selected[1].material;
    }
}
