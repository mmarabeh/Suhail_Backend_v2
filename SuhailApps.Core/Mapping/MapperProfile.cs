using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SuhailApps.Core.Models;
using SuhailApps.Core.ViewModels;
using SuhailApps.Core.ViewModels.BrokerViewModel;

namespace SuhailApps.Core.Mapping
{
    public class MapperProfile:Profile
    {

        public MapperProfile()
        {
            MapBroker();
        }


        private void MapBroker()
        {
            CreateMap<Broker, BrokerDto>();
            CreateMap<BrokerViewModel, Broker>();

        }
    }
}
