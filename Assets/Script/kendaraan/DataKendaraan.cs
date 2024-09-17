using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataKendaraan
{
    public List<DataBar> databar;

}

[Serializable]
public class DataBar
{
    public int id;
    public string image;
    public int id_camera;
    public int id_service;
    public string data;
    public string date;
    public int sepeda;
    public int motor;
    public float mobil;
    public int bus;
    public int truk;
    public string tag;
}
