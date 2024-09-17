using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogKTP
{
    private string[] Dialog = new string[] {
        "Mohon tunggu sebentar, kami sedang memeriksa status pendaftaran Anda...",
        "break",
        "Silahkan isi nama lengkap, NIK, dan nomor kartu keluarga pada formulir berikut.",
        "break",
        "Silahkan isi informasi alamat tinggal Anda sesuai dengan Kartu Keluarga.",
        "break",
        "Lengkapi informasi biodata Anda (1/2).",
        "break",
        "Lengkapi informasi biodata Anda (2/2).",
        "break",
        "Lengkapi data biometrik dan dokumen Kartu Keluarga untuk memverifikasi identitas Anda.",
        "break",
        "Konfirmasi identitas Anda dan sertakan tanda tangan.",
        //13
        "break",
        "peringatan error",
        "back",
        "Identitas Anda telah berhasil direkam ke dalam sistem <b>blockchain</b>. Petugas akan memverifikasi biodata dan dokumen-dokumen Anda, mohon tunggu.",
        "end",
        //18 menunggu verifikasi
        "Anda telah melakukan pendaftaran identitas digital pada tanggal {insert date}. Saat ini pendaftaran identitas digital Anda masih dalam status <b>menunggu verifikasi</b>. Mohon kesediaan Anda untuk menunggu, petugas akan segera memproses permohonan pendaftaran identitas digital Anda.",
        "end",
        //20 rejeted
        "Pendaftaran identitas digital Anda tidak dapat diverifikasi karena ketidaksesuaian informasi dengan dokumen pendukung, Tekan ENTER jika Anda ingin melakukan pendaftaran ulang.",
        "new",
        //22 verifikasi
        "Anda telah memiliki identitas digital yang terverifikasi. Tekan ENTER jika Anda ingin melakukan perubahan data atau informasi.",
        "edit",
    };

    private string[] updateDialog = new string[]
    {
        "Silahkan centang kolom identitas yang ingin Anda perbarui.",
        "break",
    };
    public string[] DialogListGet()
    {
        return Dialog;
    }
    public string[] UpdateDialogListGet()
    {
        return updateDialog;
    }
}
