Imports System.IO.Ports
Public Class Form1
    Private WithEvents serialPort As New SerialPort()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'serialPort.PortName = "COM4"
        serialPort.BaudRate = 9600
        serialPort.Parity = Parity.None
        serialPort.DataBits = 8
        serialPort.StopBits = StopBits.One

        Try
            ' Obtén la lista de puertos serie disponibles
            Dim availablePorts() As String = SerialPort.GetPortNames()

            ' Verifica si hay al menos un puerto disponible
            If availablePorts.Length > 0 Then
                ' Usa el primer puerto disponible
                serialPort.PortName = availablePorts(0)
                ' Abre la conexión serie
                serialPort.Open()

                lblPortName.Text = "Puerto Serie: " & serialPort.PortName
            Else
                MessageBox.Show("No se encontraron puertos serie disponibles.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error al abrir el puerto serie: " & ex.Message)
        End Try
    End Sub

    Private Sub SerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles serialPort.DataReceived
        Dim data As String = serialPort.ReadExisting()

        If TextBox1.InvokeRequired Then
            TextBox1.Invoke(Sub() TextBox1.Text = "Datos recibidos: " & data)
        Else
            TextBox1.Text = "Datos recibidos: " & data
        End If

        Console.WriteLine("Datos recibidos: " & data)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If serialPort.IsOpen Then
            serialPort.Close()
        End If
    End Sub
End Class
