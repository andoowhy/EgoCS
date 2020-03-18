using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor( typeof( EgoInterface ), true )]
public class EgoInterfaceEditor : Editor
{
    private ReorderableList fixedUpdateSystemList;
    private ReorderableList updateSystemList;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if( !Application.isPlaying ) { return; }

        var egoInterface = target as EgoInterface;
        if( egoInterface == null ) { return; }
        if( egoInterface.baseFixedUpdateSystems == null ) { return; }
        if( egoInterface.baseUpdateSystems == null ) { return; }

        if( fixedUpdateSystemList == null || updateSystemList == null )
        {
            InitLists( egoInterface );
        }
        
        EditorGUILayout.Space();

        fixedUpdateSystemList.DoLayoutList();
        updateSystemList.DoLayoutList();
    }

    private void InitLists(EgoInterface egoInterface)
    {
        fixedUpdateSystemList = new ReorderableList( egoInterface.baseFixedUpdateSystems, typeof( EgoSystem ), false, true, false, false );
        updateSystemList = new ReorderableList( egoInterface.baseUpdateSystems, typeof( EgoSystem ), false, true, false, false );

        fixedUpdateSystemList.drawHeaderCallback = ( rect ) =>
        {
            EditorGUI.LabelField( rect, "Fixed Update Systems:" );
        };

        fixedUpdateSystemList.drawHeaderCallback = ( rect ) =>
        {
            EditorGUI.LabelField( rect, "Update Systems:" );
        };

        fixedUpdateSystemList.drawElementCallback = ( rect, index, isActive, isFocused ) =>
        {
            var label = egoInterface.baseFixedUpdateSystems[ index ].GetType().Name;
            if( label.EndsWith( "System" ) || label.EndsWith( "system" ) ) label = label.Substring( 0, label.Length - 6 );
            label = " " + label;

            egoInterface.baseFixedUpdateSystems[ index ].enabled = EditorGUI.ToggleLeft( rect, label, egoInterface.baseFixedUpdateSystems[ index ].enabled );
        };
        updateSystemList.drawElementCallback = ( rect, index, isActive, isFocused ) =>
        {
            var label = egoInterface.baseUpdateSystems[ index ].GetType().Name;
            if( label.EndsWith( "System" ) || label.EndsWith( "system" ) ) label = label.Substring( 0, label.Length - 6 );
            label = " " + label;

            egoInterface.baseUpdateSystems[ index ].enabled = EditorGUI.ToggleLeft( rect, label, egoInterface.baseUpdateSystems[ index ].enabled );
        };
    }
}