''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' helper class used to fill combo boxes with name, value pairs
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class GenericListItem(Of T)
    Private mText As String
    Private mValue As T

    Public Sub New(ByVal pText As String, ByVal pValue As T)
        mText = pText
        mValue = pValue
    End Sub

    Public ReadOnly Property Text() As String
        Get
            Return mText
        End Get
    End Property

    Public ReadOnly Property Value() As T
        Get
            Return mValue
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return mText
    End Function
End Class