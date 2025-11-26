using System;
using Clinic.Library.Models;
using Clinic.Library.Utilities;
using Newtonsoft.Json;

namespace Clinic.Library.Services;

public class PhysicianServiceProxy
{
    private List<Physician?> physicianList { get; set; }
    private PhysicianServiceProxy()
    {
        physicianList = new List<Physician?>();
        var physiciansResponse = new WebRequestHandler().Get("/Physician").Result;
        if (physiciansResponse != null)
        {
            physicianList = JsonConvert.DeserializeObject<List<Physician?>>(physiciansResponse) ?? new List<Physician?>();
        }
    }

    private static PhysicianServiceProxy? instance;
    private static object instanceLock = new object();
    public static PhysicianServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PhysicianServiceProxy();
                }
                return instance;
            }
        }
    }

    public List<Physician?> PhysicianList
    {
        get { return physicianList; }
    }

    public async Task<Physician?> AddOrUpdate(Physician? physician)
    {
        //make sure physician not null
        if (physician == null)
        {
            return null;
        }

        var physicianPayload = await new WebRequestHandler().Post("/Physician", physician);
        var physicianFromServer = JsonConvert.DeserializeObject<Physician>(physicianPayload);
        //assign ID
        if (physician.ID <= 0)
        {
            //adding new physician
            int maxID = -1;
            if (physicianList.Any())
            {
                maxID = physicianList.Select(p => p?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            physician.ID = ++maxID;
            physicianList.Add(physicianFromServer);
        }
        else
        {
            var physicianToEdit = PhysicianList.FirstOrDefault(p => (p?.ID ?? 0) == physician.ID);
            if(physicianToEdit != null)
            {
                var index = PhysicianList.IndexOf(physicianToEdit);
                PhysicianList.RemoveAt(index);
                physicianList.Insert(index, physician);
            }
        }

        return physician;
    }

    public Physician? Delete(int physicianID)
    {
        var existingPhysician = physicianList.FirstOrDefault(p => p?.ID == physicianID);
        if (existingPhysician == null) { return null; }

        //remove physician from list
        physicianList.Remove(existingPhysician);
        return existingPhysician;
    }

    public Physician? GetByID(int ID)
    {
        if (ID <= 0)
        {
            return null;
        }
        return physicianList.FirstOrDefault(p => p?.ID == ID);
    }
}
