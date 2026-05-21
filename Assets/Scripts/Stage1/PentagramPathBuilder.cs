using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 오각형(5) + 오각별(5) = 총 10개 경로를 자동 생성한다.
/// </summary>
public class PentagramPathBuilder : MonoBehaviour
{
    private static readonly int[,] EdgePairs =
    {
        { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 0 },
        { 0, 2 }, { 2, 4 }, { 4, 1 }, { 1, 3 }, { 3, 0 }
    };

    [SerializeField] private Transform pathsRoot;
    [SerializeField] private Color inactiveColor = new Color(0.45f, 0.42f, 0.5f, 0.55f);
    [SerializeField] private Color activeColor = new Color(0.92f, 0.12f, 0.1f, 1f);
    [SerializeField] private float pathWidth = 0.1f;

    [ContextMenu("Build Pentagram Paths")]
    public void BuildPaths()
    {
        var runeByIndex = CollectRunesByIndex();
        if (runeByIndex.Count != 5)
        {
            Debug.LogWarning($"PentagramPathBuilder: 룬 5개가 필요합니다. 현재 {runeByIndex.Count}개.");
            return;
        }

        EnsurePathsRoot();
        ClearExistingPaths();

        var edges = new List<RunePathEdge>();

        for (int i = 0; i < EdgePairs.GetLength(0); i++)
        {
            int a = EdgePairs[i, 0];
            int b = EdgePairs[i, 1];

            var pathObject = new GameObject($"Path_{a}_{b}");
            pathObject.transform.SetParent(pathsRoot, false);

            var line = pathObject.AddComponent<LineRenderer>();
            line.useWorldSpace = true;
            line.positionCount = 2;
            line.startWidth = pathWidth;
            line.endWidth = pathWidth;
            line.sortingOrder = 1;
            line.material = new Material(Shader.Find("Sprites/Default"));

            var edge = pathObject.AddComponent<RunePathEdge>();
            edge.Configure(
                a,
                b,
                runeByIndex[a].WorldPosition,
                runeByIndex[b].WorldPosition);

            edges.Add(edge);
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        if (pathsRoot != null)
            UnityEditor.EditorUtility.SetDirty(pathsRoot.gameObject);
#endif

        Debug.Log($"PentagramPathBuilder: 경로 {edges.Count}개 생성 완료.");
    }

    [ContextMenu("Refresh Path Positions")]
    public void RefreshPathPositions()
    {
        var runeByIndex = CollectRunesByIndex();
        var edges = pathsRoot != null
            ? pathsRoot.GetComponentsInChildren<RunePathEdge>()
            : GetComponentsInChildren<RunePathEdge>();

        foreach (var edge in edges)
        {
            if (!runeByIndex.TryGetValue(edge.RuneIndexA, out var runeA)
                || !runeByIndex.TryGetValue(edge.RuneIndexB, out var runeB))
                continue;

            edge.SetPositions(runeA.WorldPosition, runeB.WorldPosition);
        }
    }

    public RunePathEdge[] GetPathEdges()
    {
        return pathsRoot != null
            ? pathsRoot.GetComponentsInChildren<RunePathEdge>()
            : GetComponentsInChildren<RunePathEdge>();
    }

    private Dictionary<int, RuneNode> CollectRunesByIndex()
    {
        var map = new Dictionary<int, RuneNode>();
        foreach (var rune in GetComponentsInChildren<RuneNode>())
            map[rune.RuneIndex] = rune;
        return map;
    }

    private void EnsurePathsRoot()
    {
        if (pathsRoot != null)
            return;

        var existing = transform.Find("Paths");
        if (existing != null)
        {
            pathsRoot = existing;
            return;
        }

        var rootObject = new GameObject("Paths");
        rootObject.transform.SetParent(transform, false);
        pathsRoot = rootObject.transform;
    }

    private void ClearExistingPaths()
    {
        EnsurePathsRoot();

        for (int i = pathsRoot.childCount - 1; i >= 0; i--)
        {
            var child = pathsRoot.GetChild(i).gameObject;
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(child);
            else
#endif
                Destroy(child);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PentagramPathBuilder))]
public class PentagramPathBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var builder = (PentagramPathBuilder)target;

        if (GUILayout.Button("Build Pentagram Paths"))
            builder.BuildPaths();

        if (GUILayout.Button("Refresh Path Positions"))
            builder.RefreshPathPositions();
    }
}
#endif
