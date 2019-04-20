[![Build status](https://ci.appveyor.com/api/projects/status/80n6r6lbn2c7p34o?svg=true)](https://ci.appveyor.com/project/morpher/yandex-checkout-v3) | [Nuget Package](https://www.nuget.org/packages/Yandex.Checkout.V3/)

# .NET-клиент для Яндекс.Кассы

[Swith to English](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/README.en.md) 

Клиент разработан на основе [документации по API](https://kassa.yandex.ru/developers).

Клиент поддерживает классическую синхронную модель вызовов (класс Client) и async / await (класс AsyncClient).

Покрытие API почти полное и включает в себя создание платежа, подтверждение, возврат или отмену платежа, авиабилеты и чеки по ФЗ-54.

## Начало использования

Для проведения платежей сайт вашего магазина должен быть доступен для получения уведомлений от Яндекс.Кассы. URL для уведомлений нужно указать в [настройках магазина](https://kassa.yandex.ru/my/tunes).

Все вызовы API проводятся через класс Client. Для его создания нужны номер магазина и секретный ключ:

```csharp
    var client = new Yandex.Checkout.V3.Client(
        shopId: "12345", 
        secretKey: "ASDLsdFgsJnbKeJnOuQImWuJEuRPyIrOEwsRK");
```

Чтобы использовать async/await, создайте AsyncClient:

```csharp
    AsyncClient asyncClient = client.MakeAsync();
```

AsyncClient содержит те же методы, что и Client, только с суффиксом "Async". Дальше пример для Client.

Для проведения платежа по инструкции [Быстрый старт](https://kassa.yandex.ru/developers/payments/quick-start) (шаги 1-4):

```csharp
    // 1. Создайте платеж и получите ссылку для оплаты
    var newPayment = new NewPayment
    {
        Amount = new Amount { Value = 100.00m, Currency = "RUB" },
        Confirmation = new Confirmation { 
            Type = ConfirmationType.Redirect,
            ReturnUrl = "http://myshop.ru/thankyou"
        }
    };
    Payment payment = client.CreatePayment(newPayment);
    
    // 2. Перенаправьте пользователя на страницу оплаты
    string url = payment.Confirmation.ConfirmationUrl;
    Response.Redirect(url);

    // 3. Дождитесь получения уведомления
    Message message = Client.ParseMessage(Request.HttpMethod, Request.ContentType, Request.InputStream);
    Payment payment = message?.Object;
    
    if (message?.Event == Event.PaymentWaitingForCapture && payment.Paid)
    {
        // 4. Подтвердите готовность принять платеж
        _client.CapturePayment(payment.Id);
    }
```

Полный код в [примере на ASP.NET](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/AspNetSample/Default.aspx.cs).

## Минимальные требования

[Nuget-пакет](https://www.nuget.org/packages/Yandex.Checkout.V3) содержит версии для трех платформ:

* **.NET 4.0:** нет поддержки async / await, не содержит класса AsyncClient.
* **.NET 4.5**
* **.NET Standard 2.0:** этот вариант подойдет для большинства современных проектов.

Нужный вариант библиотеки выбирается автоматически при установке пакета. Инструкции по установке на [странице nuget.org](https://www.nuget.org/packages/Yandex.Checkout.V3).

## Versioning Policy

&lt;PackageVersion&gt; follows [SemVer](https://semver.org/).

&lt;FileVersion&gt; and &lt;InformationalVersion&gt; are both equal to &lt;PackageVersion&gt; followed by a dot and the build number. They are [patched by AppVeyor](https://ci.appveyor.com/project/morpher/yandex-checkout-v3/settings).

&lt;AssemblyVersion&gt; is currently set to 0.0.0.0 in the csproj file and is not patched during CI. It will go up to 1.0.0.0 once the API is stable and then the major version will go up ONLY when there is a breaking change.

## Troubleshooting

"Authentication failed because the remote party has closed the transport stream."

If you get this error while trying to create a new payment, it may mean that you are using a security protocol that YC does not support. Currently (July 2018) it supports TLS 1.2 but that may change in the future. Add this code before the create payment request:

```csharp
    using System.Net;

    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
```
  
Or, if you are targeting .NET 4.0 or lower:

```csharp
    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
```
