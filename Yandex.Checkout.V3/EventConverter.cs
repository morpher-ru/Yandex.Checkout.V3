using System;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    class EventConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Event enumValue = (Event)value;

            switch (enumValue)
            {
                case Event.PaymentWaitingForCapture:
                    writer.WriteValue(waitingForCapture);
                    break;
                case Event.Succeeded:
                    writer.WriteValue(succeeded);
                    break;
                default:
                    throw new JsonSerializationException($"Invalid Event: {enumValue}");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            switch (enumString)
            {
                case waitingForCapture:
                    return Event.PaymentWaitingForCapture;
                case succeeded:
                    return Event.Succeeded;
                default:
                    throw new JsonSerializationException($"Invalid Event: {enumString}");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        const string waitingForCapture = "payment.waiting_for_capture";
        const string succeeded = "payment.succeeded";
    }
}
