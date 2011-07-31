Public Class ProtoImage
    Inherits ProtoXChildBase


    Private _FolderPath As String
    Public Property FolderPath As String
        Get
            If _FolderPath Is Nothing Then
                If TypeOf ParentClass Is ProtoFile Then
                    If ParentClass.NodeName = "tvshow" Then
                        Dim TempFile As ProtoFile = ParentClass
                        _FolderPath = TempFile.FolderPath
                    End If
                End If
            End If
            Return _FolderPath
        End Get
        Set(ByVal value As String)
            _FolderPath = value
        End Set
    End Property

    Private _FileName As String
    Public Property FileName As String
        Get
            Return _FileName
        End Get
        Set(value As String)
            _FileName = value
        End Set
    End Property



    'Private _PathOveride As String = Nothing
    Public Property Path As String
        Get
            'If _PathOveride IsNot Nothing Then Return _PathOveride

            Dim TempPath As String
            If Me.FolderPath Is Nothing OrElse Me.FileName Is Nothing Then Return Nothing

            TempPath = IO.Path.Combine(Me.FolderPath, Me.FileName)

            If IO.File.Exists(TempPath) Then
                Return TempPath
            End If

            Return Nothing
        End Get
        Set(ByVal value As String)
            '_PathOveride = value
            Me.FileName = IO.Path.GetFileName(value)
            Me.FolderPath = value.Replace(Me.FileName, "")
            'If IO.File.Exists(value) Then
            '    _Image = Drawing.Bitmap.FromFile(value)
            'End If
        End Set
    End Property

    Public Property DefaultPath As String

    Private _Image As Drawing.Image
    Public Property Image As Drawing.Image
        Get
            If _Image Is Nothing Then
                If IO.File.Exists(Me.Path) Then
                    _Image = Drawing.Bitmap.FromFile(Me.Path)
                Else
                    _Image = Drawing.Bitmap.FromFile(Me.DefaultPath)
                End If
            End If
            Return _Image
        End Get
        Set(ByVal value As Drawing.Image)
            _Image = value
            Try
                _Image.Save(Path)
            Catch
            End Try
        End Set
    End Property

    Public Sub New(ByRef ParentClass As IProtoXBase, ByVal TagNameNotImplimented As String, ByVal DefaultPath As String)
        Me.ParentClass = ParentClass
        If TypeOf ParentClass Is ProtoFile Then
            Dim TempFile As ProtoFile = ParentClass
            Me.FolderPath = TempFile.FolderPath
        End If
        Me.DefaultPath = DefaultPath
    End Sub

    Public ReadOnly Property Exists As Boolean
        Get
            Return IO.File.Exists(Me.FileName)
        End Get
    End Property

    Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)

    End Sub

    Public Overrides Function CreateNew() As ProtoXChildBase
        Return Nothing
    End Function
End Class
