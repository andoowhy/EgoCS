using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor( typeof( EgoInterface ) ) ]
public class EgoInterfaceEditor : Editor
{
    ReorderableList reorderableList;

    public void OnEnable()
    {
        reorderableList = new ReorderableList( EgoSystems.systems, typeof( EgoSystem ), false, true, false, false );

        reorderableList.drawHeaderCallback = ( Rect rect ) =>
        {
            EditorGUI.LabelField( rect, "Systems:" );
        };

        reorderableList.drawElementCallback = ( Rect rect, int index, bool isActive, bool isFocused ) =>
        {
            var label = EgoSystems.systems[ index ].GetType().Name;
            if( label.EndsWith( "System") || label.EndsWith( "system" ) ) label = label.Substring( 0, label.Length - 6 );
            label = " " + label;

            EgoSystems.systems[ index ].enabled = EditorGUI.ToggleLeft( rect, label, EgoSystems.systems[ index ].enabled );
        };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        reorderableList.DoLayoutList();
    }
}
