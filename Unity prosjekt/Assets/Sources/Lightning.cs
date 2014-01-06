
using UnityEngine;
using System.Collections;
using System;

public class Lightning : MonoBehaviour
{
   
    //Modes 
    public bool b_Particles = true;
    public bool b_ION_Trail = true;
    
    
    public Transform target;
    public Transform prefab_ION;
	public  int zigs = 200;   //the parameter for density -> UI 
    public Light startLight;   //automatically repositioned to this.transform in the  Start()- function
    public Light endLight;     //automatically repositioned to the target Transform,Gamobject  in the  Start()- function
       
     float speed = 1f;         //candidates to go public/inspector , no need so far   
	  float scale = 1.1f;

  
   
    //IONs
    public int ION_count = 9;          //the count of spiral  trailrender-coroutines 
    private float ION_Speed = 0.9F;    //experiment with this 
    private float init_StartLightintensity = 1;  //backup var for light-modulation, if a "startLight"-prefab is assigned

    private CNoise noise;              // perlin-noise class reference
    private float oneOverZigs;              
	
	private Particle[] particles;
    private Transform[] IONs;
    private float [] ION_frame = null;
    private Vector3 ION_Offset;

    private Vector3 last_pos ;
    private Vector3 distance;
    private Quaternion quat ; 

	void Start()
    {
       
        noise = new CNoise();
		oneOverZigs = 1f / (float)zigs;
        if (b_Particles)
        {
            particleEmitter.renderer.enabled = true;
            particleEmitter.emit = b_Particles;

            particleEmitter.Emit(zigs);
            particles = particleEmitter.particles;
        }
   

        distance = target.position - transform.position;


        //if (target == null ) { Debug.Log( "Error: no target set. Please set a target-transform in the inspector." ); }

        if (startLight)     {init_StartLightintensity = startLight.intensity;
                             startLight.transform.position = this.transform.position;  //place the start-light to lighting position
                             }

        if (endLight)        {  endLight.transform.position = target.transform.position; }





        if (prefab_ION && b_ION_Trail)  //check for ION-Rail-feature
        {    
             b_ION_Trail = true;
             ION_frame = new float[ION_count];
             IONs = new Transform[ION_count];
             ION_Offset = new Vector3(0.4F, 0.0F, 0.2F);

             for (int i=0; i<ION_count; i++)
             {
                quat = Quaternion.FromToRotation(transform.position, target.position);
                quat.eulerAngles += (new Vector3(0, 0, 360 / (float)ION_count) * i);
                IONs[i] = (Transform)Instantiate(prefab_ION, transform.position, quat);
               ION_frame[i] = 0;
                StartCoroutine ( ION_Trail(i) );
             }

         }
        else b_ION_Trail = false;


	}


    public void Restart() //just for the GUI-call  - remove that in production code
    {
        //clear out instances / help the Garbage-Collector  NET/Mono
        noise = null;
        if (ION_frame != null) ION_frame = null;

        StopAllCoroutines();
        if (IONs != null) { foreach( Transform t_ion  in IONs  )
                            if (t_ion != null ) Destroy( t_ion ) ;     
                            IONs = null;
        }
        particleEmitter.emit = false;
        particleEmitter.renderer.enabled = false;
        particleEmitter.ClearParticles();


        Start();
    }


	void Update ()
	{

        if (!b_Particles) return;

		float timex = Time.time * speed * 0.1365143f;
		float timey = Time.time * speed * 1.21688f;
		float timez = Time.time * speed * 2.5564f;
		
		for (int i=0; i < particles.Length; i++)
		{
			Vector3 position = Vector3.Lerp(transform.position, target.position, oneOverZigs * (float)i);
			Vector3 offset = new Vector3(noise.Noise(timex + position.x, timex + position.y, timex + position.z),
										noise.Noise(timey + position.x, timey + position.y, timey + position.z),
										noise.Noise(timez + position.x, timez + position.y, timez + position.z));
			position += (offset * scale * ((float)i * oneOverZigs));
			
			particles[i].position = position;
			particles[i].color = Color.white;
			particles[i].energy = 1f;

           
		}
		
        
		particleEmitter.particles = particles;
		
		if (particleEmitter.particleCount >= 2)
        {
            if (startLight != null )
            {
                startLight.intensity = init_StartLightintensity* (last_pos - particles[particles.Length - 1].position).magnitude * 10.0F;
            }
            
			if (endLight)
				endLight.transform.position = particles[particles.Length - 1].position;
            
           last_pos = particles[particles.Length - 1].position;
		}


    
	}



    IEnumerator ION_Trail(int idx_ION)
    {
        TrailRenderer trl_rnd = (TrailRenderer)IONs[idx_ION].GetComponent<TrailRenderer>();
        Vector3 i_pos = IONs[idx_ION].transform.position;
        Vector3 v_pos ;
        float field_constrain_factor = 1;
 
    
        while (b_ION_Trail)
        {

            //IONs
            v_pos = i_pos + distance.normalized * ((float)ION_Speed) * (distance.magnitude * ((float)ION_frame[idx_ION]) / (float)zigs);


            if ((v_pos - i_pos).magnitude >= (distance.magnitude))
            {


                ION_frame[idx_ION] = 0;
                v_pos = i_pos;
                trl_rnd.enabled = false;
                yield return null;
            }

            else
            { trl_rnd.enabled = true; }


            //calculate the the field constrain
            field_constrain_factor = 4.5F / ( Math.Abs(  Math.Abs( ( v_pos - i_pos).magnitude)   - ( distance.magnitude / 2.0F) ) + /*zero/div clamping*/ 0.01F);
            field_constrain_factor = Mathf.Clamp(field_constrain_factor, 0.2F, 1.6F);


            IONs[idx_ION].position = v_pos + ( ION_Offset * ((float)idx_ION + 1.8F)) * field_constrain_factor;
            IONs[idx_ION].transform.RotateAround(v_pos, distance.normalized,/*the angle*/  6.8F * ((float)idx_ION) * ION_frame[idx_ION]);
           //  IONs[idx_ION].position -= ION_Offset;
            //    Debug.Log("ION.position: " + ION.position.ToString() + "   rotation:  " +  ION.rotation.ToString() );
              ION_frame[idx_ION] += ION_Speed;




            yield return null;
        }

    }



}


