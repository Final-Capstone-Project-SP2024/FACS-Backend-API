using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.State
{
    public enum CacheType
    {
        //? how many people have vote the id
        VotingCount = 14,
        //? is have any voting 
        IsVoting = 11,
        
        //? Count hav
        Voting = 1,
        
        //? get the highest value of 5 voting 
        VotingLevel = 9,
        
        //? count value ?
        VotingValue = 12,

        //? Count fire notify have been send 
        FireNotify = 0,
        
        //? can action with the value smaller than the value before 
        ActionValue = 13, 
        
        /// <summary>
        /// Count alarm level 
        /// </summary>
        AlarmLevel1 = 2,
        AlarmLevel2 = 3,
        AlarmLevel3 = 4,
        AlarmLevel4 = 5,
        AlarmLevel5 = 6,
        FakeAlarm = 7,
        DisconnectCamera = 8,
        
        /// <summary>
        /// 
        /// </summary>
        Action = 10,
        
        
        IsFinish = 13,
        

    }
}
