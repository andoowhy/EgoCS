using UnityEditor;

[InitializeOnLoad]
public sealed class EgoEditor
{
    [MenuItem( "EgoCS/Generate Component IDs" )]
    private static void GenerateComponentIDs()
    {
        EgoGenerator.GenerateComponentIDs();
    }
}
