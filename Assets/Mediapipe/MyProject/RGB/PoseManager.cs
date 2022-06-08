using System;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using Mediapipe;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class PoseManager : MonoBehaviour
{
	// Token: 0x0600014F RID: 335 RVA: 0x0000669C File Offset: 0x0000489C
	private void Start()
	{
		this.angleCalculators = new List<AngleCalculator>();
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("AngleCalculator"))
		{
			this.angleCalculators.Add(gameObject.GetComponent<AngleCalculator>());
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x000066E2 File Offset: 0x000048E2
	private void Update()
	{
		 
	}

	// Token: 0x06000151 RID: 337 RVA: 0x000066E4 File Offset: 0x000048E4
	public bool hasPose()
	{
		return this.pose != null;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x000066F0 File Offset: 0x000048F0
	public void SetPose(RepeatedField<NormalizedLandmark> landmarks)
	{
		if (landmarks.Count == 0)
		{
			return;
		}
		this.rawPose = landmarks;
		this.pose = new global::Pose(landmarks);
		if (this.moveAvatar)
		{
			this.moveAvatar.MoveUpperJoints();
			this.moveAvatar.MoveLowerJoints();
		}
		foreach (AngleCalculator angleCalculator in this.angleCalculators)
		{
			angleCalculator.UpdateCalculation();
		}
		//if (this.jigsaw.isActiveAndEnabled)
		//{
		//	this.jigsaw.UpdateHand(this.pose.IndexLeft, this.pose.IndexRight);
		//}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000067B0 File Offset: 0x000049B0
	public global::Pose GetPose()
	{
		return this.pose;
	}

	// Token: 0x0400016B RID: 363
	private global::Pose pose;

	// Token: 0x0400016C RID: 364
	public RepeatedField<NormalizedLandmark> rawPose;

	// Token: 0x0400016D RID: 365
	private bool calibrated;

	// Token: 0x0400016E RID: 366
	private float forearmSize;

	// Token: 0x0400016F RID: 367
	private float armSize;

	// Token: 0x04000170 RID: 368
	public MoveAvatar moveAvatar;

	// Token: 0x04000171 RID: 369
	private List<AngleCalculator> angleCalculators;

	// Token: 0x04000172 RID: 370
	//public grabPiece jigsaw;
}
