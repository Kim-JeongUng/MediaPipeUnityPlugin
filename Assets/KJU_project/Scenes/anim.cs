using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
  public Transform Point;
  public Transform[] Anims;
  public Transform hip;
  // Start is called before the first frame update
  void Start()
  {
    
  }

    // Update is called once per frame
  void Update()
  {
    if (Point.childCount > 0)
    {
      for (int i = 0; i < Point.childCount; i++)
      {
        if(null!=Anims[i])
          Anims[i].position = Point.GetChild(i).position;
      }
      if (null != Point.GetChild(11) )
      {
        Anims[11].parent.position = Anims[11].position;
      }
      if (null != Point.GetChild(12))
      {
        Anims[12].parent.position = Anims[12].position;
      }
      if (null != Point.GetChild(23) && null != Point.GetChild(24))
      {
        hip.position = (Point.GetChild(23).position + Point.GetChild(24).position) / 2;
      }
      
    }
  }
}
