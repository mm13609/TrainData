using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstTutorial
{
    public class TrainContext : DbContext
    {
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainStation> TrainStations { get; set; }
    }

   public class Train
   {
        [Key]
        public int TrainID { get; set; }
        public string TrainSymbol { get; set; }
        public int Speed { get; set; }
        public string Description { get; set; }

        public int StationID { get; set; }
        public TrainStation Station { get; set; }
    }

    public class TrainStation
    {
        [Key]
        public int StationID { get; set; }
        public string StationName { get; set; }
        public string StationAddress { get; set; }
        public virtual List<Train> Trains { get; set; }
    }
}
