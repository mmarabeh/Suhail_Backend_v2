using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuhailApps.Core.Interfaces;
using SuhailApps.Core.ViewModels.BrokerViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuhailApps.Api.Controllers
{

    public class BrokerController : BaseController
    {
        #region Private Variables

        private readonly IBrokerService _brokerService;
        #endregion

        #region Constructers
        public BrokerController(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }
        #endregion

        #region Apis

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _brokerService.GetBroker(id);
            return await GetResponse(result);
        }


        /// <summary>
        /// create/update broker
        /// </summary>
        /// <param name="brokerViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] BrokerViewModel brokerViewModel)
        {
            var result = await _brokerService.SaveBroker(brokerViewModel);
            return await GetResponse(result);
        }

        /// <summary>
        /// change broker mobile number.
        /// </summary>
        /// <param name="brokerViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> ChangeMobileNumber([FromRoute] BrokerViewModel brokerViewModel)
        {
            var result = await _brokerService.SaveBroker(brokerViewModel);
            return await GetResponse(result);
        }



        #endregion



    }
}
