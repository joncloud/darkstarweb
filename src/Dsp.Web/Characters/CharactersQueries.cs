using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Characters
{
    public static class CharactersQueries
    {
        public static IQueryable<Character> ToCharacter(this IQueryable<character> query, long ownerAccountId)
        {
            return query.Select(c => new Character
            {
                AccountId = c.accid,
                character_exp = c.character_exp,
                character_job = c.character_job,
                CharacterId = c.charid,
                CurrentZoneName = c.pos_zone.name,
                HomeZoneName = c.home_zone.name,
                InJail = c.pos_zone_id == ZoneIds.MordionGaol,
                Name = c.charname,
                Online = c.account_sessions.Any(),
                Owned = (ownerAccountId == c.accid)
            });
        }

        public static IQueryable<Character> ToPartialCharacter(this IQueryable<character> query, long ownerAccountId)
        {
            return query.Select(c => new Character
            {
                AccountId = c.accid,
                CharacterId = c.charid,
                CurrentZoneName = c.pos_zone.name,
                HomeZoneName = c.home_zone.name,
                InJail = c.pos_zone_id == ZoneIds.MordionGaol,
                Name = c.charname,
                Online = c.account_sessions.Any(),
                Owned = (ownerAccountId == c.accid)
            });
        }
    }
}