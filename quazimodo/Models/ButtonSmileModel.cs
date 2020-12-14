using quazimodo.Enums;

namespace quazimodo.Models
{
    public class ButtonSmileModel
    {
        public SoundParameter CommandParameter { get; set; }
        public string Image { get; set; }
        public SmileType SmileType { get; set; }
    }
}