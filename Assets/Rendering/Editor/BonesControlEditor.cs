using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
[CustomEditor(typeof(BonesControl))]

public class BonesControlEditor : Editor
{
    private bool[] foldout = new bool[(int)KneadType.MAX];

    void DrawBoneControlRot(BonesControl target, KneadType type)
    {
        EditorGUILayout.Space();
        using (var scope1 = new EditorGUI.ChangeCheckScope())
        {
            foldout[(int)type] = EditorGUILayout.Foldout(foldout[(int)type], BonesControl.GetLabel(type));
            if (foldout[(int)type])
            {
                target.UnlockTransformElement(type);
                float angle;
                Vector3 axis;
                GUILayout.Label("Min Value");
                target.MRotation[0].ToAngleAxis(out angle, out axis);
                EditorGUILayout.Vector3Field("Axis", axis);
                EditorGUILayout.FloatField("Angle", angle);

                GUILayout.Label("Max Value");
                target.MRotation[1].ToAngleAxis(out angle, out axis);
                EditorGUILayout.Vector3Field("Axis", axis);
                EditorGUILayout.FloatField("Angle", angle);

                using (var scope = new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("SaveMin"))
                    {
                        target.SaveBonesValue(BonesControl.StoreType.StoreMinValue, type);
                        MakeSceneDirty();
                    }
                    using (var ccs = new EditorGUI.ChangeCheckScope())
                    {
                        target.progress[(int)type] = EditorGUILayout.Slider(target.progress[(int)type], -1, 1);
                        if (ccs.changed) target.UpdateProgress(type);
                    }
                    if (GUILayout.Button("SaveMax"))
                    {
                        target.SaveBonesValue(BonesControl.StoreType.StoreMaxValue, type);
                        MakeSceneDirty();
                    }
                }
            }
            GUILayout.Space(2);
            if (scope1.changed)
            {
                MakeSceneDirty();
            }
        }

    }

    void DrawBoneControl(BonesControl target, KneadType type)
    {
        EditorGUILayout.Space();
        using (var scope1 = new EditorGUI.ChangeCheckScope())
        {
            foldout[(int)type] = EditorGUILayout.Foldout(foldout[(int)type], BonesControl.GetLabel(type));
            if (foldout[(int)type])
            {
                target.UnlockTransformElement(type);

                using (var scope = new EditorGUILayout.HorizontalScope())
                {
                    string minValue = target.GetBonesValue(BonesControl.StoreType.StoreMinValue, type).ToString();
                    if (GUILayout.Button(minValue))
                    {
                        target.SaveBonesValue(BonesControl.StoreType.StoreMinValue, type);
                        MakeSceneDirty();
                    }
                    using (var ccs = new EditorGUI.ChangeCheckScope())
                    {
                        target.progress[(int)type] = EditorGUILayout.Slider(target.progress[(int)type], -1, 1);
                        if (ccs.changed) target.UpdateProgress(type);
                    }
                    minValue = target.GetBonesValue(BonesControl.StoreType.StoreMaxValue, type).ToString();
                    if (GUILayout.Button(minValue))
                    {
                        target.SaveBonesValue(BonesControl.StoreType.StoreMaxValue, type);
                        MakeSceneDirty();
                    }
                }
            }
            GUILayout.Space(2);
            if (scope1.changed)
            {
                MakeSceneDirty();
            }
        }
    }


    private void MakeSceneDirty()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#endif
    }

    public override void OnInspectorGUI()
    {
        BonesControl target = (BonesControl)serializedObject.targetObject;
        serializedObject.Update();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Init"))
        {
            target.InitDefautData(true);
            MakeSceneDirty();
        }
        if (GUILayout.Button("Reset"))
        {
            target.ResetData();
            MakeSceneDirty();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        for (int i = 0; i < (int)KneadType.MAX; i++)
        {
            if (i != (int)KneadType.Rotation)
            {
                DrawBoneControl(target, (KneadType)i);
            }
            else
            {
                DrawBoneControlRot(target, (KneadType)i);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

}