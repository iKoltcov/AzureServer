using System;

namespace Server.Services.Implementation
{
    public class InstanceService : IInstanceService
    {
        protected static volatile string InstanceId;

        private static readonly object LockObject = new object();
        
        public InstanceService()
        {
        }

        public string GetInstanceId()
        {
            if (InstanceId == null)
            {
                lock (LockObject)
                {
                    if (InstanceId == null)
                    {
                        InstanceId = Guid.NewGuid().ToString();
                    }
                }
            }

            return InstanceId;
        }
    }
}