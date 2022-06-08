using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;

public class TrackerMappingScript : MonoBehaviour
{
    public readonly NormalizedLandmarkList PoseLandmarkList;
    public readonly Detection PoseDetection;

    [SerializeField] GameObject faceMeshGraph = null;
    [SerializeField] GameObject handTrackingGraph = null;
    [SerializeField] GameObject poseTrackingGraph = null;
    //  [SerializeField] GameObject holisticGraph = null;
    private Dictionary<string, GameObject> graphs;
    void Start()
    {
       // sceneDirector = GameObject.Find("SceneDirector");

      //  var graphSelector = GetComponent<Dropdown>();
      //  graphSelector.onValueChanged.AddListener(delegate { OnValueChanged(graphSelector); });
        InitializeOptions();
    }

    void InitializeOptions()
    {
        graphs = new Dictionary<string, GameObject>();

       
        AddGraph("Face Mesh", faceMeshGraph);
        
        AddGraph("Hand Tracking", handTrackingGraph);
        AddGraph("Pose Tracking", poseTrackingGraph);
        // AddGraph("Holistic", holisticGraph);
       
      //  HolisticValue.PoseLandmarks.Landmark
    }
    void AddGraph(string label, GameObject graph)
    {
        if (graph != null)
        {
            graphs.Add(label, graph);
        }
    }
    public TrackerMappingScript(NormalizedLandmarkList landmarkList, Detection detection)
    {
        PoseLandmarkList = landmarkList;
        PoseDetection = detection;
    }
    public TrackerMappingScript(NormalizedLandmarkList landmarkList) : this(landmarkList, new Detection()) { }

    public TrackerMappingScript() : this(new NormalizedLandmarkList()) { }
}
