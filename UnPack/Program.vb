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

            'Kiểm tra signature
            If New String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetChars(br.ReadBytes(&H100))).TrimEnd(ChrW(0), "ý"c) <> "EyedentityGames Packing File 0.1" Then
                Console.WriteLine("This is not an EyedentityGames Packing File.")
            End If

            Dim unknow As Int32 = br.ReadInt32
            Dim count As Int32 = br.ReadInt32
            Dim offset As Int32 = br.ReadInt32

            ' Khởi tạo và đọc dữ liệu Block đầu tiên
            br.BaseStream.Position = offset

            ' Đọc dữ liệu Block và lưu vào danh sách
            Dim subfiles As New List(Of FileData)
            For i As Int32 = 0 To count - 1
                subfiles.Add(New FileData)

                If subfiles(subfiles.Count - 1).size1 <> subfiles(subfiles.Count - 1).size2 Then
                    Console.WriteLine("Fuck!")
                End If
                br.BaseStream.Position += 44
            Next


            des = Path.GetDirectoryName(source) & "\" & Path.GetFileNameWithoutExtension(source)

            For Each fd As FileData In subfiles

                Console.WriteLine("File Offset : {0} - File Size : {1} - File Name : {2}", fd.offset, fd.size1, fd.name)

                br.BaseStream.Position = fd.offset

                Directory.CreateDirectory(des & "\" & Path.GetDirectoryName(fd.name))
                Dim buffer As Byte() = br.ReadBytes(fd.size1)
                Dim fp As String = des & "\" & fd.name.Replace("/", "\")

                Using bw As New BinaryWriter(File.Create(fp))
                    bw.Write(buffer)
                End Using
            Next

            ' Xong tiến trình
            br.Close()
            Console.WriteLine("unpack done!!!")
        End If

        Console.ReadLine()
    End Sub

    ' Cấu trúc dữ liệu của Block
    Class FileData
        Public name As String = New String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetChars(br.ReadBytes(&H100))).TrimEnd(ChrW(0), "ý"c)
        Public size1 As Int32 = br.ReadInt32
        Public unknown As Int32 = br.ReadInt32
        Public size2 As Int32 = br.ReadInt32
        Public offset As Int32 = br.ReadInt32
    End Class

End Module
