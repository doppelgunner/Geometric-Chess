using UnityEngine;
using System.Collections;
using System;

public interface IHeapItem<T> : IComparable<T> {

	int HeapIndex {
		get;
		set;
	}
}
