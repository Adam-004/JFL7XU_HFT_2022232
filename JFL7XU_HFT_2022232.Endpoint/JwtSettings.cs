namespace JFL7XU_HFT_2022232.Endpoint
{
    public class JwtSettings
    {
        public string Key { get; set; } = "YourSuperSecretKey1234567890123456";
        public string Issuer { get; set; } = "JFL7XU_HFT_2022232";
        public string Audience { get; set; } = "JFL7XU_HFT_2022232Client";
    }
}
