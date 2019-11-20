using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SearchSiteManager.Dto
{
    [DataContract]
    public class SearchResultViewDto
    {

        [DataMember]
        public Guid ResultId { get; set; }

        [DataMember]
        public string ResultTitle{ get; set; }

        [DataMember]
        public string LinkURL { get; set; }

        [DataMember]
        public string ResultText { get; set; }

        
    }
}
