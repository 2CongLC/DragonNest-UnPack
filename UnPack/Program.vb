Imports System
Imports System.IO
Imports System.IO.Compression
Imports System.Text


Module Program

    ' Đọc dữ liệu
    Private br As BinaryReader
    ' Nguồn dữ liệu
    Private source As String
    ' Lưu dữ liệu
    Private des As String


    Sub Main(args As String())

        'Kiểm tra đầu vào
        If args.Count = 0 Then
            Console.WriteLine("UnPack Tool 2024 - 2CongLC")
        Else
            source = args(0)
        End If

        'Đọc dữ liệu
        If IO.File.Exists(source) Then
            br = New BinaryReader(IO.File.OpenRead(source))




        End If

        Console.ReadLine()
    End Sub
End Module
