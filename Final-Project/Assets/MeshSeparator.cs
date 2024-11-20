using UnityEngine;

public class MeshSeparator : MonoBehaviour
{
    public GameObject sourceObject; // The object to split into parts

    void Start()
    {
        // Get the MeshFilter and MeshRenderer components
        MeshFilter meshFilter = sourceObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = sourceObject.GetComponent<MeshRenderer>();

        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("Source object must have a MeshFilter with a valid sharedMesh.");
            return;
        }

        Mesh mesh = meshFilter.sharedMesh;

        if (mesh.subMeshCount <= 0)
        {
            Debug.LogError("The mesh has no sub-meshes to separate.");
            return;
        }

        // Process each sub-mesh
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            // Create a new GameObject for each sub-mesh
            GameObject part = new GameObject($"Part_{i}");
            part.transform.position = sourceObject.transform.position;
            part.transform.rotation = sourceObject.transform.rotation;
            part.transform.localScale = sourceObject.transform.localScale;

            // Add MeshFilter and MeshRenderer
            MeshFilter partMeshFilter = part.AddComponent<MeshFilter>();
            MeshRenderer partMeshRenderer = part.AddComponent<MeshRenderer>();

            // Create a new mesh for the sub-mesh
            Mesh partMesh = new Mesh();
            partMesh.vertices = mesh.vertices; // Copy vertices
            partMesh.normals = mesh.normals;   // Copy normals
            partMesh.uv = mesh.uv;            // Copy UVs
            partMesh.triangles = mesh.GetTriangles(i); // Copy triangles for this sub-mesh

            // Assign the mesh to the new GameObject
            partMeshFilter.mesh = partMesh;

            // Assign the material for this sub-mesh
            if (i < meshRenderer.materials.Length)
            {
                partMeshRenderer.material = meshRenderer.materials[i];
            }
            else
            {
                Debug.LogWarning($"No material found for sub-mesh {i}. Default material will be used.");
            }

            // Parent the new part to the original object's parent
            part.transform.SetParent(sourceObject.transform.parent);
        }

        // Deactivate the original source object
        sourceObject.SetActive(false);
    }
}
