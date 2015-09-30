using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using System.IO;

public sealed class EgoGenerator
{
    public static void GenerateComponentIDs()
    {
        // Get all built-in and user created Component Types
        var componentTypes = new List<Type>();
        var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        foreach( var assembly in assemblies )
        {
            var types = assembly.GetTypes();
            foreach( var type in types )
            {
                if( type.IsSubclassOf( typeof( Component ) ) )
                {
                    componentTypes.Add( type );
                }
            }
        }
        var componentTypeStrings = new List<String>();
        foreach( var componentType in componentTypes )
        {
            componentTypeStrings.Add( componentType.Name );
        }
        componentTypeStrings.Sort();

        // Remove Hidden, Internal and Experimental Component Types
        componentTypeStrings.Remove( "Mask" );
        componentTypeStrings.Remove( "GameObserver" );
        componentTypeStrings.Remove( "NetworkScenePostProcess" );
        componentTypeStrings.Remove( "UserAuthorizationDialog" );
        componentTypeStrings.Remove( "DirectorPlayer" );
        componentTypeStrings.Remove( "DropdownItem" );

        // Build Mask Class file
        var maskClassContents = new StringBuilder();

        maskClassContents.AppendLine( "using System;" );
        maskClassContents.AppendLine( "using System.Collections.Generic;" );
        maskClassContents.AppendLine( "using UnityEngine;" );
        maskClassContents.AppendLine( "using UnityEngine.EventSystems;" );
        maskClassContents.AppendLine( "using UnityEngine.Networking;" );
        maskClassContents.AppendLine( "using UnityEngine.Networking.Match;" );
        maskClassContents.AppendLine( "using UnityEngine.UI;" );

        maskClassContents.AppendLine( "public static class ComponentIDs" );
        maskClassContents.AppendLine( "{" );
        maskClassContents.AppendLine( "\tpublic static readonly int size = " + componentTypeStrings.Count + ";" );
        maskClassContents.AppendLine( "\tpublic static readonly Dictionary<Type, int> types = new Dictionary<Type, int>();" );
        maskClassContents.AppendLine();
        maskClassContents.AppendLine( "\tstatic ComponentIDs()" );
        maskClassContents.AppendLine( "\t{" );
        for( var i = 0; i < componentTypeStrings.Count; i++ )
        {
            maskClassContents.Append( "\t\ttypes[typeof(" );
            maskClassContents.Append( componentTypeStrings[i] );
            maskClassContents.AppendLine( " )] = "+ i.ToString() + ";" );
        }
        maskClassContents.AppendLine( "\t}" );
        maskClassContents.AppendLine( "}" );
        maskClassContents.AppendLine();

        maskClassContents.AppendLine( "public static class ComponentIDs<C> where C : Component" );
        maskClassContents.AppendLine( "{" );
        maskClassContents.AppendLine( "\tprivate static int _ID;" );
        maskClassContents.AppendLine( "\tpublic static int ID { get { return _ID; } }" );

        maskClassContents.AppendLine( "\tstatic ComponentIDs()" );
        maskClassContents.AppendLine( "\t{" );
        for( var i = 0; i < componentTypeStrings.Count; i++ )
        {
            maskClassContents.Append( "\t\tComponentIDs<" );
            maskClassContents.Append( componentTypeStrings[i] );
            maskClassContents.Append( ">._ID = " );
            maskClassContents.Append( i.ToString() );
            maskClassContents.AppendLine( ";");
        }
        maskClassContents.AppendLine( "\t}" );

        maskClassContents.AppendLine( "}" );

        // Find the EgoCS folder
        var GUIDs = AssetDatabase.FindAssets("EgoEditor");
        var EgoEditorPath =  AssetDatabase.GUIDToAssetPath( GUIDs[0] );
        var EgoDirectoryInfo = ( new DirectoryInfo(EgoEditorPath) ).Parent.Parent;

        // Create Generated Directory, if it wasn't already
        // Delete previously generated files
        var generatedDirectory = EgoDirectoryInfo.CreateSubdirectory( "Generated" );
        foreach( var file in generatedDirectory.GetFiles() )
        {
            file.Delete();
        }

        // Write Mask Class file
        File.WriteAllText( generatedDirectory.ToString() + "/ComponentIDs.cs", maskClassContents.ToString() );

        AssetDatabase.Refresh();
    }
}
