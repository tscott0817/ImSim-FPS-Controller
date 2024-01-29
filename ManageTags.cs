using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManageTags
{
    public static string doorTag = "Door";
    public static string physObjTag = "PhysObject";
    public static string lightSwitchTag = "LightSwitch";
    public static string ladderTag = "Ladder";
    public static string ropeTag = "Rope";
    public static string enemyTag = "Enemy";
    public static string electricityTag = "Electricity";
    public static string fireTag = "Fire";
    public static string poisonTag = "Poison";

    /*public static string[] tags = { "Door", "PhysObject", "LightSwitch", "Ladder", "Rope", "Enemy", "Electricity", "Fire", "Poison" };*/
    public static List<string> tagsList = new List<string>
    {
        doorTag,
        physObjTag,
        lightSwitchTag,
        ladderTag,
        ropeTag,
        enemyTag,
        electricityTag,
        fireTag,
        poisonTag
    };
}
