/* Author:          ezhex1991@outlook.com
 * CreateTime:      2017-01-19 14:03:26
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace EZhex1991.XLuaExtension
{
    [CustomEditor(typeof(EZLuaSettings))]
    public class EZLuaSettingsEditor : Editor
    {
        SerializedProperty m_LuaSourceModeInEditor;
        SerializedProperty m_LuaSourceMode;

        SerializedProperty m_LuaFolders;
        SerializedProperty m_LuaBundles;
        ReorderableList luaFolderList;
        ReorderableList luaBundleList;

        private float height = EditorGUIUtility.singleLineHeight;

        public static Rect DrawReorderableListIndex(Rect rect, SerializedProperty listProperty, int index)
        {
            float labelWidth = listProperty.arraySize > 100 ? 35 : 30;
            if (GUI.Button(new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight), index.ToString(), EditorStyles.label))
            {
                DrawReorderMenu(listProperty, index).ShowAsContext();
            }
            rect.x += labelWidth; rect.width -= labelWidth;
            return rect;
        }
        public static GenericMenu DrawReorderMenu(SerializedProperty listProperty, int index)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Insert"), false, delegate
            {
                listProperty.InsertArrayElementAtIndex(index);
                listProperty.serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("Delete"), false, delegate
            {
                listProperty.DeleteArrayElementAtIndex(index);
                listProperty.serializedObject.ApplyModifiedProperties();
            });
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Move to Top"), false, delegate
            {
                listProperty.MoveArrayElement(index, 0);
                listProperty.serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("Move to Bottom"), false, delegate
            {
                listProperty.MoveArrayElement(index, listProperty.arraySize - 1);
                listProperty.serializedObject.ApplyModifiedProperties();
            });
            return menu;
        }

        private void OnEnable()
        {
            m_LuaSourceModeInEditor = serializedObject.FindProperty("m_LuaSourceModeInEditor");
            m_LuaSourceMode = serializedObject.FindProperty("m_LuaSourceMode");

            m_LuaFolders = serializedObject.FindProperty("m_LuaFolders");
            m_LuaBundles = serializedObject.FindProperty("m_LuaBundles");
            luaFolderList = new ReorderableList(serializedObject, m_LuaFolders, true, true, true, true)
            {
                drawHeaderCallback = DrawLuaFolderListHeader,
                drawElementCallback = DrawLuaFolderListElement
            };
            luaBundleList = new ReorderableList(serializedObject, m_LuaBundles, true, true, true, true)
            {
                drawHeaderCallback = DrawLuaBundleListHeader,
                drawElementCallback = DrawLuaBundleListElement
            };
        }

        private void DrawLuaFolderListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Lua Folders");
        }
        private void DrawLuaFolderListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect = DrawReorderableListIndex(rect, m_LuaFolders, index);
            SerializedProperty dir = m_LuaFolders.GetArrayElementAtIndex(index);
            rect.y += 1;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, height), dir, GUIContent.none);
        }

        private void DrawLuaBundleListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Lua Bundles");
        }
        private void DrawLuaBundleListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect = DrawReorderableListIndex(rect, m_LuaBundles, index);
            SerializedProperty bundle = m_LuaBundles.GetArrayElementAtIndex(index);
            rect.y += 1;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, height), bundle, GUIContent.none);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject(target as ScriptableObject), typeof(MonoScript), false);
            GUI.enabled = true;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Lua Source Mode", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_LuaSourceModeInEditor);
            EditorGUILayout.PropertyField(m_LuaSourceMode);
            if (m_LuaSourceMode.enumValueIndex == (int)LuaSourceMode.Develop)
            {
                Debug.LogWarning("Develop Mode is only available in Editor");
                m_LuaSourceMode.enumValueIndex = (int)LuaSourceMode.Local;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Lua Source", EditorStyles.boldLabel);
            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 30;
            luaFolderList.DoLayoutList();
            luaBundleList.DoLayoutList();
            EditorGUIUtility.labelWidth = labelWidth;

            serializedObject.ApplyModifiedProperties();
        }
    }
}