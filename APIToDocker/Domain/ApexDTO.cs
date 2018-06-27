using System;
namespace APIToDocker.Domain
{
    public class ApexDTO
    {
        public int AgencyID
        {
            get;
            set;
        }
    public string HostedURL
        {
            get;
            set;
        }    
   
        public string ConsumerURL
        {
            get;
            set;
        }

        public string ProductDescription
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }
        public Guid InstanceID
        {
            get;
            set;
        }
    }
}
