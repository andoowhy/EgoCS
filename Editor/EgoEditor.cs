using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class EgoNewComponentEditor : EditorWindow
{
    public List<string> currentComponentNames = new List<string>();
    public string newComponentName = "";

    [ MenuItem( "Assets/Create/Ego Component", false, 50 ) ]
    public static void NewComponentWindow()
    {
        // Get or make a new EgoNewComponentEditor
        var window = GetWindow<EgoNewComponentEditor>("EgoCS");

        // Get all Component Type names
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach( var assembly in assemblies )
        {
            var types = assembly.GetTypes();
            foreach( var type in types )
            {
                if( type.IsSubclassOf( typeof( Component ) ) )
                {
                    window.currentComponentNames.Add( type.Name );
                }
            }
        }

        // Sort Component Names
        window.currentComponentNames.Sort();

        // Show Window
        window.Show();
    }

    void OnGUI()
    {
        // Draw New Component Menu
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField( "Name:" );
        newComponentName = EditorGUILayout.TextField( "", newComponentName );
        if( GUILayout.Button( "Create Component" ) && newComponentName.Length > 0 )
        {
            CreateComponent();
            Close();
        }
        EditorGUILayout.EndVertical();
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
        var fullWritePath = writePathInfo.Directory.ToString() + "\\" + newComponentName + ".cs";
        File.WriteAllText( fullWritePath, componentScriptStr );

        AssetDatabase.Refresh();
    }
}
