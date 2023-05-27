﻿using Wholesaler.Backend.DataAccess.Factories;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using StorageDb = Wholesaler.Backend.DataAccess.Models.Storage;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly WholesalerContext _context;
        private readonly IStorageDbFactory _storageDbFactory;

        public StorageRepository(WholesalerContext context, IStorageDbFactory storageDbFactory)
        {
            _context = context;
            _storageDbFactory = storageDbFactory;
        }

        public Storage Add(Storage storage)
        {
            var storageDb = new StorageDb()
            {
                Id = storage.Id,
                Name = storage.Name,
                State = storage.State
            };

            _context.Storages.Add(storageDb);
            _context.SaveChanges();

            return storage;
        }

        public Storage? GetOrDefault(Guid storageId)
        {
            var storageDb = _context.Storages
                .FirstOrDefault(s => s.Id == storageId);

            if (storageDb == null)
                return default;

            var storage = _storageDbFactory.Create(storageDb);

            return storage;
        }

        public List<Storage> GetAll()
        {
            var storagesDb = _context.Storages
                .ToList();

            if (storagesDb == null)
                return new List<Storage>();

            var storages = storagesDb.Select(storageDb =>
            {
                return _storageDbFactory.Create(storageDb);

            }).ToList();

            return storages;
        }

        public Storage UpdateState(Storage storage)
        {
            var storageDb = _context.Storages
                .FirstOrDefault(s => s.Id == storage.Id);

            if (storageDb == null)
                throw new InvalidProcedureException($"There is no storage with id: {storage.Id}");

            storageDb.State = storage.State;
            _context.SaveChanges();

            return storage;
        }

        public Storage Get(Guid storageId)
        {
            var storageDb = _context.Storages
                .FirstOrDefault(s => s.Id == storageId);

            if (storageDb == null)
                throw new InvalidDataProvidedException($"There is no storage with id {storageId}.");

            var storage = _storageDbFactory.Create(storageDb);

            return storage;
        }
    }
}
