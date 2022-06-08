using System;
using Google.Protobuf.Collections;
using Mediapipe;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class Pose
{
	// Token: 0x0600014C RID: 332 RVA: 0x000063FA File Offset: 0x000045FA
	public Vector3 MapMediapipePosition(NormalizedLandmark landmark)
	{
		return new Vector3(landmark.X * 640f, (1f - landmark.Y) * 480f, -320f * landmark.Z);
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000642C File Offset: 0x0000462C
	public Pose(RepeatedField<NormalizedLandmark> landmarks)
	{
		this.ShoulderLeft = this.MapMediapipePosition(landmarks[global::Pose.L_SHOULDER]);
		this.ElbowLeft = this.MapMediapipePosition(landmarks[global::Pose.L_ELBOW]);
		this.WristLeft = this.MapMediapipePosition(landmarks[global::Pose.L_WRIST]);
		this.ShoulderRight = this.MapMediapipePosition(landmarks[global::Pose.R_SHOULDER]);
		this.ElbowRight = this.MapMediapipePosition(landmarks[global::Pose.R_ELBOW]);
		this.WristRight = this.MapMediapipePosition(landmarks[global::Pose.R_WRIST]);
		this.IndexLeft = this.MapMediapipePosition(landmarks[global::Pose.L_INDEX]);
		this.IndexRight = this.MapMediapipePosition(landmarks[global::Pose.R_INDEX]);
		this.HipLeft = this.MapMediapipePosition(landmarks[global::Pose.L_HIP]);
		this.HipRight = this.MapMediapipePosition(landmarks[global::Pose.R_HIP]);
		this.KneeLeft = this.MapMediapipePosition(landmarks[global::Pose.L_KNEE]);
		this.KneeRight = this.MapMediapipePosition(landmarks[global::Pose.R_KNEE]);
		this.AnkleLeft = this.MapMediapipePosition(landmarks[global::Pose.L_ANKLE]);
		this.AnkleRight = this.MapMediapipePosition(landmarks[global::Pose.R_ANKLE]);
		this.HeelLeft = this.MapMediapipePosition(landmarks[global::Pose.L_HEEL]);
        this.HeelRight = this.MapMediapipePosition(landmarks[global::Pose.R_HEEL]);
	/*	GameObject Right0 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand");
        Right0.transform.position = new Vector3(this.WristRight.x, this.WristRight.y, this.WristRight.z);
		Debug.Log(Right0.transform.position); */
	}

	// Token: 0x0400013A RID: 314
	private static readonly int NOSE = 0;

	// Token: 0x0400013B RID: 315
	private static readonly int L_EYE_INNER = 1;

	// Token: 0x0400013C RID: 316
	private static readonly int L_EYE = 2;

	// Token: 0x0400013D RID: 317
	private static readonly int L_EYE_OUTER = 3;

	// Token: 0x0400013E RID: 318
	private static readonly int R_EYE_INNER = 4;

	// Token: 0x0400013F RID: 319
	private static readonly int R_EYE = 5;

	// Token: 0x04000140 RID: 320
	private static readonly int R_EYE_OUTER = 6;

	// Token: 0x04000141 RID: 321
	private static readonly int L_EAR = 7;

	// Token: 0x04000142 RID: 322
	private static readonly int R_EAR = 8;

	// Token: 0x04000143 RID: 323
	private static readonly int L_MOUTH = 9;

	// Token: 0x04000144 RID: 324
	private static readonly int R_MOUTH = 10;

	// Token: 0x04000145 RID: 325
	private static readonly int L_SHOULDER = 12;

	// Token: 0x04000146 RID: 326
	private static readonly int R_SHOULDER = 11;

	// Token: 0x04000147 RID: 327
	private static readonly int L_ELBOW = 14;

	// Token: 0x04000148 RID: 328
	private static readonly int R_ELBOW = 13;

	// Token: 0x04000149 RID: 329
	private static readonly int L_WRIST = 16;

	// Token: 0x0400014A RID: 330
	private static readonly int R_WRIST = 15;

	// Token: 0x0400014B RID: 331
	private static readonly int L_PINKY = 17;

	// Token: 0x0400014C RID: 332
	private static readonly int R_PINKY = 18;

	// Token: 0x0400014D RID: 333
	private static readonly int L_INDEX = 19;

	// Token: 0x0400014E RID: 334
	private static readonly int R_INDEX = 20;

	// Token: 0x0400014F RID: 335
	private static readonly int L_THUMB = 21;

	// Token: 0x04000150 RID: 336
	private static readonly int R_THUMB = 22;

	// Token: 0x04000151 RID: 337
	private static readonly int L_HIP = 24;

	// Token: 0x04000152 RID: 338
	private static readonly int R_HIP = 23;

	// Token: 0x04000153 RID: 339
	private static readonly int L_KNEE = 26;

	// Token: 0x04000154 RID: 340
	private static readonly int R_KNEE = 25;

	// Token: 0x04000155 RID: 341
	private static readonly int L_ANKLE = 28;

	// Token: 0x04000156 RID: 342
	private static readonly int R_ANKLE = 27;

	// Token: 0x04000157 RID: 343
	private static readonly int L_HEEL = 30;

	// Token: 0x04000158 RID: 344
	private static readonly int R_HEEL = 29;

	// Token: 0x04000159 RID: 345
	private static readonly int L_FINDEX = 32;

	// Token: 0x0400015A RID: 346
	private static readonly int R_FINDEX = 31;

	// Token: 0x0400015B RID: 347
	public Vector3 ShoulderLeft;

	// Token: 0x0400015C RID: 348
	public Vector3 ShoulderRight;

	// Token: 0x0400015D RID: 349
	public Vector3 ElbowLeft;

	// Token: 0x0400015E RID: 350
	public Vector3 ElbowRight;

	// Token: 0x0400015F RID: 351
	public Vector3 WristLeft;

	// Token: 0x04000160 RID: 352
	public Vector3 WristRight;

	// Token: 0x04000161 RID: 353
	public Vector3 IndexLeft;

	// Token: 0x04000162 RID: 354
	public Vector3 IndexRight;

	// Token: 0x04000163 RID: 355
	public Vector3 HipLeft;

	// Token: 0x04000164 RID: 356
	public Vector3 HipRight;

	// Token: 0x04000165 RID: 357
	public Vector3 KneeLeft;

	// Token: 0x04000166 RID: 358
	public Vector3 KneeRight;

	// Token: 0x04000167 RID: 359
	public Vector3 AnkleLeft;

	// Token: 0x04000168 RID: 360
	public Vector3 AnkleRight;

	// Token: 0x04000169 RID: 361
	public Vector3 HeelLeft;

	// Token: 0x0400016A RID: 362
	public Vector3 HeelRight;
}