public class CNoise
{
    
    const int B = 0x100;
    const int BM = 0xff;
    const int N = 0x1000;

    int[] p = new int[B + B + 2];
    float[,] g3 = new float[B + B + 2, 3];
    float[,] g2 = new float[B + B + 2, 2];
    float[] g1 = new float[B + B + 2];

    float s_curve(float t)
    {
        return t * t * (3.0F - 2.0F * t);
    }

    float lerp(float t, float a, float b)
    {
        return a + t * (b - a);
    }

    void setup(float value, out int b0, out int b1, out float r0, out float r1)
    {
        float t = value + N;
        b0 = ((int)t) & BM;
        b1 = (b0 + 1) & BM;
        r0 = t - (int)t;
        r1 = r0 - 1.0F;
    }

    float at2(float rx, float ry, float x, float y) { return rx * x + ry * y; }
    float at3(float rx, float ry, float rz, float x, float y, float z) { return rx * x + ry * y + rz * z; }

   

    public float Noise(float x, float y, float z)
    {
        int bx0, bx1, by0, by1, bz0, bz1, b00, b10, b01, b11;
        float rx0, rx1, ry0, ry1, rz0, rz1, sy, sz, a, b, c, d, t, u, v;
        int i, j;

        setup(x, out bx0, out bx1, out rx0, out rx1);
        setup(y, out by0, out by1, out ry0, out ry1);
        setup(z, out bz0, out bz1, out rz0, out rz1);

        i = p[bx0];
        j = p[bx1];

        b00 = p[i + by0];
        b10 = p[j + by0];
        b01 = p[i + by1];
        b11 = p[j + by1];

        t = s_curve(rx0);
        sy = s_curve(ry0);
        sz = s_curve(rz0);

        u = at3(rx0, ry0, rz0, g3[b00 + bz0, 0], g3[b00 + bz0, 1], g3[b00 + bz0, 2]);
        v = at3(rx1, ry0, rz0, g3[b10 + bz0, 0], g3[b10 + bz0, 1], g3[b10 + bz0, 2]);
        a = lerp(t, u, v);

        u = at3(rx0, ry1, rz0, g3[b01 + bz0, 0], g3[b01 + bz0, 1], g3[b01 + bz0, 2]);
        v = at3(rx1, ry1, rz0, g3[b11 + bz0, 0], g3[b11 + bz0, 1], g3[b11 + bz0, 2]);
        b = lerp(t, u, v);

        c = lerp(sy, a, b);

        u = at3(rx0, ry0, rz1, g3[b00 + bz1, 0], g3[b00 + bz1, 2], g3[b00 + bz1, 2]);
        v = at3(rx1, ry0, rz1, g3[b10 + bz1, 0], g3[b10 + bz1, 1], g3[b10 + bz1, 2]);
        a = lerp(t, u, v);

        u = at3(rx0, ry1, rz1, g3[b01 + bz1, 0], g3[b01 + bz1, 1], g3[b01 + bz1, 2]);
        v = at3(rx1, ry1, rz1, g3[b11 + bz1, 0], g3[b11 + bz1, 1], g3[b11 + bz1, 2]);
        b = lerp(t, u, v);

        d = lerp(sy, a, b);

        return lerp(sz, c, d);
    }

    void normalize2(ref float x, ref float y)
    {
        float s;

        s = (float)Math.Sqrt(x * x + y * y);
        x = y / s;
        y = y / s;
    }

    void normalize3(ref float x, ref float y, ref float z)
    {
        float s;
        s = (float)Math.Sqrt(x * x + y * y + z * z);
        x = y / s;
        y = y / s;
        z = z / s;
    }

    public CNoise()
    {
        int i, j, k;
        System.Random rnd = new System.Random();

        for (i = 0; i < B; i++)
        {
            p[i] = i;
            g1[i] = (float)(rnd.Next(B + B) - B) / B;

            for (j = 0; j < 2; j++)
                g2[i, j] = (float)(rnd.Next(B + B) - B) / B;
            normalize2(ref g2[i, 0], ref g2[i, 1]);

            for (j = 0; j < 3; j++)
                g3[i, j] = (float)(rnd.Next(B + B) - B) / B;


            normalize3(ref g3[i, 0], ref g3[i, 1], ref g3[i, 2]);
        }

        while (--i != 0)
        {
            k = p[i];
            p[i] = p[j = rnd.Next(B)];
            p[j] = k;
        }

        for (i = 0; i < B + 2; i++)
        {
            p[B + i] = p[i];
            g1[B + i] = g1[i];
            for (j = 0; j < 2; j++)
                g2[B + i, j] = g2[i, j];
            for (j = 0; j < 3; j++)
                g3[B + i, j] = g3[i, j];
        }
    }
}