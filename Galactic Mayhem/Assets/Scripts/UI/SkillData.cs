using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillData : MonoBehaviour {

    public SkillMenuController mc;
    public Skill skill;
    public void skall() { }


    public void clicked()
    {
        mc.displaySkill(skill);
    }
}
