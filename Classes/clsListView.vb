Public Class clsListView
    Inherits clsVariables
    Public Sub ListView_CheckAllItems(ByRef lv As ListView)
        If lv.Items.Count > 0 Then
            Dim i As Integer = 1
            While i < lv.Items.Count
                lv.Items(i).Checked = True
                i = i + 1
            End While
        End If
    End Sub

    Public Function ListView_CheckCount(ByRef lv As ListView, ByVal sDisplay As String) As Boolean
        If lv.Items.Count > 0 Then
            Return True
        End If
        MessageBox.Show("No record to " + sDisplay, App_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return False
    End Function

    Public Sub ListView_CheckSelectedItems(ByRef lv As ListView)
        If lv.Items.Count > 0 Then
            Dim i As Integer = 1
            While i < lv.Items.Count
                If lv.Items(i).Selected Then
                    lv.Items(i).Checked = True
                End If
                i = i + 1
            End While
        End If
    End Sub

    Public Function ListView_CountCheckedItems(ByRef lv As ListView) As Long
        Dim lCount As Long = CLng(0)
        If lv.Items.Count > 0 Then
            Dim i As Integer = 1
            While i < lv.Items.Count
                If lv.Items(i).Checked Then
                    lCount = lCount + CLng(1)
                End If
                i = i + 1
            End While
        End If
        Return lCount
    End Function

    Public Function ListView_CountSearchItems(ByRef lv As ListView, ByVal colIndex As Integer, ByVal sValue As String) As Long
        Dim i As Integer

        Dim n As Long = CLng(0)
        If colIndex = 0 Then
            i = 1
            While i < lv.Items.Count
                If lv.Items(i).Text.CompareTo(sValue) = 0 Then
                    n = n + CLng(1)
                End If
                i = i + 1
            End While
        Else
            i = 1
            While i < lv.Items.Count
                If lv.Items(i).SubItems(colIndex).Equals(sValue) Then
                    n = n + CLng(1)
                End If
                i = i + 1
            End While
        End If
        Return n
    End Function

    Public Function ListView_CountSelectedItems(ByRef lv As ListView) As Long
        Dim lCount As Long = CLng(0)
        Dim i As Integer = 1
        While i < lv.Items.Count
            If lv.Items(i).Selected Then
                lCount = lCount + CLng(1)
            End If
            i = i + 1
        End While
        Return lCount
    End Function

    Public Function ListView_DistinctCount(ByRef lv As ListView, ByVal colIndex As Integer, ByVal FromChecked As Boolean) As Long
        Dim lCount As Long = CLng(0)
        If lv.Items.Count > 0 Then
            Dim Str1 As String = ""
            Dim i As Integer = 1
            While i < lv.Items.Count
                If (colIndex = 0) AndAlso Not FromChecked Then
                    GoTo label_1
                End If
                If lv.Items(i).Checked Then
                    If Str1.CompareTo(lv.Items(i).Text) <> 0 Then
                        Str1 = lv.Items(i).Text
                        lCount = lCount + CLng(1)
                        GoTo label_2
label_1:
                        If Str1.CompareTo(lv.Items(i).Text) <> 0 Then
                            Str1 = lv.Items(i).Text
                            lCount = lCount + CLng(1)
                            GoTo label_2
                            If FromChecked AndAlso (Not lv.Items(i).Checked OrElse (Str1.CompareTo(lv.Items(i).SubItems(colIndex)) <> 0)) Then
                                Str1 = lv.Items(i).SubItems(colIndex).ToString()
                                lCount = lCount + CLng(1)
                                GoTo label_2
                                If Str1.CompareTo(lv.Items(i).SubItems(colIndex)) <> 0 Then
                                    Str1 = lv.Items(i).Text
                                    lCount = lCount + CLng(1)
                                End If
                            End If
                        End If
                    End If
                End If
label_2:
                i = i + 1
            End While
        End If
        Return lCount
    End Function

    Public Function ListView_GetColumnLength(ByVal str As String) As Long
        Dim lReturn As Long

        If str.CompareTo("Name") = 0 Then
            lReturn = CLng(170)
        ElseIf (str.CompareTo("Address") = 0) OrElse (str.CompareTo("Remarks") = 0) OrElse (str.CompareTo("Notes") = 0) OrElse (str.CompareTo("Location") = 0) Then
            lReturn = CLng(250)
        ElseIf (str.CompareTo("Amount") = 0) OrElse (str.CompareTo("Price") = 0) OrElse (str.CompareTo("Date") = 0) OrElse (str.CompareTo("ContactNo") = 0) OrElse (str.CompareTo("Modified") = 0) Then
            lReturn = CLng(120)
        ElseIf (str.CompareTo("Description") = 0) OrElse (str.CompareTo("Brand") = 0) OrElse (str.CompareTo("Department") = 0) Then
            lReturn = CLng(150)
        ElseIf (str.CompareTo("Unit") = 0) OrElse (str.CompareTo("Qty") = 0) OrElse (str.CompareTo("ID") = 0) OrElse (str.CompareTo("EmployeeID") = 0) OrElse (str.CompareTo("Code") = 0) Then
            lReturn = CLng(100)
        ElseIf str.CompareTo("Email/Website") = 0 Then
            lReturn = CLng(300)
        Else
            lReturn = CLng(130)
        End If
        Return lReturn
    End Function

    Public Function ListView_HitTest(ByRef lv As ListView) As clsTypeVariables.LVHITTESTINFO
        Dim tHt As clsTypeVariables.LVHITTESTINFO

        Dim point1 As Point = lv.Location
        Dim lX As Long = CLng(point1.X) / CLng(15)
        Dim point2 As Point = lv.Location
        Dim lY As Long = CLng(point2.Y) / CLng(15)
        tHt.lFlags = CLng(0)
        tHt.lItem = CLng(0)
        tHt.lSubItem = CLng(0)
        tHt.pt.x = lX
        tHt.pt.y = lY
        Return tHt
    End Function

    Public Sub ListView_RemoveCheckedItems(ByVal lv As ListView)
        If lv.Items.Count > 0 Then
            Dim i As Integer = 1
            While i < lv.Items.Count
                If lv.Items(i).Checked Then
                    lv.Items.Remove(lv.SelectedItems(i))
                    i = 1
                End If
                i = i + 1
            End While
            If lv.Items.Count > 0 Then
                lv.Items(i).Selected = True
            End If
        End If
    End Sub

    Public Sub ListView_RemoveSelectedItems(ByVal lv As ListView)
        Dim i As Integer = 0
        If lv.Items.Count > 0 Then
            i = 1
            While i < lv.Items.Count
                If lv.Items(i).Selected Then
                    lv.Items.Remove(lv.SelectedItems(i))
                    i = 1
                End If
                i = i + 1
            End While
            If lv.Items.Count > 0 Then
                lv.Items(1).Selected = True
            End If
        End If
    End Sub

    Public Function ListView_SearchItem(ByRef lv As ListView, ByVal colIndex As Integer, ByVal sValue As String) As Integer
        Dim i As Integer

        Dim iReturn As Integer = 0
        If colIndex = 0 Then
            i = 0
            While i < lv.Items.Count
                If lv.Items(i).Text.CompareTo(sValue) = 0 Then
                    iReturn = i
                Else
                    i = i + 1
                End If
            End While
        Else
            i = 0
            While i < lv.Items.Count
                If sValue.CompareTo(lv.Items(i).SubItems(colIndex)) = 0 Then
                    iReturn = i
                    Exit While
                End If
                i = i + 1
            End While
        End If
        Return iReturn
    End Function

    Public Sub ListView_SelectAllItems(ByRef lv As ListView)
        Dim i As Integer = 0
        While i < lv.Items.Count
            lv.Items(i).Selected = True
            i = i + 1
        End While
    End Sub

    Public Sub ListView_SortColumns(ByRef lv As ListView, ByVal colIndex As Integer)
        If lv.Items.Count > 0 Then
            If lv.Sorting = SortOrder.Ascending Then
                lv.Sorting = SortOrder.Descending
                Return
            End If
            lv.Sorting = SortOrder.Ascending
        End If
    End Sub

    Public Function ListView_SumAmount(ByRef lv As ListView, ByVal colIndex As Integer, ByVal bIsChecked As Boolean) As Decimal
        Dim lAmount As Decimal = 0D
        Dim i As Integer = 0
        While i < lv.Items.Count
            If Not bIsChecked Then
                If colIndex = 0 Then
                    lAmount = lAmount + Convert.ToDecimal(lv.Items(i).Text)
                Else
                    lAmount = lAmount + Convert.ToDecimal(lv.Items(i).SubItems(colIndex).Text)
                End If
            ElseIf lv.Items(i).Checked Then
                lAmount = lAmount + Convert.ToDecimal(lv.Items(i).SubItems(colIndex).Text)
            End If
            i = i + 1
        End While
        Return lAmount
    End Function

    Public Sub ListView_UnCheckAllItems(ByRef lv As ListView)
        If lv.Items.Count > 0 Then
            Dim i As Integer = 1
            While i < lv.Items.Count
                lv.Items(i).Checked = False
                i = i + 1
            End While
        End If
    End Sub

    Public Sub ListView_UnCheckSelectedItems(ByRef lv As ListView)
        If lv.Items.Count > 0 Then
            Dim i As Integer = 1
            While i < lv.Items.Count
                If lv.Items(i).Selected Then
                    lv.Items(i).Checked = False
                End If
                i = i + 1
            End While
        End If
    End Sub

End Class ' class clsListView

