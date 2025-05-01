'This form should display all winners in the raffle
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class DisplayWinner
    Dim Msg, Style, Title, Response, MyString 'To display warning message when closing window
    Dim count As Integer
    Dim WithEvents saveTimer As New Timer() ' Set up a Timer that triggers at a regular interval

    Public Sub List_column()
        With WinnersList
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            'create columns in listview
            .Columns.Add("Pale:", 800)
            .Columns.Add("Fika Tikite", 200)
            .Columns.Add("Hingoa", 500)
            .Columns.Add("Feitu'u", 420)
            .Columns.Add("Fika Telefoni", 320)

        End With

    End Sub

    Private Sub DisplayWinner_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ' In the form's Load or Constructor, initialize the timer
        saveTimer.Interval = 5000 ' Save every 5 seconds (you can adjust this interval)
        saveTimer.Start()


        Call List_column()
        ' Load winners from the file when the form is loaded
        LoadWinnersFromFile()

    End Sub

    ' Event handler that saves to the text file periodically
    Private Sub SaveTimer_Tick(sender As Object, e As EventArgs) Handles saveTimer.Tick
        SaveWinnersToFile()
    End Sub

    Private Sub DisplayWinner_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Msg = "You will lose all stored winners. Are you sure you want to close this window?"
        Style = vbOKCancel Or vbCritical Or vbQuestion
        Title = "WARNING!"
        Response = MsgBox(Msg, Style, Title)
        If Response = vbCancel Then
            e.Cancel = True

        End If
        SaveWinnersToFile()

    End Sub
    Private Sub LoadWinnersFromFile()
        Dim filePath As String = "C:\winners.txt"

        If IO.File.Exists(filePath) Then
            Dim lines As String() = IO.File.ReadAllLines(filePath)
            For Each line As String In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    ' Assuming each line contains "Name, Prize"
                    Dim parts As String() = line.Split(","c)
                    If parts.Length >= 2 Then
                        Dim item As New ListViewItem(parts(0).Trim())  ' Winner's name
                        item.SubItems.Add(parts(1).Trim())              ' Prize
                        item.SubItems.Add(parts(2).Trim())
                        item.SubItems.Add(parts(3).Trim())
                        item.SubItems.Add(parts(4).Trim())
                        item.SubItems.Add(parts(5).Trim())
                        WinnersList.Items.Add(item)
                    End If
                End If
            Next
        End If
    End Sub
    ' In the DisplayWinner form, when saving the winner list
    Private Sub SaveWinnersToFile()
        Dim sw As New StreamWriter("D:\winners.txt", True)
        'Dim sw As String = "D:\winners.txt"
        'Dim winnerList As New List(Of String)

        ' Loop through the ListView and collect the winners
        'For Each item As ListViewItem In WinnersList.Items
        'winnerList.Add(item.Text & "," & item.SubItems(1).Text & "," & item.SubItems(2).Text & "," & item.SubItems(3).Text & "," & item.SubItems(4).Text) ' Assuming winner names are in the Text field
        'Next
        For Each item As ListViewItem In WinnersList.Items
            sw.WriteLine(item.Text & "," & item.SubItems(1).Text & "," & item.SubItems(2).Text & "," & item.SubItems(3).Text & "," & item.SubItems(4).Text)
        Next
        ' Write the winners to the text file
        'System.IO.File.WriteAllLines(sw, winnerList)
        sw.Close()
    End Sub
End Class