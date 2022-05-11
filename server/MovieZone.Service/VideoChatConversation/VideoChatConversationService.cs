namespace MovieZone.Service.VideoChatConversation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MovieZone.Domain.Entities;
    using MovieZone.Persistence.Common.Repositories;
    using MovieZone.Service.User;

    public class VideoChatConversationService : IVideoChatConversationService
    {
        private readonly IUserService userService;
        private readonly IDeletableEntityRepository<VideoChatConversation> videoChatConversationRepository;

        public VideoChatConversationService(
            IDeletableEntityRepository<VideoChatConversation> videoChatConversationRepository,
            IUserService userService)
        {
            this.videoChatConversationRepository = videoChatConversationRepository;
            this.userService = userService;
        }

        public string GetConversationId(string[] participants)
        {
            return this.videoChatConversationRepository
                .AllAsNoTracking()
                .Where(x => participants.Length == x.Participants.Count)
                .Where(x => x.Participants.All(p => participants.Any(x => p.Id == x)))
                .Select(x => x.Id)
                .SingleOrDefault();
        }

        public async Task<string> CreateConversationAsync(string[] participantsId)
        {
            var conversation = new VideoChatConversation();

            foreach (var participantId in participantsId)
            {
                var participant = this.userService.GetUserById(participantId);
                if (participant == null)
                {
                    throw new ArgumentException($"Participant with Id:{participantId} doesn't exist!");
                }

                conversation.Participants.Add(participant);
            }

            await this.videoChatConversationRepository.AddAsync(conversation);

            return conversation.Id;
        }
    }
}
