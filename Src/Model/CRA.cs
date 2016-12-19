using System;
using System.Collections.Generic;

namespace sonet.cra.Model
{

    public class CRA
{
    public int Id { get; set; }
    public DateTime Month { get; set; }
    public int IdCol { get; set; }
    public bool HasPrev { get; set; }
    public bool HasNext { get; set; }
    
     public Dictionary<int, Mission> Missions { get; set; }
  
}

    public class Mission
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int IdClient { get; set; }
        public string Client { get; set; }
          

    }
}
