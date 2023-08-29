using System;
namespace AplikasiCitraLele.Models
{
    public class tb_citra
    {
        public int Id { get; set; }
        public int total_pixel { get; set; }
        public byte[] gambar { get; set; }
        public string keterangan { get; set; }
    }
}

