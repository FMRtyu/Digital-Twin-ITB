using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DummyEdit : MonoBehaviour
{
    public Texture2D domisiliFoto;
    public Texture2D kk;

    public byte[] domisiliFotoByte;
    public byte[] kkByte;

    private string provinsiBaru;
    private string kotaBaru;
    private string kecamatanBaru;
    private string kelurahanBaru;

    private string RTInput;
    private string RWInput;
    private string alamatInput;
    private void Start()
    {
        provinsiBaru = "JAWA BARAT";
        kotaBaru = "KOTA BANDUNG";
        kecamatanBaru = "BUAHBATU";
        kelurahanBaru = "SEKEJATI";

        RTInput = "001";
        RWInput = "002";
        alamatInput = "Jalan aceh 23 No. 44";
        domisiliFotoByte = domisiliFoto.EncodeToJPG();
        kkByte = kk.EncodeToJPG();

        StartCoroutine(DummyData());
    }
    IEnumerator DummyData()
    {
        WWWForm form = new WWWForm();
        form.AddField("address_province", provinsiBaru);
        form.AddField("address_city", kotaBaru);
        form.AddField("address_sub_district", kecamatanBaru);
        form.AddField("address_village", kelurahanBaru);
        form.AddField("address_rt", RTInput);
        form.AddField("address_rw", RWInput);
        form.AddField("address_street", alamatInput);
        form.AddBinaryData("proof_of_resident", domisiliFotoByte, "Domisili.jpg", "image/jpg");
        form.AddBinaryData("family_card_image", kkByte, "KK.jpg", "image/jpg");

        UnityWebRequest request1 = UnityWebRequest.Post("https://blockchain-itb.com/resident-identity/update", form);

        //request1.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        //request1.SetRequestHeader("Content-Type", "application/json");
        request1.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        //request.SetRequestHeader("token", "02e2310edc5eaf62dfe406abaf1a65af");
        request1.SetRequestHeader("access-token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6NSwiaWF0IjoxNjg3NzEzMjAxLCJleHAiOjE2OTI4OTcyMDF9.5saniE-Guwb5XbBKbij3B-hU6hjK_TPbsRFdqKyqWzg");

        yield return request1.SendWebRequest();

        if (request1.result == UnityWebRequest.Result.ConnectionError || request1.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request1.error);
            Debug.Log("Response: " + request1.downloadHandler.text);

            Debug.Log("error");
        }
        else
        {
            Debug.Log("Response: " + request1.downloadHandler.text);
            Debug.Log("Fadil");
        }
    }
}
