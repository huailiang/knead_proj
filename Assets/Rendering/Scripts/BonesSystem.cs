using System;
using System.Collections.Generic;
using UnityEngine;

public enum KneadType
{
    PosX = 0,
    PosY,
    PosZ,
    Rotation,
    ScaleX,
    ScaleY,
    ScaleZ,
    MaxCount,
}

public class BonesSystem : MonoBehaviour
{
    [Serializable]
    public class BonesControlElement
    {
        public BonesControl control;
        public KneadType type;
    }

    [Serializable]
    public class BonesGroup
    {
        public string Label = "";
        public List<BonesControlElement> Bones;
    }

    public List<BonesGroup> Group = new List<BonesGroup>();
    public string SaveName = "";

    public string GetSaveName()
    {
        if (SaveName.Equals(""))
        {
            return name;
        }
        return SaveName;
    }
    
    public void AddGroup()
    {
        var gp = new BonesGroup()
        {
            Bones = new List<BonesControlElement>()
        };
        Group.Add(gp);
    }

    public void AddBone(BonesGroup bg)
    {
        bg.Bones.Add(new BonesControlElement());
    }

    public void RemoveBone(BonesGroup bg, int index)
    {
        bg.Bones.RemoveAt(index);
    }

    public void RemoveGroup(int index)
    {
        Group.RemoveAt(index);
    }
}