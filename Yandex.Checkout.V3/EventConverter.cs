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
            }
            throw new Exception("Invalid Event.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            if (enumString == waitingForCapture)
                return Event.PaymentWaitingForCapture;

            throw new Exception("Invalid Event.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        const string waitingForCapture = "payment.waiting_for_capture";
    }
}