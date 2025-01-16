using Newtonsoft.Json.Linq;

namespace Yandex.Checkout.V3
{
    public class ReceiverConverter : JsonConverter<Receiver>
    {
        public override bool CanWrite => false;

        public override Receiver ReadJson(JsonReader reader, Type objectType, Receiver existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var receiver = JObject.Load(reader);
            var receiverType = receiver["type"].ToObject<ReceiverType>(serializer);
            Receiver receiverInstance;
            switch (receiverType)
            {
                case ReceiverType.MobileBalance:
                    receiverInstance = new ReceiverMobileBalance();
                    break;
                case ReceiverType.BankAccount:
                    receiverInstance = new ReceiverBankAccount();
                    break;
                case ReceiverType.DigitalWallet:
                    receiverInstance = new ReceiverDigitalWallet();
                    break;
                default:
                    throw new ArgumentException($"Unknown receive type ({receiver["type"].Value<string>()}).");
            }

            serializer.Populate(receiver.CreateReader(), receiverInstance);
            return receiverInstance;
        }

        public override void WriteJson(JsonWriter writer, Receiver value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
