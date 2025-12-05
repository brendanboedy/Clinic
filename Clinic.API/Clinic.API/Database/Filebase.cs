using Clinic.Library.Models;
using Newtonsoft.Json;

namespace Clinic.API.Database
{
    public class Filebase
    {
        private string _root;
        private string _appointmentRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = "/Users/brendanboedy/temp";
            _appointmentRoot = $"{_root}/Appointments";
        }

        public int LastAppointmentKey
        {
            get
            {
                if (Appointments.Any())
                {
                    return Appointments.Select(x => x.ID).Max();
                }
                return 0;
            }
        }

        public Appointment AddOrUpdate(Appointment appointment)
        {
            //set up a new Id if one doesn't already exist
            if(appointment.ID <= 0)
            {
                appointment.ID = LastAppointmentKey + 1;
            }

            //go to the right place
            string path = $"{_appointmentRoot}/{appointment.ID}.json";
            

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(appointment));

            //return the item, which now has an id
            return appointment;
        }
        
        public List<Appointment> Appointments
        {
            get
            {
                var root = new DirectoryInfo(_appointmentRoot);
                var _appointments = new List<Appointment>();
                foreach(var patientFile in root.GetFiles())
                {
                    var patient = JsonConvert
                        .DeserializeObject<Appointment>
                        (File.ReadAllText(patientFile.FullName));
                    if(patient != null)
                    {
                        _appointments.Add(patient);
                    }

                }
                return _appointments;
            }
        }


        public bool Delete(string type, string id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            return true;
        }
    } 
}