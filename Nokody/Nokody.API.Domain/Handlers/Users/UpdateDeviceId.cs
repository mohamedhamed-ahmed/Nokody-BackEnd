using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nokody.API.Domain.Model;
using Nokody.API.Domain.Model.Models;
using Nokody.API.Domain.Repositories;

namespace Nokody.API.Domain.Handlers.Users
{
    public class UpdateDeviceId
    {
        private readonly IRepository<User> _repository;


        public UpdateDeviceId(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Execute(User user)
        {
            if (user == null)
                return new Result { ResultType = ResultType.NotFound, Object = null };

            if (string.IsNullOrWhiteSpace(user.DeviceId) )
                return new Result { ResultType = ResultType.BadRequest, Object = $"{nameof(user.DeviceId)} are mandatory" };

            var oldUser = await _repository.GetAll().Where(_ => _.Id == user.Id).FirstOrDefaultAsync();

            if (oldUser == null)
                return new Result { ResultType = ResultType.NotFound, Object = null };

            oldUser.DeviceId = user.DeviceId;
            _repository.Update(oldUser, new[] { "DeviceId" });

            await _repository.UnitOfWork.CommitAsync();

            return new Result { ResultType = ResultType.Success, Object = oldUser };
        }
    }
}
