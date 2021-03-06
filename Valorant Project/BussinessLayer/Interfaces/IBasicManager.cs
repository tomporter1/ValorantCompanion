﻿using BussinessLayer.Args;
using System.Collections.Generic;

namespace BussinessLayer.Interfaces
{
    public interface IBasicManager
    {
        List<object> GetAllEntries();

        void RemoveEntry(object selectedMap);

        void AddNewEntry(SuperArgs args);

        void UpdateEntry(object selectedEntry, SuperArgs args);
    }
}