using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustLevels : MonoBehaviour
{

    public PlayerSkills playerSkills;
    // Start is called before the first frame update
    void Start()
    {
        //      playerSkills.UpdateCategoryLevel(SkillCategory.Physical, 5);
        playerSkills.UpdateSkillLevel(PhysicalSkill.Strength, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
