using Dsp.Web.Accounting;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dsp.Web.Characters
{
    public class ResetLocationAction : IControllerAction
    {
        private readonly DspContext _context;

        public ResetLocationAction(DspContext context)
        {
            _context = context;
        }

        public long CharacterId { get; set; }
        public string IPAddress { get; set; }
        public ResetLocation Location { get; set; }
        public long OwnerAccountId { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            // Find the character for the current account.
            character character = await _context.accounts.Where(a => a.id == OwnerAccountId)
                .SelectMany(a => a.characters)
                .SingleOrDefaultAsync(c => c.charid == CharacterId);

            if (character == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Check to see if the character is online.
            bool online = await _context.account_sessions.AnyAsync(a => a.accid == OwnerAccountId &&
                                                                        a.charid == character.charid);

            // Do not let the location change if the character is online, because the value 
            // is just going to be overwritten when the client re-zones the character.
            if (online)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            if (character.pos_zone_id == ZoneIds.MordionGaol)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            using (AuditActivityAction auditor = new AuditActivityAction())
            {
                auditor.AccountId = OwnerAccountId;
                auditor.Description = "Unstuck";
                auditor.IPAddress = IPAddress;

                auditor.AddChange("From", new
                {
                    character.pos_location.rot,
                    character.pos_location.x,
                    character.pos_location.y,
                    character.pos_location.z,
                    character.pos_zone_id
                });

                switch (Location.Type)
                {
                    case ResetLocationType.Home:
                        // TODO Security risk - Limit the number of times to 
                        // "unstuck" the character to their home location.
                        character.pos_location = new character_location
                        {
                            rot = character.home_location.rot,
                            x = character.home_location.x,
                            y = character.home_location.y,
                            z = character.home_location.z
                        };
                        character.pos_zone_id = character.home_zone_id;
                        break;
                    case ResetLocationType.RuludeGardens:
                        character.pos_location = new character_location
                        {
                            rot = 66,
                            x = 0,
                            y = 3,
                            z = 116
                        };
                        character.pos_zone_id = ZoneIds.RuludeGardens;
                        break;
                }

                auditor.AddChange("To", new
                {
                    character.pos_location.rot,
                    character.pos_location.x,
                    character.pos_location.y,
                    character.pos_location.z,
                    character.pos_zone_id
                });

                await _context.SaveChangesAsync();


                await auditor.Execute();
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}