namespace Clinic.Dtos
{
    public class PrescriptionDetailInVoiceDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public int MedicinePrice { get; set; }
        public long TotalPrice { get; set; }

    }
}