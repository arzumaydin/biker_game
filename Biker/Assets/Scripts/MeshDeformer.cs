// Vertices: A vertex is a point in 3D space. Often abbreviated to “vert”.
// Lines / Edges: The invisible lines that connect vertices to one another.
// Triangles: Formed when edges connect three vertices.
// UV Map: Maps a material to an object, specifying how textures wrap around the object’s shape.
// Normals: The directional vector of a vertex or a surface. This characteristically points outward, perpendicular to the mesh surface, and helps determine how light bounces off of the object.
// Mesh: Holds all the vertices, edges, triangles, normals and UV data of a model.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    Mesh mesh;
    Vector3[] newVertices;
    int[] triangles;
    
    private void Start() {
        mesh = transform.GetComponent<MeshFilter>().mesh;
        newVertices = new Vector3[mesh.vertices.Length];
        newVertices = mesh.vertices;
        triangles = new int[mesh.triangles.Length];
        triangles = mesh.triangles;
    }
    public void DeformPlane(int index) 
    {
        /*
        for(int i = 0; i < newVertices.Length; i++) {
            var distance = (point - newVertices[i]).magnitude;
            if(distance < 2f) {
                var newVert = newVertices[i] + Vector3.down * 2f;
                newVertices[i] = newVert;
            }
        }
        */
        
        Vector3 p0 = newVertices[triangles[index * 3 + 0]];
        Vector3 p1 = newVertices[triangles[index * 3 + 1]];
        Vector3 p2 = newVertices[triangles[index * 3 + 2]];

        /*
        int j = 0;
        for(int i = 0; i < vertCount; i++) {
            if(oldVertices[i] != p0 && oldVertices[i] != p1 && oldVertices[i] != p2) {
                newVertices[j] = oldVertices[i];
            }
            else {
                newVertices[j] = oldVertices[i] + Vector3.down * 5f;
            }
        }*/

        newVertices[triangles[index * 3 + 0]] = p0 + Vector3.down * 5f;
        newVertices[triangles[index * 3 + 1]] = p1 + Vector3.down * 5f;
        newVertices[triangles[index * 3 + 2]] = p2 + Vector3.down * 5f;
        
        // Clears all the data that the mesh currently has
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }
}