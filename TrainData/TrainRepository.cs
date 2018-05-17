using CodeFirstTutorial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainData
{
    public interface ITrainRepository
    {
        IEnumerable<Train> ListTrains();
        void DeleteAllTrains();
        void Delete(int id);
        void Edit(string oldTrainSymbol, string trainSymbol, int speed = 0, string description = "");
        void Add(string trainSymbol, int speed, string stationName, string stationAddress, string description = "");
    }
    public class TrainRepository : ITrainRepository
    {
        public IEnumerable<Train> ListTrains()
        {
            using (var db = new TrainContext())
            {
                return db.Trains.AsNoTracking().ToList();
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
               
        public void Edit(string oldTrainSymbol, string trainSymbol, int speed = 0, string description = "")
        {
            using (var db = new TrainContext())
            {
                var train = db.Trains.FirstOrDefault(x => x.TrainSymbol == oldTrainSymbol);

                if (train != null)
                {
                    train.TrainSymbol = trainSymbol;
                    train.Speed = speed;
                    train.Description = description;
                    db.SaveChanges();
                }
            }
        }
               
        public void Add(string trainSymbol, int speed, string stationName, string stationAddress, string description = "")
        {
            using (var db = new TrainContext())
            {
                db.Trains.Add(new Train { TrainSymbol = trainSymbol, Speed = speed, Description = description });
                db.TrainStations.Add(new TrainStation { StationName = stationName, StationAddress = stationAddress });
                db.SaveChanges();
            }
        }
    }
}
