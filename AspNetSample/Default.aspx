<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspNetSample.ycheckout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="YandexPay" runat="server">
        <p>
            <asp:Label ID="Labelsum" runat="server" Text="Сумма"></asp:Label>
            <asp:TextBox ID="sum" runat="server">2000</asp:TextBox>
            <asp:Button ID="submit_YandexPay"  OnClick="submit_YandexPay_Click" runat="server" Text="Оплата"  />
        </p>

        <p>
            <asp:Label ID="LabelInfo" runat="server" Text=""> Для тестирования оплаты c банковской карты. Номер карты:  1111 1111 1111 1026. 
                Действует до: любой год и месяц в будущем. Код CVV: 000</asp:Label>
        </p>
            
        <p>
            <a href="log.txt">log.txt</a>
        </p>
    </form>
</body>
</html>
