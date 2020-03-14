using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class EgoNewSystemEditor : EditorWindow
{
    string newSystemName = "";
    
    [ MenuItem( "Assets/Create/EgoCS/System", false, 51 ) ]
    public static void NewSystemWindow()
    {
        // Get or make a new EgoNewSystemEditor, and show it
        GetWindow<EgoNewSystemEditor>( "EgoCS" ).Show();
    }

    void OnGUI()
    {
        // Draw New System Menu
        EditorGUILayout.BeginVertical();
        {
            DrawSystemName();

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                DrawCreateSytemButton();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    void DrawSystemName()
    {
        GUILayout.Label( "System Name:" );
        newSystemName = GUILayout.TextField( newSystemName, EditorGUIUtility.GetBuiltinSkin( EditorSkin.Inspector ).textField );
    }

    void DrawCreateSytemButton()
    {
        if( GUILayout.Button( "Create System" ) ||
           ( Event.current.type == EventType.Layout && Event.current.keyCode == KeyCode.Return ) )
        {
            if( newSystemName.Length > 0 )
            {
                CreateSystem();
                Close();
            }
        }
    }

    void CreateSystem()
    {
        // Read in EgoSystemTemplate
        var templatePath = Directory.GetFiles( Application.dataPath + "/", "System.EgoTemplate", SearchOption.AllDirectories )[0];
        var templateStream = new StreamReader( templatePath );
        var templateStr = templateStream.ReadToEnd();

        // Put System name in EgoSystemTemplate
        var systemScriptStr = templateStr.Replace( "_CLASS_NAME_", newSystemName );

        // Write out
        var writePath = "Assets/";
        if( Selection.activeObject )
        {
            writePath = AssetDatabase.GetAssetPath( Selection.activeObject );
        }
        var writePathInfo = new FileInfo( writePath );

        //Check if write path is on directory or folder
        var fullWritePath = "";
        var writeAttr = File.GetAttributes( writePath );
        if( writeAttr == FileAttributes.Directory )
        {
            fullWritePath = writePathInfo.ToString();
        }
        else
        {
            fullWritePath = writePathInfo.Directory.ToString();
        }

        fullWritePath += "/" + newSystemName + ".cs";
        File.WriteAllText( fullWritePath, systemScriptStr );

        AssetDatabase.Refresh();
    }
}
