using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DMIManager : IInitializable, ITickable, IFixedTickable
{
    [Inject]
    LevelGeneratorService levelGeneratorSerivce;

    [Inject]
    DMISettingsInstaller.GameSettings _settings;

    int seed;  //TODO: set property for constant seed


    public void Initialize()
    {
        seed = setSeed();
        levelGeneratorSerivce.generate(seed);
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
