using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelGeneratorService : MonoBehaviour {

    [Inject]
    IDMILevelGenerator levelGenerator;
    IDMIRoomGenerator roomGenerator;

	// Use this for initialization
	void Start () {
		//levelGenerator.generate();
        //roomGenerator.generate();
	}
	
}
