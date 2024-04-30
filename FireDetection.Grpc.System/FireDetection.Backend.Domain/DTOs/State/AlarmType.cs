﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.State
{
    public static class AlarmType
    {

        public static string FireDetectNotify = "Fire Detect Notify";
        public static string Level1 = "Alarm level 1";
        public static string Level2 = "Alarm level 2";
        public static string Level3 = "Alarm Level 3";
        public static string Level4 = "Alarm Level 4";
        public static string Level5 = "Alarm Level 5";
        public static string FakeAlarm = "Fake Alarm";
        public static string Voting = "Voting";
        public static string DisconnectCamera = "Disconnect camera";
        public static string Remind = "Action Remind";


    }
}
