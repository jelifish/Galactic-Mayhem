using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillMenuController : MonoBehaviour {

    public SkillSystem skillSystem;//player skillsystem

    //public Skill currentSkill;


    //skillinformation data
    public Text skillNum;
    public Text skillName;
    public Text expCurrentMax;
    public Text levelLegendary;
    public Text subtype;
    public Text cooldown;
    public Text description;
    public Text affinity;



    void initSkill(Skill skill) {
        int num = skill.skillNum;
        string numstr = "";
        if (num < 100) { numstr = "0" + num; }
        if (num < 10) { numstr = "00" + num; }
        skillNum.text = "No. " + numstr;


        skillName.text = skill.skillName;

        if (skill.skillType == SkillType.MaterialType)
        {
            skillName.color = new Color(152 / 255.0F, 203 / 255.0F, 74 / 255.0F, 255f);
            //spawn.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(152 / 255.0F, 203 / 255.0F, 74 / 255.0F, 255f));
        }
        else if (skill.skillType == SkillType.ControlType)
        {
            skillName.color =  new Color(247 / 255.0F, 216 / 255.0F, 66 / 255.0F, 255f);

        }
        else if (skill.skillType == SkillType.BarrierType)
        {

        }
        else if (skill.skillType == SkillType.BarrageType)
        {
            skillName.color = new Color(250 / 255.0F, 130 / 255.0F, 40 / 255.0F, 255f);
        }
        else if (skill.skillType == SkillType.AuraType)
        {
        }

    }

    public void displaySkill(Skill skill) {
        initSkill(skill);

    }





    public void equip() { }


    public void equipSkill(Skill skill) {

    }

    public void salvageSkill(Skill skill) {
    }
}
