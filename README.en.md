[![Build status](https://ci.appveyor.com/api/projects/status/80n6r6lbn2c7p34o?svg=true)](https://ci.appveyor.com/project/morpher/yandex-checkout-v3) | [Nuget Package](https://www.nuget.org/packages/Yandex.Checkout.V3/)

[По-русски](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/README.md) 

# Yandex.Checkout .NET client

This client library implements the [Yandex.Checkout API](https://kassa.yandex.ru/developers).

The library supports async / await (via the AsyncClient class) as well as the classical synchronous model (via the Client class).

The API methods covered include create payment, capture payment, cancel payment, refunds, airline tickets and FZ-54 compliant receipts.

## Getting Started

In order to take payments your website has to be able to accept notifications from Yandex.Checkout. The URL for notifications must be set on your [shop settings page](https://kassa.yandex.ru/my/tunes).

Most API calls are done using the Client class. It takes your shopId and secretKey in the constructor:

```csharp
    var client = new Yandex.Checkout.V3.Client(
        shopId: "12345", 
        secretKey: "<<<insert your secrect key here>>>");
```

In order to use async/await, create an AsyncClient:

```csharp
    AsyncClient asyncClient = client.MakeAsync();
```

AsyncClient contains the same methods as Client, with the "Async" suffix. The following example uses Client.

The sample code below implements taking a payment as detailed in the [Quick Start](https://checkout.yandex.com/docs/guides/#quick-start) guide:

```csharp
    // 1. Create a payment and get the payment link
    var newPayment = new NewPayment
    {
        Amount = new Amount { Value = 100.00m, Currency = "RUB" },
        Confirmation = new Confirmation { 
            Type = ConfirmationType.Redirect,
            ReturnUrl = "http://myshop.ru/thankyou"
        }
    };
    Payment payment = client.CreatePayment(newPayment);
    
    // 2. Redirect the user to the payment page
    string url = payment.Confirmation.ConfirmationUrl;
    Response.Redirect(url);

    // 3. Wait until the payment is processed
    Message message = Client.ParseMessage(Request.HttpMethod, Request.ContentType, Request.InputStream);
    Payment payment = message?.Object;
    
    if (message?.Event == Event.PaymentWaitingForCapture && payment.Paid)
    {
        // 4. Confirm taking the payment
        _client.CapturePayment(payment.Id);
    }
```

For the full code, see the [ASP.NET sample project](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/AspNetSample/Default.aspx.cs).

## Minimum Requirements

The [Nuget package](https://www.nuget.org/packages/Yandex.Checkout.V3) contains versions for three platforms:

* **.NET 4.0:** no async / await and hence no class AsyncClient.
* **.NET 4.5**
* **.NET Standard 2.0:** This version will fit most modern project types.

The required version is chosen automatically for you when you install the Nuget package. Installation instructions can be found on the [nuget.org page](https://www.nuget.org/packages/Yandex.Checkout.V3).

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
