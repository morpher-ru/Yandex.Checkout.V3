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
                ReceiverType.MobileBalance => new MobileBalance(),
                ReceiverType.BankAccount => new BankAccount(),
                ReceiverType.DigitalWallet => new DigitalWallet(),
                _ => throw new ArgumentException($"Unknown receive type ({receiver["type"].Value<string>()}).")
            };

            serializer.Populate(receiver.CreateReader(), receiverInstance);
            return receiverInstance;
        }

        public override void WriteJson(JsonWriter writer, Receiver value, JsonSerializer serializer)
        {
        }
    }
}

public static class StringUtils
{
    public static string ToSnakeCase(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
}
