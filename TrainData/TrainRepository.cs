using CodeFirstTutorial;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainData
{
    public interface ITrainRepository
    {
        IEnumerable ListTrains();
        void DeleteAllTrains();
        void Delete(int id);
        void Edit(int id, string trainSymbol, int speed = 0, string description = "");
        Train GetTrain(int id);
        void Add(Train train, TrainStation trainStation);
    }
    public class TrainRepository : ITrainRepository
    {
        public IEnumerable ListTrains()
        {
            using (var db = new TrainContext())
            {

                var train = from x in db.Trains.AsNoTracking()
                             join y in db.TrainStations.AsNoTracking() on x.StationID equals y.StationID
                             select new
                             {
                                 x.TrainSymbol,
                                 x.Speed,
                                 x.Description,
                                 y.StationName,
                                 y.StationAddress
                             };

                return train.ToList();
            }

        }

        public Train GetTrain(int id)
        {
            using (var db = new TrainContext())
            {
                var train = db.Trains.FirstOrDefault(x => x.TrainID == id);
                    return train;
            }
        }

        public void DeleteAllTrains()
        {
            using (var db = new TrainContext())
            {
                db.Trains.ToList().ForEach(x => db.Trains.Remove(x));
                db.SaveChanges();
            }
        }
               
        public void Delete(int id)
        {
            using (var db = new TrainContext())
            {
                var trains = db.Trains.Where(x => x.TrainSymbol == "Mustafa Train");
                db.Trains.RemoveRange(trains);
                db.SaveChanges();
            }
        }
               
        public void Edit(int id, string trainSymbol, int speed = 0, string description = "")
        {
            using (var db = new TrainContext())
            {
                var train = db.Trains.FirstOrDefault(x => x.TrainID == id);

                if (train != null)
                {
                    train.TrainSymbol = trainSymbol;
                    train.Speed = speed;
                    train.Description = description;
                    db.SaveChanges();
                }
            }
        }
               
        public void Add(Train train, TrainStation trainStation)
        {
            using (var db = new TrainContext())
            {   
                if(!(db.Trains.Any(x => x.TrainSymbol == train.TrainSymbol) || db.TrainStations.Any(x => x.StationName == trainStation.StationName)))
                {
                    db.Trains.Add(new Train { TrainSymbol = train.TrainSymbol, Speed = train.Speed, Description = train.Description });
                    db.TrainStations.Add(new TrainStation { StationName = trainStation.StationName, StationAddress = trainStation.StationAddress });
                    db.SaveChanges();
                }
            }
        }
    }
}
