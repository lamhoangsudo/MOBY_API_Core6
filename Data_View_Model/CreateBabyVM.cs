namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateBabyVM
    {
        public int UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Sex { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
