using System.Collections.Generic;
using NAudio.CoreAudioApi;

namespace StagePlayout.App.Services;

public static class AudioDeviceService
{
    public static List<(string id, string name)> GetOutputDevices()
    {
        var list = new List<(string id, string name)>();
        try
        {
            using var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            int idx = 0;
            foreach (var dev in devices)
            {
                list.Add((dev.ID, $"{dev.FriendlyName}"));
                idx++;
            }
        }
        catch { }
        return list;
    }
}
