﻿using AdoptADrain.Areas.Adopt.Models;
using AdoptADrain.DomainModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AdoptADrain.Services
{
    public class DrainService : IDrainService
    {
        private readonly AdoptADrainDataContext _context;
        private readonly IMapper _mapper;

        public DrainService(AdoptADrainDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Create
        public async Task<int> CreateDrain(Drain drain)
        {
            await _context.AddAsync(drain);
            await _context.SaveChangesAsync();
            return drain.DrainId;
        }

        public async Task<int> CreateDrainStatusHistory(DrainStatusHistory drainStatusHistory)
        {
            await _context.AddAsync(drainStatusHistory);
            await _context.SaveChangesAsync();
            return drainStatusHistory.DrainStatusHistoryId;
        }

        public async Task<int> CreateFlowDirection(FlowDirection flowDirection)
        {
            await _context.AddAsync(flowDirection);
            await _context.SaveChangesAsync();
            return flowDirection.FlowDirectionId;
        }
        #endregion Create
        
        #region Get
        public Task<Drain> GetDrain(int drainId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DrainDTO>> GetDrainAll(DrainSearchOptions opts)
        {
            var drains = _context.Drain
                .Include(x => x.FlowDirection)
                .Include(x => x.RoadType)
                .Include(x => x.DrainType)
                .Include(x => x.DrainStatusHistory);

            if (opts.FlowDirectionId > 0)
            {
                drains.Where(x => x.FlowDirectionId == opts.FlowDirectionId);
            }

            if(opts.DrainTypeId > 0)
            {
                drains.Where(x => x.DrainTypeId == opts.DrainTypeId);
            }

            if (opts.RoadTypeId > 0)
            {
                drains.Where(x => x.RoadTypeId == opts.RoadTypeId);
            }

            if (opts.excludeAdopted)
            {
                drains.Where(x => !x.IsAdopted);
            }

            if (!String.IsNullOrEmpty(opts.AdoptedUserId))
            {
                drains.Where(x => x.AdoptedUserId == opts.AdoptedUserId);
            }

            List<Drain> drainList = await drains.ToListAsync();

            return _mapper.Map<List<DrainDTO>>(drainList);
        }

        public async Task<List<FlowDirectionDTO>> GetFlowDirectionAll()
        {
            var flowDirection =  await _context.FlowDirection.ToListAsync();
            return _mapper.Map<List<FlowDirectionDTO>>(flowDirection);
        }

        #endregion Get

        public Task<int> RemoveDrain(int drainId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateDrain(Drain drain)
        {
            throw new NotImplementedException();
        }
    }
}