using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapYbot : MonoBehaviour
{
  private PoseManager _poseManager;
  
  public Transform shoulderRight;
  public Transform shoulderLeft;
  public Transform elbowRight;
  public Transform elbowLeft;
  private Vector3 shoulderRightBase = new Vector3(0, -180, -90f);
  private Vector3 shoulderLeftBase = new Vector3(0, 180, 90f);


  /*test line*/
  public Transform hipRight;
  public Transform hipLeft;
  public Transform kneeRight;
  public Transform kneeLeft;
  /*test line end*/

  // Start is called before the first frame update b
  void Start()
    {
      _poseManager=GameObject.Find("PoseManager").GetComponent<PoseManager>();
      
      //shoulderRight.localEulerAngles = new Vector3(0, -180, -90f);
    }

    // Update is called once per frame
    void Update()
    {
    if (_poseManager.hasPose())
      {
        var currentUpAngle = _poseManager.get3DAngle(
          PoseManager.pose.L_HIP,
          PoseManager.pose.L_SHOULDER,
          PoseManager.pose.L_ELBOW);
        var currentFrontAngle = _poseManager.get3DAngle(
          PoseManager.pose.R_SHOULDER,
          PoseManager.pose.L_SHOULDER,
          PoseManager.pose.L_ELBOW);
      shoulderLeft.localEulerAngles = shoulderLeftBase + new Vector3(0, -currentFrontAngle, -currentUpAngle);
        currentUpAngle = _poseManager.get3DAngle(
          PoseManager.pose.R_HIP,
          PoseManager.pose.R_SHOULDER,
          PoseManager.pose.R_ELBOW);
        currentFrontAngle = _poseManager.get3DAngle(
          PoseManager.pose.L_SHOULDER,
          PoseManager.pose.R_SHOULDER,
          PoseManager.pose.R_ELBOW);
        shoulderRight.localEulerAngles = shoulderRightBase + new Vector3(0, currentFrontAngle, currentUpAngle);
        currentUpAngle = _poseManager.get3DAngle(
          PoseManager.pose.L_SHOULDER,
          PoseManager.pose.L_ELBOW,
          PoseManager.pose.L_WRIST);
        /*
         * 1. Plane 1: Shoulder, Elbow and Wrist
         *    Calculate a Normal Vector with Elbow as the origin
         * 2. Plane 2: Shoulder, Elbow and Hip
         *    Calculate a Normal Vector with Elbow as the origin
         * 3. The angle between the Vectors matches the second rotation of the forearm
         */
        var shoulderPoint = _poseManager.getRawPoint(PoseManager.pose.L_SHOULDER);
        var elbowPoint = _poseManager.getRawPoint(PoseManager.pose.L_ELBOW);
        var wristPoint = _poseManager.getRawPoint(PoseManager.pose.L_WRIST);
        var hipPoint = _poseManager.getRawPoint(PoseManager.pose.L_HIP);
        var vector1 = shoulderPoint - elbowPoint;
        var vector2 = hipPoint - elbowPoint;
        var normalHip = Vector3.Cross(vector1,vector2);
        vector1 = shoulderPoint - elbowPoint;
        vector2 = wristPoint - elbowPoint;
        var normalForearm = Vector3.Cross(vector1,vector2);
        currentFrontAngle = Vector3.Angle(normalHip, normalForearm);
        elbowLeft.localEulerAngles = new Vector3(0,180,0) + new Vector3(-currentFrontAngle, -currentUpAngle, 0);
        currentUpAngle = _poseManager.get3DAngle(
          PoseManager.pose.R_SHOULDER,
          PoseManager.pose.R_ELBOW,
          PoseManager.pose.R_WRIST);
        shoulderPoint = _poseManager.getRawPoint(PoseManager.pose.R_SHOULDER);
        elbowPoint = _poseManager.getRawPoint(PoseManager.pose.R_ELBOW);
        wristPoint = _poseManager.getRawPoint(PoseManager.pose.R_WRIST);
        hipPoint = _poseManager.getRawPoint(PoseManager.pose.R_HIP);
        vector1 = shoulderPoint - elbowPoint;
        vector2 = hipPoint - elbowPoint;
        normalHip = Vector3.Cross(vector1,vector2);
        vector1 = shoulderPoint - elbowPoint;
        vector2 = wristPoint - elbowPoint;
        normalForearm = Vector3.Cross(vector1,vector2);
        currentFrontAngle = Vector3.Angle(normalHip, normalForearm);
        elbowRight.localEulerAngles = new Vector3(0,-180,0) + new Vector3(-currentFrontAngle, currentUpAngle, 0);


      /*test line*/

      /*
      currentUpAngle = _poseManager.get3DAngle(
        PoseManager.pose.L_KNEE,
        PoseManager.pose.L_HIP, 
        PoseManager.pose.L_SHOULDER);
      currentFrontAngle = _poseManager.get3DAngle(
        PoseManager.pose.R_HIP,
        PoseManager.pose.L_HIP,
        PoseManager.pose.L_SHOULDER);
      var currentSideAngle = _poseManager.get3DAngle(
        PoseManager.pose.R_HIP,
        PoseManager.pose.L_HIP,
        PoseManager.pose.L_KNEE);
      hipLeft.localEulerAngles = new Vector3(currentUpAngle, currentFrontAngle, currentSideAngle);
      Debug.Log(hipLeft.localEulerAngles);
      //hipLeft.localEulerAngles = new Vector3(0, currentFrontAngle, currentUpAngle);
      currentUpAngle = _poseManager.get3DAngle(
        PoseManager.pose.R_SHOULDER,
        PoseManager.pose.R_HIP,
        PoseManager.pose.R_KNEE);
      currentFrontAngle = _poseManager.get3DAngle(
        PoseManager.pose.L_HIP,
        PoseManager.pose.R_HIP,
        PoseManager.pose.R_KNEE);
      hipRight.localEulerAngles = new Vector3(0, currentFrontAngle, currentUpAngle);
      

      currentUpAngle = _poseManager.get3DAngle(
          PoseManager.pose.L_HIP,
          PoseManager.pose.L_KNEE,
          PoseManager.pose.L_ANKLE);
      
      var kneePoint = _poseManager.getRawPoint(PoseManager.pose.L_KNEE);
      var anklePoint = _poseManager.getRawPoint(PoseManager.pose.L_ANKLE);
      hipPoint = _poseManager.getRawPoint(PoseManager.pose.L_HIP);
      vector1 = hipPoint - kneePoint;
      vector2 = shoulderPoint - kneePoint;
      normalHip = Vector3.Cross(vector1, vector2);
      vector1 = hipPoint - kneePoint;
      vector2 = anklePoint - kneePoint;
      normalForearm = Vector3.Cross(vector1, vector2);
      currentFrontAngle = Vector3.Angle(normalHip, normalForearm);
      kneeLeft.localEulerAngles =new Vector3(-currentFrontAngle, -currentUpAngle, 0);

      currentUpAngle = _poseManager.get3DAngle(
          PoseManager.pose.R_HIP,
          PoseManager.pose.R_KNEE,
          PoseManager.pose.R_ANKLE);

      kneePoint = _poseManager.getRawPoint(PoseManager.pose.R_KNEE);
      anklePoint = _poseManager.getRawPoint(PoseManager.pose.R_ANKLE);
      hipPoint = _poseManager.getRawPoint(PoseManager.pose.R_HIP);
      vector1 = hipPoint - kneePoint;
      vector2 = shoulderPoint - kneePoint;
      normalHip = Vector3.Cross(vector1, vector2);
      vector1 = hipPoint - kneePoint;
      vector2 = anklePoint - kneePoint;
      normalForearm = Vector3.Cross(vector1, vector2);
      currentFrontAngle = Vector3.Angle(normalHip, normalForearm);
      kneeRight.localEulerAngles =new Vector3(-currentFrontAngle, -currentUpAngle, 0);
      */
      /*test line end*/
      //Debug.Log(currentUpAngle);
    }
  }
}
