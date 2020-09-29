using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SuhailApps.Core.Classes;
using SuhailApps.Core.Interfaces;
using SuhailApps.Core.Models;
using SuhailApps.Core.Resources;
using SuhailApps.Core.ViewModels;
using SuhailApps.Core.ViewModels.Accounts;
using SuhailApps.Core.ViewModels.BrokerViewModel;

namespace SuhailApps.Core.Services
{
    public class BrokerService : IBrokerService
    {
        #region Private variables

        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public BrokerService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        #endregion


        #region Private Helpers

        #endregion


        #region Public Methods

        /// <summary>
        /// Get Broker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProcessResult<BrokerDto>> GetBroker(int id)
        {
            var result = new ProcessResult<BrokerDto>();

            var broker = await _repository.FindAsync<Broker>(x => x.Id == id);
            if (broker != null)
            {
                var brokerDto = _mapper.Map<Broker, BrokerDto>(broker);
                result.ResultObj = brokerDto;
                result.Succeeded = true;
            }
            else
            {
                result.Message = Messages.MsgBrokerNotFound;
            }


            return result;
        }

        /// <summary>
        /// Save broker to db.
        /// </summary>
        /// <param name="brokerViewModel"></param>
        /// <returns></returns>
        public async Task<ProcessResult<Broker>> SaveBroker(BrokerViewModel brokerViewModel)
        {
            var result = new ProcessResult<Broker>();
            Broker broker;
            
            //Update existing
            if (brokerViewModel.Id != 0)
            {
                broker = await _repository.FindAsync<Broker>(x => x.Id == brokerViewModel.Id);
                _mapper.Map(brokerViewModel, broker);
            }
            else //Add fresh copy
            {
                broker = _mapper.Map<BrokerViewModel, Broker>(brokerViewModel);
                _repository.Add(broker);
            }


            if (_repository.CanSave())
            {
                await _repository.SaveChangesAsync();
            }

            return result;
        }


        public async Task<ProcessResult<string>> ChangeNumber(ChangeNumberViewModel changeNumberViewModel)
        {
            var result = new ProcessResult<string>()
            {
                ResultObj = changeNumberViewModel.PhoneNumber
            };

            var brokerId = "";

            //var broker = _repository.Find()
            if (_repository.CanSave())
            {
                await _repository.SaveChangesAsync();
            }

            return result;
        }



        #endregion

    }
}
