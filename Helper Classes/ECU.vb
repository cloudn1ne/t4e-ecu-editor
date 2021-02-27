'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' this class is the main interface to any ECU functions
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class ECU
    Public Shared Adapter As New CAN2USB
    Public Shared AccessLevel As New ECUAccessLevel
    Public Shared Poller As New ECUPoller(Adapter)

    ' ECU Access Levels
    Enum ECUAccessLevel
        Unknown = 0
        KLINE_CAN = 1
        CANOnlyLocked = 2
        CANOnlyUnlocked = 3
    End Enum
End Class
