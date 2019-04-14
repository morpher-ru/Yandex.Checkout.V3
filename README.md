[![Build status](https://ci.appveyor.com/api/projects/status/80n6r6lbn2c7p34o?svg=true)](https://ci.appveyor.com/project/morpher/yandex-checkout-v3) | [Nuget Package](https://www.nuget.org/packages/Yandex.Checkout.V3/)


# Yandex Checkout .NET Client

.NET-клиент для Яндекс.Кассы

На данный момент API реализован в мере, достаточной для проведения платежей по инструкции [Быстрый старт](https://kassa.yandex.ru/developers/payments/quick-start) (шаги 1-4). 

[Пример вызова из ASP.NET](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/AspNetSample/Default.aspx.cs)

## Versioning Policy

&lt;PackageVersion&gt; follows [SemVer](https://semver.org/).

&lt;FileVersion&gt; and &lt;InformationalVersion&gt; are both equal to &lt;PackageVersion&gt; followed by a dot and the build number. They are [patched by AppVeyor](https://ci.appveyor.com/project/morpher/yandex-checkout-v3/settings).

&lt;AssemblyVersion&gt; is currently set to 0.0.0.0 in the csproj file and is not patched during CI. It will go up to 1.0.0.0 once the API is stable and then the major version will go up ONLY when there is a breaking change.

## Troubleshooting

"Authentication failed because the remote party has closed the transport stream."

If you get this error while trying to create a new payment, it may mean that you are using a security protocol that YC does not support. Currently (July 2018) it supports TLS 1.2 but that may change in the future. Add this code before the create payment request:

    using System.Net;

    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
  
Or, if you are targeting .NET 4.0 or lower:

    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
