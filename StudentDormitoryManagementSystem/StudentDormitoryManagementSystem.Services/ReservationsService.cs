﻿using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Data.Repositories;
using StudentDormitoryManagementSystem.Data.SaveContext;
using StudentDormitoryManagementSystem.Services.Abstracts;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Services
{
    public class ReservationsService : Service<ItemReservation>, IReservationsService
    {
        public ReservationsService(IEfRepository<ItemReservation> repo, ISaveContext context) : base(repo, context)
        {
        }
    }
}
