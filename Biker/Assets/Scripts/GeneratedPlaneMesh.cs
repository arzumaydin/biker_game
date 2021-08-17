using System.Collections;
using System.Collections.Generic;
using UnityEngine; 


// Require object to have a mesh filter attached to it
[RequireComponent(typeof(MeshFilter))]
public class GeneratedPlaneMesh : MonoBehaviour
{
    public float size = 4;
    public int gridSize = 16; // number of grids on the plane

    private MeshFilter filter; // reach mesh filter
    public Mesh mesh {
        get { return filter.mesh; } // getter function for the mesh to accesh the mesh from outside
    }

    // Start is called before the first frame update
    void Awake()
    {
        filter = GetComponent<MeshFilter>(); // reach mesh filter attached to the object
        filter.mesh = GenerateMesh(); // mesh attached to the filter is created via generate mesh function
        SendMessage("MeshRegenerated");
    }

    Mesh GenerateMesh() {
        Mesh mesh = new Mesh();

        // when we add quads side by side some of the vertices will overlap

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>(); 

        for(int x = 0; x < gridSize + 1; ++x) { // for loops here loop through the x and y

            for(int y = 0; y < gridSize + 1; ++y) {
                // calculate x and y position of the grid
                vertices.Add(new Vector3(-size * 0.5f + (size * (x / (float)gridSize)), 0, -size * 0.5f + (size * (y / (float)gridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)gridSize, y / (float)gridSize));
            }
        }

        var triangles = new List<int>();

        // gridSize * gridSize is the total number of vertices
        // so there will be gridSize * gridSize / 2 number of triangles

        var vertCount = gridSize + 1;
        for(int i = 0; i < vertCount * vertCount - vertCount; ++i) { // assign triangles for the new mesh
            // 0, 1, 3
            // 1, 2, 3
            if((i+1) % vertCount == 0) { // don't generate triangles for the last grid
                continue;
            }
            triangles.AddRange(new List<int>() { 
                i + vertCount + 1, i + vertCount, i + 0,
                i + 0, i + 1, i + vertCount + 1
            });
        }

        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0); // SetTriangles gets two argument: a list of integers and the number of submeshes

        // for single quad only
        /*
        mesh.SetVertices(new List<Vector3>() { // set four new vertices for the quad being generated
            new Vector3(-size * 0.5f, 0f, -size * 0.5f), // bottom left corner
            new Vector3(-size * 0.5f, 0f, size * 0.5f), // top left corner
            new Vector3(size * 0.5f, 0f, size * 0.5f), // top right corner
            new Vector3(size * 0.5f, 0f, -size * 0.5f) // bottom right corner
        });

        mesh.SetTriangles(new List<int>() { // set two triangles that make up the quad
            0, 1, 3, // upper triangle
            1, 2, 3 // lower triangle
        }, 0);

        mesh.SetNormals(new List<Vector3>() { // set normals for the vertices to upside, quad will face upwards

            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up

        });

        // uvs are coordinates on the mesh that indicates where the mesh is
        mesh.SetUVs(0, new List<Vector2>() { // should be the same direction that we set for vertices
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(1,1),
            new Vector2(0,1)
        });*/
        return mesh;
    }
}
