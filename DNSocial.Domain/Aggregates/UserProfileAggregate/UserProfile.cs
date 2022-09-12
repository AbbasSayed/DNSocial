using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Domain.Aggregates.UserProfileAggregate
{
    public class UserProfile
    {
        private UserProfile()
        {
        }
        public Guid UserProfileId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public string IdentityId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; private set; }

        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            var userProfile = new UserProfile() {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            return userProfile;
        }

        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            ModifiedAt = DateTime.UtcNow;
        }

    }
}
