Public Class clsTypeVariables

    Public Enum COMBOBOX_STATE As SByte
        adStateGenderMode
        adStateStatusMode
    End Enum

    Public Enum DATA_PROVIDER As SByte
        Oracle
        SqlServer
        OleDB
        ODBC
    End Enum

    Public Enum FILTER_STATE As SByte
        adStateFieldMode
        adStateDateMode
    End Enum

    Public Enum FORM_STATE As SByte
        adStateAddMode
        adStateEditMode
    End Enum

    Public Enum LOADING_STATE As SByte
        Saving
        Loading
    End Enum

    Public Enum PC_STATE As SByte
        TurnOff
        Restart
        Logoff
        Sleep
    End Enum

    Public Enum SealedKey
        ALT = 262144
        CTRL = 131072
        SHIFT = 65536
    End Enum

    Public Structure LVHITTESTINFO

        Public lFlags As Long
        Public lItem As Long
        Public lSubItem As Long
        Public pt As clsTypeVariables.POINTAPI

    End Structure

    Public Structure POINTAPI

        Public x As Long
        Public y As Long

    End Structure

    Public Structure Sys_Company

        Public Company_Address As String
        Public Company_Name As String
        Public Company_No As String

    End Structure

    Public Structure Sys_Database

        Public dbPassword As String
        Public dbServer As String
        Public dbUsername As String

    End Structure

    Public Structure USER_INFO

        Public USER_FULLNAME As String
        Public USER_LEVEL As Integer
        Public USER_NAME As String
        Public USER_STATUS As Integer

    End Structure

    Public Class TypeSysLogin

        Public Active As Boolean
        Public ID As Integer
        Public Name As String
        Public Password As String
        Public Rights As String()
        Public Username As String

        Public Sub New()
            Rights = New String(99) {}
        End Sub

    End Class ' class TypeSysLogin

    Public Sub New()
    End Sub

End Class ' class clsTypeVariables

