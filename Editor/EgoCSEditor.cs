namespace EgoCS
{ 
    using UnityEngine;
    using UnityEditor;
    using UnityEditorInternal;

    [CustomEditor( typeof( EgoCS ), true )]
    public class EgoCSEditor : Editor
    {
        private ReorderableList fixedUpdateSystemList;
        private ReorderableList updateSystemList;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if( !Application.isPlaying ) { return; }

            var egoInterface = target as EgoCS;
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

        private void InitLists( EgoCS egoCS )
        {
            fixedUpdateSystemList = new ReorderableList( egoCS.baseFixedUpdateSystems, typeof( System ), false, true, false, false );
            updateSystemList = new ReorderableList( egoCS.baseUpdateSystems, typeof( System ), false, true, false, false );

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
                var label = egoCS.baseFixedUpdateSystems[ index ].GetType().Name;
                if( label.EndsWith( "System" ) || label.EndsWith( "system" ) ) label = label.Substring( 0, label.Length - 6 );
                label = " " + label;

                egoCS.baseFixedUpdateSystems[ index ].enabled = EditorGUI.ToggleLeft( rect, label, egoCS.baseFixedUpdateSystems[ index ].enabled );
            };
            updateSystemList.drawElementCallback = ( rect, index, isActive, isFocused ) =>
            {
                var label = egoCS.baseUpdateSystems[ index ].GetType().Name;
                if( label.EndsWith( "System" ) || label.EndsWith( "system" ) ) label = label.Substring( 0, label.Length - 6 );
                label = " " + label;

                egoCS.baseUpdateSystems[ index ].enabled = EditorGUI.ToggleLeft( rect, label, egoCS.baseUpdateSystems[ index ].enabled );
            };
        }
    }
}