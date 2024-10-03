using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class modelUser
{
    public int status;
    public string error;
    public string message;
    public string accessToken;
    public mydata data;
}

[System.Serializable]
public class mydata
{
    public int id;
    public string username;
    public string full_name;
    public string avatar;
    public string role;
    public DigitalIdentity digital_identity;
}
[System.Serializable]
public class DigitalIdentity
{
    public string nik;
    public int verification_status;
}
[System.Serializable]
public class ShowUserStatus
{
    public int status;
    public string error;
    public string message;
    public string accessToken;
    public UserData data;
}
[System.Serializable]
public class UserData
{
    public int id;
    public string full_name;
    public string email;
    public string role;
    public string avatar;
    public string registration_status;
    public string verification_status;
    public string created_at;
    public string updated_at;
}
[System.Serializable]
public class DataForKTP
{
    public int status;
    public string error;
    public string message;
    public dataKTP data;
}
[System.Serializable]
public class DataByNIK
{
    public int status;
    public string error;
    public string message;
    public dataPendudukDariNIK data;
}
[System.Serializable]
public class dataPendudukDariNIK
{
    public dataPendudukByNIK resident;
    public dataDocumentPenduduk document;
}
[System.Serializable]
public class dataDocumentPenduduk
{
    public string family_card_image;
    public SidikJariGroup fingerprints_image;
    public string iris_image;
    public string marriage_certificate;
    public string proof_of_residence;
    public string education_certificate;
    public string certificate_of_employment;
    public string belief_change_letter;
}
[System.Serializable]
public class SidikJariGroup
{
    public string fingerprint_r1;
    public string fingerprint_r2;
    public string fingerprint_r3;
    public string fingerprint_r4;
    public string fingerprint_r5;
    public string fingerprint_l1;
    public string fingerprint_l2;
    public string fingerprint_l3;
    public string fingerprint_l4;
    public string fingerprint_l5;
}
[System.Serializable]
public class dataKTP
{
    public string address_city;
    public string address_province;
    public string address_rt;
    public string address_rw;
    public string address_street;
    public string address_sub_district;
    public string address_village;
    public string birth_place;
    public string birthday;
    public string blood_type;
    public string created_at;
    public string deleted_at;
    public string family_card_number;
    public string full_name;
    public string gender;
    public string job;
    public string last_education;
    public string marital_status;
    public string nationality;
    public string profile_image;
    public string registration_type;
    public string religion;
    public string signature_image;
    public string updated_at;
    public string verification_status;
    public string verification_status_text;
    public string verified_at;
    public string email;
}
[System.Serializable]
public class dataPendudukByNIK
{
    public string address_city;
    public string address_province;
    public string address_rt;
    public string address_rw;
    public string address_street;
    public string address_sub_district;
    public string address_village;
    public string birth_place;
    public string birthday;
    public string blood_type;
    public string created_at;
    public string deleted_at;
    public string family_card_number;
    public string full_name;
    public string gender;
    public string job;
    public string last_education;
    public string marital_status;
    public string nationality;
    public string profile_image;
    public string registration_type;
    public string religion;
    public string signature_image;
    public string updated_at;
    public string verification_status;
    public string verification_status_text;
    public string verified_at;
}
[System.Serializable]
public class DataTablePenduduk
{
    public int status;
    public string error;
    public string message;
    public TableDataResident[] data;
    public TotalData pagination;
}
[System.Serializable]
public class TableDataResident
{
    public string full_name;
    public string nik;
    public string created_at;
    public string verification_status;
}
[System.Serializable]
public class TotalData
{
    public int size;
    public int page;
    public int total;
}
[System.Serializable]
public class KTPGenerator
{
    public int status;
    public string error;
    public string message;
    public KTPGeneratorResident data;
}
[System.Serializable]
public class KTPGeneratorResident
{

    public KTPGeneratorResult resident;
}
[System.Serializable]
public class KTPGeneratorResult
{
    public dataKTP result;
}
