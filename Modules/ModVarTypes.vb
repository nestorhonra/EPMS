Option Explicit On
Module ModVarTypes
    Public Enum FormState
        AddStateMode = 0
        EditStateMode = 1
        ViewStateMode = 2
    End Enum

    Public Structure USER_INFO
        Dim m_LoginName As String
        Dim m_Password As String
    End Structure

    Public Structure SETUP_INFO
        Dim m_LocationID As String
        Dim m_Description As String
        Dim m_Address As String
    End Structure
End Module
