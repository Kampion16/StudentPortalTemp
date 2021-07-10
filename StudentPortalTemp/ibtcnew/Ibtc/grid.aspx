<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grid.aspx.cs" Inherits="grid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" CellPadding="2" AllowPaging="True" PageSize="4" ForeColor="#333333" Width="850px"  EmptyDataText="No Record Found" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" EnableModelValidation="True">
            <RowStyle BackColor="#dadfbc" Height="30px"  Wrap="false" />
               <HeaderStyle Width="10%" />
             <Columns>
                <asp:BoundField DataField="_venue" HeaderText="Venue Name" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_venue" />
                <asp:BoundField DataField="_SdateTime" HeaderText="Start Date" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_SdateTime" />
                <asp:BoundField DataField="_EdateTime" HeaderText="End Date" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_EdateTime" />
                <asp:BoundField DataField="_name" HeaderStyle-Width="15%" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderText="Name" SortExpression="_name" />
                <asp:BoundField DataField="_surname" HeaderStyle-Width="15%" HeaderStyle-Height="35px" HeaderText="Surname" HeaderStyle-Wrap="false" SortExpression="_surname" />
                <asp:BoundField DataField="_email" HeaderText="Email" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" SortExpression="_email" />
                 <asp:BoundField DataField="_Cell" HeaderText="Cell Number" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" SortExpression="_Cell" />
                 <asp:BoundField DataField="_Tell" HeaderText="Work Number" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" SortExpression="_Tell" />
            </Columns>
             <FooterStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#838F57" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" Wrap="False" />
        <AlternatingRowStyle BackColor="White" Height="40px" Wrap="True" />
        </asp:GridView>

        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="LoadCourses" TypeName="IBTCWebService"></asp:ObjectDataSource>

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Visible="false" DataSourceID="ObjectDataSource1"  CellPadding="2" AllowPaging="True" PageSize="4" ForeColor="#333333" Width="850px"  EmptyDataText="No Record Found"  EnableModelValidation="True">
             <RowStyle BackColor="#dadfbc" Height="30px"  Wrap="False" />
               <HeaderStyle Width="10%" />
            <Columns>
                <asp:BoundField DataField="courseName" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Course Name" SortExpression="courseName" />
                <asp:BoundField DataField="_noAttendees" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="No. Atendees" SortExpression="_noAttendees" />
                <asp:BoundField DataField="_SdateTime" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Start Date" SortExpression="_SdateTime" />
                <asp:BoundField DataField="_EdateTime" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="End Date" SortExpression="_EdateTime" />
                <asp:BoundField DataField="_venue" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Venue Name" SortExpression="_venue" />
                <asp:BoundField DataField="_name" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Name" SortExpression="_name" />
                <asp:BoundField DataField="_surname" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Surname" SortExpression="_surname" />
                <asp:BoundField DataField="_email" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Email" SortExpression="_email" />
                <asp:BoundField DataField="_Cell" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Cellphone Number" SortExpression="_Cell" />
                <asp:BoundField DataField="_Tell" HeaderStyle-Height="35px" HeaderStyle-Wrap="false" HeaderStyle-Width="15%" HeaderText="Work Number" SortExpression="_Tell" />
            </Columns>
             <FooterStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#838F57" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#838F57" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" Wrap="False" />
        <AlternatingRowStyle BackColor="White" Height="40px" Wrap="True" />
        </asp:GridView>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="LoadVenues" TypeName="IBTCWebService"></asp:ObjectDataSource>
        <asp:Button ID="btn" runat="server" Text="Load Venues" OnClick="btn_Click" />
        <asp:Button ID="Button1" runat="server" Text="Load Courses" OnClick="Button1_Click" />
    </form>

</body>
</html>
