using System;
using System.Collections.Generic;
using UnityEngine;

public enum KneadType
{
    PosX,
    PosY,
    PosZ,
    Rotation,
    ScaleX,
    ScaleY,
    ScaleZ,
    MAX
}

[ExecuteInEditMode]
public class BonesControl : MonoBehaviour
{
    public enum StoreType
    {
        StoreMinValue = 0,
        StoreMaxValue,
        StoreCount
    }

    public Quaternion[] MRotation { get { return mRotation; } }
    public float[] progress = new float[(int)KneadType.MAX];
    bool isInitData = false;
    int unlockBoneState = 0;
    [SerializeField]
    Vector3 defaultPosition = Vector3.zero;
    [SerializeField]
    Quaternion defaultRotation = Quaternion.identity;
    [SerializeField]
    Vector3 defaultScale = Vector3.one;
    [SerializeField]
    Vector3 curPosition = Vector3.zero;
    [SerializeField]
    Quaternion curRotation = Quaternion.identity;
    [SerializeField]
    Vector3 curScale = Vector3.one;
    [SerializeField]
    Vector3[] mPosition = new Vector3[(int)StoreType.StoreCount] { Vector3.zero, Vector3.zero };
    [SerializeField]
    Quaternion[] mRotation = new Quaternion[(int)StoreType.StoreCount] { Quaternion.identity, Quaternion.identity };
    [SerializeField]
    Vector3[] mScale = new Vector3[(int)StoreType.StoreCount] { Vector3.one, Vector3.one };

    public void InitDefautData(bool isForce)
    {
        if (isForce || !isInitData)
        {
            isInitData = true;
            defaultPosition = transform.localPosition;
            defaultRotation = transform.localRotation;
            defaultScale = transform.localScale;
            for (var i = 0; i < 2; i++)
            {
                mPosition[i] = defaultPosition;
                mRotation[i] = defaultRotation;
                mScale[i] = defaultScale;
            }
            curPosition = defaultPosition;
            curScale = defaultScale;
            curRotation = defaultRotation;
            for (var i = 0; i < (int)KneadType.MAX; i++)
            {
                progress[i] = 0.0f;
            }
        }
    }

    public void ResetData()
    {
        transform.localPosition = defaultPosition;
        transform.localScale = defaultScale;
        transform.localRotation = defaultRotation;
        for (var i = 0; i < (int)KneadType.MAX; i++)
        {
            progress[i] = 0.0f;
        }
    }

    static public string GetLabel(KneadType type)
    {
        switch (type)
        {
            case KneadType.PosX:
            case KneadType.PosY:
            case KneadType.PosZ:
            case KneadType.Rotation:
            case KneadType.ScaleX:
            case KneadType.ScaleY:
            case KneadType.ScaleZ:
                return type.ToString();
            default:
                return "";
        }
    }

    public void UnlockTransformElement(KneadType type)
    {
        unlockBoneState |= (1 << (int)type);
    }

    public void SaveBonesValue(StoreType id, KneadType type)
    {
        switch (type)
        {
            case KneadType.PosX:
                mPosition[(int)id].x = transform.localPosition.x;
                break;
            case KneadType.PosY:
                mPosition[(int)id].y = transform.localPosition.y;
                break;
            case KneadType.PosZ:
                mPosition[(int)id].z = transform.localPosition.z;
                break;
            case KneadType.Rotation:
                mRotation[(int)id] = transform.localRotation;
                break;
            case KneadType.ScaleX:
                mScale[(int)id].x = transform.localScale.x;
                break;
            case KneadType.ScaleY:
                mScale[(int)id].y = transform.localScale.y;
                break;
            case KneadType.ScaleZ:
                mScale[(int)id].z = transform.localScale.z;
                break;
        }
    }

    public float GetBonesValue(StoreType id, KneadType type)
    {
        switch (type)
        {
            case KneadType.PosX:
                return mPosition[(int)id].x;
            case KneadType.PosY:
                return mPosition[(int)id].y;
            case KneadType.PosZ:
                return mPosition[(int)id].z;
            case KneadType.ScaleX:
                return mScale[(int)id].x;
            case KneadType.ScaleY:
                return mScale[(int)id].y;
            case KneadType.ScaleZ:
                return mScale[(int)id].z;
            default:
                return 0.0f;
        }
    }

    public void UpdateProgress(KneadType type, float d)
    {
        int id = d < 0 ? 0 : 1;
        switch (type)
        {
            case KneadType.PosX:
                curPosition.x = Mathf.Lerp(defaultPosition.x, mPosition[(int)id].x, Mathf.Abs(d));
                break;
            case KneadType.PosY:
                curPosition.y = Mathf.Lerp(defaultPosition.y, mPosition[(int)id].y, Mathf.Abs(d));
                break;
            case KneadType.PosZ:
                curPosition.z = Mathf.Lerp(defaultPosition.z, mPosition[(int)id].z, Mathf.Abs(d));
                break;
            case KneadType.Rotation:
                curRotation = Quaternion.Lerp(defaultRotation, mRotation[(int)id], Mathf.Abs(d));
                break;
            case KneadType.ScaleX:
                curScale.x = Mathf.Lerp(defaultScale.x, mScale[(int)id].x, Mathf.Abs(d));
                break;
            case KneadType.ScaleY:
                curScale.y = Mathf.Lerp(defaultScale.y, mScale[(int)id].y, Mathf.Abs(d));
                break;
            case KneadType.ScaleZ:
                curScale.z = Mathf.Lerp(defaultScale.z, mScale[(int)id].z, Mathf.Abs(d));
                break;
        }
        RefreshTransform();
    }

    public void UpdateProgress(KneadType type)
    {
        float d = progress[(int)type];
        UpdateProgress(type, d);
    }

    public void RefreshTransform()
    {
        transform.localPosition = curPosition;
        transform.localScale = curScale;
        transform.localRotation = curRotation;
    }

}