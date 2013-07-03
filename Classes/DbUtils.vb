Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SQLite

Public Class DbUtils

    Public Shared Function ExecuteReader(conn As SQLiteConnection, query As string) As DataTable

        Dim dt As DataTable = New DataTable

        Using cmd As SQLiteCommand = new SQLiteCommand(conn)

            cmd.CommandText = query

            Using reader As SQLiteDataReader = cmd.ExecuteReader
			    dt.Load(reader)
            End Using
        End Using

        Return dt
    End Function
    
	 Public Shared Sub ExecuteNonQuery(conn As SQLiteConnection, query As string)

        Using cmd As SQLiteCommand = new SQLiteCommand(query,conn)
			cmd.ExecuteNonQuery
        End Using
    End Sub

End Class
