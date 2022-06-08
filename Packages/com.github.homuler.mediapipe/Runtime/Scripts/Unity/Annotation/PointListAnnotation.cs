// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections.Generic;
using UnityEngine;

using mplt = Mediapipe.LocationData.Types;

namespace Mediapipe.Unity
{
#pragma warning disable IDE0065
  using Color = UnityEngine.Color;
#pragma warning restore IDE0065

  public class PointListAnnotation : ListAnnotation<PointAnnotation>
  {
    [SerializeField] private Color _color = Color.green;
    [SerializeField] private float _radius = 15.0f;

    private MeshFilter meshFilter; // オブジェクトのMeshFilter
    private Mesh faceMesh; // オブジェクトのMesh
    private List<Vector3> vertextList = new List<Vector3>(); // Meshの頂点の座標リスト
    private readonly List<int> Except = new List<int>{10,
21,
54,
58,
67,
93,
103,
109,
127,
132,
136,
148,
149,
150,
152,
162,
172,
176,
234,
251,
284,
288,
297,
323,
332,
338,
356,
361,
365,
377,
378,
379,
389,
397,
400,
454 };

    private void Awake()
    {
      Debug.Log("SADAD");
      meshFilter = GameObject.Find("default").GetComponent<MeshFilter>(); // defaultオブジェクトからMeshFilterを取得
      faceMesh = meshFilter.mesh; // Meshをセット
      vertextList.AddRange(faceMesh.vertices); // Meshから頂点座標リストを取得
    }
    private void OnValidate()
    {
      ApplyColor(_color);
      ApplyRadius(_radius);
    }

    public void SetColor(Color color)
    {
      _color = color;
      ApplyColor(_color);
    }

    public void SetRadius(float radius)
    {
      _radius = radius;
      ApplyRadius(_radius);
    }

    public void Draw(IList<Vector3> targets)
    {
      if (ActivateFor(targets))
      {
        CallActionForAll(targets, (annotation, target) =>
        {
          if (annotation != null) { annotation.Draw(target); }
        });
      }
    }

    public void Draw(IList<Landmark> targets, Vector3 scale, bool visualizeZ = true)
    {
      if (ActivateFor(targets))
      {
        CallActionForAll(targets, (annotation, target) =>
        {
          if (annotation != null) { annotation.Draw(target, scale, visualizeZ); }
        });
      }
    }

    public void Draw(LandmarkList targets, Vector3 scale, bool visualizeZ = true)
    {
      Draw(targets.Landmark, scale, visualizeZ);
    }

    public void Draw(IList<NormalizedLandmark> targets, bool visualizeZ = true)
    {
      if (ActivateFor(targets))
      {
        CallActionForAll(targets, (annotation, target) =>
        {
          if (annotation != null) { annotation.Draw(target, visualizeZ); }
          if (targets.Count>=460)
          {
          UpdateFaceMesh(targets);
          }
        });
      }
    }


    private int meshScale = -5; // サイズ調整用の変数
    private void UpdateFaceMesh(IList<NormalizedLandmark> landmarkList)
    {
      if(null == meshFilter)
      {
        //FindMesh();
      }

      // 顔の頂点分だけ実行（478 - 10 = 468）
      for (var i = 0; i < landmarkList.Count; i++)
      {
        var landmark = landmarkList[i];
        // 検出したLandmarkをMeshの頂点座標にセット
        if (Except.Contains(i))
        {
          vertextList[i] = new Vector3(meshScale * landmark.X*0.9f, meshScale * landmark.Y*0.9f, meshScale * landmark.Z * 0.9f);
        }
        else
          vertextList[i] = new Vector3(meshScale * landmark.X, meshScale * landmark.Y, meshScale * landmark.Z);
      }
      // 座標リストをMeshに適用
      faceMesh.SetVertices(vertextList);
    }
    public void Draw(NormalizedLandmarkList targets, bool visualizeZ = true)
    {
      Draw(targets.Landmark, visualizeZ);
    }

    public void Draw(IList<AnnotatedKeyPoint> targets, Vector2 focalLength, Vector2 principalPoint, float zScale, bool visualizeZ = true)
    {
      if (ActivateFor(targets))
      {
        CallActionForAll(targets, (annotation, target) =>
        {
          if (annotation != null) { annotation.Draw(target, focalLength, principalPoint, zScale, visualizeZ); }
        });
      }
    }

    public void Draw(IList<mplt.RelativeKeypoint> targets, float threshold = 0.0f)
    {
      if (ActivateFor(targets))
      {
        CallActionForAll(targets, (annotation, target) =>
        {
          if (annotation != null) { annotation.Draw(target, threshold); }
        });
      }
    }

    protected override PointAnnotation InstantiateChild(bool isActive = true)
    {
      var annotation = base.InstantiateChild(isActive);
      annotation.SetColor(_color);
      annotation.SetRadius(_radius);
      return annotation;
    }

    private void ApplyColor(Color color)
    {
      foreach (var point in children)
      {
        if (point != null) { point.SetColor(color); }
      }
    }

    private void ApplyRadius(float radius)
    {
      foreach (var point in children)
      {
        if (point != null) { point.SetRadius(radius); }
      }
    }
  }
}
