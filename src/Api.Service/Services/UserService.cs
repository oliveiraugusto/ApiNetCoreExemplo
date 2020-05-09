using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Services.User;

namespace Api.Service.Services
{
    //Regras de Negocio
    public class UserService : IUserService
    {
        private IRepository<UserEntity> Repository;
        public UserService(IRepository<UserEntity> repository)
        {
            Repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        public async Task<UserEntity> Get(Guid id)
        {
            return await Repository.SelectAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await Repository.SelectAsync();
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await Repository.InsertAsync(user);
        }

        public async Task<UserEntity> Put(UserEntity user)
        {
            return await Repository.UpdateAsync(user);
        }
    }
}