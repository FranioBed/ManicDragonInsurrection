using UnityEngine;
using System.Collections;

public class ParticleSystemGenerator : MonoBehaviour {

    public GameObject particleEffect;
    public bool toggle = false;
    bool generatedParticle = false;

    //Set true to generate particle effect, false to destroy it
    public void ToggleParticleEffect(bool toToggle)
    {
        if (toToggle) toggle = true;
        else toggle = false;
    }

    void Start()
    {
        toggle = false;
        generatedParticle = false;
    }

    //Generate particle effect and set it as child to this game object
    void GenerateParticleEffect()
    {
        Vector3 pos = new Vector3(0, 0, -1);
        GameObject particles = Instantiate(particleEffect) as GameObject;
        particles.transform.SetParent(this.transform);
        particles.transform.localPosition = pos;
    }

    //Destroy all children with component ParticleSystem
    void DestroyParticleEffect()
    {
        ParticleSystem[] allChildren = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem child in allChildren)
        {
            if(child!=this.transform)
                 Destroy(child.gameObject);
        }
    }
   	
	void Update () {

        if (toggle && !generatedParticle)
        {
            GenerateParticleEffect();
            generatedParticle = true;
        }

        if (!toggle && generatedParticle)
        {
            DestroyParticleEffect();
            generatedParticle = false;
        }
	}
}
