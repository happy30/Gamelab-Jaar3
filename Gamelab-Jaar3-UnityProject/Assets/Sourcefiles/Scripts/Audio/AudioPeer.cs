using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioPeer : MonoBehaviour
{
    AudioSource source;
    public float[] samples = new float[512];
    public float[] freqBand = new float[8];
    public float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];


	// Use this for initialization
	void Start ()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetSpectrumData();
        MakeFrequencyBands();
        BandBuffer();
        
    }

    void GetSpectrumData()
    {
        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for(int g = 0; g < 8; g++)
        {
            if(freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }

            if (freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for(int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2 , i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }
    }
}
