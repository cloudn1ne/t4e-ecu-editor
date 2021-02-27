'****************************************
'* Search in TreeView controls
'****************************************

Public Class TVSearch
    Private tv As TreeView

    Public Sub New(ByRef t As TreeView)
        Me.tv = t
    End Sub

    Public Sub FindByText(ByVal txt As String)
        Dim nodes As TreeNodeCollection = tv.Nodes
        Dim n As TreeNode
        For Each n In nodes            
            FindRecursive(n, txt)
        Next
    End Sub

    Private Sub FindRecursive(ByRef tNode As TreeNode, ByVal txt As String)

        Dim tn As TreeNode
        For Each tn In tNode.Nodes
            ' if the text properties match, color the item
            If tn.Text.ToLower.Contains(txt.ToLower) Then
                tn.BackColor = Color.Yellow
            Else
                tn.BackColor = Color.White
            End If
            FindRecursive(tn, txt)
        Next
    End Sub
End Class
