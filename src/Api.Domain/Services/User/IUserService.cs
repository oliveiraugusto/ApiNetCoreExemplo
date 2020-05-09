using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Services.User
{
    public interface IUserService
    {
        //Obter um usuario
         Task<UserEntity> Get(Guid id);

         //Obter todos os usuarios
         Task<IEnumerable<UserEntity>> GetAll();

         //criar um usuario
         Task<UserEntity> Post (UserEntity user);

         //atualizar um usuario
         Task<UserEntity> Put(UserEntity user);
         
         //deletar um usuario
         Task<bool> Delete (Guid id);
    }
}