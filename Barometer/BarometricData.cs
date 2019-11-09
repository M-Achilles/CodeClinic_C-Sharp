namespace Barometer
{
    public class BarometricData
    {
        public float AirTemp { get; set; }
        public float BarometricPress { get; set; }
        public float DewPoint { get; set; }
        public float RelativeHumidity { get; set; }
        public float WindDir { get; set; }
        public int WindGust { get; set; }
        public float WindSpeed { get; set; }
    }
}