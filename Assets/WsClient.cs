using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
public class WsClient : MonoBehaviour
{
    WebSocket ws;

    public List<WindTurbineScriptableObject> turbineData = new List<WindTurbineScriptableObject>();

    class IncomingTurbineData
    {
        public string name;
        public string rpm;
        public string power;
        public string windspeed;
        public string airspeed;
    }
    
    private void Start()
    {
        Debug.Log("helo");
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();


        List<GameObject> windmills = new List<GameObject>();
        //List<string> objectNames = new List<string>();
        ws.OnMessage += (sender, e) =>
        {
            ws.Send("hi");

            //if(e.Data!="")
            //Debug.Log(e.Data);
            IncomingTurbineData[] deserializedProduct = JsonConvert.DeserializeObject<IncomingTurbineData[]>(e.Data);

            /*if(windmills.Count == 0)
            {
                foreach(IncomingTurbineData itd in deserializedProduct)
                {
                    windmills.Add(GameObject.Find(itd.name));
                }
                
            }*/
            Debug.Log(deserializedProduct);
            Debug.Log(deserializedProduct[0].rpm);
            Debug.Log(deserializedProduct.Length);
            int i = 0;
            foreach (IncomingTurbineData item in deserializedProduct){

                //Debug.Log("hi");
                turbineData[i].windTurbineData.Power = double.Parse(item.power);
                turbineData[i].windTurbineData.AmbientTemperature = double.Parse(item.windspeed);
                turbineData[i].windTurbineData.RotorSpeed = double.Parse(item.rpm);
                 Debug.Log("00");
                i++;
                
                
            }
            Debug.Log(deserializedProduct[0].name);
        };
    }
    private void Update()
    {
        
        if (ws == null)
        {
            Debug.Log("null");
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pressed ");
            ws.Send("asda");
        }

        //turbineData.windTurbineData.
    }
}