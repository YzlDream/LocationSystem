﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Location.Model;
using TModel.Location.AreaAndDev;

namespace LocationServices.Locations.Interfaces
{
    [ServiceContract]
    public interface IAreaService
    {
        /// <summary>
        /// 园区统计
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [OperationContract]
        AreaStatistics GetAreaStatistics(int id);

    }
}
