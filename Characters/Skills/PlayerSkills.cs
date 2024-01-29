using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    Dictionary<SkillCategory, int> skillLevels = new Dictionary<SkillCategory, int>();
    Dictionary<PhysicalSkill, int> physicalSkillLevels = new Dictionary<PhysicalSkill, int>();
    Dictionary<UtilitySkill, int> utilitySkillLevels = new Dictionary<UtilitySkill, int>();

    void Start()
    {
        InitializeSkillLevels();
    }

    void InitializeSkillLevels()
    {
        foreach (SkillCategory category in System.Enum.GetValues(typeof(SkillCategory)))
        {
            skillLevels[category] = 0;
        }

        foreach (PhysicalSkill skill in System.Enum.GetValues(typeof(PhysicalSkill)))
        {
            physicalSkillLevels[skill] = 0;
        }

        foreach (UtilitySkill skill in System.Enum.GetValues(typeof(UtilitySkill)))
        {
            utilitySkillLevels[skill] = 0;
        }
    }

    public void UpdateCategoryLevel(SkillCategory category, int newLevel)
    {
        if (skillLevels.ContainsKey(category))
        {
            skillLevels[category] = newLevel;
        }
    }

    public void UpdateSkillLevel<T>(T skill, int newLevel) where T : System.Enum
    {
        if (typeof(T) == typeof(PhysicalSkill) && physicalSkillLevels.ContainsKey((PhysicalSkill)(object)skill))
        {
            physicalSkillLevels[(PhysicalSkill)(object)skill] = newLevel;
        }
        else if (typeof(T) == typeof(UtilitySkill) && utilitySkillLevels.ContainsKey((UtilitySkill)(object)skill))
        {
            utilitySkillLevels[(UtilitySkill)(object)skill] = newLevel;
        }
    }

    public int GetSkillLevel<T>(T skill) where T : System.Enum
    {
        if (typeof(T) == typeof(PhysicalSkill) && physicalSkillLevels.ContainsKey((PhysicalSkill)(object)skill))
        {
            return physicalSkillLevels[(PhysicalSkill)(object)skill];
        }
        else if (typeof(T) == typeof(UtilitySkill) && utilitySkillLevels.ContainsKey((UtilitySkill)(object)skill))
        {
            return utilitySkillLevels[(UtilitySkill)(object)skill];
        }

        return 0;
    }

    // How to use:
    //      playerSkills.UpdateCategoryLevel(SkillCategory.Physical, 5);
    //      playerSkills.UpdateSkillLevel(PhysicalSkill.Strength, 3);
}
