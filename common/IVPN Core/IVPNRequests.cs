﻿using System;
using IVPN.VpnProtocols;
using IVPN.VpnProtocols.OpenVPN;
using IVPN.VpnProtocols.WireGuard;
    
namespace IVPN.Requests
{
    [Serializable]
    public abstract class Request
    {
        public string Command => GetType().Name;
    }

    public class Hello : Request
    {
        public string Version;
    }

    /// <summary>
    /// Request service to remove service-crash file
    /// (Platform.ServiceCrashInfoFilePath)
    /// 
    /// Client itseld can not remove it, because file was creaded by service with admin rights
    /// </summary>
    public class RemoveServiceCrashFile : Request {}

    /// <summary>
    /// Ping servers and return ping-time for each server.
    /// 
    /// We need to do it from privilaged mode (agent\service)
    /// because there is a problems of using 'Ping' class from user mode for macOS (Mono implementation limitation)
    /// </summary>
    public class PingServers : Request 
    {
        /// <summary>
        /// Count tells pinger to stop after sending (and receiving) Count echo packets.
        /// </summary>
        public int RetryCount;

        /// <summary>
        /// Timeout specifies a timeout before ping exits, regardless of how many packets have been received.
        /// </summary>
        public int TimeOutMs;
    }

    public class Connect : Request
    {
        public VpnType VpnType; 

        public WireGuardConnectionParameters WireGuardParameters;

        public OpenVPNConnectionParameters OpenVpnParameters;
        
        public string CurrentDNS;

        public override string ToString()
        {
            return $"{base.ToString()} type={VpnType}; OpeVpnPrarams=({OpenVpnParameters}); WireGuardParams=({WireGuardParameters})";
        }
    }

    public class Disconnect : Request    {}

    public class KillSwitchGetStatus : Request    {}

    public class KillSwitchSetEnabled : Request
    {
        public bool IsEnabled;
    }

    public class KillSwitchSetAllowLAN : Request
    {
        public bool AllowLAN;
    }

    public class KillSwitchSetAllowLANMulticast : Request
    {
        public bool AllowLANMulticast;
    }

    public class KillSwitchSetIsPersistent : Request
    {
        public bool IsPersistent;
    }

    public class KillSwitchGetIsPestistent : Request    {}

    public class SecurityPolicyAction : Request    {}

    public class SetPreference : Request
    {
        public string Key;

        public string Value;
    }

    public class GenerateDiagnostics : Request
    {
        public VpnType VpnProtocolType;
    }

    public class PauseConnection : Request {}

    public class ResumeConnection : Request {}

    public class SetAlternateDns : Request
    {
        /// <summary>
        /// if DNS == IPAddress.None (255.255.255.255) -> disable alternate DNS
        /// </summary>
        public string DNS; 
    }
}