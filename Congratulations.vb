'This form should show the Congratulations message to the winner and
'Display the information of the winner
Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Congratulations

    Dim sTicket As String

    Public sName As String
    Public sAddress As String
    Public sPhone As String

    Private Sub Congratulations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtPrize.Multiline = True
        TxtName.Text = Main.wName
        TxtAddress.Text = Main.wAddress
        TxtPhone.Text = Main.wPhone

        ' If the winner's name is "UNKNOWN", no need to query the database again
        If Main.wName = "UNKNOWN" Then
            ' Instead of triggering reshuffling here, let the main form handle it
            MsgBox("Winner is unknown. Reshuffling ticket numbers...")
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click

        Dim temp As Integer
        temp = DisplayWinner.WinnersList.Items.Count
        Dim lst As New ListViewItem(TxtPrize.Text)
        lst.SubItems.Add(TxtTicket.Text)
        lst.SubItems.Add(TxtName.Text)
        lst.SubItems.Add(TxtAddress.Text)
        lst.SubItems.Add(TxtPhone.Text)


        DisplayWinner.WinnersList.Items.Add(lst)

        TxtPrize.Text = Nothing
        TxtTicket.Text = Nothing
        TxtName.Text = Nothing
        TxtAddress.Text = Nothing
        TxtPhone.Text = Nothing



        Me.Hide()


    End Sub


End Class