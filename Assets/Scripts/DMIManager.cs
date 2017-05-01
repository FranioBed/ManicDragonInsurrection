using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DMIManager : IInitializable, ITickable, IFixedTickable
{
    [Inject]
    LevelGeneratorService _levelGeneratorSerivce;
    //TODO
    //AI
    //UIController
    //anything that somes to mind...

    [Inject]
    DMISettingsInstaller.GameSettings _settings;

    int seed;  //TODO: use as readonly property??


    public void Initialize()
    {
        seed = setSeed();
        LevelInfo levelInfo = _levelGeneratorSerivce.generate(seed);
    }

    private int setSeed()
    {
       if (_settings.useFixedSeed)
            return _settings.fixedSeed;
       return new System.Random().Next();
    }

    public void FixedTick()
    {
        //todo
    }

    public void Tick()
    {
        //todo
    }
}
