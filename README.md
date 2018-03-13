![Build status](https://ci.appveyor.com/api/projects/status/80n6r6lbn2c7p34o?svg=true) | [Nuget Package](https://www.nuget.org/packages/Yandex.Checkout.V3/)


# Yandex Checkout .NET Client

.NET-клиент для Яндекс.Кассы

На данный момент API реализован в мере, достаточной для проведения платежей по инструкции [Быстрый старт](https://kassa.yandex.ru/docs/guides/#bystryj-start) (шаги 1-4). 

[Пример вызова из ASP.NET](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/AspNetSample/Default.aspx.cs)

## Versioning Policy

&lt;PackageVersion&gt; follows [SemVer](https://semver.org/).

&lt;FileVersion&gt; and &lt;InformationalVersion&gt; are both equal to &lt;PackageVersion&gt; followed by a dot and the build number. They are patched by AppVeyor.

&lt;AssemblyVersion&gt; is currently set to 0.0.0.0 in the csproj file and is not patched during CI. It will go up to 1.0.0.0 once the API is stable and then the major version will will go up ONLY when there is a breaking change.

