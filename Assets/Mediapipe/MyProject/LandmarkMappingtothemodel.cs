using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;

public class LandmarkMappingtothemodel : MonoBehaviour
{
    //public GameObject LeftArm, RightArm, Leftforearm, Rightforearm, Rightarm, Lefthand, Righthand, LeftThigh, RightFebula, LeftFebula, Rightfoot, Leftfoot;
    // doc: https://google.github.io/mediapipe/solutions/pose.html#pose_landmarks
    private static int NOSE = 0;
    public GameObject Nose;
    private static int L_EYE_INNER = 1;
    private static int L_EYE = 2;
    private static int L_EYE_OUTER = 3;
    private static int R_EYE_INNER = 4;
    private static int R_EYE = 5;
    private static int R_EYE_OUTER = 6;
    private static int L_EAR = 7;
    private static int R_EAR = 8;
    private static int L_MOUTH = 9;
    private static int R_MOUTH = 10;
    private static int L_SHOULDER = 11;
    public GameObject L_Shoulder;
    private static int R_SHOULDER = 12;
    public GameObject R_Shoulder;
    private static int L_ELBOW = 13;
    public GameObject L_Elbow;
    private static int R_ELBOW = 14;
    public GameObject R_Elbow;
    private static int L_WRIST = 15;
    public GameObject L_Wrist;
    private static int R_WRIST = 16;
    public GameObject R_Wrist;
    private static int L_PINKY = 17;
    private static int R_PINKY = 18;
    private static int L_INDEX = 19;
    private static int R_INDEX = 20;
    private static int L_THUMB = 21;
    private static int R_THUMB = 22;
    private static int L_HIP = 23;
    public GameObject L_Hip;
    private static int R_HIP = 24;
    public GameObject R_Hip;

    private float XrangeStart = -0.5f;
    private float XrangeEnd = 0.5f;
    private float YrangeStart = -0.5f;
    private float YrangeEnd = 0.5f;
    private float ZrangeStart = -0.5f;
    private float ZrangeEnd = 0.5f;
    private GameObject box;
    private Google.Protobuf.Collections.RepeatedField<Mediapipe.NormalizedLandmark> poseTrack = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (poseTrack == null) { return; }
        R_Wrist.transform.localPosition = PoseToWorld(poseTrack[R_WRIST]);
        R_Elbow.transform.localPosition = PoseToWorld(poseTrack[R_ELBOW]);
        R_Shoulder.transform.localPosition = PoseToWorld(poseTrack[R_SHOULDER]);
        Nose.transform.localPosition = PoseToWorld(poseTrack[NOSE]);
        L_Wrist.transform.localPosition = PoseToWorld(poseTrack[L_WRIST]);
        L_Elbow.transform.localPosition = PoseToWorld(poseTrack[L_ELBOW]);
        L_Shoulder.transform.localPosition = PoseToWorld(poseTrack[L_SHOULDER]);
        R_Hip.transform.localPosition = PoseToWorld(poseTrack[R_HIP]);
        L_Hip.transform.localPosition = PoseToWorld(poseTrack[L_HIP]);
    }

    public void setPose(Google.Protobuf.Collections.RepeatedField<Mediapipe.NormalizedLandmark> pose)
    {
        if (pose.Count == 0) { return; }
        Debug.Log($"{pose[L_WRIST]}{pose[L_ELBOW]}{pose[L_SHOULDER]}{pose[NOSE]}{pose[R_SHOULDER]}{pose[R_ELBOW]}{pose[R_WRIST]}");
        Debug.Log($"{PoseToWorld(pose[L_WRIST])}");
        poseTrack = pose;
        float smallest = pose[0].Z;
        int j = 0;
        int k = 0;
        float max = pose[0].Z;
        for (int i = 0; i < pose.Count; i++)
        {
            if ((new List<int> { 0, 11, 12, 13, 14, 15, 16, 23, 24 }).IndexOf(i) == -1) { continue; }
            if (pose[i].Z <= smallest)
            {
                k = j;
                j = i;
            }
            if (pose[i].Z > max)
            {
                max = pose[i].Z;
            }
        }
        Debug.Log($"smallest pose index is {j} ({pose[j].Z}) and {k} ({pose[k].Z}) max = {max} range={max-smallest}");
    }

    private Vector3 PoseToWorld(Mediapipe.NormalizedLandmark joint)
    {
        return new Vector3(
            (1 - joint.X) * (XrangeEnd - XrangeStart) + XrangeStart,
            (1 - joint.Y) * (YrangeEnd - YrangeStart) + YrangeStart,
            //(1-joint.Z)*(ZrangeEnd - ZrangeStart)+ZrangeStart
            joint.Z
        );
    }
}
