using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillMenuController : MonoBehaviour {

    public SkillSystem skillSystem;//player skillsystem
    public PlayerController pc;

    public Skill currentSkill;
    public SkillType currentSkillType;


    //skillinformation data
    public Text skillNum; //done
    public Text skillName; //done
    public Text expCurrentMax;
    public Text levelrarity;
    public Text subtype;
    public Text cooldown;
    public Text description;
    public Text affinity;

    //skillslots area
    public Text slotnum1, slotnum2, slotnum3, slotnum4;
    //+icons
    public Text skillslotname1, skillslotname2, skillslotname3, skillslotname4;

    public List<Text> skillSlots = new List<Text>();
    void Awake() {
        skillNum.text = "";
        skillName.text = "";
        expCurrentMax.text = "";
        levelrarity.text = "";
        subtype.text = "";
        cooldown.text = "";
        description.text = "";
        affinity.text = "";

        skillslotname1.text = "";
        skillslotname2.text = "";
        skillslotname3.text = "";
        skillslotname4.text = "";

        skillSlots.Add(skillslotname1);
        skillSlots.Add(skillslotname2);
        skillSlots.Add(skillslotname3);
        skillSlots.Add(skillslotname4);

        currentSkillType = SkillType.MaterialType;
    }
    void initSkill(Skill skill) {
        
        //number
        int num = skill.skillNum;
        string numstr = "";
        if (num < 100) { numstr = "0" + num; }
        if (num < 10) { numstr = "00" + num; }
        skillNum.text = "No. " + numstr;

        //name
        skillName.text = skill.skillName;

        if (skill.skillType == SkillType.MaterialType)
        {
            skillName.color = new Color(164 / 255.0F, 208 / 255.0F, 95 / 255.0F, 255f);
        }
        else if (skill.skillType == SkillType.ControlType)
        {
            skillName.color =  new Color(247 / 255.0F, 216 / 255.0F, 66 / 255.0F, 255f);

        }
        else if (skill.skillType == SkillType.BarrierType)
        {
            skillName.color = new Color(37 / 255.0F, 162 / 255.0F, 166 / 255.0F, 255f);
        }
        else if (skill.skillType == SkillType.BarrageType)
        {
            skillName.color = new Color(244 / 255.0F, 143 / 255.0F, 151 / 255.0F, 255f);
        }
        else if (skill.skillType == SkillType.AuraType)
        {
        }

        //expcurrentmax

        //level + rarity


        if (skill.skillType == SkillType.MaterialType)
        {
            skillName.color = new Color(152 / 255.0F, 203 / 255.0F, 74 / 255.0F, 255f);
        }
        else if (skill.skillType == SkillType.ControlType)
        {
            skillName.color = new Color(247 / 255.0F, 216 / 255.0F, 66 / 255.0F, 255f);

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


        //subtype
        subtype.text = skill.subType + " Type";

        //cooldown
        cooldown.text = "Cooldown: "+skill.coolDown+" secs";

        //desc
        description.text = "" + skill.skillDesc;

        //affinity
        //affinity.text = "" + skill.asdg;


    }

    public void clearInformation() {
        skillNum.text = "";
        skillName.text = "";
        expCurrentMax.text = "";
        levelrarity.text = "";
        subtype.text = "";
        cooldown.text = "";
        description.text = "";
        affinity.text = "";
        currentSkill = null;
    }

    public void displaySkill(Skill skill) {
        currentSkill = skill;
        initSkill(skill);

    }

    public void changeCurrentSkillType(SkillType type) { currentSkillType = type; }

    public void clearSkills() {

        foreach (Text skillslotname in skillSlots) {
            skillslotname.text = "";
            skillslotname.GetComponent<SkillData>().skill = null;
            
        }
        skillSystem.removeSkills(currentSkillType);
        skillSystem.wipeSlot();


    }
    public bool checkIfSkillAlreadyExists(Skill skill) {
        foreach (Text skillslotname in skillSlots) {
            if (skillslotname.GetComponent<SkillData>().skill == null) { }
            else if (skillslotname.GetComponent<SkillData>().skill.Equals(skill)) return true;
        }
        return false;
    }
    public bool checkIfSkillTypeDuplicatesExists(SubType subtype) {
        foreach (Text skillslotname in skillSlots)
        {
            if (skillslotname.GetComponent<SkillData>().skill == null) { }
            else if (skillslotname.GetComponent<SkillData>().skill.subType == subtype) return true;
        }
        return false;
    }

    public bool canBeEquipped(Skill skill)
    {
        //Debug.Log(checkIfSkillTypeDuplicatesExists(skill.subType));
        if (checkIfSkillAlreadyExists(skill) || checkIfSkillTypeDuplicatesExists(skill.subType)) return false;
        return true;
    }

    public void populateSlots()
    {
        foreach (Text skillslotname in skillSlots)
        {
            skillslotname.text = "";
            skillslotname.GetComponent<SkillData>().skill = null;

        }
        int i = 0;
        if (skillSystem.getSlots().Count >=1)
        {
            foreach (Skill skill in skillSystem.getSlots())
            {
               
                if (skill!=null)
                {
                    //Debug.Log(skill.skillName);
                    skillSlots[i].GetComponent<SkillData>().skill = skill;
                    skillSlots[i].text = skill.skillName;
                }
                    i++;
            }
        }

    }
    public void equipSkill() {
        if (currentSkill == null)
        {
            Debug.Log("no skill found");
            return;
        }
        int i = 0;
        foreach (Text skillslotname in skillSlots)
        {
            if (skillslotname.IsActive() && skillslotname.GetComponent<SkillData>().skill == null)
            {
                if (canBeEquipped(currentSkill))
                {
                    skillslotname.text = currentSkill.skillName;
                    skillslotname.GetComponent<SkillData>().skill = currentSkill;
                    skillslotname.GetComponent<SkillData>().skill.slotPosition = i;
                    skillSystem.equipSkills(skillslotname.GetComponent<SkillData>().skill.gameObject);
                    return;
                }
            }
            i += 1;
        }
        
        
        // if (skillslotname2.GetComponent<SkillData>().skill == null)
        //{
        //    if (canBeEquipped(currentSkill)){
        //        skillslotname2.text = currentSkill.skillName;
        //        skillslotname2.GetComponent<SkillData>().skill = currentSkill;
        //        return;
        //    }
        //}
        // if (skillslotname3.GetComponent<SkillData>().skill == null)
        //{
        //    if (canBeEquipped(currentSkill)){
        //        skillslotname3.text = currentSkill.skillName;
        //        skillslotname3.GetComponent<SkillData>().skill = currentSkill;
        //        return;
        //    }
        //}
        // if (skillslotname4.GetComponent<SkillData>().skill == null)
        //{
        //    if (canBeEquipped(currentSkill)){
        //        skillslotname4.text = currentSkill.skillName;
        //        skillslotname4.GetComponent<SkillData>().skill = currentSkill;
        //        return;
        //    }
        //}
            Debug.Log("could not find empty slot");
            return;
        
    }

    public void salvageSkill(Skill skill) {


    }
}
