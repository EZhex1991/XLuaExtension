/* Author:          ezhex1991@outlook.com
 * CreateTime:      2018-02-27 12:36:14
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEditor;
using UnityEngine;

namespace EZhex1991.XLuaExtension
{
    [CustomEditor(typeof(EZLuaBehaviour))]
    public class EZLuaBehaviourEditor : EZPropertyListEditor
    {
        protected SerializedProperty m_ModuleName;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_ModuleName = serializedObject.FindProperty("moduleName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(target as MonoBehaviour), typeof(MonoScript), false);
            GUI.enabled = true;

            EditorGUILayout.PropertyField(m_ModuleName);
            elementList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(EZLuaInjector))]
    public class EZLuaInjectorEditor : EZPropertyListEditor
    {

    }
}
