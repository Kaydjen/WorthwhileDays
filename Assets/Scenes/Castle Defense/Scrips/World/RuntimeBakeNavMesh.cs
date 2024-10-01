using Unity.AI.Navigation;
using UnityEngine;

public class RuntimeBakeNavMesh : MonoBehaviour
{
    private NavMeshSurface _mesh;

    private void Awake()
    {
        if (!TryGetComponent<NavMeshSurface>(out _mesh))
            Debug.LogError($"{gameObject.name}, {this.GetType().Name}, the NavMeshSurface is empty");

        Invoke(nameof(BakeNavMesh), 2);
    }

    public void BakeNavMesh()
    {
        _mesh.BuildNavMesh();
    }
}
