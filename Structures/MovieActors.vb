
Public Structure MovieActors
    Public actorname As String
    Public actorrole As String
    Public actorthumb As String
    Public actorid As String

    Shared Widening Operator CType(ByVal Input As Nfo.Actor) As MovieActors
        Dim Temp As New MovieActors

        Temp.actorid = Input.ActorId.Value
        Temp.actorname = Input.Name.Value
        Temp.actorrole = Input.Role.Value
        Temp.actorthumb = Input.Thumb.Value

        Return Temp
    End Operator

    Shared Widening Operator CType(ByVal Input As MovieActors) As Nfo.Actor
        Dim Temp As New Nfo.Actor

        Temp.ActorId.Value = Input.actorid
        Temp.Name.Value = Input.actorname
        Temp.Role.Value = Input.actorrole
        Temp.Thumb.Value = Input.actorthumb

        Return Temp
    End Operator
End Structure
