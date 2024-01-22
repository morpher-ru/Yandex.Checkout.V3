namespace Yandex.Checkout.V3
{
    public class ThreeDSecure
    {
        public bool Applied { get; set; }

        public string Protocol { get; set; }

        public bool MethodCompleted { get; set; }

        public bool ChallengeCompleted { get; set; }
    }
}