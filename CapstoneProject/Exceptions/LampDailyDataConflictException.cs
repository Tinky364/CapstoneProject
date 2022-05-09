using System;
using CapstoneProject.Models;

namespace CapstoneProject.Exceptions;

public class LampDailyDataConflictException : Exception
{
    public LampDailyData ExistingLampDailyData { get; }
    public LampDailyData IncomingLampDailyData { get; }

    public LampDailyDataConflictException(
        LampDailyData existingLampDailyData, LampDailyData incomingLampDailyData
    )
    {
        ExistingLampDailyData = existingLampDailyData;
        IncomingLampDailyData = incomingLampDailyData;
    }

    public LampDailyDataConflictException(
        string message, LampDailyData existingLampDailyData, LampDailyData incomingLampDailyData
    ) : base(message)
    {
        ExistingLampDailyData = existingLampDailyData;
        IncomingLampDailyData = incomingLampDailyData;
    }

    public LampDailyDataConflictException(
        string message, Exception innerException, LampDailyData existingLampDailyData,
        LampDailyData incomingLampDailyData
    ) : base(message, innerException)
    {
        ExistingLampDailyData = existingLampDailyData;
        IncomingLampDailyData = incomingLampDailyData;
    }
}
