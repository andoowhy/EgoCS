using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class EgoNewEventEditor : EditorWindow
{
    public string newEventName = "";

    [MenuItem( "Assets/Create/EgoCS/Event", false, 52 )]
    public static void NewEventWindow()
    {
        // Get or make a new EgoNewEventEditor and show it
        GetWindow<EgoNewEventEditor>( "EgoCS" ).Show();
    }

    void OnGUI()
    {
        // Draw New Event Menu
        EditorGUILayout.BeginVertical();
        {
            DrawEventName();

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                DrawCreateEventButton();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    void DrawEventName()
    {
        EditorGUILayout.LabelField( "Event Name:" );
        newEventName = EditorGUILayout.TextField( "", newEventName );
    }

    void DrawCreateEventButton()
    {
        if( GUILayout.Button( "Create Event" ) ||
           ( Event.current.type == EventType.Layout && Event.current.keyCode == KeyCode.Return ) )
        {
            if( newEventName.Length > 0 )
            {
                CreateEvent();
                Close();
            }
        }
    }

    void CreateEvent()
    {
        // Read in EgoEventTemplate
        var templatePath = Directory.GetFiles( Application.dataPath + "/", "Event.EgoTemplate", SearchOption.AllDirectories )[0];
        var templateStream = new StreamReader( templatePath );
        var templateStr = templateStream.ReadToEnd();

        // Put Event name in EgoEventTemplate
        var eventScriptStr = templateStr.Replace( "_CLASS_NAME_", newEventName );

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

		fullWritePath += "/" + newEventName + ".cs";
        File.WriteAllText( fullWritePath, eventScriptStr );

        AssetDatabase.Refresh();
    }
}
