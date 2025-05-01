'Author: Hehea Lino
'Organization: His Majesty's Armed Forces
'Unit: CIT
'Description: This raffle draw application was created for the Catholic Congregation in Fua'amotu, Tonga.
'It should conduct the following:
'       :- accept excel file containing ticket numbers
'       :- randomize seletion of a ticket number
'       :- display the selected ticket number and winner
'       :- store the selected ticket numbers in a text file

Imports System.Runtime.CompilerServices
Imports System.Runtime.Remoting.Channels
Imports System.Data.OleDb
Imports System.Security.Cryptography.X509Certificates
Imports System.Media

Public Class Main
    Public wName As String
    Public wAddress As String
    Public wPhone As String
    Public wTicket As String
    Dim shuffleLimit As Integer 'store number of times you can shuffle
    Public winningTicket As String  'store whole raffle ticket number
    Dim countdown As Integer    'store the countdown for the timers to stop one after the other 
    Dim digit1 As Integer      'store individual digits to be randomized
    Dim digit2 As Integer
    Dim digit3 As Integer
    Dim digit4 As Integer
    Dim digit5 As Integer
    Dim digit6 As Integer

    Dim sName As String     'store Name of winner
    Dim sAddress As String  'store Address of winner
    Dim sPhone As String    'store Phone number of winner
    Dim Msg, Style, Title, Response, MyString 'To display warning message when clearing data

    Dim blinktime As Integer = 0

    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data source=D:\names1.xlsx;Extended Properties=Excel 8.0")
    ' Define the list of prizes
    Dim prizes As New List(Of String) From {
    "1.TOYOTA VAN", "2.NGATU LAUNIMA NGATUGNGATU", "3.NGATU LAUNIMA PEPA",
    "4.PULU MO'UI", "5.PUAKA TOHO + CASH $500", "6.CASH $3,000",
    "7.CASH $2,000", "8.CASH $1,000 + TOKA 10 PEPA", "9.TOKA 20 PEPA + TA'OVALA PUTU",
    "10.CASH $1,000 + TOKA 10 PEPA", "11.TOKA 20 PEPA+FAKAHA'APAI+KAHOA SIZE 20", "12.PULU MO'UI",
    "13.HOOSI MO'UI", "14.CASH $1,000 + TOKA 10 PEPA", "15.PUAKA HULA + CASH $500", "16.CASH $2,000",
    "17.CASH $1,000 + TOKA 10 PEPA", "18.NGATU LAUNIMA PEPA", "19.PULU MO'UI", "20.CASH $3,000",
    "21.CASH $1,000 + TOKA 10 PEPA", "22.TOKA 20 PEPA+SALUSALU SIZE 20", "23.CASH $2,000",
    "24.CASH $1,000+TOKA 10 PEPA", "25.TOYOTA MINI CAR", "26.KIE TONGA 10FT+TOKA 10 PEPA",
    "27.PULU MO'UI", "28.NGATU LAUNIMA PEPA", "29.KAU'UFI TOKAMU'A+PUHA MOA(X5)",
    "30.CASH $1,000+TOKA 10 PEPA", "31.SHOPPING($1,000)-PTH", "32.IPHONE 12", "33.LAUNIMA NGATUNGATU",
    "34.KAU'UFI TOKAMU'A+PUHA SIPI(X2)", "35.NGATU LAUNIMA PEPA", "36.CASH $1,000+TOKA 10 PEPA",
    "37.CASH $1,000+TOKA 10 PEPA", "38.NGATU LAUNIMA PEPA", "39.TOKA 20 PEPA+CASH $500",
    "40.CASH $1,000+TOKA 10 PEPA", "41.NGATU LAUNIMA PEPA", "42.PULU MO'UI", "43.CASH $1,000+TOKA 10 PEPA",
    "44.TOKA 20 PEPA+KIE TONGA 6*3", "45.CASH $2,000", "46.NGATU LAUNIMA PEPA", "47.PULU MO'UI",
    "48.TA'ANGATUTU(X1) + ROLL PEPA KOKA'ANGA", "49.CASH $1,000+TOKA 10 PEPA", "50.TOYOTA MINIBUS",
    "51.CASH $1,000+TOKA 10 PEPA", "52.TOKA 20 PEPA+CASH $500", "53.PULU MO'UI", "54.CASH $1,000+TOKA 10 PEPA",
    "55.CASH $1,000+TOKA 10 PEPA", "56.'AISI FREEZER+PUHA MOA(X5)", "57.CASH $2,000",
    "58.NGATU LAUNIMA PEPA", "59.PULU MO'UI", "60.CASH $1,000+TOKA 10 PEPA", "61.NGATU LAUNIMA PEPA",
    "62.CASH $1,000+PUAKA TUNU(X2)", "63.CASH $1,000+PUAKA TUNU(X2)", "64.KIE TONGA 30FT", "65.TOYOTA MINI CAR",
    "66.CASH $1,000+PUHA PULU", "67. PULU MO'UI", "68.TOKA 20 PEPA+CASH $500", "69.CASH $2,000",
    "70.CASH $1,000+PUHA MOA(X3)", "71.CASH $1,000", "72.KAU'UFI TOKAMU'A+PUHAMOA(X5)",
    "73.TOKA 20 NGATUNGATU", "74. PULU MO'UI", "75.CASH $1,000", "76.KONGA KIE TONGA 6*3+TOKA 10 NGATUNGATU",
    "77.PULU MO'UI", "78.NGATU LAUNIMA PEPA", "79.CASH $1,000+TOKA 10 PEPA", "80.CASH $1,000+TOKA 10 PEPA"
    }

    Dim prizeIndex As Integer = 0 ' Track the current prize to be given



    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conn.Open()
            ' Check if the connection is open
            If conn.State = ConnectionState.Open Then
                MsgBox("Database connected successfully!")
            Else
                MsgBox("Database connection failed.")
            End If
        Catch ex As Exception
            MsgBox("Error opening database: " & ex.Message)
        End Try

        ' Disable all timers initially
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Timer4.Enabled = False
        Timer5.Enabled = False
        Timer6.Enabled = False
        Timer7.Enabled = False
    End Sub

    Private Sub BtnShuffle_Click(sender As Object, e As EventArgs) Handles BtnShuffle.Click
        'This should randomized the 5 digit number individually and stop one after the other
        'This should reduce the number of times it should be able to be clicked then when limit
        'is reached, the button should be disabled

        shuffleLimit = 25 'number of times the Shuffle button can be clicked
        countdown = 100

        ' Disable the Shuffle button to prevent multiple clicks during shuffle
        BtnShuffle.Enabled = False

        TextBox1.BackColor = Color.White
        TextBox2.BackColor = Color.White
        TextBox3.BackColor = Color.White
        TextBox4.BackColor = Color.White
        TextBox5.BackColor = Color.White
        TextBox6.BackColor = Color.White

        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Timer4.Enabled = True
        Timer5.Enabled = True
        Timer6.Enabled = True
        Timer7.Enabled = True

        Congratulations.TxtTicket.Text = ""
        Congratulations.TxtName.Text = ""
        Congratulations.TxtAddress.Text = ""
        Congratulations.TxtPhone.Text = ""

    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        'This should clear all numbers and enable the BtnShuffle
        'This should have a warning when clicked so as to not override everything accidentally

        Msg = "Are you sure you want to clear all?"    ' Define message.
        Style = vbYesNo Or vbCritical Or vbDefaultButton2    ' Define buttons.
        Title = "WARNING!"    ' Define title.
        Response = MsgBox(Msg, Style, Title) ' Display message.
        If Response = vbYes Then    ' User chose Yes.
            MyString = "Yes"    ' Perform some action.
            ClearAllBoxes()

        Else    ' User chose No.
            MyString = "No"    ' Perform some action.
            Exit Sub

        End If

    End Sub
    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Ensure the connection is closed when the form is closed
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
                MsgBox("Connection to Excel closed.")
            End If
        Catch ex As Exception
            MsgBox("Error closing connection: " & ex.Message)
        End Try
    End Sub
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        'This ends the program and closes the dialog box
        Msg = "Are you sure you want to quit this session?"    ' Define message.
        Style = vbYesNo Or vbCritical Or vbDefaultButton2    ' Define buttons.
        Title = "WARNING!"    ' Define title.
        Response = MsgBox(Msg, Style, Title) ' Display message.
        If Response = vbYes Then    ' User chose Yes.
            MyString = "Yes"    ' Perform some action.
            ' Close the connection to Excel
            Try
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    MsgBox("Connection to Excel closed.")
                End If
            Catch ex As Exception
                MsgBox("Error closing connection: " & ex.Message)
            End Try

            ' Exit the application
            End
        Else    ' User chose No.
            MyString = "No"    ' Perform some action.
            Exit Sub

        End If

    End Sub

    Private Sub BtnSeeWinners_Click(sender As Object, e As EventArgs) Handles BtnSeeWinners.Click
        'This should open a new dialog box that will list all the winners that were selected
        'by the random number

        DisplayWinner.Show()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'This timer should randomize the first digit only until 7
        Randomize()
        digit1 = Int(2 * Rnd())
        TextBox1.Text = digit1.ToString


    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'This timer should randomize the second digit until 9 only if first digit is not 7
        Randomize()
        digit2 = Int(10 * Rnd())
        TextBox2.Text = digit2.ToString


    End Sub



    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        'This timer should randomize the third digit until 9 only if first digit is not 7
        Randomize()
        digit3 = Int(10 * Rnd())
        TextBox3.Text = digit3.ToString


    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        'This timer should randomize the fourth digit until 9 only if first digit is not 7
        Randomize()
        digit4 = Int(10 * Rnd())
        TextBox4.Text = digit4.ToString

    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        'This timer should randomize the fifth digit until 9 only if the first digit is not 7
        Randomize()
        digit5 = Int(10 * Rnd())
        TextBox5.Text = digit5.ToString

    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        'This timer should randomize the last digit until 9 only if the first digit is not 7
        Randomize()
        digit6 = Int(10 * Rnd())
        TextBox6.Text = digit6.ToString
    End Sub
    Private Async Sub Timer7_Tick(sender As Object, e As EventArgs) Handles Timer7.Tick
        countdown -= 1 ' Decrementing the timer to stop the randomization process

        If countdown = 0 Then
            ' Disable the shuffle button so the user cannot shuffle again while the process is running
            BtnShuffle.Enabled = False

            ' Perform the database fetch and winner selection asynchronously
            Dim selectedTicket As String = Await Task.Run(Function() SelectWinner())

            ' Display the winner details in the Congratulations form
            If selectedTicket <> "" Then
                ' Call the function to show the winner in the Congratulations form
                ShowCongratulationsForm(selectedTicket)
            Else
                ' If no winner was found, reshuffle and try again
                MsgBox("No winner found. Shuffle again.")
                BtnShuffle.Enabled = True ' Re-enable shuffle
            End If

            ' Re-enable the shuffle button so the user can shuffle again after the process is complete
            BtnShuffle.Enabled = True
        End If
    End Sub
    Private Async Sub ShowCongratulationsForm(selectedTicket As String)
        ' Retrieve the winner's details based on the selected ticket number
        Dim cmd As New OleDbCommand("SELECT Name, Address, Phone FROM [List$] WHERE Ticket_No = ?", conn)
        cmd.Parameters.AddWithValue("?", selectedTicket)

        Try
            ' Execute the query to find the winner
            Using myreader As OleDbDataReader = cmd.ExecuteReader()
                If myreader.Read() Then
                    ' Store winner details
                    wName = myreader("Name")
                    wAddress = myreader("Address")
                    wPhone = myreader("Phone")

                    ' If the winner is not 'UNKNOWN', show details
                    If wName <> "UNKNOWN" Then
                        ' Ensure that there are still prizes available
                        If prizeIndex < prizes.Count Then
                            ' Assign the prize to the winner based on the current prizeIndex
                            Dim prize As String = prizes(prizeIndex)

                            ' Display the winner's details and prize in the Congratulations form
                            Dim congratulationsForm As New Congratulations()
                            congratulationsForm.TxtTicket.Text = selectedTicket
                            congratulationsForm.TxtName.Text = wName
                            congratulationsForm.TxtAddress.Text = wAddress
                            congratulationsForm.TxtPhone.Text = wPhone
                            congratulationsForm.TxtPrize.Text = prize ' Display the selected prize

                            ' Increment the prize index for the next winner
                            prizeIndex += 1

                            ' Stop all timers after showing the congratulations form
                            StopAllTimers()

                            ' Update the textboxes with the winning ticket number
                            UpdateWinningTicket(selectedTicket)
                            TextBox1.BackColor = Color.Aqua
                            TextBox2.BackColor = Color.Aqua
                            TextBox3.BackColor = Color.Aqua
                            TextBox4.BackColor = Color.Aqua
                            TextBox5.BackColor = Color.Aqua
                            TextBox6.BackColor = Color.Aqua

                            ' Show the Congratulations form after a short delay
                            Await Task.Delay(500) ' Add delay for visual effect
                            Await Task.Delay(500) ' Add delay for visual effect
                            congratulationsForm.ShowDialog()
                        Else
                            MsgBox("All prizes have been awarded!")
                        End If
                    Else
                        ' If the winner is UNKNOWN, reshuffle
                        MsgBox("This ticket is associated with an UNKNOWN entry. Reshuffling...")
                        BtnShuffle.PerformClick()
                    End If
                Else
                    ' If no winner is found for the selected ticket
                    MsgBox("No winner found for ticket number " & selectedTicket)
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error fetching winner data: " & ex.Message)
        End Try

        ' Re-enable the shuffle button
        BtnShuffle.Enabled = True
    End Sub


    Private Sub UpdateWinningTicket(ticket As String)
        ' Update the textboxes with the digits of the selected ticket
        TextBox1.Text = ticket(0).ToString()
        TextBox2.Text = ticket(1).ToString()
        TextBox3.Text = ticket(2).ToString()
        TextBox4.Text = ticket(3).ToString()
        TextBox5.Text = ticket(4).ToString()
        TextBox6.Text = ticket(5).ToString()

        ' Play the sound after the ticket is revealed
        PlayWinningSound()
    End Sub

    Private Sub BtnStop_Click(sender As Object, e As EventArgs) Handles BtnStop.Click
        StopAllTimers()
        ClearAllBoxes()
        BtnShuffle.Enabled = True




    End Sub

    Private Sub PlayWinningSound()
        Try
            ' Create a new instance of the SoundPlayer class
            Dim player As New SoundPlayer("D:\winner_sound.wav")

            ' Load and play the sound asynchronously
            player.LoadAsync()
            player.Play()
        Catch ex As Exception
            ' If an error occurs (like file not found), display an error message
            MsgBox("Error playing sound: " & ex.Message)
        End Try
    End Sub
    Private Function SelectWinner() As String
        ' Initialize a list to hold valid ticket numbers
        Dim validTickets As New List(Of String)()

        Try
            ' Fetch valid tickets from the database
            Dim cmd As New OleDbCommand("SELECT Ticket_No FROM [List$] WHERE Name <> 'UNKNOWN'", conn)
            Dim myreader As OleDbDataReader = cmd.ExecuteReader()

            ' Loop through the results and add valid ticket numbers to the list
            While myreader.Read()
                Dim ticketNo As String = myreader("Ticket_No").ToString().Trim()
                validTickets.Add(ticketNo)
            End While

            ' If there are valid tickets, randomly select one
            If validTickets.Count > 0 Then
                ' Create a new Random object to pick a random ticket
                Dim rnd As New Random()
                ' Pick a random ticket from the list
                Dim selectedTicket As String = validTickets(rnd.Next(validTickets.Count))
                Return selectedTicket
            Else
                ' If no valid tickets found, return an empty string
                Return String.Empty
            End If
        Catch ex As Exception
            MsgBox("Error selecting winner: " & ex.Message)
            Return String.Empty
        End Try
    End Function
    Private Sub StopAllTimers()
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Timer4.Enabled = False
        Timer5.Enabled = False
        Timer6.Enabled = False
        Timer7.Enabled = False
    End Sub
    Private Sub ClearAllBoxes()
        TextBox1.Text = ""
        TextBox1.BackColor = Color.White
        TextBox2.Text = ""
        TextBox2.BackColor = Color.White
        TextBox3.Text = ""
        TextBox3.BackColor = Color.White
        TextBox4.Text = ""
        TextBox4.BackColor = Color.White
        TextBox5.Text = ""
        TextBox5.BackColor = Color.White
        TextBox6.Text = ""
        TextBox6.BackColor = Color.White
    End Sub
    Private Async Sub ProcessWinner(selectedTicket As String)
        ' Debugging: Show the selected ticket before processing it in the database
        'MsgBox("Processing Winner for Ticket: " & selectedTicket)

        ' Now that the textboxes are updated, stop the timers immediately
        StopAllTimers()

        ' Concatenate all digits into one string for the winning ticket
        winningTicket = TextBox1.Text & TextBox2.Text & TextBox3.Text & TextBox4.Text & TextBox5.Text & TextBox6.Text

        ' Declare a command to retrieve the winner's information based on the selected ticket number
        Dim cmd As New OleDbCommand("SELECT Name, Address, Phone FROM [List$] WHERE Ticket_No = ?", conn)
        cmd.Parameters.AddWithValue("?", selectedTicket)

        ' Execute the query to find the winner
        Dim myreader As OleDbDataReader = cmd.ExecuteReader()

        If myreader.Read() Then
            ' If the winner's details are found, store them
            wName = myreader("Name")
            wAddress = myreader("Address")
            wPhone = myreader("Phone")

            ' Check if the winner is not 'UNKNOWN'
            If wName <> "UNKNOWN" Then

                ' Display the winner's details in the Congratulations form
                Dim congratulationsForm As New Congratulations()
                congratulationsForm.TxtTicket.Text = selectedTicket
                congratulationsForm.TxtName.Text = wName
                congratulationsForm.TxtAddress.Text = wAddress
                congratulationsForm.TxtPhone.Text = wPhone

                ' Convert the number to a string
                Dim ticketString As String = selectedTicket.ToString()
                ' Assign each digit to the corresponding TextBox and stop the timers for the textboxes

                TextBox1.Text = ticketString(0).ToString()  ' First digit
                TextBox2.Text = ticketString(1).ToString()  ' Second digit
                TextBox3.Text = ticketString(2).ToString()  ' Third digit
                TextBox4.Text = ticketString(3).ToString()  ' Fourth digit
                TextBox5.Text = ticketString(4).ToString()  ' Fifth digit
                TextBox6.Text = ticketString(5).ToString()  ' Sixth digit

                ' Introduce a delay before showing the form to show the individual digits
                Await Task.Delay(500) ' 2000 ms = 2 seconds (adjust as needed)

                congratulationsForm.ShowDialog() ' Show the Congratulations form
            Else
                ' If the winner is UNKNOWN, reshuffle and try again
                MsgBox("This ticket is associated with an UNKNOWN entry. Reshuffling...")
                BtnShuffle.PerformClick() ' Trigger a reshuffle
            End If
        Else
            ' If no winner is found for the selected ticket
            MsgBox("No winner found for ticket number " & selectedTicket)
        End If


        ' Re-enable the shuffle button for the user to click again
        BtnShuffle.Enabled = True

    End Sub

End Class
