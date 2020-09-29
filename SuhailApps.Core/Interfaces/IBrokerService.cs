using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SuhailApps.Core.Classes;
using SuhailApps.Core.Models;
using SuhailApps.Core.ViewModels;
using SuhailApps.Core.ViewModels.Accounts;
using SuhailApps.Core.ViewModels.BrokerViewModel;

namespace SuhailApps.Core.Interfaces
{
    public interface IBrokerService
    {

        /// <summary>
        /// Get broker details mapped to dto object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProcessResult<BrokerDto>> GetBroker(int id);

        /// <summary>
        /// Save broker 
        /// </summary>
        /// <param name="brokerViewModel"></param>
        /// <returns></returns>
        Task<ProcessResult<Broker>> SaveBroker(BrokerViewModel brokerViewModel);


        /// <summary>
        /// Change Mobile Number 
        /// </summary>
        /// <param name="changeNumberViewModel"></param>
        /// <returns></returns>
        Task<ProcessResult<string>> ChangeNumber(ChangeNumberViewModel changeNumberViewModel);


    }
}
