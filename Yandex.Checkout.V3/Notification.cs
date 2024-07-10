namespace Yandex.Checkout.V3;

public abstract record Notification;
public record PaymentWaitingForCaptureNotification(Payment Object) : Notification;
public record PaymentSucceededNotification(Payment Object) : Notification;
public record PaymentCanceledNotification(Payment Object) : Notification;
public record RefundSucceededNotification(Refund Object) : Notification;
public record PayoutSucceededNotification(Payout Object) : Notification;
public record PayoutCanceledNotification(Payout Object) : Notification;
