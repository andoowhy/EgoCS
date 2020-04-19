using System.IO;
using UnityEngine;
using UnityEditor;

namespace EgoCS
{
    public class NewEgoCSSystemEditor : EditorWindow
    {
        private enum SystemType
        {
            Start,
            Update,
            FixedUpdate
        }

        private string newSystemName = "";
        private SystemType systemType = SystemType.Update;
        private MonoScript egoCSMonoScript;
        private Constraint[] rootConstraints;

        [MenuItem( "Assets/Create/EgoCS/System", false, 51 )]
        public static void NewSystemWindow()
        {
            // Get or make a new NewEgoCSSystemEditor, and show it
            GetWindow< NewEgoCSSystemEditor >( "EgoCS" ).Show();
        }

        private void OnGUI()
        {
            using( new EditorGUI.DisabledScope( EditorApplication.isCompiling || EditorApplication.isPlaying ) )
            {
                // Draw New System Menu
                using( new EditorGUILayout.VerticalScope() )
                {
                    systemType = ( SystemType ) EditorGUILayout.EnumPopup( systemType );

                    egoCSMonoScript = EditorGUILayout.ObjectField( "EgoCS", egoCSMonoScript, typeof( MonoScript ), false ) as MonoScript;

                    OnGUI_SystemName();

                    using( new EditorGUILayout.HorizontalScope() )
                    {
                        GUILayout.FlexibleSpace();

                        OnGUI_CreateSystemButton();
                    }
                }
            }
        }

        private void OnGUI_SystemName()
        {
            GUILayout.Label( "System Name:" );
            newSystemName = GUILayout.TextField( newSystemName, EditorGUIUtility.GetBuiltinSkin( EditorSkin.Inspector ).textField );
        }

        private void OnGUI_CreateSystemButton()
        {
            using( new EditorGUI.DisabledScope( newSystemName.Length <= 0 || egoCSMonoScript == null ) )
            {
                if( GUILayout.Button( "Create System" ) || ( Event.current.type == EventType.Layout && Event.current.keyCode == KeyCode.Return ) )
                {
                    CreateSystem();
                    Close();
                }
            }
        }

        private void CreateSystem()
        {
            // Read in EgoSystemTemplate
            var templatePath = Directory.GetFiles( Application.dataPath + "/", "System.EgoTemplate", SearchOption.AllDirectories )[ 0 ];
            var templateStr = File.ReadAllText( templatePath );

            // Put System name in EgoSystemTemplate
            var systemScriptStr = templateStr
                .Replace( "__CLASS_NAME__", newSystemName )
                .Replace( "__EGOCS_TYPE__", egoCSMonoScript.GetClass().Name )
                .Replace( "__SYSTEM_TYPE__", systemType.ToString() );

            // Write out
            var writePath = ( Selection.activeObject ) ? AssetDatabase.GetAssetPath( Selection.activeObject ) : "Assets/";

            var writePathInfo = new FileInfo( writePath );

            //Check if write path is on directory or folder
            var fullWritePath = ( File.GetAttributes( writePath ) == FileAttributes.Directory )
                ? writePathInfo.ToString()
                : writePathInfo.Directory.ToString();

            fullWritePath += "/" + newSystemName + systemType + "System.cs";

            File.WriteAllText( fullWritePath, systemScriptStr );

            AssetDatabase.Refresh();
        }
    }
}