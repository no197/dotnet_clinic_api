namespace Clinic.Dtos
{
    public class PrescriptionDetailDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public string Instruction { get; set; }
    }
}