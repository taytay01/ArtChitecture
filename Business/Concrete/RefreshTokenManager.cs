﻿using Business.Abstract;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class RefreshTokenManager : BusinessService, IRefreshTokenService
    {
        private readonly IRefreshTokenDal _refreshTokenDal;

        public RefreshTokenManager(IRefreshTokenDal refreshTokenDal)
        {
            _refreshTokenDal = refreshTokenDal;
        }

        public IResult Add(RefreshToken entity)
        {
            _refreshTokenDal.Add(entity);
            return new SuccessResult();
        }

        [LogAspect(typeof(MsSqlLogger))]
        public IResult Delete(RefreshToken entity)
        {
            var entityToDelete = GetById(entity.Id).Data;
            _refreshTokenDal.Delete(entityToDelete);
            return new SuccessResult();
        }

        public IDataResult<List<RefreshToken>> GetAll()
        {
            return new SuccessDataResult<List<RefreshToken>>(_refreshTokenDal.GetAll());
        }

        public IDataResult<RefreshToken> GetByClientId(string clientId)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r => r.ClientId == clientId));
        }

        public IDataResult<RefreshToken> GetById(int id)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r => r.Id == id));
        }

        public IDataResult<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r => r.RefreshTokenValue == refreshToken));
        }

        public IResult Update(RefreshToken entity)
        {
            _refreshTokenDal.Update(entity);
            return new SuccessResult();
        }

        public IDataResult<RefreshToken> GetByClientNameAndUserId(string clientName, int userId)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r =>
                r.ClientName == clientName && r.UserId == userId));
        }
    }
}