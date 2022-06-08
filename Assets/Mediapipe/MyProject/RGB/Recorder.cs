using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000036 RID: 54
//[RequireComponent(typeof(NetworkManagement))]
public class Recorder : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600015E RID: 350 RVA: 0x000068CC File Offset: 0x00004ACC
	public string Device
	{
		get
		{
			if (this.device == Recorder.Devices.Kinect)
			{
				return "Kinect";
			}
			if (this.device == Recorder.Devices.MediaPipe)
			{
				return "MediaPipe";
			}
			if (this.device == Recorder.Devices.BSN)
			{
				return "BSN";
			}
			return "none";
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600015F RID: 351 RVA: 0x00006900 File Offset: 0x00004B00
	// (set) Token: 0x06000160 RID: 352 RVA: 0x00006908 File Offset: 0x00004B08
	public int SessionDuration { get; private set; }

	// Token: 0x06000161 RID: 353 RVA: 0x00006911 File Offset: 0x00004B11
	private void Awake()
	{
		QualitySettings.vSyncCount = 0;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00006919 File Offset: 0x00004B19
	private void Start()
	{
		this.registerList = new List<Register>();
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00006928 File Offset: 0x00004B28
	private void Update()
	{
		if (this.finalizado)
		{
			return;
		}
		Register register = new Register();
		register.SetArticulations(this.articulations.ToArray());
		if (this.articulations.Contains(1) && this.hipCenter != null)
		{
			register.SetArticulationCoordinates(1, this.hipCenter.rotation.eulerAngles);
		}
		if (this.articulations.Contains(2) && this.spine != null)
		{
			register.SetArticulationCoordinates(2, this.spine.rotation.eulerAngles);
		}
		if (this.articulations.Contains(3) && this.shoulderCenter != null)
		{
			register.SetArticulationCoordinates(3, this.shoulderCenter.rotation.eulerAngles);
		}
		if (this.articulations.Contains(4) && this.head != null)
		{
			register.SetArticulationCoordinates(4, this.head.rotation.eulerAngles);
		}
		if (this.articulations.Contains(5) && this.leftShoulder != null)
		{
			register.SetArticulationCoordinates(5, this.leftShoulder.rotation.eulerAngles);
		}
		if (this.articulations.Contains(6) && this.leftElbow != null)
		{
			register.SetArticulationCoordinates(6, this.leftElbow.rotation.eulerAngles);
		}
		if (this.articulations.Contains(7) && this.leftWrist != null)
		{
			register.SetArticulationCoordinates(7, this.leftWrist.rotation.eulerAngles);
		}
		if (this.articulations.Contains(8) && this.leftHand != null)
		{
			register.SetArticulationCoordinates(8, this.leftHand.rotation.eulerAngles);
		}
		if (this.articulations.Contains(9) && this.rightShoulder != null)
		{
			register.SetArticulationCoordinates(9, this.rightShoulder.rotation.eulerAngles);
		}
		if (this.articulations.Contains(10) && this.rightElbow != null)
		{
			register.SetArticulationCoordinates(10, this.rightElbow.rotation.eulerAngles);
		}
		if (this.articulations.Contains(11) && this.rightWrist != null)
		{
			register.SetArticulationCoordinates(11, this.rightWrist.rotation.eulerAngles);
		}
		if (this.articulations.Contains(12) && this.rightHand != null)
		{
			register.SetArticulationCoordinates(12, this.rightHand.rotation.eulerAngles);
		}
		if (this.articulations.Contains(13) && this.leftHip != null)
		{
			register.SetArticulationCoordinates(13, this.leftHip.rotation.eulerAngles);
		}
		if (this.articulations.Contains(14) && this.leftKnee != null)
		{
			register.SetArticulationCoordinates(14, this.leftKnee.rotation.eulerAngles);
		}
		if (this.articulations.Contains(15) && this.leftAnkle != null)
		{
			register.SetArticulationCoordinates(15, this.leftAnkle.rotation.eulerAngles);
		}
		if (this.articulations.Contains(16) && this.leftFoot != null)
		{
			register.SetArticulationCoordinates(16, this.leftFoot.rotation.eulerAngles);
		}
		if (this.articulations.Contains(17) && this.rightHip != null)
		{
			register.SetArticulationCoordinates(17, this.rightHip.rotation.eulerAngles);
		}
		if (this.articulations.Contains(18) && this.rightKnee != null)
		{
			register.SetArticulationCoordinates(18, this.rightKnee.rotation.eulerAngles);
		}
		if (this.articulations.Contains(19) && this.rightAnkle != null)
		{
			register.SetArticulationCoordinates(19, this.rightAnkle.rotation.eulerAngles);
		}
		if (this.articulations.Contains(20) && this.rightFoot != null)
		{
			register.SetArticulationCoordinates(20, this.rightFoot.rotation.eulerAngles);
		}
		this.registerList.Add(register);
		this.timeCounter += Time.deltaTime;
		this.registerCount++;
		if (this.timeCounter >= 1f)
		{
			this.registerCounts.Add(this.registerCount);
			this.timeCounter = 0f;
			this.registerCount = 0;
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00006E04 File Offset: 0x00005004
	public void StartRecording(string articulationString)
	{
		this.init = Time.time;
		this.articulations = new List<int>(this.GetArticulationIndexes(articulationString));
		this.registerCounts = new List<int>();
		Debug.Log("Recording...");
		this.registerList.Clear();
		this.finalizado = false;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00006E58 File Offset: 0x00005058
	public string FinishRecording()
	{
		this.finalizado = true;
		this.SessionDuration = (int)(Time.time - this.init);
		List<List<Register>> list = new List<List<Register>>();
		foreach (int count in this.registerCounts)
		{
			list.Add(this.registerList.GetRange(0, count));
			this.registerList.RemoveRange(0, count);
		}
		foreach (List<Register> list2 in list)
		{
			this.registerList.AddRange(ResamplingTest.ResampleRegisterList(list2, 30));
		}
		string text = "\"registers\":[";
		foreach (Register register in this.registerList)
		{
			text = text + "\"" + register.ToString() + "\",";
		}
		text = text.TrimEnd(new char[]
		{
			','
		}) + "]";
		Debug.Log("Finalizing...");
		return text;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00006FB4 File Offset: 0x000051B4
	private int[] GetArticulationIndexes(string artIndexPattern)
	{
		string[] array = artIndexPattern.Split(new char[]
		{
			';'
		});
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = int.Parse(array[i].Trim(new char[]
			{
				'a'
			}));
		}
		Debug.Log(array2);
		return array2;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000700C File Offset: 0x0000520C
	public void FindJoints()
	{
		foreach (Transform transform in base.GetComponentsInChildren<Transform>())
		{
			if (this.Contains(transform.name, "hipscenter,hips") && this.hipCenter == null)
			{
				this.hipCenter = transform;
			}
			else if (this.Contains(transform.name, "spine") && this.spine == null)
			{
				this.spine = transform;
			}
			else if (this.Contains(transform.name, "shouldercenter,neck") && this.shoulderCenter == null)
			{
				this.shoulderCenter = transform;
			}
			else if (this.Contains(transform.name, "head") && this.head == null)
			{
				this.head = transform;
			}
			else if (this.Contains(transform.name, "leftarm") && this.leftShoulder == null)
			{
				this.leftShoulder = transform;
			}
			else if (this.Contains(transform.name, "leftforearm") && this.leftElbow == null)
			{
				this.leftElbow = transform;
			}
			else if (this.Contains(transform.name, "lefthand") && this.leftWrist == null)
			{
				this.leftWrist = transform;
			}
			else if (this.Contains(transform.name, "lefthandmiddle") && this.leftHand == null)
			{
				this.leftHand = transform;
			}
			else if (this.Contains(transform.name, "rightarm") && this.rightShoulder == null)
			{
				this.rightShoulder = transform;
			}
			else if (this.Contains(transform.name, "rightforearm") && this.rightElbow == null)
			{
				this.rightElbow = transform;
			}
			else if (this.Contains(transform.name, "righthand") && this.rightWrist == null)
			{
				this.rightWrist = transform;
			}
			else if (this.Contains(transform.name, "righthandmiddle") && this.rightHand == null)
			{
				this.rightHand = transform;
			}
			else if (this.Contains(transform.name, "leftupleg,lefthip") && this.leftHip == null)
			{
				this.leftHip = transform;
			}
			else if (this.Contains(transform.name, "leftleg,leftknee") && this.leftKnee == null)
			{
				this.leftKnee = transform;
			}
			else if (this.Contains(transform.name, "leftankle,leftfoot") && this.leftAnkle == null)
			{
				this.leftAnkle = transform;
			}
			else if (this.Contains(transform.name, "lefttoebase") && this.leftFoot == null)
			{
				this.leftFoot = transform;
			}
			else if (this.Contains(transform.name, "rightupleg,righthip") && this.rightHip == null)
			{
				this.rightHip = transform;
			}
			else if (this.Contains(transform.name, "rightleg,rightknee") && this.rightKnee == null)
			{
				this.rightKnee = transform;
			}
			else if (this.Contains(transform.name, "rightankle,rightfoot") && this.rightAnkle == null)
			{
				this.rightAnkle = transform;
			}
			else if (this.Contains(transform.name, "righttoebase") && this.rightFoot == null)
			{
				this.rightFoot = transform;
			}
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000073B0 File Offset: 0x000055B0
	private bool Contains(string baseString, string strings)
	{
		foreach (string value in strings.Split(new char[]
		{
			','
		}))
		{
			if (baseString.ToLower().Contains(value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000179 RID: 377
	[SerializeField]
	private Recorder.Devices device;

	// Token: 0x0400017A RID: 378
	[SerializeField]
	private Transform hipCenter;

	// Token: 0x0400017B RID: 379
	[SerializeField]
	private Transform spine;

	// Token: 0x0400017C RID: 380
	[SerializeField]
	private Transform shoulderCenter;

	// Token: 0x0400017D RID: 381
	[SerializeField]
	private Transform head;

	// Token: 0x0400017E RID: 382
	[SerializeField]
	private Transform leftShoulder;

	// Token: 0x0400017F RID: 383
	[SerializeField]
	private Transform leftElbow;

	// Token: 0x04000180 RID: 384
	[SerializeField]
	private Transform leftWrist;

	// Token: 0x04000181 RID: 385
	[SerializeField]
	private Transform leftHand;

	// Token: 0x04000182 RID: 386
	[SerializeField]
	private Transform rightShoulder;

	// Token: 0x04000183 RID: 387
	[SerializeField]
	private Transform rightElbow;

	// Token: 0x04000184 RID: 388
	[SerializeField]
	private Transform rightWrist;

	// Token: 0x04000185 RID: 389
	[SerializeField]
	private Transform rightHand;

	// Token: 0x04000186 RID: 390
	[SerializeField]
	private Transform leftHip;

	// Token: 0x04000187 RID: 391
	[SerializeField]
	private Transform leftKnee;

	// Token: 0x04000188 RID: 392
	[SerializeField]
	private Transform leftAnkle;

	// Token: 0x04000189 RID: 393
	[SerializeField]
	private Transform leftFoot;

	// Token: 0x0400018A RID: 394
	[SerializeField]
	private Transform rightHip;

	// Token: 0x0400018B RID: 395
	[SerializeField]
	private Transform rightKnee;

	// Token: 0x0400018C RID: 396
	[SerializeField]
	private Transform rightAnkle;

	// Token: 0x0400018D RID: 397
	[SerializeField]
	private Transform rightFoot;

	// Token: 0x0400018E RID: 398
	private float init;

	// Token: 0x04000190 RID: 400
	private Coroutine RecordingCoroutine;

	// Token: 0x04000191 RID: 401
	private List<int> articulations;

	// Token: 0x04000192 RID: 402
	private List<Register> registerList;

	// Token: 0x04000193 RID: 403
	private bool finalizado = true;

	// Token: 0x04000194 RID: 404
	private float timeCounter;

	// Token: 0x04000195 RID: 405
	private int registerCount;

	// Token: 0x04000196 RID: 406
	private List<int> registerCounts;

	// Token: 0x0200005E RID: 94
	private enum Devices
	{
		// Token: 0x04000248 RID: 584
		Device,
		// Token: 0x04000249 RID: 585
		Kinect,
		// Token: 0x0400024A RID: 586
		MediaPipe,
		// Token: 0x0400024B RID: 587
		BSN
	}
}
