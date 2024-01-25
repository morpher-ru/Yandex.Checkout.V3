[![Build status](https://ci.appveyor.com/api/projects/status/mgyl8ebfc5149uy5?svg=true)](https://ci.appveyor.com/project/morpher/yandex-checkout-v3)
 | [Nuget Package](https://www.nuget.org/packages/Yandex.Checkout.V3/)

# .NET-клиент для Яндекс.Кассы

[Switсh to English](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/README.en.md) 

Клиент разработан на основе [документации по API](https://yookassa.ru/developers).

Клиент поддерживает классическую синхронную модель вызовов (класс Client) и async / await (класс AsyncClient).

Покрытие API почти полное и включает в себя создание платежа, подтверждение, возврат или отмену платежа, авиабилеты и чеки по ФЗ-54.

## Начало использования

Для проведения платежей сайт вашего магазина должен быть доступен для получения уведомлений от Яндекс.Кассы. URL для уведомлений нужно указать в [настройках магазина](https://yookassa.ru/my/shop-settings).

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

Для проведения платежа по инструкции [Быстрый старт](https://yookassa.ru/developers/payment-acceptance/getting-started/quick-start) (шаги 1-3):

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


## Сборка Nuget-пакета

Для сборки пакета достаточно выполнить команду ```dotnet pack```:

```cmd
C:\Code\Yandex.Checkout.V3\Yandex.Checkout.V3> dotnet pack             
MSBuild version 17.6.1+8ffc3fe3d for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  Yandex.Checkout.V3 -> C:\Code\Yandex.Checkout.V3\Yandex.Checkout.V3\bin\Debug\net45\Yandex.Checkout.V3.dll
  Yandex.Checkout.V3 -> C:\Code\Yandex.Checkout.V3\Yandex.Checkout.V3\bin\Debug\netstandard2.0\Yandex.Checkout.V3.dll
```

Пакет будет создан в папке bin/Debug.


## Политика версионирования

Версия пакета задается тегом &lt;Version&gt; в [Yandex.Checkout.V3.csproj](https://github.com/morpher-ru/Yandex.Checkout.V3/blob/master/Yandex.Checkout.V3/Yandex.Checkout.V3.csproj#L5) и следует правилам [семантического версионирования](https://semver.org/lang/ru/).

## Устранение проблем

"Authentication failed because the remote party has closed the transport stream."

Если вы получили эту ошибку при попытке создания платежа, возможно, вы используете протокол, не поддерживаемый Яндекс.Кассой. По состоянию на июль 2018 поддерживается TLS 1.2, что может измениться в будущем. Добавьте следующий код перед посылкой запроса:

```csharp
    using System.Net;

    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
```
  
Или, если вы используете .NET 4.0 или ниже:

```csharp
    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
```

## Совместная работа над проектом

Если вы решили взять в работу одну из [открытых задач](https://github.com/morpher-ru/Yandex.Checkout.V3/issues),
сообщите об этом в комментарии к задаче, чтобы предотвратить дублирование усилий.

На каждое изменение лучше заводить отдельный пул-реквест. Так больше шансов, что ваш пул-реквест будет принят.
