using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
   public class model_residenceUser{
    public int status;
    public string error;
    public string message;
    public dataResidence data;
}

public class dataResidence
{
    public string id;
    public string full_name;
    public string email;
    public string role;
    public string password;
    public string email_verified_at;
    public string email_otp;
    public string email_otp_send_at;
    public string email_otp_false_attempt;
    public string latest_session_id;
    public string meta_avatar;
    public string created_at;
    public string updated_at;
    public string deleted_at;
    public string user_id;
    public string nik;
    public string registration_type;
    public string last_generated_at;
}

public class pagination
{
    public int size = 1;
    public int page = 2;
    public int totak = 3;
}

