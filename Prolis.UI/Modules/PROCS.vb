Module PROCS

    Public SenderID As String = "B050005B-1D42-4C48-AD6B-EA00C82DC013"

    Public Function BuildISA(ByVal PayloadID As String, ByVal Userinfo() As String) As String
        'ISA*00*          *00*          *ZZ*P176M807       *ZZ*CMS            *140221*1921*^*00501*006516576*0*P*:~
        Dim ISA As String = "ISA*00*" & Space(10) & "*00*" & Space(10) & "*ZZ*" & Userinfo(0) &
        Space(15 - Len(Userinfo(0))) & "*ZZ*ABILITY" & Space(8) & "*" & Date.Now.ToString("yyMMdd") &
        "*" & Date.Now.ToString("HHmm") & "*^*00501*" & PayloadID & "*0*P*:~"
        Return ISA
    End Function

    Public Function BuildGS(ByVal UserInfo() As String) As String
        'GS*HS*P176M807*CMS*20140221*1921*1*X*005010X279A1~
        Dim GS As String = "GS*HS*" & UserInfo(0) & "*ABILITY*" & Date.Now.ToString("yyyyMMdd") &
        "*" & Date.Now.ToString("HHmm") & "*01*X*005010X279A1~"
        Return GS
    End Function

End Module
