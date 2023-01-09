namespace TBRPGF_API.Heroes
{
    public class HeroPortrait
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
        public byte[] HeroImage { get; set; }

    }
}
