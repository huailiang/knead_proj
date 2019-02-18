using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
[CustomEditor(typeof(BonesSystem))]

public class BonesSystemEditor : Editor
{
    public void DrawGroup(BonesSystem target)
    {
        EditorGUI.indentLevel += 1;
        int id = 0;
        for (var j = 0; j < target.Group.Count; j++)
        {
            var group = target.Group[j];
            EditorGUI.indentLevel += 1;
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            using (var scope = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.IntField("Group ID", id);
                if (GUILayout.Button("Add Bone"))
                {
                    target.AddBone(group);
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
                if (GUILayout.Button("Delete"))
                {
                    target.RemoveGroup(j);
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    j--;
                    continue;
                }
            }

            group.Label = EditorGUILayout.TextField("Label", group.Label);
            EditorGUILayout.IntField("Bone Count", group.Bones != null ? group.Bones.Count : 0);
            for (var i = 0; i < group.Bones.Count; i++)
            {
                var bc = group.Bones[i];
                using (var scope = new EditorGUILayout.HorizontalScope())
                {
                    bc.control = EditorGUILayout.ObjectField(bc.control, typeof(BonesControl), true) as BonesControl;
                    if (bc.control)
                    {
                        var labels = bc.control.GetLabels();
                        if (labels.Count > 0)
                        {
                            var selectId = bc.control.GetIndex(bc.type);
                            if (selectId >= labels.Count)
                            {
                                selectId = 0;
                            }
                            using (var ccs = new EditorGUI.ChangeCheckScope())
                            {
                                selectId = EditorGUILayout.Popup(selectId, labels.ToArray());
                                bc.type = BonesControl.GetEnum(labels[selectId]);
                                if (ccs.changed)
                                {
                                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                                }
                            }
                        }
                    }
                    if (GUILayout.Button("Delete"))
                    {
                        target.RemoveBone(group, i);
                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                        i--;
                    }
                }
            }
            EditorGUI.indentLevel -= 1;
            id++;
        }
        EditorGUI.indentLevel -= 1;
    }

    public override void OnInspectorGUI()
    {
        BonesSystem self = (BonesSystem)serializedObject.targetObject;
        serializedObject.Update();
        EditorGUILayout.Space();
        self.SaveName = EditorGUILayout.TextField("Save Name", self.GetSaveName());
        if (GUILayout.Button("Add Group"))
        {
            self.AddGroup();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        EditorGUILayout.IntField("Group Count", self.Group.Count);
        DrawGroup(self);
        serializedObject.ApplyModifiedProperties();
    }

}