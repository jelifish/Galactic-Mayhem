using UnityEngine;
using System.Collections;

public class MusicSizePulsate : MonoBehaviour {

    //int qSamples =  1024;  // array size
    //float refValue = 0.1f;// RMS value for 0 dB
    //float rmsValue;   // sound level - RMS
    //float dbValue;    // sound level - dB
    //float volume = 1; // set how much the scale will vary
    public AudioSource source;



    private float[] freqData;
 private int nSamples = 1024;
    private float fMax;
 
private float[] samples; // audio samples
 
void Start()
    {
        //samples = new float[qSamples];
        StartCoroutine(sizeControl());
    }

    private float GetVolume(float fLow, float fHigh)
    {
        //source.GetOutputData(samples, 0);
        // //GetComponent.<AudioSource> ().GetOutputData(samples, 0); // fill array with samples

        // //var i: int;
        // float sum = 0f;
        // //var sum: float = 0;
        // for (int i = 0; i < qSamples; i++)
        // {
        //     sum += samples[i] * samples[i]; // sum squared samples
        // }
        // rmsValue = Mathf.Sqrt(sum / qSamples); // rms = square root of average
        // dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        // if (dbValue < -160) dbValue = -160; // clamp it to -160dB min


        fLow = Mathf.Clamp(fLow, 20, fMax); // limit low...
        fHigh = Mathf.Clamp(fHigh, fLow, fMax); // and high frequencies
                                                // get spectrum
        source.GetSpectrumData(freqData, 0, FFTWindow.BlackmanHarris);
        int n1 = (int)Mathf.Floor(fLow * nSamples / fMax);
        int n2 = (int)Mathf.Floor(fHigh * nSamples / fMax);
        float sum = 0;
        // average the volumes of frequencies fLow to fHigh
        for (var i = n1; i <= n2; i++)
        {
            sum += freqData[i];
        }
        return sum / (n2 - n1 + 1);

    }
    
    void Update()
    {
        //GetVolume();
        //Debug.Log(volume * rmsValue);
        //this.transform.localScale = new Vector3(50 *volume * rmsValue, 50 * volume * rmsValue, this.transform.localScale.z);
        //transform.localScale.y = volume * rmsValue;


        freqData = new float[nSamples];
        fMax = AudioSettings.outputSampleRate / 2;
        //float v = GetVolume(200f, 400f);
        //Debug.Log(1000 * v);
        //this.transform.localScale = new Vector3(1+ (1000 * v)/5, 1 + (1000 * v) / 5, this.transform.localScale.z);
    }
    IEnumerator sizeControl()
    {
        float lastv = 0f;
        yield return new WaitForSeconds(.03f);
        while (true) {
            float v = GetVolume(2500f, 4500f);
            if (Mathf.Abs(lastv - v) > (lastv/4))
            {
                //Debug.Log(1000 * v);
                if ((1000 * v) > 1f) {
                    v = (1f / 500);
                }
                this.transform.localScale = new Vector3(2f + (500 * v) , 2f + (500 * v) , this.transform.localScale.z);
                lastv = v;

            }

            yield return new WaitForSeconds(.016f);
        }
        
    }
}
