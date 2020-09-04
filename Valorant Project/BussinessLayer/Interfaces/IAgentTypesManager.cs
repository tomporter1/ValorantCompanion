﻿namespace BussinessLayer.Interfaces
{
    public interface IAgentTypesManager : IBasicManager
    {
        public enum Fields
        {
            Name,
            ImagePath
        }

        string GetTypeDataStr(object selectedType, Fields field);
    }
}