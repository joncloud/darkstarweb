using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Dsp.Web.Characters
{
    public class Character
    {
        #region Properties

        public long AccountId { get; set; }

        public bool CanUnstuck
        {
            get { return !Online && Owned && !InJail; }
        }

        [JsonIgnore]
        public character_exp character_exp
        {
            get { return _character_exp; }
            set
            {
                _character_exp = value;
                if (Jobs == null) { Jobs = new Dictionary<string, Job>(); }

                if (value != null)
                {
                    GetOrAdd("BLM").Experience = value.blm;
                    GetOrAdd("BLM").Experience = value.blm;
                    GetOrAdd("BLU").Experience = value.blu;
                    GetOrAdd("BRD").Experience = value.brd;
                    GetOrAdd("BST").Experience = value.bst;
                    GetOrAdd("COR").Experience = value.cor;
                    GetOrAdd("DNC").Experience = value.dnc;
                    GetOrAdd("DRG").Experience = value.drg;
                    GetOrAdd("DRK").Experience = value.drk;
                    GetOrAdd("GEO").Experience = value.geo;
                    GetOrAdd("MNK").Experience = value.mnk;
                    GetOrAdd("NIN").Experience = value.nin;
                    GetOrAdd("PLD").Experience = value.pld;
                    GetOrAdd("PUP").Experience = value.pup;
                    GetOrAdd("RDM").Experience = value.rdm;
                    GetOrAdd("RNG").Experience = value.rng;
                    GetOrAdd("RUN").Experience = value.run;
                    GetOrAdd("SAM").Experience = value.sam;
                    GetOrAdd("SCH").Experience = value.sch;
                    GetOrAdd("SMN").Experience = value.smn;
                    GetOrAdd("THF").Experience = value.thf;
                    GetOrAdd("WAR").Experience = value.war;
                    GetOrAdd("WHM").Experience = value.whm;
                }
            }
        }
        private character_exp _character_exp;

        [JsonIgnore]
        public character_job character_job
        {
            get { return _character_job; }
            set
            {
                _character_job = value;
                if (Jobs == null) { Jobs = new Dictionary<string, Job>(); }

                if (value != null)
                {
                    GetOrAdd("BLM").Level = value.blm;
                    GetOrAdd("BLM").Level = value.blm;
                    GetOrAdd("BLU").Level = value.blu;
                    GetOrAdd("BRD").Level = value.brd;
                    GetOrAdd("BST").Level = value.bst;
                    GetOrAdd("COR").Level = value.cor;
                    GetOrAdd("DNC").Level = value.dnc;
                    GetOrAdd("DRG").Level = value.drg;
                    GetOrAdd("DRK").Level = value.drk;
                    GetOrAdd("GEO").Level = value.geo;
                    GetOrAdd("MNK").Level = value.mnk;
                    GetOrAdd("NIN").Level = value.nin;
                    GetOrAdd("PLD").Level = value.pld;
                    GetOrAdd("PUP").Level = value.pup;
                    GetOrAdd("RDM").Level = value.rdm;
                    GetOrAdd("RNG").Level = value.rng;
                    GetOrAdd("RUN").Level = value.run;
                    GetOrAdd("SAM").Level = value.sam;
                    GetOrAdd("SCH").Level = value.sch;
                    GetOrAdd("SMN").Level = value.smn;
                    GetOrAdd("THF").Level = value.thf;
                    GetOrAdd("WAR").Level = value.war;
                    GetOrAdd("WHM").Level = value.whm;
                }
            }
        }
        private character_job _character_job;
        public long CharacterId { get; set; }
        public string CurrentZoneName { get; set; }
        public string HomeZoneName { get; set; }
        
        [JsonIgnore]
        public bool InJail { get; set; }

        public bool Online { get; set; }
        public bool Owned { get; set; }

        public Dictionary<string, Job> Jobs { get; set; }
        public string Name { get; set; }

        #endregion

        #region Methods

        private Job GetOrAdd(string name)
        {
            Job job;
            if (!Jobs.TryGetValue(name, out job))
            {
                job = new Job();
                Jobs.Add(name, job);
            }

            return job;
        }

        #endregion
    }
}