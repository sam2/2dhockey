using UnityEngine;
using System.Collections;
using ProtoBuf;
using System;

[ProtoContract]
public class LPlayer 
{
	[ProtoMember(1)]
	public int id;

	public LPlayer()
	{
		id = 0;
	}
}
