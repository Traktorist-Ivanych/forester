using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;
using Zenject;

namespace VisualEffects
{
    public class ClickableObjectOutline : MonoBehaviour, IClickableOutline
    {
        [Inject] private readonly VisualEffectsConfig _visualEffectsConfig;

        private static readonly HashSet<Mesh> _registeredMeshes = new();

        [SerializeField] private List<MeshFilter> _outlineMeshFilters = new();

        [SerializeField, HideInInspector] private List<Mesh> _bakeKeys = new();
        [SerializeField, HideInInspector] private List<ListVector3> _bakeValues = new();
        [SerializeField] private List<Renderer> _outlineRenderers = new();

        [Serializable]
        private class ListVector3
        {
            public List<Vector3> data;
        }

        private bool _isOutlineEnable;

        private void OnValidate()
        {
            if (_outlineMeshFilters.Count == 0 && TryGetComponent(out MeshFilter mainMeshFilter))
            {
                _outlineMeshFilters.Add(mainMeshFilter);
            }

            _outlineRenderers.Clear();
            foreach (MeshFilter meshFilter in _outlineMeshFilters)
            {
                _outlineRenderers.Add(meshFilter.gameObject.GetComponent<MeshRenderer>());
            }

            if (_bakeKeys.Count != _bakeValues.Count)
            {
                _bakeKeys.Clear();
                _bakeValues.Clear();
            }

            if (_bakeKeys.Count == 0)
            {
                HashSet<Mesh> bakedMeshes = new();

                foreach (MeshFilter meshFilter in _outlineMeshFilters)
                {
                    if (!bakedMeshes.Add(meshFilter.sharedMesh))
                    {
                        continue;
                    }

                    List<Vector3> smoothNormals = SmoothNormals(meshFilter.sharedMesh);

                    _bakeKeys.Add(meshFilter.sharedMesh);
                    _bakeValues.Add(new ListVector3() { data = smoothNormals });
                }
            }
        }

        private void Awake()
        {
            LoadSmoothNormals();
        }

        public void EnableOutline()
        {
            if (!_isOutlineEnable)
            {
                AddOutlineMaterials();
            }
        }

        public void DisableOutline()
        {
            if (_isOutlineEnable)
            {
                RemoveOutlineMaterials();
            }
        }

        private void AddOutlineMaterials()
        {
            _isOutlineEnable = true;

            foreach (Renderer renderer in _outlineRenderers)
            {
                List<Material> materials = renderer.sharedMaterials.ToList();

                materials.Add(_visualEffectsConfig.ClickableOutlineMaskMaterial);
                materials.Add(_visualEffectsConfig.ClickableOutlineFillMaterial);

                renderer.materials = materials.ToArray();
            }
        }

        private void RemoveOutlineMaterials()
        {
            foreach (Renderer renderer in _outlineRenderers)
            {
                List<Material> materials = renderer.sharedMaterials.ToList();

                materials.Remove(_visualEffectsConfig.ClickableOutlineMaskMaterial);
                materials.Remove(_visualEffectsConfig.ClickableOutlineFillMaterial);

                renderer.materials = materials.ToArray();
            }

            _isOutlineEnable = false;
        }

        private void LoadSmoothNormals()
        {
            for (int i = 0; i < _outlineMeshFilters.Count; i++)
            {
                MeshFilter meshFilter = _outlineMeshFilters[i];

                if (!_registeredMeshes.Add(meshFilter.sharedMesh))
                {
                    continue;
                }

                int index = _bakeKeys.IndexOf(meshFilter.sharedMesh);
                List<Vector3> smoothNormals = (index >= 0) ? _bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

                meshFilter.sharedMesh.SetUVs(3, smoothNormals);

                Renderer renderer = _outlineRenderers[i];

                if (renderer != null)
                {
                    CombineSubmeshes(meshFilter.sharedMesh, renderer.sharedMaterials);
                }
            }

            //// Clear UV3 on skinned mesh renderers
            // foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            // {

            //     // Skip if UV3 has already been reset
            //     if (!_registeredMeshes.Add(skinnedMeshRenderer.sharedMesh))
            //     {
            //         continue;
            //     }

            //     // Clear UV3
            //     skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];

            //     // Combine submeshes
            //     CombineSubmeshes(skinnedMeshRenderer.sharedMesh, skinnedMeshRenderer.sharedMaterials);
            // }
        }

        private List<Vector3> SmoothNormals(Mesh mesh)
        {
            // Group vertices by location
            var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index)).GroupBy(pair => pair.Key);

            // Copy normals to a new list
            var smoothNormals = new List<Vector3>(mesh.normals);

            // Average normals for grouped vertices
            foreach (var group in groups)
            {

                // Skip single vertices
                if (group.Count() == 1)
                {
                    continue;
                }

                // Calculate the average normal
                var smoothNormal = Vector3.zero;

                foreach (var pair in group)
                {
                    smoothNormal += smoothNormals[pair.Value];
                }

                smoothNormal.Normalize();

                // Assign smooth normal to each vertex
                foreach (var pair in group)
                {
                    smoothNormals[pair.Value] = smoothNormal;
                }
            }

            return smoothNormals;
        }

        private void CombineSubmeshes(Mesh mesh, Material[] materials)
        {
            if (mesh.subMeshCount == 1)
            {
                return;
            }

            if (mesh.subMeshCount > materials.Length)
            {
                return;
            }

            mesh.subMeshCount++;
            mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
        }
    }
}
