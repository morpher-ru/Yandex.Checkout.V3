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
            Receiver receiverInstance = receiverType switch
            {
                ReceiverType.MobileBalance => new ReceiverMobileBalance(),
                ReceiverType.BankAccount => new ReceiverBankAccount(),
                ReceiverType.DigitalWallet => new ReceiverDigitalWallet(),
                _ => throw new ArgumentException($"Unknown receive type ({receiver["type"].Value<string>()}).")
            };

            serializer.Populate(receiver.CreateReader(), receiverInstance);
            return receiverInstance;
        }

        public override void WriteJson(JsonWriter writer, Receiver value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
