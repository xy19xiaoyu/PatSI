Public Class Code
    Private Shared rm As New Random()
    ''' <summary>
    ''' 加密
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function encrypt(ByVal str As String) As String
        Dim retString As New System.Text.StringBuilder
        Dim ary() As Char = str.ToCharArray

        For i As Integer = 0 To ary.Length - 1
            retString.Append(EncryptChar(ary(i)))
        Next
        Return retString.ToString
    End Function
    ''' <summary>
    ''' 加密一个字符
    ''' </summary>
    ''' <param name="c"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncryptChar(ByVal c As Char) As String
        Dim rmd1 As Integer
        Dim rmd2 As Integer
        Dim rmd3 As Integer
        Dim sum As Integer

        rmd1 = rm.Next(1, 9)
        rmd2 = rm.Next(1, 9)
        rmd3 = rm.Next(1, 9)

        sum = Asc(c) + rmd1 + rmd2 + 17
        Dim s1, s2, s3, s4 As String
        s1 = Chr(sum).ToString
        s2 = Chr(Asc(rmd1) + 17)
        s3 = Chr(Asc(rmd2) + 17)
        s4 = Chr(Asc(rmd3) + 17)

        Return s1 & s2 & s3 & s4
    End Function
    ''' <summary>
    ''' 解密一个字符串 4个解密成一个
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Decryption(ByVal str As String) As Char
        If str.Length <> 4 Then
            Return String.Empty
        End If
        Dim chrs() As Char = str.ToCharArray
        Dim sum As Integer = Asc(chrs(0)) - 17
        Dim c1 As Integer = Val(Chr(Asc(chrs(1)) - 17))
        Dim c2 As Integer = Val(Chr(Asc(chrs(2)) - 17))
        Return Chr(sum - (c1 + c2))
    End Function
    ''' <summary>
    ''' 解密整个字符串
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DecryptionAll(ByVal str As String) As String
        If str.Length Mod 4 <> 0 Then
            Return String.Empty
        End If
        Dim strtmp As String
        Dim chrS() As Char = str.ToCharArray
        Dim strContent As New System.Text.StringBuilder
        For i As Integer = 0 To str.Length - 1 Step 4
            strtmp = chrS(i) & chrS(i + 1) & chrS(i + 2) & chrS(i + 3)
            strContent.Append(Decryption(strtmp))
        Next
        Return strContent.ToString
    End Function
End Class
