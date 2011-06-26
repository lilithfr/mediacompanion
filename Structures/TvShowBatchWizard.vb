
Public Structure str_TvShowBatchWizard
    Dim shYear As Boolean
    Dim shRating As Boolean
    Dim shPlot As Boolean
    Dim shRuntime As Boolean
    Dim shMpaa As Boolean
    Dim shGenre As Boolean
    Dim shStudio As Boolean
    Dim shActor As Boolean
    Dim shPosters As Boolean
    Dim shFanart As Boolean
    Dim epStreamDetails As Boolean
    Dim epAired As Boolean
    Dim epPlot As Boolean
    Dim epDirector As Boolean
    Dim epCredits As Boolean
    Dim epRating As Boolean
    Dim epRuntime As Boolean
    Dim epActor As Boolean
    Dim epScreenshot As Boolean
    Dim epCreateScreenshot As Boolean
    Dim includeLocked As Boolean
    Dim activate As Boolean
    Dim doEpisodes As Boolean
    Dim doShows As Boolean
    Dim doShowBody As Boolean
    Dim doShowArt As Boolean
    Dim doShowActors As Boolean
    Dim doEpisodeBody As Boolean
    Dim doEpisodeArt As Boolean
    Dim doEpisodeActors As Boolean
    Dim doEpisodeMediaTags As Boolean
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        shYear = False
        shRating = False
        shPlot = False
        shRuntime = False
        shMpaa = False
        shGenre = False
        shStudio = False
        shActor = False
        shPosters = False
        shFanart = False
        epStreamDetails = False
        epAired = False
        epPlot = False
        epDirector = False
        epCredits = False
        epRating = False
        epRuntime = False
        epActor = False
        epScreenshot = False
        epCreateScreenshot = False
        includeLocked = False
        activate = False
        doEpisodes = False
        doShows = False
        doShowBody = False
        doShowArt = False
        doShowActors = False
        doEpisodeBody = False
        doEpisodeArt = False
        doEpisodeActors = False
        doEpisodeMediaTags = False
    End Sub
End Structure
