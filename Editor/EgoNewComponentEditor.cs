using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class EgoNewComponentEditor : EditorWindow
{
    public string newComponentName = "";

    [ MenuItem( "Assets/Create/EgoCS/Component", false, 50 ) ]
    public static void NewComponentWindow()
    {
        // Get or make a new EgoNewComponentEditor and show it
        GetWindow<EgoNewComponentEditor>("EgoCS").Show();
    }

    void OnGUI()
    {
        // Draw New Component Menu
        EditorGUILayout.BeginVertical();
        {
            DrawSystemName();

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                DrawCreateComponentButton();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }
    
    void DrawSystemName()
    {
        EditorGUILayout.LabelField( "Component Name:" );
        newComponentName = EditorGUILayout.TextField( "", newComponentName );
    }

    void DrawCreateComponentButton()
    {
        if( GUILayout.Button( "Create Component" ) ||
           ( Event.current.type == EventType.Layout && Event.current.keyCode == KeyCode.Return ) )
        {
            if( newComponentName.Length > 0 )
            {
                CreateComponent();
                Close();
            }
        }
    }

    void CreateComponent()
    {
        // Read in EgoComponentTemplate
        var templatePath = Directory.GetFiles( Application.dataPath + "/", "Component.EgoTemplate", SearchOption.AllDirectories )[0];
        var templateStream = new StreamReader( templatePath );
        var templateStr = templateStream.ReadToEnd();

        // Put Component name in EgoComponentTemplate
        var componentScriptStr = templateStr.Replace( "_CLASS_NAME_", newComponentName );

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
        if( writeAttr  == FileAttributes.Directory )
        {
            fullWritePath = writePathInfo.ToString();
        }
        else
        {
            fullWritePath = writePathInfo.Directory.ToString();
        }

		fullWritePath += "/" + newComponentName + ".cs";
        File.WriteAllText( fullWritePath, componentScriptStr );

        AssetDatabase.Refresh();
    }
}
