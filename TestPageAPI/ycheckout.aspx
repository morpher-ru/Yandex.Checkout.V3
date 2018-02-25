<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ycheckout.aspx.cs" Inherits="TestPageAPI.ycheckout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <br />
    <form id="YandexPay" runat="server">
        <div>

            <div>

                <asp:Label ID="Labelsum" runat="server" Text="Сумма"></asp:Label>
                <asp:TextBox ID="sum" runat="server">2000</asp:TextBox>

            </div>

            <br/>

            <asp:Button ID="submit_YandexPay"  OnClick="submit_YandexPay_Click" runat="server" Text="Оплата"  />

            <br />

            <asp:Label ID="LabelInfo" runat="server" Text=""> Для тестирования оплаты c банковской карты. Номер карты:  1111 1111 1111 1026. 
                                                                Действует до: любой год и месяц в будущем. Код CVV: 000</asp:Label>
        </div>
    </form>
</body>
</html>
