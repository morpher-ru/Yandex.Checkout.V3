[![Build status](https://ci.appveyor.com/api/projects/status/mgyl8ebfc5149uy5?svg=true)](https://ci.appveyor.com/project/morpher/yandex-checkout-v3)
 | [Nuget Package](https://www.nuget.org/packages/Yandex.Checkout.V3/) | [По-русски](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/README.md) 

# Yandex.Checkout .NET client

This client library implements the [Yandex.Checkout API](https://yookassa.ru/developers?lang=en).

The library supports async / await (via the AsyncClient class) as well as the classical synchronous model (via the Client class).

The API methods covered include create payment, capture payment, cancel payment, refunds, airline tickets and FZ-54 compliant receipts.

## Getting Started

In order to take payments your website has to be able to accept notifications from Yandex.Checkout. The URL for notifications must be set on your [shop settings page](https://yookassa.ru/my/shop-settings?lang=en).

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
    var notification = Client.ParseMessage(Request.HttpMethod, Request.ContentType, Request.InputStream);

    if (notification is PaymentWaitingForCaptureNotification paymentWaitingForCaptureNotification)
    {
        Payment payment = paymentWaitingForCaptureNotification.Object;
        
        if (payment.Paid)
        {
            // 4. Confirm taking the payment
            _client.CapturePayment(payment.Id);
        }
    }
```

For the full code, see the [ASP.NET sample project](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/AspNetSample/Default.aspx.cs).

## Minimum Requirements

The [Nuget package](https://www.nuget.org/packages/Yandex.Checkout.V3) contains versions for three platforms:

* **.NET 4.0:** no async / await and hence no class AsyncClient.
* **.NET 4.5**
* **.NET Standard 2.0:** This version will fit most modern project types.

The required version is chosen automatically for you when you install the Nuget package. Installation instructions can be found on the [nuget.org page](https://www.nuget.org/packages/Yandex.Checkout.V3).


## Building the Nuget package

To build the Nuget package, simply run ```dotnet pack```:

```cmd
C:\Code\Yandex.Checkout.V3\Yandex.Checkout.V3> dotnet pack             
MSBuild version 17.6.1+8ffc3fe3d for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  Yandex.Checkout.V3 -> C:\Code\Yandex.Checkout.V3\Yandex.Checkout.V3\bin\Debug\net45\Yandex.Checkout.V3.dll
  Yandex.Checkout.V3 -> C:\Code\Yandex.Checkout.V3\Yandex.Checkout.V3\bin\Debug\netstandard2.0\Yandex.Checkout.V3.dll
```

The package will be created in the ```bin/Debug``` folder.


## Versioning Policy

The package version is set by the &lt;Version&gt; tag in [Yandex.Checkout.V3.csproj](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/Yandex.Checkout.V3/Yandex.Checkout.V3.csproj#L5) and follows [SemVer](https://semver.org/).

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

## Contributing

If you would like to work on one of the [open issues](https://github.com/morpher-ru/Yandex.Checkout.V3/issues),
please indicate this first by commenting on the issue. This is to avoid duplicating efforts.
